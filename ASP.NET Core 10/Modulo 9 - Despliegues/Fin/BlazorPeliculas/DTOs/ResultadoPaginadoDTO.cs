namespace BlazorPeliculas.DTOs;

public class ResultadoPaginadoDTO<T>
{
    public IEnumerable<T> Elementos { get; set; } = [];
    public int CantidadTotalRegistros { get; set; }
}
