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
        //TODO find specific date
        //update activityState        

        var activityIndicatorObj = await db.Table<ActivityIndicatorModel>().Where(v => v.Date.Equals(date)).FirstOrDefaultAsync();
        activityIndicatorObj.ActivityState = activityState;
        await db.UpdateAsync(activityIndicatorObj);



        /*
        var test3 = await db.Table<ActivityIndicatorModel>().Where(v => v.Date.Equals(date)).FirstOrDefaultAsync();
        var bruh = test3.ActivityState;*/
        //TODO update obj


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