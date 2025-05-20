using webApi.Models;

namespace webApi.Repository.Interfaces;

public interface IRegistroReciclajeRepositorio
{
    Task<RegistrosDeReciclaje> GetRegistrosDeReciclaje(int idRegistroReciclaje);
    Task<IEnumerable<RegistrosDeReciclaje>> GetRegistrosDeReciclaje();
    Task<RegistrosDeReciclaje> CreateRegistrosDeReciclaje(RegistrosDeReciclaje registrosDeReciclaje);
    Task<RegistrosDeReciclaje> UpdateRegistrosDeReciclaje(RegistrosDeReciclaje registrosDeReciclaje);
    Task DeleteRegistrosDeReciclaje(int idRegistroReciclaje);
}
