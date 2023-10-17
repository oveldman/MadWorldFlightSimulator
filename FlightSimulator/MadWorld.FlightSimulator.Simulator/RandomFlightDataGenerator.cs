using MadWorld.FlightSimulator.Domain.DataRetriever;
using System.Diagnostics;

namespace MadWorld.FlightSimulator.Simulator;

public static class RandomFlightDataGenerator<TType>
{
    private const string Name = "Simulator Airplane";
    private static double AutoPilot = 0;
    private static double AutoPilotAltitude = 5000;

    public static TType Generate()
    {
        if (typeof(TType) == typeof(AirplaneInfo))
        {
            return (TType)(object)new AirplaneInfo
            {
                title = Name,
                altitude = new Random().Next(0, 10000),
                autopilotMaster = AutoPilot,
                autopilotAltitude = AutoPilotAltitude,
                onGround = 0
            };
        }
        
        return default!;
    }

    public static void SetAutoPilot(bool isActivated)
    {
        AutoPilot = isActivated ? 1 : 0;
    }

    public static void Reset()
    {
        AutoPilot = 0;
    }

    public static void IncreaseAltitudeAutoPilot(uint altitudeChange)
    {
        AutoPilotAltitude += altitudeChange;
    }
    
    public static void DecreaseAltitudeAutoPilot(uint altitudeChange)
    {
        if (AutoPilotAltitude - altitudeChange < 0)
        {
            AutoPilotAltitude = 0;
            return;
        }
        
        AutoPilotAltitude -= altitudeChange;
    }
}