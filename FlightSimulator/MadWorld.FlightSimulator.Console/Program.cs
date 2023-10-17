using MadWorld.FlightSimulator.Connector;
using MadWorld.FlightSimulator.Domain.DataRetriever;
using MadWorld.FlightSimulator.Domain.Panels;
using MadWorld.FlightSimulator.PC.Application;
using MadWorld.FlightSimulator.PC.Application.Panels;

IPanelSubject subject = new MadWorld.FlightSimulator.Console.PanelSubject();
ISimClient client = new SimClient();
IPanelButtonsClient panelClient = new PanelButtonsClient(client);

if (client.TryOpen()) 
{
    IAirplaneInformationClient airplaneInformationClient = new AirplaneInformationClient(subject, client);
    airplaneInformationClient.Init();

    panelClient.IncreaseAltitudeAutoPilot(AltitudeChangeValues.Thousand);

    while (true)
    {
        client.ReceiveMessage();
        Thread.Sleep(1000);
    }
}

Console.WriteLine("Finsihed");

Console.ReadLine();