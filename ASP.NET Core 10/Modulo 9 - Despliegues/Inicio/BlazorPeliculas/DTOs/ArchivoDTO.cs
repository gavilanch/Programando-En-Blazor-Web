namespace BlazorPeliculas.DTOs;

public record ArchivoDTO(string Nombre, string ContentType, Func<Stream> AbrirStream);
