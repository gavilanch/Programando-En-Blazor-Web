using BlazorPeliculas.DTOs;
using BlazorPeliculas.Entidades;

namespace BlazorPeliculas.Servicios;

public interface IServicioGeneros
{
    Task<int> Crear(Genero genero);
    Task<ResultadoPaginadoDTO<Genero>> Obtener(PaginacionDTO paginacionDTO);
    Task Actualizar(Genero genero);
    Task<Genero?> ObtenerPorId(int id);
    Task<bool> Borrar(int id);
    Task<IEnumerable<Genero>> ObtenerTodos();
}
