using BlazorPeliculas.DTOs;

namespace BlazorPeliculas.Servicios;

public class AlmacenadorArchivosLocal(IWebHostEnvironment env,
    IHttpContextAccessor httpContextAccessor
    ) : IAlmacenadorArchivos
{
    public async Task<string> Almacenar(string contenedor, ArchivoDTO archivo)
    {
        var extension = Path.GetExtension(archivo.Nombre);
        var nombreArchivo = $"{Guid.NewGuid()}{extension}";
        string folder = Path.Combine(env.WebRootPath, contenedor);

        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        string ruta = Path.Combine(folder, nombreArchivo);

        await using var archivoStream = archivo.AbrirStream();
        await using var fs = new FileStream(
                                ruta,
                                FileMode.Create);

        await archivoStream.CopyToAsync(fs);

        var request = httpContextAccessor.HttpContext!.Request;
        var url = $"{request.Scheme}://{request.Host}";
        var urlArchivo = Path.Combine(url, contenedor, nombreArchivo).Replace("\\", "/");

        return urlArchivo;

    }

    public Task Borrar(string? ruta, string contenedor)
    {
        if (string.IsNullOrEmpty(ruta))
        {
            return Task.CompletedTask;
        }

        var nombreArchivo = Path.GetFileName(ruta);
        var directorioArchivo = Path.Combine(env.WebRootPath, contenedor, nombreArchivo);

        if (File.Exists(directorioArchivo))
        {
            File.Delete(directorioArchivo);
        }

        return Task.CompletedTask;
    }
}
