using MadWorld.FlightSimulator.Domain.DataRetriever;
using MadWorld.FlightSimulator.Domain.Panels;

namespace MadWorld.FlightSimulator.Console
{
    internal class PanelSubject : IPanelSubject
    {
        public void NotifyHubs()
        {
        }

        public void RegisterHub(IHub hub)
        {
        }

        public void SetAirplaneInformation(AirplaneInfo info)
        {
            System.Console.WriteLine($"{info.title}: Altitude: {info.altitude} & Autopilot on?: {info.autopilotMaster != 0} & On ground?: {info.onGround != 0}");
        }

        public void UnregisterHub(IHub hub)
        {
        }
    }
}
