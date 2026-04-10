using BlazorPeliculas.Client.Entidades;

namespace BlazorPeliculas.Client.DTOs
{
    public record EditarPeliculaDTO(Pelicula Pelicula, 
        List<Actor> Actores, List<Genero> GenerosSeleccionados,
    List<Genero> GenerosNoSeleccionados);

}
