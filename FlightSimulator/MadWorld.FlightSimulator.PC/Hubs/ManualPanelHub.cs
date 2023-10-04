using MadWorld.FlightSimulator.Domain.DataRetriever;
using MadWorld.FlightSimulator.Domain.Panels;
using MadWorld.FlightSimulator.PC.Application.Panels;
using Microsoft.AspNetCore.SignalR;

namespace MadWorld.FlightSimulator.PC.Hubs
{
    public class ManualPanelHub : IHub
    {
        private readonly IPanelSubject subject;
        private readonly IHubContext<PanelHub> panelHub;

        public ManualPanelHub(IPanelSubject subject, IHubContext<PanelHub> hub)
        {
            this.subject = subject;
            this.panelHub = hub;
        }

        public async Task SendAirplaneInformation(AirplaneInfo info)
        {
            var model = AirplaneInfoContractMapper.ToContract(info);

            await panelHub.Clients.All.SendAsync("ReceiveAirplaneInformation", model);
        }
    }
}
