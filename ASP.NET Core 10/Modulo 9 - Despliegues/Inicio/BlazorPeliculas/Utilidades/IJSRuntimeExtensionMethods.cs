using Microsoft.JSInterop;

namespace BlazorPeliculas.Utilidades;

public static class IJSRuntimeExtensionMethods
{
    public async static ValueTask<bool> Confirm(this IJSRuntime JS, string mensaje)
    {
        return await JS.InvokeAsync<bool>("confirm", mensaje);
    }

    public async static ValueTask MostrarAlertaExitosa(this IJSRuntime JS, string titulo, string cuerpo)
    {
        await JS.InvokeVoidAsync("mostrarAlerta", titulo, cuerpo, "success");
    }

    public async static ValueTask MostrarAlertaError(this IJSRuntime JS, string titulo, string cuerpo)
    {
        await JS.InvokeVoidAsync("mostrarAlerta", titulo, cuerpo, "error");
    }

}
