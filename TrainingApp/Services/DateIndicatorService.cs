using SQLite;

namespace TrainingApp.Services;

public class DateIndicatorService
{
    static SQLiteAsyncConnection db;

    static async Task Init()
    {
        if (db != null)
        {
            return;
        }

        var databasePath = Path.Combine(FileSystem.AppDataDirectory, "DateDataBase.db");
        db = new SQLiteAsyncConnection(databasePath);

        await db.CreateTableAsync<ActivityIndicatorModel>();
    }

    public static async Task AddDate(DateTime date, ActivityState activityState)
    {
        await Init();

        var activityIndicator = new ActivityIndicatorModel()
        {
            Date = date,
            ActivityState = activityState
        };

        var id = await db.InsertAsync(activityIndicator);
    }

    public static async Task RemoveDate(int id)
    {
        await Init();

        await db.DeleteAsync<ActivityIndicatorModel>(id);
    }

    public static async Task<IEnumerable<ActivityIndicatorModel>> GetDates()
    {
        await Init();

        var dates = await db.Table<ActivityIndicatorModel>().ToListAsync();


        return dates;
    }
}