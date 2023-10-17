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

        public void SelectAtcOption(int option)
        {
            switch (option)
            {
                case 0:
                    Client.PressButton(EventTypes.KEY_ATC_MENU_SELECT_0);
                    break;
                case 1:
                    Client.PressButton(EventTypes.KEY_ATC_MENU_SELECT_1);
                    break;
                case 2:
                    Client.PressButton(EventTypes.KEY_ATC_MENU_SELECT_2);
                    break;
                case 3:
                    Client.PressButton(EventTypes.KEY_ATC_MENU_SELECT_3);
                    break;
                case 4:
                    Client.PressButton(EventTypes.KEY_ATC_MENU_SELECT_4);
                    break;
                case 5:
                    Client.PressButton(EventTypes.KEY_ATC_MENU_SELECT_5);
                    break;
                case 6:
                    Client.PressButton(EventTypes.KEY_ATC_MENU_SELECT_6);
                    break;
                case 7:
                    Client.PressButton(EventTypes.KEY_ATC_MENU_SELECT_7);
                    break;
                case 8:
                    Client.PressButton(EventTypes.KEY_ATC_MENU_SELECT_8);
                    break;
                case 9:
                    Client.PressButton(EventTypes.KEY_ATC_MENU_SELECT_9);
                    break;
            }
        }
    }
}
