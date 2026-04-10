using BlazorPeliculas.Client.DTOs;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorPeliculas.Client.Utilidades;

public static class ArchivosExtensions
{
    public static ArchivoDTO ConvertirAArchivoDTO(this IBrowserFile browserFile)
    {
        var limite = 2 * 1024 * 1024;
        var archivo = new ArchivoDTO(browserFile.Name, browserFile.ContentType,
            () => browserFile.OpenReadStream(limite));
        return archivo;
    }
}
