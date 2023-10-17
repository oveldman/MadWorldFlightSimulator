using MadWorld.FlightSimulator.Domain.DataRetriever;
using System.Diagnostics;

namespace MadWorld.FlightSimulator.Simulator;

public static class RandomFlightDataGenerator<TType>
{
    private const string Name = "Simulator Airplane";
    // ReSharper disable once StaticMemberInGenericType
    private static double _autoPilot = 0;
    // ReSharper disable once StaticMemberInGenericType
    private static double _autoPilotAltitude = 5000;

    public static TType Generate()
    {
        if (typeof(TType) == typeof(AirplaneInfo))
        {
            return (TType)(object)new AirplaneInfo
            {
                title = Name,
                altitude = new Random().Next(0, 10000),
                autopilotMaster = _autoPilot,
                autopilotAltitude = _autoPilotAltitude,
                onGround = 0
            };
        }
        
        return default!;
    }

    public static void SetAutoPilot(bool isActivated)
    {
        _autoPilot = isActivated ? 1 : 0;
    }

    public static void Reset()
    {
        _autoPilot = 0;
    }

    public static void IncreaseAltitudeAutoPilot(uint altitudeChange)
    {
        _autoPilotAltitude += altitudeChange;
    }
    
    public static void DecreaseAltitudeAutoPilot(uint altitudeChange)
    {
        if (_autoPilotAltitude - altitudeChange < 0)
        {
            _autoPilotAltitude = 0;
            return;
        }
        
        _autoPilotAltitude -= altitudeChange;
    }
}