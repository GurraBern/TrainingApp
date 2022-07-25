﻿using TrainingApp.Services;

namespace TrainingApp;

public partial class MainPage : ContentPage
{
    private List<ActivityIndicator> activityIndicators;
    private Activity activityIndicatorModel;

    private int _daysOffset = 0;
    private DateIndicatorService db;



    public MainPage()
	{
        StartUpAsync();
    }

    private async Task StartUpAsync()
    {
        InitializeComponent();
        db = new DateIndicatorService();
        FillActivityGridAsync();
    }

    private async Task FillMonthAsync()
    {
        await DateIndicatorService.AddDatesMonth(DateTime.Today);
    }

    private async Task<IEnumerable<Activity>> GetActivityDates()
    {
        var activityDatesEnum = await Task.Run(() => DateIndicatorService.GetDates());
        return activityDatesEnum;
    }

    private async Task<IEnumerable<Activity>> GetIntervalDates(string startDate, string endDate)
    {
        var dates = await Task.Run(() => DateIndicatorService.QueryValuationsAsync(startDate, endDate));
        return dates;
    }

    private async Task AddDate(DateTime dateTime)
    {
        await DateIndicatorService.AddDate(dateTime, ActivityState.PRESENT);
    }

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
           

            //Thickness margin = dayLabel.Margin;
            //margin.Right = 5;
            //dayLabel.Margin = margin;

            daysLabels.Add(dayLabel);

        }
    }

    public async void FillActivityGridAsync()
	{
        //int year = System.DateTime.Today.Year;
        //int month = System.DateTime.Today.Month;
        //int daysInMonth = System.DateTime.DaysInMonth(year, month);

        //TODO should be a XAML Component?
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

        //Get last month final days if not monday
        var dates = await GetIntervalDates("2022-07-20","2022-07-31");

        var datesee = await GetActivityDates()



        List<Activity> activityDates = dates.ToList();
        foreach (Activity activityDate in activityDates)
        {
            ActivityIndicator dateIndicatorBox = new ActivityIndicator(activityDate);
            dateIndicatorBox.SetActivityStatus(activityDate.ActivityState);
            flexLayout.Add(dateIndicatorBox.GetBoxIndicator());

        }
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
