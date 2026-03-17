using BlazorPeliculas.Entidades;

namespace BlazorPeliculas.Servicios;

public class ServicioPeliculasEnMemoria : IServicioPeliculas
{
    public List<Pelicula> ObtenerPeliculas()
    {
        return new List<Pelicula>
                            {
                                new Pelicula
                                {
                                    Id = 1,
                                    Titulo = "Captain America: Brave New World",
                                    FechaLanzamiento = new DateTime(2025, 2, 14),
                                    PosterURL = "https://upload.wikimedia.org/wikipedia/en/a/a4/Captain_America_Brave_New_World_poster.jpg"
                                },
                                new Pelicula
                                {
                                    Id = 2,
                                    Titulo = "Mission: Impossible – Dead Reckoning Part Two",
                                    FechaLanzamiento = new DateTime(2025, 5, 23),
                                    PosterURL = "https://upload.wikimedia.org/wikipedia/en/1/1f/Mission_Impossible_%E2%80%93_The_Final_Reckoning_Poster.jpg"
                                }
                            };
    }
}
