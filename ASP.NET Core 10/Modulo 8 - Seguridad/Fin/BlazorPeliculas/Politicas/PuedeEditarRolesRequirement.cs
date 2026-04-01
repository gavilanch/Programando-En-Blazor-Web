using BlazorPeliculas.Constantes;
using BlazorPeliculas.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace BlazorPeliculas.Politicas;

public class PuedeEditarRolesRequirement: IAuthorizationRequirement
{
}

public class PuedeEditarRolesHandler(UserManager<ApplicationUser> userManager) : AuthorizationHandler<PuedeEditarRolesRequirement>
{
    protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
                    PuedeEditarRolesRequirement requirement)
    {
        var usuario = await userManager.GetUserAsync(context.User);

        if (usuario is null)
        {
            return;
        }

        var usuarioEsAdmin = await userManager.IsInRoleAsync(usuario, Roles.ROL_ADMIN);

        if (usuarioEsAdmin)
        {
            context.Succeed(requirement);
        }

        var claimsDB = await userManager.GetClaimsAsync(usuario);
        var tieneClaimSuperAdmin = claimsDB.Any(c => c.Type == "superadmin");

        if (tieneClaimSuperAdmin)
        {
            context.Succeed(requirement);
        }
    }
}
