using MadWorld.FlightSimulator.Domain.DataRetriever;
using Microsoft.FlightSimulator.SimConnect;

namespace MadWorld.FlightSimulator.Connector
{
    public class AirplaneInformationClient : IAirplaneInformationClient
    {
        private ISimClient _client;

        public AirplaneInformationClient(ISimClient client)
        {
            _client = client;
        }
    }
}
