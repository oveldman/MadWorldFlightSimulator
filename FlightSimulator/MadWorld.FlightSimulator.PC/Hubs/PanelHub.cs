using MadWorld.FlightSimulator.Domain.DataRetriever;
using MadWorld.FlightSimulator.Domain.Panels;
using MadWorld.FlightSimulator.PC.Application.Panels;
using Microsoft.AspNetCore.SignalR;

namespace MadWorld.FlightSimulator.PC.Hubs
{
    public sealed class PanelHub : Hub
    {
        private readonly IPanelButtonsClient Client;

        public PanelHub(IPanelButtonsClient client)
        {
            Client = client;
        }

        public void TurnOnAutoPilot()
        {
            Client.TurnOnAutoPilot();
        }

        public void TurnOffAutoPilot()
        {
            Client.TurnOffAutoPilot();
        }
    }
}
