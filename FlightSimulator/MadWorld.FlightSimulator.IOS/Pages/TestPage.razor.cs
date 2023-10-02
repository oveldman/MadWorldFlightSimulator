using Microsoft.AspNetCore.SignalR.Client;

namespace MadWorld.FlightSimulator.IOS.Pages
{
    public partial class TestPage
    {
        private const string User = "TestUser";
        private const string Message = "Test message";

        private string ServerReturned = string.Empty;

        private HubConnection _hubConnection;

        protected override async Task OnInitializedAsync()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7131/TestHub")
                .Build();

            await _hubConnection.StartAsync();

            _hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                ServerReturned = message;
            });

            await base.OnInitializedAsync();
        }

        private async Task SendMessage()
        {
            await _hubConnection.InvokeAsync("SendMessage", User, Message);
        }
    }
}
