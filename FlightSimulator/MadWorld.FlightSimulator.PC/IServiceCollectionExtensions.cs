using MadWorld.FlightSimulator.Domain.Panels;
using MadWorld.FlightSimulator.PC.Hubs;

namespace MadWorld.FlightSimulator.PC
{
    public static class IServiceCollectionExtensions
    {
        public static void AddPC(this IServiceCollection services)
        {
            services.AddSingleton<IHub, ManualPanelHub>();
        }
    }
}
