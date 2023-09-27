using MadWorld.FlightSimulator.Domain.DataRetriever;
using Microsoft.FlightSimulator.SimConnect;
using System.Runtime.InteropServices;

namespace MadWorld.FlightSimulator.Connector;

public class SimClient : ISimClient, IDisposable
{
    const int RequestID = 1;
    const int WM_USER_SIMCONNECT = 0x0402;

    private SimConnect? simConnect;

    public bool TryOpen()
    {
        try
        {
            simConnect = new SimConnect("Managed Data Request", IntPtr.Zero, WM_USER_SIMCONNECT, null, 0);
            RegisterDefinitions(SimConnect_OnRevSimobjectData);

            simConnect.AddToDataDefinition(
                RequestTypes.AirplaneInformation,
                "Plane Altitude",
                "feet",
                SIMCONNECT_DATATYPE.FLOAT64,
                0,
                SimConnect.SIMCONNECT_UNUSED);

            simConnect.RegisterDataDefineStruct<Struct1>(RequestTypes.AirplaneInformation);

            return true;
        }
        catch (COMException)
        {
            return false;
        }
    }

    public void Dispose()
    {
        if (simConnect != null)
        {
            simConnect.Dispose();
            simConnect = null;
        }
    }

    public void RegisterDefinitions(SimConnect.RecvSimobjectDataBytypeEventHandler dataRetriever)
    {
        simConnect!.OnRecvSimobjectDataBytype += dataRetriever;
    }

    private static void SimConnect_OnRevSimobjectData(SimConnect sender, SIMCONNECT_RECV_SIMOBJECT_DATA_BYTYPE data)
    {
        Console.WriteLine("Ping! V2");
    }

    public void RegisterDefinitions()
    {
        Console.WriteLine("Ping! V3");
    }

    public void ReceiveMessage()
    {
        simConnect!.RequestDataOnSimObjectType(RequestTypes.AirplaneInformation, RequestTypes.AirplaneInformation, 0, SIMCONNECT_SIMOBJECT_TYPE.USER);
        Console.WriteLine("Ping!");
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    struct Struct1
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public String title;
        public double latitude;
        public double longitude;
        public double altitude;
    };
}