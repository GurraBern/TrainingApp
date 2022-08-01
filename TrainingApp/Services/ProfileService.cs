using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingApp.Model;

namespace TrainingApp.Services;

public class ProfileService
{

    static SQLiteAsyncConnection db;

    static async Task Init()
    {
        if (db != null)
        {
            return;
        }

        var databasePath = Path.Combine(FileSystem.AppDataDirectory, "Profile.db");
        db = new SQLiteAsyncConnection(databasePath);
        await db.CreateTableAsync<Profile>();

        //await UpdateLatestActivity(DateTime.Now, ActivityState.RESTDAY);
    }


    //TODO add logic for streak
    public static async Task UpdateLatestActivity(DateTime date, ActivityState activityState)
{
        await Init();                                                                                                     
        var dateShort = date.ToShortDateString();
        var profile = await db.Table<Profile>().FirstOrDefaultAsync();

        if (profile != null)
        {
            //if green or orange keep streak update db




            profile.ActivityState = activityState;
            profile.LastDate = date.ToShortDateString();
            profile.LastTime = date.ToShortTimeString();
            await db.UpdateAsync(profile);
        }
        else
        {
            Profile newProfile = new Profile
            {
                Name = "Gustav",
                LastName = "Berndtzen",
                LastDate = date.ToShortDateString(),
                LastTime = date.ToShortTimeString(),
                ActivityState = activityState,
            };

            await db.InsertAsync(newProfile);
                //TODO throw exeption, cant update unless date exist in db
        }
    }

    //public static async Task AddDate(DateTime date, ActivityState activityState)
    //{
    //    await Init();

    //    var dateString = date.ToShortDateString();
    //    var activityIndicator = new Activity()
    //    {
    //        Date = dateString,
    //        ActivityState = activityState
    //    };

    //    var id = await db.InsertAsync(activityIndicator);
    //}
}