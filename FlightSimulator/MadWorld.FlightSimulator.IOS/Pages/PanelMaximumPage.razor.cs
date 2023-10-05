using Microsoft.AspNetCore.Components;

namespace MadWorld.FlightSimulator.IOS.Pages;

public partial class PanelMaximumPage
{
    [Inject] 
    private NavigationManager NavigationManager { get; set; } = null!;

    private void OpenPanelMinimum()
    {
        NavigationManager.NavigateTo("/panelMinimum");
    }
}