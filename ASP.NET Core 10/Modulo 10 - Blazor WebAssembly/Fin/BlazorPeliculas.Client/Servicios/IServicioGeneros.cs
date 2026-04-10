using BlazorPeliculas.Client.DTOs;
using BlazorPeliculas.Client.Entidades;

namespace BlazorPeliculas.Client.Servicios;

public interface IServicioGeneros
{
    Task<int> Crear(Genero genero);
    Task<ResultadoPaginadoDTO<Genero>> Obtener(PaginacionDTO paginacionDTO);
    Task Actualizar(Genero genero);
    Task<Genero?> ObtenerPorId(int id);
    Task<bool> Borrar(int id);
    Task<IEnumerable<Genero>> ObtenerTodos();
}
