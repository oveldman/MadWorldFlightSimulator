using MadWorld.FlightSimulator.Domain.DataRetriever;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadWorld.FlightSimulator.Domain.Panels
{
    public interface IHub
    {
        Task SendAirplaneInformation(AirplaneInfo info);
    }
}
