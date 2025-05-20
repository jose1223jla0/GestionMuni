
using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using webApi.Models;
using webApi.Repository.Interfaces;

namespace webApi.Repository;

public class RegistroReciclajeRepositorio : IRegistroReciclajeRepositorio
{
    private readonly IDbConnection _db;
    public RegistroReciclajeRepositorio(IConfiguration configuration)
    {
        _db = new MySqlConnection(configuration.GetConnectionString("conexionMySQL"));
    }

    public async Task<RegistrosDeReciclaje> CreateRegistrosDeReciclaje(RegistrosDeReciclaje registrosDeReciclaje)
    {
        string verificarRegistro = "SELECT COUNT(*) FROM Residuo WHERE IdResiduo = @IdResiduo";
        int existeRegistro = await _db.ExecuteScalarAsync<int>(verificarRegistro, new { registrosDeReciclaje.IdResiduo });
        if (existeRegistro > 0)
        {
            throw new InvalidOperationException("El registro ya existe.");
        }
        string mysql = "INSERT INTO RegistrosDeReciclaje (IdResidente, IdResiduo, PesoKilogramo, FechaRegistro, TicketsGanados) " +
                       "VALUES (@IdResidente, @IdResiduo, @PesoKilogramo, @FechaRegistro, @TicketsGanados)";

        registrosDeReciclaje.IdRegistrosReciclaje = await _db.ExecuteScalarAsync<int>(mysql, registrosDeReciclaje);
        return registrosDeReciclaje;

    }

    public async Task DeleteRegistrosDeReciclaje(int idRegistroReciclaje)
    {
        string verificarRegistro = "SELECT COUNT(*) FROM RegistrosDeReciclaje WHERE IdRegistrosReciclaje = @IdRegistrosReciclaje";
        int existeRegistro = await _db.ExecuteScalarAsync<int>(verificarRegistro, new { IdRegistrosReciclaje = idRegistroReciclaje });
        if (existeRegistro == 0)
        {
            throw new InvalidOperationException("El registro de reciclaje no existe.");
        }

        string mysql = "DELETE FROM RegistrosDeReciclaje WHERE IdRegistrosReciclaje = @IdRegistrosReciclaje";
        await _db.ExecuteAsync(mysql, new { IdRegistrosReciclaje = idRegistroReciclaje });

    }

    public async Task<RegistrosDeReciclaje> GetRegistrosDeReciclaje(int idRegistroReciclaje)
    {
        string mysql = "SELECT * FROM RegistrosDeReciclaje WHERE IdRegistrosReciclaje = @IdRegistrosReciclaje";
        RegistrosDeReciclaje? resultado = await _db.QueryFirstOrDefaultAsync<RegistrosDeReciclaje>(mysql, new { IdRegistrosReciclaje = idRegistroReciclaje });
        if (resultado == null)
        {
            throw new InvalidOperationException("El registro de reciclaje no existe.");
        }
        return resultado;

    }

    public async Task<IEnumerable<RegistrosDeReciclaje>> GetRegistrosDeReciclaje()
    {
        string mysql = "SELECT * FROM RegistrosDeReciclaje";
        IEnumerable<RegistrosDeReciclaje> resultado = await _db.QueryAsync<RegistrosDeReciclaje>(mysql);
        if (resultado == null || !resultado.Any())
        {
            throw new InvalidOperationException("No se encontraron registros de reciclaje.");
        }
        return resultado;

    }

    public async Task<RegistrosDeReciclaje> UpdateRegistrosDeReciclaje(RegistrosDeReciclaje registrosDeReciclaje)
    {
        string verificarRegistro = "SELECT COUNT(*) FROM RegistrosDeReciclaje WHERE IdRegistrosReciclaje = @IdRegistrosReciclaje";
        int existeRegistro = await _db.ExecuteScalarAsync<int>(verificarRegistro, new { registrosDeReciclaje.IdRegistrosReciclaje });
        if (existeRegistro == 0)
        {
            throw new InvalidOperationException("El registro de reciclaje no existe.");
        }
        string mysql = "UPDATE RegistrosDeReciclaje SET IdResidente = @IdResidente, IdResiduo = @IdResiduo, PesoKilogramo = @PesoKilogramo, FechaRegistro = @FechaRegistro, TicketsGanados= @TicketsGanados" +
                       "WHERE IdRegistrosReciclaje = @IdRegistrosReciclaje";

        await _db.ExecuteAsync(mysql, registrosDeReciclaje);
        return registrosDeReciclaje;
    }
}
