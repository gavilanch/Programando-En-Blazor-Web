using BlazorPeliculas.DTOs;

namespace BlazorPeliculas.Servicios
{
    public interface IServicioSeguridad
    {
        Task<ResultadoPaginadoDTO<UsuarioDTO>> Obtener(PaginacionDTO paginacionDTO);
        Task<ResultadoAccion> HacerAdmin(string email);
        Task<ResultadoAccion> RemoverAdmin(string email);
    }
}
