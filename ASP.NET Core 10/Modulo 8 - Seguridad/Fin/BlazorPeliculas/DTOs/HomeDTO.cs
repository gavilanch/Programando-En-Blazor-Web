using BlazorPeliculas.Entidades;

namespace BlazorPeliculas.DTOs;

public class HomeDTO
{
    public List<Pelicula>? EnCartelera { get; set; }
    public List<Pelicula>? FuturosEstrenos { get; set; }
}
