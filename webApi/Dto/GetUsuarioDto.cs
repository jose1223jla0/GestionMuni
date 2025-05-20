using webApi.Models;
namespace webApi.Dto;

public class GetUsuarioDto
{
    public int IdUsuario { get; set; }
    public Rol IdRol { get; set; }
    public string? NombreUsuario { get; set; }
    public bool EstadoUsuario { get; set; }
    public DateTime FechaCreacionUsuario { get; set; }
}
