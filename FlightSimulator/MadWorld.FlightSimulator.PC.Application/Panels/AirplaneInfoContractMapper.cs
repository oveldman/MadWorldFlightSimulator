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
                Title = info.title,
                Altitude = info.altitude,
                IsPlaneOnGround = info.onGround != 0,
                IsAutoPilotOn = info.autopilotMaster != 0
            };
        }
    }
}
