using BlazorPeliculas.Datos;
using BlazorPeliculas.DTOs;
using BlazorPeliculas.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlazorPeliculas.Servicios;

public class ServicioVotos(IHttpContextAccessor httpContextAccessor,
   IDbContextFactory<ApplicationDbContext> dbFactory) : IServicioVotos
{
    public async Task Votar(VotoPeliculaDTO votoPeliculaDTO)
    {
        var usuario = httpContextAccessor.HttpContext!.User;

        if (usuario.Identity is not null && !usuario.Identity.IsAuthenticated)
        {
            return;
        }

        var usuarioId = usuario.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;

        var context = dbFactory.CreateDbContext();

        var votoActual = await context.VotosPeliculas
            .FirstOrDefaultAsync(x => x.PeliculaId == votoPeliculaDTO.PeliculaId
            && x.UsuarioId == usuarioId);

        if (votoActual is null)
        {
            var votoPelicula = new VotoPelicula
            {
                FechaVoto = DateTime.UtcNow,
                PeliculaId = votoPeliculaDTO.PeliculaId,
                Voto = votoPeliculaDTO.Voto,
                UsuarioId = usuarioId
            };

            context.Add(votoPelicula);
        } else
        {
            votoActual.FechaVoto = DateTime.UtcNow;
            votoActual.Voto = votoPeliculaDTO.Voto;
        }

        await context.SaveChangesAsync();
    }
}
