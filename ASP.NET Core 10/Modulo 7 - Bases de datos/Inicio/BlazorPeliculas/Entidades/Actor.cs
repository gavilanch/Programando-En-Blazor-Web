using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace BlazorPeliculas.Entidades;

public class Actor
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El campo {0} es requerido")]
    public string? Nombre { get; set; }
    public string? FotoURL { get; set; }
    public DateTime? FechaNacimiento { get; set; }
    public IBrowserFile? FotoArchivo { get; set; }
    public string? Personaje { get; set; }

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
