using BlazorPeliculas.Datos;
using BlazorPeliculas.DTOs;
using BlazorPeliculas.Entidades;
using BlazorPeliculas.Utilidades;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlazorPeliculas.Servicios;

public class ServicioPeliculas(IDbContextFactory<ApplicationDbContext> dbFactory,
    IAlmacenadorArchivos almacenadorArchivos, IHttpContextAccessor httpContextAccessor) : IServicioPeliculas
{
    private readonly string contenedor = "peliculas";

    public async Task Actualizar(Pelicula pelicula)
    {
        if (pelicula.Archivo is not null)
        {
            pelicula.PosterURL = await almacenadorArchivos.Editar(pelicula.PosterURL,
                        contenedor, pelicula.Archivo);
        }

        using var context = dbFactory.CreateDbContext();

        var peliculaDB = await context.Peliculas
            .Include(p => p.GenerosPelicula)
            .Include(p => p.ActoresPelicula)
            .FirstAsync(p => p.Id == pelicula.Id);

        context.Entry(peliculaDB).CurrentValues.SetValues(pelicula);
        var generosIds = pelicula.GenerosPelicula.Select(x => x.GeneroId).ToList();
        SincronizarGeneros(peliculaDB, generosIds);
        SincronizarActores(peliculaDB, pelicula.ActoresPelicula);
        await context.SaveChangesAsync();
    }

    private void SincronizarGeneros(Pelicula pelicula, List<int> generosIds)
    {
        var actuales = pelicula.GenerosPelicula.Select(x => x.GeneroId);
        pelicula.GenerosPelicula.RemoveAll(x => !generosIds.Contains(x.GeneroId));
        var faltantes = generosIds.Except(actuales);
        foreach (var generoId in faltantes)
        {
            pelicula.GenerosPelicula.Add(new GeneroPelicula
            {
                PeliculaId = pelicula.Id,
                GeneroId = generoId
            });
        }
    }

    private void SincronizarActores(Pelicula pelicula,
                List<ActorPelicula> actoresSeleccionados)
    {
        var actuales = pelicula.ActoresPelicula.ToList();
        var actoresSeleccionadosIds = actoresSeleccionados.Select(x => x.ActorId).ToList();
        pelicula.ActoresPelicula.RemoveAll(x => !actoresSeleccionadosIds.Contains(x.ActorId));

        foreach (var actorPelicula in pelicula.ActoresPelicula)
        {
            var actorPeliculaSeleccionado =
                    actoresSeleccionados.Single(x => x.ActorId == actorPelicula.ActorId)!;

            actorPelicula.Personaje = actorPeliculaSeleccionado.Personaje;
            actorPelicula.Orden = actorPeliculaSeleccionado.Orden;
        }

        var actoresActualesIds = pelicula.ActoresPelicula.Select(x => x.ActorId);
        var faltantesIds = actoresSeleccionadosIds.Except(actoresActualesIds);
        foreach (var actorId in faltantesIds)
        {
            var actorPeliculaSeleccionado =
                    actoresSeleccionados.Single(x => x.ActorId == actorId)!;

            pelicula.ActoresPelicula.Add(new ActorPelicula
            {
                PeliculaId = actorPeliculaSeleccionado.PeliculaId,
                ActorId = actorPeliculaSeleccionado.ActorId,
                Orden = actorPeliculaSeleccionado.Orden,
                Personaje = actorPeliculaSeleccionado.Personaje
            });
        }
    }

    public async Task<ResultadoPaginadoDTO<Pelicula>> Buscar(ParametrosBusquedaPeliculaDTO parametros)
    {
        using var context = dbFactory.CreateDbContext();

        var peliculasQueryable = context.Peliculas.AsQueryable();

        if (!string.IsNullOrWhiteSpace(parametros.Titulo))
        {
            peliculasQueryable = peliculasQueryable
                            .Where(x => x.Titulo!.Contains(parametros.Titulo));
        }

        if (parametros.EnCartelera)
        {
            peliculasQueryable = peliculasQueryable.Where(x => x.EnCartelera);
        }

        if (parametros.Estrenos)
        {
            var hoy = DateTime.Today;
            peliculasQueryable = peliculasQueryable.Where(x => x.FechaLanzamiento >= hoy);
        }

        if (parametros.GeneroId != 0)
        {
            peliculasQueryable = peliculasQueryable
                .Where(p =>
                p.GenerosPelicula.Select(gp => gp.GeneroId).Contains(parametros.GeneroId));
        }

        if (parametros.MasVotadas)
        {
            peliculasQueryable = peliculasQueryable.OrderByDescending(p =>
                        p.VotosPeliculas.Average(vp => vp.Voto));
        }

        var peliculas = await peliculasQueryable.Paginar(parametros.PaginacionDTO).ToListAsync();
        var conteo = await peliculasQueryable.CountAsync();

        var respuesta = new ResultadoPaginadoDTO<Pelicula>
        {
            Elementos = peliculas,
            CantidadTotalRegistros = conteo
        };

        return respuesta;

    }

    public async Task<int> Crear(Pelicula pelicula)
    {
        if (pelicula.Archivo is not null)
        {
            pelicula.PosterURL = await almacenadorArchivos.Almacenar(contenedor,
                                            pelicula.Archivo);
        }

        using var context = dbFactory.CreateDbContext();
        context.Add(pelicula);
        await context.SaveChangesAsync();
        return pelicula.Id;

    }

    public async Task<PeliculaDetalleDTO?> ObtenerDetalle(int id)
    {
        using var context = dbFactory.CreateDbContext();
        var pelicula = await context.Peliculas.Where(p => p.Id == id)
            .Include(p => p.GenerosPelicula)
                .ThenInclude(gp => gp.Genero)
            .Include(p => p.ActoresPelicula.OrderBy(pa => pa.Orden))
                .ThenInclude(pa => pa.Actor)
            .FirstOrDefaultAsync();

        if (pelicula is null)
        {
            return null;
        }

        var promedioVoto = 0.0;
        var votoUsuario = 0;

        if (await context.VotosPeliculas.AnyAsync(x => x.PeliculaId == id))
        {
            promedioVoto = await context.VotosPeliculas.Where(x => x.PeliculaId == id)
                                  .AverageAsync(x => x.Voto);

            if (httpContextAccessor.HttpContext is not null
                    && httpContextAccessor.HttpContext.User.Identity!.IsAuthenticated)
            {
                var usuarioId = httpContextAccessor.HttpContext
                            .User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;

                var votoUsuarioDB = await context.VotosPeliculas
        .FirstOrDefaultAsync(x => x.PeliculaId == id && x.UsuarioId == usuarioId);

                if (votoUsuarioDB is not null)
                {
                    votoUsuario = votoUsuarioDB.Voto;
                }


            }

        }

        var modelo = new PeliculaDetalleDTO();
        modelo.Pelicula = pelicula;
        modelo.Generos = pelicula.GenerosPelicula.Select(gp => gp.Genero!).ToList();
        modelo.Actores = pelicula.ActoresPelicula.Select(pa => new Actor
        {
            Id = pa.ActorId,
            Nombre = pa.Actor!.Nombre,
            Personaje = pa.Personaje,
            FotoURL = pa.Actor.FotoURL
        }).ToList();

        modelo.PromedioVotos = promedioVoto;
        modelo.VotoUsuario = votoUsuario;

        return modelo;

    }

    public async Task<EditarPeliculaDTO?> ObtenerEditarPelicula(int id)
    {
        var peliculaDetalle = await ObtenerDetalle(id);
        if (peliculaDetalle is null) { return null; }

        using var context = dbFactory.CreateDbContext();
        var generosSeleccionadosIds = peliculaDetalle!.Generos.Select(x => x.Id).ToList();
        var generosNoSeleccionados = await context.Generos
                                    .Where(x => !generosSeleccionadosIds.Contains(x.Id))
                                    .ToListAsync();

        var modelo = new EditarPeliculaDTO(peliculaDetalle.Pelicula,
    peliculaDetalle.Actores, peliculaDetalle.Generos, generosNoSeleccionados);
        return modelo;
    }

    public async Task<HomeDTO> ObtenerPeliculasHome()
    {
        var limite = 5;
        using var context = dbFactory.CreateDbContext();

        var cartelera = await context.Peliculas
            .Where(x => x.EnCartelera)
            .OrderBy(x => x.FechaLanzamiento)
            .Take(limite).ToListAsync();

        var hoy = DateTime.Today;
        var futurosEstrenos = await context.Peliculas
            .Where(x => x.FechaLanzamiento > hoy)
            .OrderBy(x => x.FechaLanzamiento)
            .Take(limite).ToListAsync();

        var respuesta = new HomeDTO
        {
            EnCartelera = cartelera,
            FuturosEstrenos = futurosEstrenos
        };

        return respuesta;

    }

    public async Task<bool> Borrar(int id)
    {
        using var context = dbFactory.CreateDbContext();
        var pelicula = await context.Peliculas.FirstOrDefaultAsync(x => x.Id == id);
        if (pelicula is null)
        {
            return false;
        }

        context.Remove(pelicula);
        await context.SaveChangesAsync();
        await almacenadorArchivos.Borrar(pelicula.PosterURL, contenedor);
        return true;
    }
}
