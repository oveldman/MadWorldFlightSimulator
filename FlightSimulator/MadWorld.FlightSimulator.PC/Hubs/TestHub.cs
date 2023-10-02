using Microsoft.AspNetCore.SignalR;

namespace MadWorld.FlightSimulator.PC.Hubs
{
    public class TestHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            message += " + ping back!";

            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
