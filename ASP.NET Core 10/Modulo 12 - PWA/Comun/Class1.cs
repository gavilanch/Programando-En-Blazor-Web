namespace Comun;

public class CrearPersonaDTO
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nombre { get; set; } = string.Empty;
}
