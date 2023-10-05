using MadWorld.FlightSimulator.Domain.DataRetriever;
using Microsoft.FlightSimulator.SimConnect;
using System.Runtime.InteropServices;

namespace MadWorld.FlightSimulator.Connector;

public class SimClient : ISimClient, IDisposable
{
    const int WM_USER_SIMCONNECT = 0x0402;

    private SimConnect? simConnect;

    public bool IsConnected { get; private set; }

    public bool TryOpen()
    {
        try
        {
            simConnect = new SimConnect("Managed Data Request", IntPtr.Zero, WM_USER_SIMCONNECT, null, 0);
            AddAutoPilot();
            AddAirplaneInfo();

            IsConnected = true;
            return IsConnected;
        }
        catch (COMException)
        {
            IsConnected = false;
            return false;
        }
    }

    public async Task StartMessageService()
    {
        while (IsConnected)
        {
            simConnect!.ReceiveMessage();
            Thread.Sleep(1000);
        }
    }

    public void Dispose()
    {
        IsConnected = false;
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
            "Title", 
            null, 
            SIMCONNECT_DATATYPE.STRING256, 
            0,
            SimConnect.SIMCONNECT_UNUSED);

        simConnect.AddToDataDefinition(
            RequestTypes.AirplaneInformation,
            "Plane Altitude",
            "feet",
            SIMCONNECT_DATATYPE.FLOAT64,
            0,
            SimConnect.SIMCONNECT_UNUSED);

        simConnect.AddToDataDefinition(
            RequestTypes.AirplaneInformation,
            "SIM ON GROUND",
            "Boolean",
            SIMCONNECT_DATATYPE.FLOAT64,
            0.0f,
            SimConnect.SIMCONNECT_UNUSED);

        simConnect.AddToDataDefinition(
            RequestTypes.AirplaneInformation,
            "AUTOPILOT MASTER",
            "Boolean",
            SIMCONNECT_DATATYPE.FLOAT64,
            0.0f,
            SimConnect.SIMCONNECT_UNUSED);


        simConnect.RegisterDataDefineStruct<AirplaneInfo>(RequestTypes.AirplaneInformation);

        simConnect.RequestDataOnSimObject(
            DataTypes.AirplaneInformation,
            RequestTypes.AirplaneInformation,
            SimConnect.SIMCONNECT_OBJECT_ID_USER,
            SIMCONNECT_PERIOD.SECOND,
            SIMCONNECT_DATA_REQUEST_FLAG.DEFAULT,
            0, 0, 0);
    }

    private void AddAutoPilot()
    {
        simConnect!.MapClientEventToSimEvent(EventTypes.KEY_AUTOPILOT_ON, "AUTOPILOT_ON");
        simConnect!.AddClientEventToNotificationGroup(Groups.Group1, EventTypes.KEY_AUTOPILOT_ON, false);

        simConnect!.MapClientEventToSimEvent(EventTypes.KEY_AUTOPILOT_OFF, "AUTOPILOT_OFF");
        simConnect!.AddClientEventToNotificationGroup(Groups.Group1, EventTypes.KEY_AUTOPILOT_OFF, false);

        simConnect.SetNotificationGroupPriority(Groups.Group1, SimConnect.SIMCONNECT_GROUP_PRIORITY_HIGHEST);
    }

    public void PressButton(EventTypes eventType)
    {
        simConnect!.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, eventType, 0, Groups.Group1, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
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