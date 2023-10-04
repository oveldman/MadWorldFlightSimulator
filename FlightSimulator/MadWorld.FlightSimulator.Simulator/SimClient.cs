using MadWorld.FlightSimulator.Domain.DataRetriever;

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

    public async Task StartMessageService()
    {
        while (IsConnected)
        {
            ReceiveMessage();
            Thread.Sleep(1000);
        }
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
}