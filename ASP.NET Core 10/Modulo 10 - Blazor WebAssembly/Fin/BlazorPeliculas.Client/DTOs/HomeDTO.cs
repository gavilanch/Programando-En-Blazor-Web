using BlazorPeliculas.Client.Entidades;

namespace BlazorPeliculas.Client.DTOs;

public class HomeDTO
{
    public List<Pelicula>? EnCartelera { get; set; }
    public List<Pelicula>? FuturosEstrenos { get; set; }
}
