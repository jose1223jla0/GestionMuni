using webApi.Models;

namespace webApi.Services.Interfaces;

public interface IJwtServicio
{
     string CrearToken(Usuario usuario);
}
