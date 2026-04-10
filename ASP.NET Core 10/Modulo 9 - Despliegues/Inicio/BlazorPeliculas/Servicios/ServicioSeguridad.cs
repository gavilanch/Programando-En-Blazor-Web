using BlazorPeliculas.Constantes;
using BlazorPeliculas.Datos;
using BlazorPeliculas.DTOs;
using BlazorPeliculas.Entidades;
using BlazorPeliculas.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlazorPeliculas.Servicios;

public class ServicioSeguridad(IDbContextFactory<ApplicationDbContext> dbFactory,
    UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor,
    IAuthorizationService authorizationService) 
                : IServicioSeguridad
{
    public async Task<ResultadoAccion> HacerAdmin(string email)
    {
        var usuarioLogueado = httpContextAccessor.HttpContext!.User;
        var resultado = await authorizationService.AuthorizeAsync(usuarioLogueado, "PuedeEditarRolesDB");

        if (!resultado.Succeeded)
        {
            return ResultadoAccion.NoTienePermiso;
        }

        using var context = dbFactory.CreateDbContext();
        var usuario = await userManager.FindByEmailAsync(email);

        if (usuario is null)
        {
            return ResultadoAccion.NoEncontrado;
        }

        await userManager.AddToRoleAsync(usuario, Roles.ROL_ADMIN);
        await userManager.UpdateSecurityStampAsync(usuario);
        return ResultadoAccion.Exitoso;
    }

    //private async Task<bool> ValidarUsuarioEsAdmin() 
    //{
    //    var usuarioLogueado = httpContextAccessor.HttpContext!.User;
    //    var usuarioLogueadoApplicationUser = 
    //            await userManager.FindByEmailAsync(usuarioLogueado.Identity!.Name!);


    //    return await userManager.IsInRoleAsync(usuarioLogueadoApplicationUser!, Roles.ROL_ADMIN);

    //}

    public async Task<ResultadoPaginadoDTO<UsuarioDTO>> Obtener(PaginacionDTO paginacionDTO)
    {
        using var context = dbFactory.CreateDbContext();
        var elementos = await context.Users.OrderBy(x => x.UserName)
            .Paginar(paginacionDTO)
            .Select(u => new UsuarioDTO
            {
                Id = u.Id,
                Email = u.Email!
            }).AsNoTracking().ToListAsync();

        var conteo = await context.Users.CountAsync();

        var respuesta = new ResultadoPaginadoDTO<UsuarioDTO>
        {
            CantidadTotalRegistros = conteo,
            Elementos = elementos
        };

        return respuesta;

    }

    public async Task<ResultadoAccion> RemoverAdmin(string email)
    {
        var usuarioLogueado = httpContextAccessor.HttpContext!.User;
        var resultado = await authorizationService.AuthorizeAsync(usuarioLogueado, "PuedeEditarRolesDB");

        if (!resultado.Succeeded)
        {
            return ResultadoAccion.NoTienePermiso;
        }

        using var context = dbFactory.CreateDbContext();
        var usuario = await userManager.FindByEmailAsync(email);

        if (usuario is null)
        {
            return ResultadoAccion.NoEncontrado;
        }

        await userManager.RemoveFromRoleAsync(usuario, Roles.ROL_ADMIN);
        await userManager.UpdateSecurityStampAsync(usuario);
        return ResultadoAccion.Exitoso;
    }
}
