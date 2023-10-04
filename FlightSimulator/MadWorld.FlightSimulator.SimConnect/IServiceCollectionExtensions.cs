using MadWorld.FlightSimulator.Domain.DataRetriever;
using Microsoft.Extensions.DependencyInjection;

namespace MadWorld.FlightSimulator.Connector
{
    public static class IServiceCollectionExtensions
    {
        public static void AddSimClient(this IServiceCollection services)
        {
            services.AddSingleton<ISimClient, SimClient>();
        } 
    }
}
