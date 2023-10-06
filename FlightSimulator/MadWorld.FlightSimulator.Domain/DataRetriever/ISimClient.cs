namespace MadWorld.FlightSimulator.Domain.DataRetriever
{
    public interface ISimClient
    {
        bool IsConnected { get; }
        bool TryOpen();
        void Disconnect();
        Task StartMessageService();
        void PressButton(EventTypes eventType);
        void ReceiveMessage();
        void RegisterDefinitions<T>(DataTypes type, Action<T> infoRetriever);
        void Dispose();
    }
}