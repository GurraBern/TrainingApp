﻿using TrainingApp.Model;
using TrainingApp.Services;

namespace TrainingApp;

public partial class MainPage : ContentPage
{
    //private DateIndicatorService db;



    public MainPage()
	{
        StartUpAsync();
    }

    private void StartUpAsync()
    {
        InitializeComponent();
        //db = new DateIndicatorService();

        FillActivityGridAsync();
    }
    
    private async Task<IEnumerable<Activity>> GetMonthActivityDates()
    {
        var daysInMonth = DateTime.DaysInMonth(DateTime.Today.Year,DateTime.Today.Month);
        var lastDay = new DateTime(DateTime.Today.Year, DateTime.Today.Month, daysInMonth);
        var firstDay = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        var activityDatesEnum = await GetIntervalDates(firstDay.ToShortDateString(), lastDay.ToShortDateString());

        return activityDatesEnum;
    }

    private async Task<IEnumerable<Activity>> GetActivityDates()
    {
        var activityDatesEnum = await Task.Run(() => DateIndicatorService.GetActivityDates());

        return activityDatesEnum;
    }

    private async Task<IEnumerable<Activity>> GetIntervalDates(string startDate, string endDate)
    {
        var dates = await Task.Run(() => DateIndicatorService.GetActivityBetween(startDate, endDate));

        return dates;
    }


    //TODO Change to xaml
    private void FillInDayLabels()
    {
        foreach (DaysOfWeek day in Enum.GetValues(typeof(DaysOfWeek)))
        {
            Button dayLabel = new Button();
            dayLabel.Text = day.ToString();
            dayLabel.FontAttributes = FontAttributes.Bold;
            dayLabel.FontSize = 12;
            dayLabel.Padding = 0;
            dayLabel.WidthRequest = 30;
            dayLabel.CornerRadius = 5;
            dayLabel.TextColor = Color.FromRgb(0, 0, 0);
            daysLabels.Add(dayLabel);
        }
    }

    private async void FillActivityGridAsync()
	{
        FillInDayLabels();
        await RefreshActivityGrid();
    }

    private async Task SetIndicatorStatusAsync(ActivityState state)
    {
        await DateIndicatorService.UpdateDate(DateTime.Today, state);
        await RefreshActivityGrid();
    }

    private async Task RefreshActivityGrid()
      {
        flexLayout.Clear();
        List<Activity> activityDates = new List<Activity>();

        var previousActivity = await getPreviousMonth() as List<Activity>;
        if(previousActivity != null)
            activityDates.AddRange(previousActivity);

        var monthActivity = await GetMonthActivityDates() as List<Activity>;
        activityDates.AddRange(monthActivity);

        foreach (Activity activityDate in activityDates)
        {
            ActivityIndicator dateIndicatorBox = new ActivityIndicator(activityDate);
            dateIndicatorBox.SetActivityStatus(activityDate.ActivityState);
            flexLayout.Add(dateIndicatorBox.GetBoxIndicator());
        }
    }

    private async Task<IEnumerable<Activity>> getPreviousMonth()
    {
        var date = DateTime.Now;
        if(date.Month == 1)
        {
            date = new DateTime(date.Year - 1, 12, 1);
        }

        //TODO Jan Proof?  double test
        var endDatePreviousMonth = new DateTime(date.Year, date.Month - 1, DateTime.DaysInMonth(date.Year, date.Month - 1));
        var firstDayOfMonth = new DateTime(date.Year, date.Month, 1).DayOfWeek;


        //var offsetDays = (int) firstDayOfMonth - 2;
        var offsetDays = (int) firstDayOfMonth-2;
        if(offsetDays <= 0)
        {
            //isMonday
            return Enumerable.Empty<Activity>();
        }

        DateTime offsetDate = new DateTime(endDatePreviousMonth.Year, endDatePreviousMonth.Month, endDatePreviousMonth.Day - offsetDays);
        var date1 = offsetDate.ToShortDateString();
        var date2 = endDatePreviousMonth.ToShortDateString();

        return await GetIntervalDates(date1,date2);
    }

    private async void Present_Clicked(object sender, EventArgs e)
    {
        await SetIndicatorStatusAsync(ActivityState.PRESENT);
    }

    private async void RestDay_Clicked(object sender, EventArgs e)
    {
        await SetIndicatorStatusAsync(ActivityState.RESTDAY);
    }

    private async void Present_Absent(object sender, EventArgs e)
    {
        await SetIndicatorStatusAsync(ActivityState.ABSENT);
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
