using MadWorld.FlightSimulator.Domain.DataRetriever;
using MadWorld.FlightSimulator.Domain.Panels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadWorld.FlightSimulator.Console
{
    internal class PanelSubject : IPanelSubject
    {
        public void NotifyHubs()
        {
        }

        public void RegisterHub(IHub hub)
        {
        }

        public void SetAirplaneInformation(AirplaneInfo info)
        {
            System.Console.WriteLine($"Altitude: {info.altitude}");
        }

        public void UnregisterHub(IHub hub)
        {
        }
    }
}
