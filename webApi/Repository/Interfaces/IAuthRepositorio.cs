using webApi.Models;

namespace webApi.Repository.Interfaces;

public interface IAuthRepositorio
{
    Task<Usuario> Login(string usuarioNombre, string contrasena);
    Task<bool> ExisteUsuario(string usuarioNombre);
}
