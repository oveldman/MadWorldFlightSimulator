using MadWorld.FlightSimulator.Connector;
using MadWorld.FlightSimulator.Domain.DataRetriever;

ISimClient client = new SimClient();

if (client.TryOpen()) 
{
    IAirplaneInformationClient airplaneInformationClient = new AirplaneInformationClient(client);

    while (true)
    {
        client.ReceiveMessage();
        Thread.Sleep(1000);
    }
}

Console.ReadLine();