using Microsoft.Maui.Controls;
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

        //await db.DeleteAllAsync<ActivityIndicatorModel>();
        //await AddDatesMonth(new DateTime(2022, 7, 25));
    }

    public static async Task AddDatesMonth(DateTime dateTime)
    {
        await Init();

        int daysCount = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);

        //TODO better to have one call to database
        for(int i = 1; i < daysCount+1; i++)
        {
            var date = new DateTime(dateTime.Year, dateTime.Month, i);
            await AddDate(date, ActivityState.ABSENT);
        }

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

    public static async Task UpdateDate(DateTime date, ActivityState activityState)
    {

        var activityIndicatorObj = await db.Table<ActivityIndicatorModel>().Where(v => v.Date.Equals(date)).FirstOrDefaultAsync();
        activityIndicatorObj.ActivityState = activityState;
        await db.UpdateAsync(activityIndicatorObj);
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