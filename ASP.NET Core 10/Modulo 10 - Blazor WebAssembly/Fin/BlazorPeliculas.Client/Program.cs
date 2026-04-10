using BlazorPeliculas.Client.Servicios;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthenticationStateDeserialization();

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

builder.Services.AddScoped<IServicioPeliculas, ServicioPeliculasHttp>();
builder.Services.AddScoped<IServicioVotos, ServicioVotosHttp>();

await builder.Build().RunAsync();
