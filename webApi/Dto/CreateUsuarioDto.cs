using webApi.Models;

namespace webApi.Dto;

public class CreateUsuarioDto
{
    public Rol IdRol { get; set; }
    public string? NombreUsuario { get; set; }
    public string? Contrasena { get; set; }
}
