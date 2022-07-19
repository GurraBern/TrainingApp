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
		InitializeComponent();
        db = new DateIndicatorService();
        FillActivityGridAsync();
    }


    private async Task<IEnumerable<ActivityIndicatorModel>> GetActivityDates()
    {
        var activityDates = await Task.Run(() => DateIndicatorService.GetDates());

        return activityDates;
    }


    private async Task<List<ActivityIndicator>> GetAllDatesAsync()
    {
        List<ActivityIndicator> test = new List<ActivityIndicator>(); 
        var dateIndicators = await DateIndicatorService.GetDates();
        //this.activityIndicators.AddRange(dateIndicators);

        return test;
    }

    private async Task AddDate(DateTime dateTime)
    {
        await DateIndicatorService.AddDate(dateTime, ActivityState.PRESENT);
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
        this.activityIndicators = new List<ActivityIndicator>();
        FillInDayLabels();

        var test = await GetActivityDates();
        List<ActivityIndicatorModel> days = test.ToList();
     
        foreach (ActivityIndicatorModel day in days)
        {
            ActivityIndicator dateIndicatorBox = new ActivityIndicator(day);
            dateIndicatorBox.SetActivityStatus(ActivityState.PRESENT);
            flexLayout.Add(dateIndicatorBox.GetBoxIndicator());
        }

    }

    private void SetIndicatorStatus(ActivityState state)
    {
        //TODO daysOffset depricated
        activityIndicators[DateTime.Now.Day + this._daysOffset].SetActivityStatus(state);
    }

    private void Present_Clicked(object sender, EventArgs e)
    {
        SetIndicatorStatus(ActivityState.PRESENT);
    }

    private void RestDay_Clicked(object sender, EventArgs e)
    {
        SetIndicatorStatus(ActivityState.RESTDAY);
    }

    private void Present_Absent(object sender, EventArgs e)
    {
        SetIndicatorStatus(ActivityState.ABSENT);
    }
}

