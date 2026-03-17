using Microsoft.JSInterop;

namespace BlazorPeliculas.Utilidades;

public static class IJSRuntimeExtensionMethods
{
    public async static ValueTask<bool> Confirm(this IJSRuntime JS, string mensaje)
    {
        return await JS.InvokeAsync<bool>("confirm", mensaje);
    }
}
