﻿using Microsoft.Maui.ApplicationModel;
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

        var profile = await db.Table<Profile>().FirstOrDefaultAsync();
        if (profile == null)
        {
            Profile newProfile = new Profile
            {
                Name = "Gustav",
                LastName = "Berndtzen",
                LastDate = DateTime.Now.ToShortDateString(),
                LastTime = DateTime.Now.ToShortTimeString(),
                ActivityState = ActivityState.ABSENT,
                StreakDays = 0,
            };
            await db.InsertAsync(newProfile);
        }
    }

    public static async Task UpdateLatestActivity(DateTime date, ActivityState activityState)
{
        await Init();                                                                                                     
        var profile = await db.Table<Profile>().FirstOrDefaultAsync();

        if(profile.ActivityState == ActivityState.PRESENT || profile.ActivityState == ActivityState.RESTDAY)
        {
            if (!profile.LastDate.Equals(DateTime.Now.ToShortDateString()))
            {
                profile.StreakDays++;
            }
        }
        else
        {
            profile.StreakDays = 0;
        }

        profile.ActivityState = activityState;
        profile.LastDate = date.ToShortDateString();
        profile.LastTime = date.ToShortTimeString();
        await db.UpdateAsync(profile);
    }

    public static int GetCurrentActivityStreak()
    {
        return db.Table<Profile>().FirstAsync().Result.StreakDays;
    }
}