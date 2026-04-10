using BlazorPeliculas.Entidades;

namespace BlazorPeliculas.DTOs;

public class PeliculaDetalleDTO
{
    public Pelicula Pelicula { get; set; } = null!;
    public List<Genero> Generos { get; set; } = [];
    public List<Actor> Actores { get; set; } = [];
    public int VotoUsuario { get; set; }
    public double PromedioVotos { get; set; }
}
