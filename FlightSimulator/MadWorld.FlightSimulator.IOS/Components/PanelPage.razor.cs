﻿using MadWorld.FlightSimulator.Domain.Panels;
using MadWorld.FlightSimulator.IOS.Extensions;
using MadWorld.FlightSimulator.IOS.Infrastructure.Database;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace MadWorld.FlightSimulator.IOS.Components
{
    public partial class PanelPage
    {
        private bool Connected = false;
        private bool HasError = false;

        private AirplaneInfoContract airplaneInfo = new();

        private HubConnection _hubConnection;

        [Inject]
        public SettingsDatabase Database { get; set; }

        public async Task Connect()
        {
            try
            {
                var currentSettings = await Database.GetSettingsAsync() ?? new Settings();

                var hubUrl = new Uri(new Uri(currentSettings.ApiUrl), "PanelHub");
                await StartSignalR(hubUrl);

                Connected = true;
            }
            catch (Exception)
            {
                HasError = true;
                Connected = false;
            }
        }

        private string GetButtonClass(bool isTurnedOn)
        {
            return isTurnedOn ? "panel-button-on" : "panel-button-off";
        }

        private async Task StartSignalR(Uri hubUrl)
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(hubUrl, (options) =>
                {
                    options.IgnoreSsl();
                })
                .Build();

            await _hubConnection.StartAsync();

            _hubConnection.On<AirplaneInfoContract>("ReceiveAirplaneInformation", (info) =>
            {
                airplaneInfo = info;
                InvokeAsync(StateHasChanged);
            });
        }

        private async Task ChangeAutoPilot()
        {
            if (airplaneInfo.IsAutoPilotOn)
            {
                await _hubConnection.InvokeAsync("TurnOffAutoPilot");
            }
            else
            {
                await _hubConnection.InvokeAsync("TurnOnAutoPilot");
            }
        }
    }
}
