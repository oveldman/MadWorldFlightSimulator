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

        public void SelectAtcOption(int option)
        {
            Client.SelectAtcOption(option);
        }

        public void IncreaseAltitudeAutoPilotByHundert()
        {
            Client.IncreaseAltitudeAutoPilot(AltitudeChangeValues.Hundert);
        }

        public void DecreaseAltitudeAutoPilotByHundert()
        {
            Client.DecreaseAltitudeAutoPilot(AltitudeChangeValues.Hundert);
        }

        public void IncreaseAltitudeAutoPilotByThousand()
        {
            Client.IncreaseAltitudeAutoPilot(AltitudeChangeValues.Thousand);
        }

        public void DecreaseAltitudeAutoPilotByThousand()
        {
            Client.DecreaseAltitudeAutoPilot(AltitudeChangeValues.Thousand);
        }
    }
}
