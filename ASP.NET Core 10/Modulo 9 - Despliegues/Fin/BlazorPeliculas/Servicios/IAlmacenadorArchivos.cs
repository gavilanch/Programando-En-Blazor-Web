using BlazorPeliculas.DTOs;

namespace BlazorPeliculas.Servicios;

public interface IAlmacenadorArchivos
{
    Task<string> Almacenar(string contenedor, ArchivoDTO archivo);
    Task Borrar(string? ruta, string contenedor);
    async Task<string> Editar(string? ruta, string contenedor, ArchivoDTO archivo)
    {
        await Borrar(ruta, contenedor);
        return await Almacenar(contenedor, archivo);
    }

}
