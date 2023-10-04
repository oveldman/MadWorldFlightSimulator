using MadWorld.FlightSimulator.Domain.DataRetriever;
using MadWorld.FlightSimulator.Domain.Panels;

namespace MadWorld.FlightSimulator.PC.Application.Panels
{
    public class PanelSubject : IPanelSubject
    {
        private AirplaneInfo airplaneInfo = new AirplaneInfo();
        private static readonly List<IHub> hubs = new();

        public void NotifyHubs()
        {
            hubs.ForEach(hub => hub.SendAirplaneInformation(airplaneInfo));
        }

        public void RegisterHub(IHub hub)
        {
            hubs.Add(hub);
        }

        public void UnregisterHub(IHub hub)
        {
            hubs.Remove(hub);
        }

        public void SetAirplaneInformation(AirplaneInfo info)
        {
            airplaneInfo = info;
            NotifyHubs();
        }
    }
}
