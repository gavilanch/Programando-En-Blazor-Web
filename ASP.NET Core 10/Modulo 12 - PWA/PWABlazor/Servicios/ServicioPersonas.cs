using Comun;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace PWABlazor.Servicios;

public class ServicioPersonas(HttpClient httpClient, IJSRuntime js, IConfiguration configuration) 
        : IServicioPersonas
{
    public async Task CrearPersona(CrearPersonaDTO dto)
    {
        var urlAPI = configuration.GetValue<string>("API") ?? throw new Exception("falta la URL del API");
        var urlEndpoint = $"{urlAPI}/api/personas";
        if (!await js.InvokeAsync<bool>("estaOnline"))
        {
            await EncolarOperacion(urlEndpoint, dto);
            return;
        }

        await httpClient.PostAsJsonAsync(urlEndpoint, dto);
    }

    private async Task EncolarOperacion(string urlEndpoint, CrearPersonaDTO dto)
    {
        await js.InvokeVoidAsync("encolar", new
        {
            url = urlEndpoint,
            method = "POST",
            body = dto
        });

    }
}
