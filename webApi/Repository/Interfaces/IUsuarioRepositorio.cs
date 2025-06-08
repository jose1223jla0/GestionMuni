using webApi.Dto;
using webApi.Models;

namespace webApi.Repository.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Task<Usuario?> GetUsuario(int id);
        Task<IEnumerable<GetUsuarioDto>> GetUsuarios();      
        Task<Usuario> AgregarUsuario(Usuario usuario);
        Task<Usuario> EditarUsuario(Usuario usuario);
    }
}