using SQLite;

namespace MadWorld.FlightSimulator.IOS.Infrastructure.Database
{
    public class Settings
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string ApiUrl { get; set; }
    }
}
