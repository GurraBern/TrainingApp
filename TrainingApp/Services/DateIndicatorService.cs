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

        var databasePath = Path.Combine(FileSystem.AppDataDirectory, "ActivityDates.db");
        db = new SQLiteAsyncConnection(databasePath);
        await db.CreateTableAsync<Activity>();

        if (!GetActivityDates().Result.Any())
        {
            await AddDatesToMonth(new DateTime(DateTime.Today.Year, DateTime.Today.Month - 1, DateTime.Today.Day));
            await AddDatesToMonth(DateTime.Today);
        }
    }

    public static async Task AddDate(DateTime date, ActivityState activityState)
    {
        await Init();

        var dateString = date.ToShortDateString();
        var activityIndicator = new Activity()
        {
            Date = dateString,
            ActivityState = activityState
        };

        var id = await db.InsertAsync(activityIndicator);
    }

    public static async Task AddDatesToMonth(DateTime date)
    {
        await Init();

        int daysCount = DateTime.DaysInMonth(date.Year, date.Month);
        List<Activity> activityDates = new List<Activity>();

        for (int i = 1; i < daysCount + 1; i++)
        {
            var incDay = new DateTime(date.Year, date.Month, i);
            var activityIndicator = new Activity()
            {
                Date = incDay.ToShortDateString(),
                ActivityState = ActivityState.ABSENT
            };

            activityDates.Add(activityIndicator);
        }

        var id = await db.InsertAllAsync(activityDates);
    }

    public static async Task UpdateDate(DateTime date, ActivityState activityState)
    {
        var dateShort = date.ToShortDateString();

        //TODO if null throw exeption
        var activityIndicatorObj = await db.Table<Activity>().Where(v => v.Date.Equals(dateShort)).FirstOrDefaultAsync();
        activityIndicatorObj.ActivityState = activityState;
        await db.UpdateAsync(activityIndicatorObj);
    }

    public static async Task RemoveDate(int id)
    {
        await Init();
        await db.DeleteAsync<Activity>(id);
    }

    public static async Task<IEnumerable<Activity>> GetActivityDates()
    {
        await Init();
        var dates = await db.Table<Activity>().ToListAsync();

        return dates;
    }

    public static async Task<IEnumerable<Activity>> GetActivityDatesBetween(string startDate, string endDate)
    {
        await Init();
        var dbConnection = db.GetConnection();

        return dbConnection.Query<Activity>($"SELECT * FROM Activity WHERE date BETWEEN '{startDate}' AND '{endDate}'", startDate, endDate);
    }

    public enum DaysOfWeek
    {
        Mon = 0,
        Tue = 1,
        Wed = 2,
        Thu = 3,
        Fri = 4,
        Sat = 5,
        Sun = 6
    };
}