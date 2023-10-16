using MadWorld.FlightSimulator.Domain.DataRetriever;
using MadWorld.FlightSimulator.Domain.Panels;

namespace MadWorld.FlightSimulator.PC.Application.Panels
{
    public class PanelButtonsClient : IPanelButtonsClient
    { 
        private ISimClient Client;

        public PanelButtonsClient(ISimClient client)
        {
            Client = client;
        }

        public void TurnOnAutoPilot()
        {
            Client.PressButton(EventTypes.KEY_AUTOPILOT_ON);
        }

        public void TurnOffAutoPilot()
        {
            Client.PressButton(EventTypes.KEY_AUTOPILOT_OFF);
        }

        public void IncreaseAltitudeAutoPilot(AltitudeChangeValues altitudeChangeValues)
        {
            Client.PressButton(EventTypes.KEY_AUTOPILOT_INCREASE_ALTITUDE, (uint)altitudeChangeValues);
        }

        public void DecreaseAltitudeAutoPilot(AltitudeChangeValues altitudeChangeValues)
        {
            Client.PressButton(EventTypes.KEY_AUTOPILOT_DECREASE_ALTITUDE, (uint)altitudeChangeValues);
        }
    }
}
