namespace BlazorPeliculas.Client.Entidades;

public class GeneroPelicula
{
    public int PeliculaId { get; set; }
    public int GeneroId { get; set; }
    public Genero? Genero { get; set; }
    public Pelicula? Pelicula { get; set; }
}
