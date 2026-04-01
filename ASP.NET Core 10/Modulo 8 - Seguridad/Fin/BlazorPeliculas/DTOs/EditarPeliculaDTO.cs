using BlazorPeliculas.Entidades;

namespace BlazorPeliculas.DTOs
{
    public record EditarPeliculaDTO(Pelicula Pelicula, 
        List<Actor> Actores, List<Genero> GenerosSeleccionados,
    List<Genero> GenerosNoSeleccionados);

}
