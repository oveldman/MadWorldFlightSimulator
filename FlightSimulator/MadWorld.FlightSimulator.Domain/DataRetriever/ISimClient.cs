namespace MadWorld.FlightSimulator.Domain.DataRetriever
{
    public interface ISimClient
    {
        bool TryOpen();
        void ReceiveMessage();
        void RegisterDefinitions<T>(DataTypes type, Action<T> infoRetriever);
        void Dispose();
    }
}