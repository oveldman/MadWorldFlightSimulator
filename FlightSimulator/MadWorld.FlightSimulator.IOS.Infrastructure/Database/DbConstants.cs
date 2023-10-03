using SQLite;

namespace MadWorld.FlightSimulator.IOS.Infrastructure.Database
{
    public class DbConstants
    {
        public const string DatabaseFilename = "CommonFlightSimulator.db3";

        public const SQLiteOpenFlags Flags =
            SQLiteOpenFlags.ReadWrite |
            SQLiteOpenFlags.Create |
            SQLiteOpenFlags.SharedCache;

        public static string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
    }
}
