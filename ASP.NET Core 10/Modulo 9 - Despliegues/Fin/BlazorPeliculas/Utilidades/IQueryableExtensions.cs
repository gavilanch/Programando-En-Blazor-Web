using BlazorPeliculas.DTOs;

namespace BlazorPeliculas.Utilidades;

public static class IQueryableExtensions
{
    public static IQueryable<T> Paginar<T>(this IQueryable<T> queryable, 
            PaginacionDTO paginacionDTO)
    {
        return queryable.Skip((paginacionDTO.Pagina - 1) * paginacionDTO.RegistrosPorPagina)
                .Take(paginacionDTO.RegistrosPorPagina);
    }
}
