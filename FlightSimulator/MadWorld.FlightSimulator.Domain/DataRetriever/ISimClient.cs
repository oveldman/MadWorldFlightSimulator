namespace MadWorld.FlightSimulator.Domain.DataRetriever
{
    public interface ISimClient
    {
        bool TryOpen();
        void RegisterDefinitions();
        void ReceiveMessage();
    }
}