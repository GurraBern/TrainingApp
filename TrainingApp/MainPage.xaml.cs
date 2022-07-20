using TrainingApp.Services;

namespace TrainingApp;

public partial class MainPage : ContentPage
{
    
    private List<ActivityIndicator> activityIndicators;
    private ActivityIndicatorModel activityIndicatorModel;

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
        //await FillMonthAsync();
        FillActivityGridAsync();
    }

    private async Task FillMonthAsync()
    {
        await DateIndicatorService.AddDatesMonth(DateTime.Today);
    }

    private async Task<IEnumerable<ActivityIndicatorModel>> GetActivityDates()
    {
        //var activityDatesEnum = await Task.Run(() => DateIndicatorService.GetDates());
        var activityDatesEnum = await Task.Run(() => DateIndicatorService.GetDates());
        return activityDatesEnum;
    }

    private async Task AddDate(DateTime dateTime)
    {
        await DateIndicatorService.AddDate(dateTime, ActivityState.PRESENT);
    }

    private void FillInDayLabels()
    {
        foreach (DaysOfWeek day in Enum.GetValues(typeof(DaysOfWeek)))
        {
            Label dayLabel = new Label();
            dayLabel.Text = day.ToString();
            dayLabel.TextColor = new Color(0, 0, 0);
            dayLabel.FontSize = 8;

            Thickness margin = dayLabel.Margin;
            margin.Right = 5;
            dayLabel.Margin = margin;

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

        RefreshActivityGridAsync();
    }

    private async Task SetIndicatorStatusAsync(ActivityState state)
    {
        await DateIndicatorService.UpdateDate(DateTime.Today, state);
        // TODO refresh

        RefreshActivityGridAsync();
    }

    private async Task RefreshActivityGridAsync()
    {

        flexLayout.Clear();
        var dates = await GetActivityDates();
        List<ActivityIndicatorModel> activityDates = dates.ToList();
        foreach (ActivityIndicatorModel activityDate in activityDates)
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
