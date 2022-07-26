using TrainingApp.Services;

namespace TrainingApp;

public partial class MainPage : ContentPage
{
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

    //OLD
    //private async Task FillMonthAsync()
    //{
    //    await DateIndicatorService.AddDatesMonth(DateTime.Today);
    //}

    private async Task FillMonthAsync()
    {
        await DateIndicatorService.AddDatesToMonth(DateTime.Today);
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
        var dates = await Task.Run(() => DateIndicatorService.GetActivityDatesBetween(startDate, endDate));

        return dates;
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
        var monthActivity = await GetMonthActivityDates() as List<Activity>;

        activityDates.AddRange(previousActivity);
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
        //TODO Jan Proof?
        var endDatePreviousMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month - 1, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month - 1));
        var firstDayOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).DayOfWeek;
        var offsetDays = (int) firstDayOfMonth - 2;
        DateTime offsetDate = new DateTime(endDatePreviousMonth.Year, endDatePreviousMonth.Month, endDatePreviousMonth.Day - offsetDays);

        return await GetIntervalDates(offsetDate.ToShortDateString(), endDatePreviousMonth.ToShortDateString());
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
