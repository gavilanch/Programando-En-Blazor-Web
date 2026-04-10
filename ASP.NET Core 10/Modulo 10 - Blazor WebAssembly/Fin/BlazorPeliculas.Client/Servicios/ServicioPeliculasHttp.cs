using BlazorPeliculas.Client.DTOs;
using BlazorPeliculas.Client.Entidades;
using System.Text.Json;

namespace BlazorPeliculas.Client.Servicios
{
    public class ServicioPeliculasHttp(HttpClient httpClient) : IServicioPeliculas
    {
        private JsonSerializerOptions jsonSerializerOptions = 
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        public Task Actualizar(Pelicula pelicula)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Borrar(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResultadoPaginadoDTO<Pelicula>> Buscar(ParametrosBusquedaPeliculaDTO parametros)
        {
            throw new NotImplementedException();
        }

        public Task<int> Crear(Pelicula pelicula)
        {
            throw new NotImplementedException();
        }

        public async Task<PeliculaDetalleDTO?> ObtenerDetalle(int id)
        {
            var respuesta = await httpClient.GetAsync($"/api/peliculas/{id}");
            if (respuesta.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

            var cuerpo = await respuesta.Content.ReadAsStringAsync();
            var modelo = JsonSerializer.Deserialize<PeliculaDetalleDTO>(cuerpo, jsonSerializerOptions);
            return modelo;
        }

        public Task<EditarPeliculaDTO?> ObtenerEditarPelicula(int id)
        {
            throw new NotImplementedException();
        }

        public Task<HomeDTO> ObtenerPeliculasHome()
        {
            throw new NotImplementedException();
        }
    }
}
