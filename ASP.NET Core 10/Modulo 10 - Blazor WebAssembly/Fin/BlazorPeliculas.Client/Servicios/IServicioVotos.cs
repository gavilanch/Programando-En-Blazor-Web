using BlazorPeliculas.Client.DTOs;

namespace BlazorPeliculas.Client.Servicios;

public interface IServicioVotos
{
    Task Votar(VotoPeliculaDTO votoPeliculaDTO);
}
