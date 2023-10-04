using MadWorld.FlightSimulator.Domain.DataRetriever;

namespace MadWorld.FlightSimulator.Simulator;

public static class RandomFlightDataGenerator<TType>
{
    public static TType Generate()
    {
        if (typeof(TType) == typeof(AirplaneInfo))
        {
            return (TType)(object)new AirplaneInfo
            {
                altitude = new Random().Next(0, 10000)
            };
        }
        
        return default(TType)!;
    }
}