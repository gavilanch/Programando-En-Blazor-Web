using BlazorPeliculas.Entidades;
using Microsoft.JSInterop;

namespace BlazorPeliculas.Components.Pages;

public partial class Counter(ServicioSingleton singleton,
    ServicioTransient transient, ServicioScoped scoped2, IJSRuntime JS)
{
    private int currentCount = 0;
    private static int currentCountStatic = 0;

    private List<Pelicula> futurosEstrenos = new List<Pelicula>
                                            {
                                                new Pelicula
                                                {
                                                    Id = 3,
                                                    Titulo = "Avengers: Secret Wars",
                                                    FechaLanzamiento = new DateTime(2027, 5, 7)
                                                },
                                                new Pelicula
                                                {
                                                    Id = 4,
                                                    Titulo = "The Batman – Part II",
                                                    FechaLanzamiento = new DateTime(2027, 10, 1)
                                                }
                                            };

    IJSObjectReference? moduloCounter;

    [JSInvokable]
    public async Task IncrementCount()
    {
        moduloCounter = await JS.InvokeAsync<IJSObjectReference>("import",
            "./js/counter.js");

        await moduloCounter.InvokeVoidAsync("mostrarAlerta", "vas a incrementar el contador");

        currentCount++;
        currentCountStatic = currentCount;
        transient.Valor = currentCount;
        singleton.Valor = currentCount;
        scoped2.Valor = currentCount;
        await JS.InvokeVoidAsync("obtenerCurrentCount");
    }

    public async Task IncrementCountJavaScript()
    {
        await JS.InvokeVoidAsync("invocarIncrementCount", DotNetObjectReference.Create(this));
    }

    [JSInvokable]
    public static Task<int> ObtenerCurrentCount()
    {
        return Task.FromResult(currentCountStatic);
    }

}
