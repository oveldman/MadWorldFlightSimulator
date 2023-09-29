using System.Runtime.InteropServices;

namespace MadWorld.FlightSimulator.Domain.DataRetriever;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct AirplaneInfo
{
    public double altitude;
};
