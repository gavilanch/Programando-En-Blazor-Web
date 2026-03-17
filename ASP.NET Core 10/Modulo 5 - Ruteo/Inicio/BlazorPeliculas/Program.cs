using BlazorPeliculas;
using BlazorPeliculas.Components;
using BlazorPeliculas.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddTransient<ServicioTransient>();
builder.Services.AddSingleton<ServicioSingleton>();
builder.Services.AddScoped<ServicioScoped>();
builder.Services.AddScoped<IServicioPeliculas, ServicioPeliculasEnMemoria>();

//builder.Services.AddCascadingValue(sp => new AppState());

builder.Services.AddScoped<AppStateService>();

builder.Services.AddCascadingValue(sp =>
{
    var state = sp.GetRequiredService<AppStateService>();
    return state.Source;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
