namespace BlazorPeliculas.DTOs;

public class PaginacionDTO(int Pagina = 1, int RegistrosPorPagina = 10)
{
    private const int _cantidadMaximaRegistrosPorPagina = 50;

    public int Pagina { get; init; } = Math.Max(1, Pagina);
    public int RegistrosPorPagina { get; init; } =
        Math.Clamp(RegistrosPorPagina, 1, _cantidadMaximaRegistrosPorPagina);

}
