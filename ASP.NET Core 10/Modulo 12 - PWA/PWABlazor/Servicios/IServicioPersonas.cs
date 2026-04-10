using Comun;

namespace PWABlazor.Servicios
{
    public interface IServicioPersonas
    {
        Task CrearPersona(CrearPersonaDTO dto);
    }
}