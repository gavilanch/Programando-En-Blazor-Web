using Comun;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/personas")]
public class PersonasController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post(CrearPersonaDTO dto)
    {
        Console.WriteLine("Petición recibida en el API: " + dto.Nombre);
        return Ok();
    }
}

