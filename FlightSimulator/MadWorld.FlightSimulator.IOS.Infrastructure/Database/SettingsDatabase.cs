using SQLite;

namespace MadWorld.FlightSimulator.IOS.Infrastructure.Database
{
    public class SettingsDatabase
    {
        SQLiteAsyncConnection Database;

        async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(DbConstants.DatabasePath, DbConstants.Flags);
            await Database.CreateTableAsync<Settings>();
        }

        public async Task<Settings> GetSettingsAsync()
        {
            await Init();
            return await Database.Table<Settings>()
                .OrderByDescending(f => f.ID)
                .FirstOrDefaultAsync();
        }

        public async Task<int> SaveAsync(Settings settings)
        {
            await Init();
            if (settings.ID != 0)
            {
                return await Database.UpdateAsync(settings);
            }
            else
            {
                settings.ID = 1;
                return await Database.InsertAsync(settings);
            }
        }
    }
}
