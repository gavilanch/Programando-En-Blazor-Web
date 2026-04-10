using BlazorPeliculas.Client.DTOs;
using BlazorPeliculas.Client.Entidades;

namespace BlazorPeliculas.Client.Servicios;

public interface IServicioActores
{
    Task<int> Crear(Actor actor);
    Task<ResultadoPaginadoDTO<Actor>> Obtener(PaginacionDTO paginacionDTO);
    Task<bool> Borrar(int id);
    Task<Actor?> ObtenerPorId(int id);
    Task Actualizar(Actor actor);
    Task<IEnumerable<Actor>> ObtenerPorNombre(string nombre);
}
