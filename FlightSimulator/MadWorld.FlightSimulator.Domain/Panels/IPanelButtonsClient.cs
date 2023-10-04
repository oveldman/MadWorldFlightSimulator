using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadWorld.FlightSimulator.Domain.Panels
{
    public interface IPanelButtonsClient
    {
        void TurnOnAutoPilot();
        void TurnOffAutoPilot();
    }
}
