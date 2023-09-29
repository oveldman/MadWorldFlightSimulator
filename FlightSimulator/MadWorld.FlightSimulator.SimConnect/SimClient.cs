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

            simConnect.AddToDataDefinition(
                RequestTypes.AirplaneInformation,
                "Plane Altitude",
                "feet",
                SIMCONNECT_DATATYPE.FLOAT64,
                0,
                SimConnect.SIMCONNECT_UNUSED);

            simConnect.RegisterDataDefineStruct<AirplaneInfo>(RequestTypes.AirplaneInformation);

            RegisterDefinitions(Ping);

            simConnect.RequestDataOnSimObject(DataTypes.GetAltitude, 
                RequestTypes.AirplaneInformation, 
                SimConnect.SIMCONNECT_OBJECT_ID_USER, 
                SIMCONNECT_PERIOD.SECOND, 
                SIMCONNECT_DATA_REQUEST_FLAG.DEFAULT,
                0, 0, 0);

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

    public void RegisterDefinitions(Action<AirplaneInfo> infoRetriever)
    {
        void handler(SimConnect x, SIMCONNECT_RECV_SIMOBJECT_DATA y) => infoRetriever(Convert(y));
        simConnect!.OnRecvSimobjectData += handler;
    }

    public void RegisterDefinitions()
    {
        Console.WriteLine("Ping! V3");
    }

    public void ReceiveMessage()
    {
        simConnect!.ReceiveMessage();
        Console.WriteLine("Ping!");
    }

    public void Ping(AirplaneInfo info)
    {
        Console.WriteLine("Ping! V4");
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct AirplaneInfo
    {
        public double altitude;
    };

    private static AirplaneInfo Convert(SIMCONNECT_RECV_SIMOBJECT_DATA data)
    {
        if (data.dwRequestID == (uint)DataTypes.GetAltitude)
        {
            return (AirplaneInfo)data.dwData[0];
        }

        return new AirplaneInfo();
    }
}