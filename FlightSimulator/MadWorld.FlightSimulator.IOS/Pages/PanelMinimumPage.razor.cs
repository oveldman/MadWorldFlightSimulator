using Microsoft.AspNetCore.Components;

namespace MadWorld.FlightSimulator.IOS.Pages
{
    public partial class PanelMinimumPage
    {
        [Inject] 
        private NavigationManager NavigationManager { get; set; } = null!;

        private void OpenPanelMaximum()
        {
            NavigationManager.NavigateTo("/panelMaximum");
        }
    }
}
