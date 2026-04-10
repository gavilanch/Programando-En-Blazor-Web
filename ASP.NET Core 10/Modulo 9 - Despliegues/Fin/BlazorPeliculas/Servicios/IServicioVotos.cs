using BlazorPeliculas.DTOs;

namespace BlazorPeliculas.Servicios;

public interface IServicioVotos
{
    Task Votar(VotoPeliculaDTO votoPeliculaDTO);
}
