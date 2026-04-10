using BlazorPeliculas.Client.DTOs;
using BlazorPeliculas.Client.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace BlazorPeliculas.Controllers
{
    [Route("api/votos")]
    [ApiController]
    public class VotosController(IServicioVotos servicioVotos): ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Post(VotoPeliculaDTO votoPeliculaDTO)
        {
            await servicioVotos.Votar(votoPeliculaDTO);
            return Ok();
        }
    }
}
