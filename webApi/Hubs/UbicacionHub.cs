using Microsoft.AspNetCore.SignalR;
namespace webApi.Hubs;
public class UbicacionHub : Hub
{
    public async Task EnviarUbicacion(string camionId, double latitud, double longitud)
    {
        await Clients.All.SendAsync("RecibirUbicacion", camionId, latitud, longitud);
    }
}
