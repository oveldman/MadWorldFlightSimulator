using System.Runtime.InteropServices;

namespace MadWorld.FlightSimulator.Domain.DataRetriever;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct AirplaneInfo
{
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
    public String title;
    public double altitude;
    public double onGround;
    public double autopilotMaster;
    public double autopilotAltitude;
};
