using MadWorld.FlightSimulator.Domain.DataRetriever;
using MadWorld.FlightSimulator.Domain.Panels;
using Microsoft.AspNetCore.Components;

namespace MadWorld.FlightSimulator.PC.Pages
{
    public partial class SimClient : IHub, IDisposable
    {
        private AirplaneInfo AirplaneInfo = new();
        private bool HasError = false;

        [Inject]
        public ISimClient Client { get; set; } = null!;

        [Inject]
        public IAirplaneInformationClient InformationClient { get; set; } = null!;

        [Inject]
        public IPanelSubject PanelSubject { get; set; } = null!;

        [Inject]
        public IHub ManualPanelHub { get; set; } = null!;

        public void Connect()
        {
            if (Client.TryOpen())
            {
                InformationClient.Init();
                PanelSubject.RegisterHub(ManualPanelHub);
                PanelSubject.RegisterHub(this);

                Task.Run(() => Client.StartMessageService());
            }
            else
            {
                HasError = true;
            }
        }

        public void Disconnect()
        {
            PanelSubject.UnregisterHub(ManualPanelHub);
            PanelSubject.UnregisterHub(this);
            Client.Disconnect();
        }

        public void Dispose()
        {
            PanelSubject.UnregisterHub(this);
        }

        public Task SendAirplaneInformation(AirplaneInfo info)
        {
            AirplaneInfo = info;
            InvokeAsync(StateHasChanged);

            return Task.CompletedTask;
        }
    }
}
