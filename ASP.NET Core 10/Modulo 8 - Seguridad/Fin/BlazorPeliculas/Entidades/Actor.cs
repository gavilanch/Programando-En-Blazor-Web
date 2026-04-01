using BlazorPeliculas.DTOs;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorPeliculas.Entidades;

public class Actor
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El campo {0} es requerido")]
    public string? Nombre { get; set; }
    public string? FotoURL { get; set; }
    public DateTime? FechaNacimiento { get; set; }
    public List<ActorPelicula> ActoresPeliculas { get; set; } = [];

    [NotMapped]
    public IBrowserFile? FotoArchivo { get; set; }
    [NotMapped]
    public string? Personaje { get; set; }
    [NotMapped]
    public ArchivoDTO? Archivo { get; set; }


    public override bool Equals(object? obj)
    {
        if (obj is Actor a2)
        {
            return Id == a2.Id;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

}
