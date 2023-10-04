using MadWorld.FlightSimulator.Domain.DataRetriever;
using MadWorld.FlightSimulator.Domain.Panels;

namespace MadWorld.FlightSimulator.PC.Application
{
    public class AirplaneInformationClient : IAirplaneInformationClient
    {
        private readonly IPanelSubject panelSubject;
        private readonly ISimClient _client;

        public AirplaneInformationClient(IPanelSubject subject, ISimClient client)
        {
            panelSubject = subject;
            _client = client;
        }

        public void Init()
        {
            _client.RegisterDefinitions<AirplaneInfo>(DataTypes.GetAltitude, GetAirplaneInfo);
        }

        private void GetAirplaneInfo(AirplaneInfo info)
        {
            panelSubject.SetAirplaneInformation(info);
        }
    }
}
