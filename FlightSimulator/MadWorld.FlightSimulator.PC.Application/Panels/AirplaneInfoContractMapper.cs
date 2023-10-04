using MadWorld.FlightSimulator.Domain.DataRetriever;
using MadWorld.FlightSimulator.Domain.Panels;

namespace MadWorld.FlightSimulator.PC.Application.Panels
{
    public static class AirplaneInfoContractMapper
    {
        public static AirplaneInfoContract ToContract(AirplaneInfo info)
        {
            return new AirplaneInfoContract()
            {
                Altitude = info.altitude
            };
        }
    }
}
