using MadWorld.FlightSimulator.Domain.DataRetriever;
using MadWorld.FlightSimulator.Domain.Panels;
using MadWorld.FlightSimulator.PC.Application.Panels;
using Microsoft.Extensions.DependencyInjection;

namespace MadWorld.FlightSimulator.PC.Application
{
    public static class IServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<IAirplaneInformationClient, AirplaneInformationClient>();
            services.AddSingleton<IPanelSubject, PanelSubject>();
        }
    }
}
