using MadWorld.FlightSimulator.Domain.DataRetriever;

namespace MadWorld.FlightSimulator.Domain.Panels
{
    public interface IPanelSubject
    {
        void RegisterHub(IHub hub);
        void UnregisterHub(IHub hub);
        void NotifyHubs();
        void SetAirplaneInformation(AirplaneInfo info);
    }
}
