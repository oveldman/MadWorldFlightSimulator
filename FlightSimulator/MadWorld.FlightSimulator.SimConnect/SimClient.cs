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
            AddComRadio();

            simConnect.SetNotificationGroupPriority(Groups.Default, SimConnect.SIMCONNECT_GROUP_PRIORITY_HIGHEST);

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

    public void Disconnect()
    {
        simConnect?.Dispose();
        simConnect = null;
        IsConnected = false;
    }

    public async Task StartMessageService()
    {
        while (IsConnected)
        {
            simConnect!.ReceiveMessage();
            Thread.Sleep(200);
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

        simConnect.AddToDataDefinition(
            RequestTypes.AirplaneInformation,
            "AUTOPILOT ALTITUDE LOCK VAR",
            "feet",
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
        simConnect!.AddClientEventToNotificationGroup(Groups.Default, EventTypes.KEY_AUTOPILOT_ON, false);

        simConnect!.MapClientEventToSimEvent(EventTypes.KEY_AUTOPILOT_OFF, "AUTOPILOT_OFF");
        simConnect!.AddClientEventToNotificationGroup(Groups.Default, EventTypes.KEY_AUTOPILOT_OFF, false);

        simConnect!.MapClientEventToSimEvent(EventTypes.KEY_AUTOPILOT_INCREASE_ALTITUDE, "AP_ALT_VAR_INC");
        simConnect!.AddClientEventToNotificationGroup(Groups.Default, EventTypes.KEY_AUTOPILOT_INCREASE_ALTITUDE, false);

        simConnect!.MapClientEventToSimEvent(EventTypes.KEY_AUTOPILOT_DECREASE_ALTITUDE, "AP_ALT_VAR_DEC");
        simConnect!.AddClientEventToNotificationGroup(Groups.Default, EventTypes.KEY_AUTOPILOT_DECREASE_ALTITUDE, false);
    }

    public void AddComRadio()
    {
        simConnect!.MapClientEventToSimEvent(EventTypes.KEY_ATC_MENU_SELECT_0, "ATC_MENU_0");
        simConnect!.AddClientEventToNotificationGroup(Groups.Default, EventTypes.KEY_ATC_MENU_SELECT_0, false);

        simConnect!.MapClientEventToSimEvent(EventTypes.KEY_ATC_MENU_SELECT_1, "ATC_MENU_1");
        simConnect!.AddClientEventToNotificationGroup(Groups.Default, EventTypes.KEY_ATC_MENU_SELECT_1, false);

        simConnect!.MapClientEventToSimEvent(EventTypes.KEY_ATC_MENU_SELECT_2, "ATC_MENU_2");
        simConnect!.AddClientEventToNotificationGroup(Groups.Default, EventTypes.KEY_ATC_MENU_SELECT_2, false);

        simConnect!.MapClientEventToSimEvent(EventTypes.KEY_ATC_MENU_SELECT_3, "ATC_MENU_3");
        simConnect!.AddClientEventToNotificationGroup(Groups.Default, EventTypes.KEY_ATC_MENU_SELECT_3, false);

        simConnect!.MapClientEventToSimEvent(EventTypes.KEY_ATC_MENU_SELECT_4, "ATC_MENU_4");
        simConnect!.AddClientEventToNotificationGroup(Groups.Default, EventTypes.KEY_ATC_MENU_SELECT_4, false);

        simConnect!.MapClientEventToSimEvent(EventTypes.KEY_ATC_MENU_SELECT_5, "ATC_MENU_5");
        simConnect!.AddClientEventToNotificationGroup(Groups.Default, EventTypes.KEY_ATC_MENU_SELECT_5, false);

        simConnect!.MapClientEventToSimEvent(EventTypes.KEY_ATC_MENU_SELECT_6, "ATC_MENU_6");
        simConnect!.AddClientEventToNotificationGroup(Groups.Default, EventTypes.KEY_ATC_MENU_SELECT_6, false);

        simConnect!.MapClientEventToSimEvent(EventTypes.KEY_ATC_MENU_SELECT_7, "ATC_MENU_7");
        simConnect!.AddClientEventToNotificationGroup(Groups.Default, EventTypes.KEY_ATC_MENU_SELECT_7, false);

        simConnect!.MapClientEventToSimEvent(EventTypes.KEY_ATC_MENU_SELECT_8, "ATC_MENU_8");
        simConnect!.AddClientEventToNotificationGroup(Groups.Default, EventTypes.KEY_ATC_MENU_SELECT_8, false);

        simConnect!.MapClientEventToSimEvent(EventTypes.KEY_ATC_MENU_SELECT_9, "ATC_MENU_9");
        simConnect!.AddClientEventToNotificationGroup(Groups.Default, EventTypes.KEY_ATC_MENU_SELECT_9, false);
    }

    public void PressButton(EventTypes eventType, uint data = 0)
    {
        simConnect!.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, eventType, data, Groups.Default, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
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