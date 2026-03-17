using BlazorPeliculas.Datos;
using BlazorPeliculas.DTOs;
using BlazorPeliculas.Entidades;
using BlazorPeliculas.Utilidades;
using Microsoft.EntityFrameworkCore;

namespace BlazorPeliculas.Servicios;

public class ServicioActores(IDbContextFactory<ApplicationDbContext> dbFactory,
    IAlmacenadorArchivos almacenadorArchivos) 
        : IServicioActores
{
    private readonly string contenedor = "actores";

    public async Task Actualizar(Actor actor)
    {
        if (actor.Archivo is not null)
        {
            actor.FotoURL = await almacenadorArchivos.Editar(actor.FotoURL,
                contenedor, actor.Archivo);
        }

        using var context = dbFactory.CreateDbContext();
        context.Update(actor);
        await context.SaveChangesAsync();

    }

    public async Task<bool> Borrar(int id)
    {
        using var context = dbFactory.CreateDbContext();
        var actor = await context.Actores.FirstOrDefaultAsync(x => x.Id == id);

        if (actor is null)
        {
            return false;
        }

        context.Remove(actor);
        await context.SaveChangesAsync();
        await almacenadorArchivos.Borrar(actor.FotoURL, contenedor);
        return true;
    }

    public async Task<int> Crear(Actor actor)
    {
        if (actor.Archivo is not null)
        {
            actor.FotoURL = await almacenadorArchivos.Almacenar(contenedor, actor.Archivo);
        }

        using var context = dbFactory.CreateDbContext();
        context.Add(actor);
        await context.SaveChangesAsync();
        return actor.Id;
    }

    public async Task<ResultadoPaginadoDTO<Actor>> Obtener(PaginacionDTO paginacionDTO)
    {
        using var context = dbFactory.CreateDbContext();
        var elementos = await context.Actores.OrderBy(x => x.Nombre)
            .Paginar(paginacionDTO)
            .AsNoTracking().ToListAsync();

        var conteo = await context.Actores.CountAsync();

        var respuesta = new ResultadoPaginadoDTO<Actor>
        {
            CantidadTotalRegistros = conteo,
            Elementos = elementos
        };

        return respuesta;

    }

    public async Task<Actor?> ObtenerPorId(int id)
    {
        using var context = dbFactory.CreateDbContext();
        return await context.Actores.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Actor>> ObtenerPorNombre(string nombre)
    {
        using var context = dbFactory.CreateDbContext();
        return await context.Actores.Where(x => x.Nombre!.Contains(nombre)).ToListAsync();
    }
}
