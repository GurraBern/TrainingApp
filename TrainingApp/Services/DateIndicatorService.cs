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

 

        //await db.DeleteAllAsync<ActivityIndicatorModel>();

        //TODO end of year bug
        //DateTime previousMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        //await AddDatesMonth(previousMonth);
        //await AddDatesMonth(previousMonth.AddMonths(1));

        //Remove
        //await AddDatesMonth(new DateTime(2022, 7, 25));
    }


    //TODO works?
    public Task<List<Activity>> GetItemsFromDateAsync(DateTime Start, DateTime end)
    {
        return db.QueryAsync<Activity>("SELECT * FROM [Activity] WHERE [Start] >= ? or [End]<= ?", Start, end);
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

        var dateString = date.ToShortDateString();

        var activityIndicator = new Activity()
        {
            Date = dateString,
            ActivityState = activityState
        };

        var id = await db.InsertAsync(activityIndicator);
    }

    public static async Task UpdateDate(DateTime date, ActivityState activityState)
    {
        var dateShort = date.ToShortDateString();

        var activityIndicatorObj = await db.Table<Activity>().Where(v => v.Date.Equals(dateShort)).FirstOrDefaultAsync();
        activityIndicatorObj.ActivityState = activityState;
        await db.UpdateAsync(activityIndicatorObj);
    }

    public static async Task RemoveDate(int id)
    {
        await Init();

        await db.DeleteAsync<Activity>(id);
    }

    public static async Task<IEnumerable<Activity>> GetDates()
    {
        await Init();
        var dates = await db.Table<Activity>().ToListAsync();

        return dates;
    }

    public static async Task<IEnumerable<Activity>> GetDatesAndFill()
    {
        var dates = GetDates();
        var firstDate = dates.Result.FirstOrDefault();


        //Check specifik date from string
        //int offset = (int)firstDate.Date.DayOfWeek - 1;

        //var daysInMonth = DateTime.DaysInMonth(firstDate.Date.Year, firstDate.Date.Month);

        //var  tet = QueryValuations(db.GetConnection(), firstDate.Date).ToList();


        //Get from previous month last days, interval between the last day and the offset
        /*var previousDates = await db.Table<ActivityIndicatorModel>()
            .Where(v => v.Date.).FirstOrDefaultAsync();
        */

        //Join together


        var test = 1;

        return (IEnumerable<Activity>)dates;
    }

    public static IEnumerable<Activity> QueryValuations(SQLiteConnection db, string stock)
    {
        return db.Query<Activity>("SELECT* FROM test WHERE joined_date BETWEEN '2022-07-01' AND '2022-07/10'");
    }

    private string SplitToDay(string date)
    {
        var splitString = date.Split("-");

        string dayString = splitString[splitString.Count()-1];

        return dayString;
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