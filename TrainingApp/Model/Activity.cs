namespace TrainingApp.Model;

using System;
using SQLite;

public class Activity
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Date { get; set; }
    public string Time { get; set; }
    public ActivityState ActivityState { get; set; }
}
