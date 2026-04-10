using BlazorPeliculas.Entidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorPeliculas.Datos;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected ApplicationDbContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<GeneroPelicula>().HasKey(x => new { x.GeneroId, x.PeliculaId });
        modelBuilder.Entity<ActorPelicula>().HasKey(x => new { x.ActorId, x.PeliculaId });
    }

    public DbSet<Genero> Generos { get; set; }
    public DbSet<Actor> Actores { get; set; }
    public DbSet<Pelicula> Peliculas { get; set; }
    public DbSet<GeneroPelicula> GenerosPeliculas { get; set; }
    public DbSet<ActorPelicula> ActoresPeliculas { get; set; }
    public DbSet<VotoPelicula> VotosPeliculas { get; set; }

}
