using MadWorld.FlightSimulator.Domain.DataRetriever;
using System.Formats.Asn1;

namespace MadWorld.FlightSimulator.Simulator;

public class SimClient<TType> : ISimClient where TType : struct
{
    private readonly List<Action<TType>> AllInfoRetrievers = new();
    
    public bool IsConnected { private set; get; }
    public bool TryOpen()
    {
        IsConnected = true;

        return IsConnected;
    }

    public void Disconnect()
    {
        RandomFlightDataGenerator<TType>.Reset();
        IsConnected = false;
    }

    public Task StartMessageService()
    {
        while (IsConnected)
        {
            ReceiveMessage();
            Thread.Sleep(1000);
        }

        return Task.CompletedTask;
    }

    public void ReceiveMessage()
    {
        AllInfoRetrievers.ForEach(r =>
        {
            var info = RandomFlightDataGenerator<TType>.Generate();
            r.Invoke(info);
        });
    }

    public void RegisterDefinitions<T>(DataTypes type, Action<T> infoRetriever)
    {
        var cast = infoRetriever as Action<TType>;

        if (cast == null)
        {
            throw new ArgumentException("infoRetriever is the wrong type", nameof(infoRetriever));
        }
        
        AllInfoRetrievers.Add(cast!);
    }

    public void Dispose()
    {
        IsConnected = false;
    }

    public void PressButton(EventTypes eventType, uint data)
    {
        if (eventType == EventTypes.KEY_AUTOPILOT_ON)
        {
            RandomFlightDataGenerator<TType>.SetAutoPilot(true);
        }

        if (eventType == EventTypes.KEY_AUTOPILOT_OFF)
        {
            RandomFlightDataGenerator<TType>.SetAutoPilot(false);
        }
    }
}