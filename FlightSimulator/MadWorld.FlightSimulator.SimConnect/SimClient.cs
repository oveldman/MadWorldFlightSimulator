using MadWorld.FlightSimulator.Domain.DataRetriever;
using Microsoft.FlightSimulator.SimConnect;
using System;
using System.Runtime.InteropServices;

namespace MadWorld.FlightSimulator.Connector;

public class SimClient : ISimClient, IDisposable
{
    const int WM_USER_SIMCONNECT = 0x0402;

    private SimConnect? simConnect;

    public bool TryOpen()
    {
        try
        {
            simConnect = new SimConnect("Managed Data Request", IntPtr.Zero, WM_USER_SIMCONNECT, null, 0);
            AddAirplaneInfo();

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

    public void RegisterDefinitions<T>(DataTypes type, Action<T> infoRetriever)
    {
        void handler(SimConnect x, SIMCONNECT_RECV_SIMOBJECT_DATA y) => infoRetriever(Convert<T>(type, y));
        simConnect!.OnRecvSimobjectData += handler;
    }

    public void ReceiveMessage()
    {
        simConnect!.ReceiveMessage();
    }

    private void AddAirplaneInfo()
    {
        simConnect!.AddToDataDefinition(
            RequestTypes.AirplaneInformation,
            "Plane Altitude",
            "feet",
            SIMCONNECT_DATATYPE.FLOAT64,
            0,
            SimConnect.SIMCONNECT_UNUSED);

        simConnect.RegisterDataDefineStruct<AirplaneInfo>(RequestTypes.AirplaneInformation);

        simConnect.RequestDataOnSimObject(DataTypes.GetAltitude,
            RequestTypes.AirplaneInformation,
            SimConnect.SIMCONNECT_OBJECT_ID_USER,
            SIMCONNECT_PERIOD.SECOND,
            SIMCONNECT_DATA_REQUEST_FLAG.DEFAULT,
            0, 0, 0);
    }

    private static T Convert<T>(DataTypes type, SIMCONNECT_RECV_SIMOBJECT_DATA data)
    {
        if (data.dwRequestID == (uint)type)
        {
            return (T)data.dwData[0];
        }

        return default!;
    }
}