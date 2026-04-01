using BlazorPeliculas;
using BlazorPeliculas.Components;
using BlazorPeliculas.Datos;
using BlazorPeliculas.Servicios;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContextFactory<ApplicationDbContext>(opciones =>
    opciones.UseSqlServer("name=DefaultConnection"));

builder.Services.AddScoped<IServicioPeliculas, ServicioPeliculas>();
builder.Services.AddScoped<IServicioGeneros, ServicioGeneros>();
builder.Services.AddScoped<IServicioActores, ServicioActores>();

builder.Services.AddScoped<IAlmacenadorArchivos, AlmacenadorArchivosAzure>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddMudServices();

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
app.UseStaticFiles();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
