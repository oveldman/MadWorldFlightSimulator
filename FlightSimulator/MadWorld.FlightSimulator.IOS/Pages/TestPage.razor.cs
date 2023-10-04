using MadWorld.FlightSimulator.IOS.Extensions;
using MadWorld.FlightSimulator.IOS.Infrastructure.Database;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace MadWorld.FlightSimulator.IOS.Pages
{
    public partial class TestPage
    {
        private const string User = "TestUser";
        private const string Message = "Test message";

        private bool Waiting = true;
        private bool HasError = false;
        private string ErrorMessage = string.Empty;
        private string ServerReturned = string.Empty;

        private HubConnection _hubConnection;

        [Inject]
        public SettingsDatabase Database { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var currentSettings = await Database.GetSettingsAsync() ?? new Settings();

            try
            {
                var hubUrl = new Uri(new Uri(currentSettings.ApiUrl), "TestHub");

                await StartSignalR(hubUrl);
            }
            catch (Exception ex)
            {
                HasError = true;
                ErrorMessage = ex.Message + currentSettings.ApiUrl;
            }

            Waiting = false;
            await base.OnInitializedAsync();
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

            _hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                ServerReturned = message;
            });
        }

        private async Task SendMessage()
        {
            await _hubConnection.InvokeAsync("SendMessage", User, Message);
        }
    }
}
