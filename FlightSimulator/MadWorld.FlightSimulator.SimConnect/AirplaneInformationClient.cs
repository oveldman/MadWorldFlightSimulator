using MadWorld.FlightSimulator.Domain.DataRetriever;
using Microsoft.FlightSimulator.SimConnect;

namespace MadWorld.FlightSimulator.Connector
{
    public class AirplaneInformationClient : IAirplaneInformationClient
    {
        private readonly ISimClient _client;

        public AirplaneInformationClient(ISimClient client)
        {
            _client = client;
            Init();
        }

        private void Init()
        {
            _client.RegisterDefinitions<AirplaneInfo>(DataTypes.GetAltitude, GetAirplaneInfo);
        }

        private void GetAirplaneInfo(AirplaneInfo info)
        {
            Console.WriteLine($"Ping {info.altitude}");
        }
    }
}
