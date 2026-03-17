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
                                    FechaLanzamiento = null
                                },
                                new Pelicula
                                {
                                    Id = 2,
                                    Titulo = "Mission: Impossible – Dead Reckoning Part Two",
                                    FechaLanzamiento = new DateTime(2025, 5, 23)
                                }
                            };
    }
}
