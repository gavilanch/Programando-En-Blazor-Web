using BlazorPeliculas.DTOs;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorPeliculas.Entidades;

public class Pelicula
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El campo {0} es requerido")]
    public string? Titulo { get; set; }
    public bool EnCartelera { get; set; }
    public string? Trailer { get; set; }
    public DateTime? FechaLanzamiento { get; set; }
    public string? PosterURL { get; set; }
    [NotMapped]
    public IBrowserFile? PosterArchivo { get; set; }
    [NotMapped]
    public ArchivoDTO? Archivo { get; set; }

    public List<GeneroPelicula> GenerosPelicula { get; set; } = [];
    public List<ActorPelicula> ActoresPelicula { get; set; } = [];
    public List<VotoPelicula> VotosPeliculas { get; set; } = [];
    public string? TituloFormateadoParaURL => Titulo?.Replace(" ", "-");
}
