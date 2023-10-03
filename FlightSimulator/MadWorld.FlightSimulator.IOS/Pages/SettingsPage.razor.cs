using MadWorld.FlightSimulator.IOS.Infrastructure.Database;
using Microsoft.AspNetCore.Components;

namespace MadWorld.FlightSimulator.IOS.Pages
{
    public partial class SettingsPage
    {
        private bool savedSuccesfully = false;

        private Settings CurrentSettings { get; set; } = new Settings();

        [Inject]
        public SettingsDatabase Database { get; set; }

        protected override async Task OnInitializedAsync()
        {
            CurrentSettings = await Database.GetSettingsAsync() ?? new Settings();

            await base.OnInitializedAsync();
        }

        private async Task SaveSettings()
        {
            var result = await Database.SaveAsync(CurrentSettings);
            savedSuccesfully = result == 1;
        }
    }
}
