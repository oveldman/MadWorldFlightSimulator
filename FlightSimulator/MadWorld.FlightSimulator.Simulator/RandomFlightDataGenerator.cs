using MadWorld.FlightSimulator.Domain.DataRetriever;
using System.Diagnostics;

namespace MadWorld.FlightSimulator.Simulator;

public static class RandomFlightDataGenerator<TType>
{
    private const string Name = "Simulator Airplane";
    private static double AutoPilot = 0;

    public static TType Generate()
    {
        if (typeof(TType) == typeof(AirplaneInfo))
        {
            return (TType)(object)new AirplaneInfo
            {
                title = Name,
                altitude = new Random().Next(0, 10000),
                autopilotMaster = AutoPilot,
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
}