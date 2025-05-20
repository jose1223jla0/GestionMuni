using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using webApi.Models;
using webApi.Repository.Interfaces;

namespace webApi.Repository;

public class UsuarioRepositorio : IUsuarioRepositorio
{
    private readonly IDbConnection _bd;
    public UsuarioRepositorio(IConfiguration configuration)
    {
        _bd = new MySqlConnection(configuration.GetConnectionString("conexionMySQL"));
    }
    public async Task<Usuario> AgregarUsuario(Usuario usuario)
    {
        string verificarUsuario = "SELECT COUNT(*) FROM Usuario WHERE NombreUsuario = @NombreUsuario";
        int existeUsuario = await _bd.ExecuteScalarAsync<int>(verificarUsuario, new { usuario.NombreUsuario });

        usuario.Contrasena = BCrypt.Net.BCrypt.HashPassword(usuario.Contrasena);
        if (existeUsuario > 0)
        {
            throw new InvalidOperationException("El usuario ya está registrado.");
        }

        string mysql = "INSERT INTO Usuario (IdRol, NombreUsuario, Contrasena,EstadoUsuario) " +
                " VALUES (@IdRol, @NombreUsuario, @Contrasena, @EstadoUsuario) ";

        usuario.IdUsuario = await _bd.ExecuteScalarAsync<int>(mysql, usuario);
        return usuario;
    }

    public async Task<Usuario> EditarUsuario(Usuario usuario)
    {
        string verificarUsuario = "SELECT COUNT(*) FROM Usuario WHERE IdUsuario = @IdUsuario";
        int existe = await _bd.ExecuteScalarAsync<int>(verificarUsuario, new { usuario.IdUsuario });

        if (existe == 0)
        {
            throw new InvalidOperationException("El usuario no existe.");
        }

        string mysql = "UPDATE Usuario SET IdRol = @IdRol, NombreUsuario = @NombreUsuario, EstadoUsuario = @EstadoUsuario " +
                    "WHERE IdUsuario = @IdUsuario";

        await _bd.ExecuteAsync(mysql, usuario);
        return usuario;
    }


    public async Task<Usuario?> GetUsuario(int id)
    {
        string mysql = "SELECT * FROM Usuario WHERE IdUsuario = @IdUsuario";
        Usuario? resultado = await _bd.QueryFirstOrDefaultAsync<Usuario>(mysql, new { IdUsuario = id });
        return resultado;
    }

    public async Task<IEnumerable<Usuario>> GetUsuarios()
    {
        string mysql = "SELECT * FROM Usuario";
        IEnumerable<Usuario> resultado = await _bd.QueryAsync<Usuario>(mysql);
        return resultado;
    }
}
