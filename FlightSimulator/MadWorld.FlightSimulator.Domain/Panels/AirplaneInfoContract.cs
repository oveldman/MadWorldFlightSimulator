namespace MadWorld.FlightSimulator.Domain.Panels
{
    public class AirplaneInfoContract
    {
        public string Title { get; set; } = string.Empty;
        public double Altitude { get; set; }
        public bool IsPlaneOnGround { get; set; }
        public bool IsAutoPilotOn { get; set; }
    }
}
