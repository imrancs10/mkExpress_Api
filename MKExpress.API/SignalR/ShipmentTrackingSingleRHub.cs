using Microsoft.AspNetCore.SignalR;

namespace MKExpress.API.SignalR
{
    public class ShipmentTrackingSingleRHub:Hub
    {
        public async Task ShipmentTrackingUpdate(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveShipmentUpdate", user, message);
        }
    }
}
