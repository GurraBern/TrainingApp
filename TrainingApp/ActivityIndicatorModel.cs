using System;
using SQLite;

public class ActivityIndicatorModel
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public ActivityState ActivityState { get; set; }
}
