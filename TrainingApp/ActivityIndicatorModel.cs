using System;
using SQLite;

public class ActivityIndicatorModel
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Date { get; set; }
    public ActivityState ActivityState { get; set; }
}
