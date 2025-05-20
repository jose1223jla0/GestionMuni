using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using webApi.Models;
using webApi.Repository.Interfaces;

namespace webApi.Repository;

public class ResidenteRepositorio : IResidenteRepositorio
{
    private readonly IDbConnection _db;
    public ResidenteRepositorio(IConfiguration configuration)
    {
        _db = new MySqlConnection(configuration.GetConnectionString("conexionMySQL"));
    }
    public async Task<Residente> AgregarResidente(Residente residente)
    {
        string verificarResidente = "SELECT COUNT(*) FROM Residente WHERE DniResidente = @DniResidente";
        int existeResidente = await _db.ExecuteScalarAsync<int>(verificarResidente, new { residente.DniResidente });
        if (existeResidente > 0)
        {
            throw new InvalidOperationException("El residente ya está registrado.");
        }

        string mysql = "INSERT INTO Residente (IdUsuario, NombreResidente, ApellidoResidente, DniResidente,CorreoResidente,DireccionResidente,EstadoResidente,TicketsTotalesGanados) " +
                       "VALUES (@IdUsuario, @NombreResidente, @ApellidoResidente, @DniResidente,@CorreoResidente,@DireccionResidente,@EstadoResidente,@TicketsTotalesGanados) " +
                       "SELECT LAST_INSERT_ID();";
        residente.IdResidente = await _db.ExecuteScalarAsync<int>(mysql, residente);
        return residente;
    }

    public async Task<Residente> BuscarPorDni(string dni)
    {
        string mysql = "SELECT * FROM Residente WHERE DniResidente=@DniResidente";
        int existeResidente = await _db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM Residente WHERE DniResidente=@DniResidente", new { DniResidente = dni });
        if (existeResidente == 0)
        {
            throw new InvalidOperationException("El residente no existe.");
        }
        Residente resultado = await _db.QueryFirstAsync<Residente>(mysql, new { DniResidente = dni });
        return resultado;

    }

    public async Task<Residente> EditarResidente(Residente residente)
    {
        string mysql = "UPDATE Residente SET NombreResidente=@NombreResidente, ApellidoResidente=@ApellidoResidente, DniResidente=@DniResidente, CorreoResidente=@CorreoResidente, DireccionResidente=@DireccionResidente, EstadoResidente=@EstadoResidente, TicketsTotalesGanados=@TicketsTotalesGanados " +
                       "WHERE IdResidente=@IdResidente";
        await _db.ExecuteAsync(mysql, residente);
        return residente;

    }

    public async Task<IEnumerable<Residente>> GetResidentes()
    {
        string mysql = "SELECT * FROM Residente";
        var resultado = await _db.QueryAsync<Residente>(mysql);
        return resultado;
    }
}
