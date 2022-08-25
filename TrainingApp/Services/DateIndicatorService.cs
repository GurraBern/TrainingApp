using SQLite;
using System.Linq;

namespace TrainingApp.Services;

public class DateIndicatorService
{
    static SQLiteAsyncConnection db;

    async Task Init()
    {

        if (db != null)
        {
            return;
        }

        var databasePath = Path.Combine(FileSystem.AppDataDirectory, "ActivityDates.db");
        db = new SQLiteAsyncConnection(databasePath);
        await db.CreateTableAsync<ExerciseActivity>();

        var today = DateTime.Today;
        var endOfMonth = new DateTime(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month));
        var dates = await GetActivityBetween(today.ToShortDateString(), endOfMonth.ToShortDateString());

        if(dates.Count <= 0)
        {
            await AddDatesToMonth(DateTime.Today);
        }
    }

    public async Task AddDate(DateTime date, ActivityState activityState)
    {
        await Init();
        
        var dateString = date.ToShortDateString();
        var activityIndicator = new ExerciseActivity()
        {
            Date = dateString,
            ActivityState = activityState
        };

        var id = await db.InsertAsync(activityIndicator);
    }

    public async Task AddDatesToMonth(DateTime date)
    {
        await Init();

        int daysCount = DateTime.DaysInMonth(date.Year, date.Month);
        List<ExerciseActivity> activityDates = new();

        for (int i = 1; i < daysCount + 1; i++)
        {
            var incDay = new DateTime(date.Year, date.Month, i);
            var activityIndicator = new ExerciseActivity()
            {
                Date = incDay.ToShortDateString(),
                Time = incDay.ToShortTimeString(),
                ActivityState = ActivityState.ABSENT
            };           

            activityDates.Add(activityIndicator);
        }

        var id = await db.InsertAllAsync(activityDates);
    }

    public static async Task UpdateDate(DateTime date, ActivityState activityState)
    {
        var dateShort = date.ToShortDateString();
        var activityIndicatorObj = await db.Table<ExerciseActivity>().Where(v => v.Date.Equals(dateShort)).FirstOrDefaultAsync();

        if(activityIndicatorObj != null)
        {
            activityIndicatorObj.ActivityState = activityState;
            activityIndicatorObj.Time = date.ToShortTimeString();
            await db.UpdateAsync(activityIndicatorObj);
        } else
        {
            //TODO throw exeption, cant update unless date exist in db
        }
    }

    public async Task RemoveDate(int id)
    {
        await Init();

        await db.DeleteAsync<ExerciseActivity>(id);
    }

    public async Task<List<ExerciseActivity>> GetActivityDates()
    {
        await Init();
        var dates = await db.Table<ExerciseActivity>().ToListAsync();

        return dates;
    }

    public async Task<List<ExerciseActivity>> GetActivityBetween(string startDate, string endDate)
    {
        await Init();
        var dbConnection = db.GetConnection();

        return dbConnection.Query<ExerciseActivity>($"SELECT * FROM Activity WHERE date BETWEEN '{startDate}' AND '{endDate}'", startDate, endDate);
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