using webApi.Models;

namespace webApi.Repository.Interfaces;
public interface IResidenteRepositorio
{
    Task<Residente> BuscarPorDni(string dni);
    Task<Residente> AgregarResidente(Residente residente);
    Task<Residente> EditarResidente(Residente residente);
    Task<IEnumerable<Residente>> GetResidentes();
}
