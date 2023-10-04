using MadWorld.FlightSimulator.Domain.DataRetriever;
using Microsoft.Extensions.DependencyInjection;

namespace MadWorld.FlightSimulator.Simulator;

public static class IServiceCollectionExtensions
{
    public static void AddSimulator(this IServiceCollection services)
    {
        services.AddSingleton<ISimClient, SimClient<AirplaneInfo>>();
    } 
}