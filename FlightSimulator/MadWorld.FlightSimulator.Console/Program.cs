using MadWorld.FlightSimulator.Connector;
using MadWorld.FlightSimulator.Console;
using MadWorld.FlightSimulator.Domain.DataRetriever;
using MadWorld.FlightSimulator.Domain.Panels;
using MadWorld.FlightSimulator.PC.Application;

IPanelSubject subject = new PanelSubject();
ISimClient client = new SimClient();

if (client.TryOpen()) 
{
    IAirplaneInformationClient airplaneInformationClient = new AirplaneInformationClient(subject, client);
    airplaneInformationClient.Init();

    while (true)
    {
        client.ReceiveMessage();
        Thread.Sleep(1000);
    }
}

Console.ReadLine();