namespace MadWorld.FlightSimulator.Domain.DataRetriever
{
    public interface ISimClient
    {
        bool IsConnected { get; }
        bool TryOpen();
        Task StartMessageService();
        void ReceiveMessage();
        void RegisterDefinitions<T>(DataTypes type, Action<T> infoRetriever);
        void Dispose();
    }
}