using BlazorPeliculas.Datos;
using BlazorPeliculas.DTOs;
using BlazorPeliculas.Entidades;
using BlazorPeliculas.Utilidades;
using Microsoft.EntityFrameworkCore;

namespace BlazorPeliculas.Servicios;

public class ServicioGeneros(IDbContextFactory<ApplicationDbContext> dbFactory) 
        : IServicioGeneros
{
    public async Task Actualizar(Genero genero)
    {
        using var context = dbFactory.CreateDbContext();
        context.Update(genero);
        await context.SaveChangesAsync();
    }

    public async Task<bool> Borrar(int id)
    {
        using var context = dbFactory.CreateDbContext();
        var elementosBorrados = await context.Generos.Where(x => x.Id == id)
            .ExecuteDeleteAsync();
        return elementosBorrados == 1;
    }

    public async Task<int> Crear(Genero genero)
    {
        using var context = dbFactory.CreateDbContext();
        context.Add(genero);
        await context.SaveChangesAsync();
        return genero.Id;
    }

    public async Task<ResultadoPaginadoDTO<Genero>> Obtener(PaginacionDTO paginacionDTO)
    {
        using var context = dbFactory.CreateDbContext();
        var elementos = await context.Generos.OrderBy(g => g.Nombre)
            .Paginar(paginacionDTO)
            .AsNoTracking().ToListAsync();
        var conteo = await context.Generos.CountAsync();
        var respuesta = new ResultadoPaginadoDTO<Genero>
        {
            CantidadTotalRegistros = conteo,
            Elementos = elementos
        };

        return respuesta;
    }

    public async Task<Genero?> ObtenerPorId(int id)
    {
        using var context = dbFactory.CreateDbContext();
        return await context.Generos.FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<IEnumerable<Genero>> ObtenerTodos()
    {
        using var context = dbFactory.CreateDbContext();
        return await context.Generos.OrderBy(x => x.Nombre).ToListAsync();
    }
}
