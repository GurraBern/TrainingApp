using TrainingApp.Services;

namespace TrainingApp.ViewModel;

public partial class MainPageViewModel : BaseViewModel
{

    public ObservableCollection<ExerciseActivity> ActivityIndicators { get; } = new();

    DateIndicatorService dateService;

    public MainPageViewModel(DateIndicatorService dateService)
    {
        Title = "Test App";
        this.dateService = dateService;


        new Action(async () => await RefreshActivityGrid()).Invoke();

    }

    private async Task RefreshStreakLabelAsync()
    {
        var streakDays = await ProfileService.GetCurrentActivityStreakAsync();
        //StreakLabel.Text = streakDays.ToString() + " 🔥 Day Streak";
    }

    private async Task<IEnumerable<ExerciseActivity>> GetMonthActivityDates()
    {
        var daysInMonth = DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month);
        var lastDay = new DateTime(DateTime.Today.Year, DateTime.Today.Month, daysInMonth);
        var firstDay = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

        return await GetIntervalDates(firstDay.ToShortDateString(), lastDay.ToShortDateString()); ;
    }

    private async Task<IEnumerable<ExerciseActivity>> GetActivityDates()
    {
        return await Task.Run(() => dateService.GetActivityDates()); ;
    }

    private async Task<IEnumerable<ExerciseActivity>> GetIntervalDates(string startDate, string endDate)
    {
        return await Task.Run(() => dateService.GetActivityBetween(startDate, endDate)); ;
    }

    private async void SetupStart()
    {
        await RefreshStreakLabelAsync();
        await RefreshActivityGrid();
    }

    private async Task SetIndicatorStatusAsync(ActivityState state)
    {
        await dateService.UpdateDate(DateTime.Now, state);
        await ProfileService.UpdateLatestActivity(DateTime.Now, state);
        await RefreshStreakLabelAsync();
        await RefreshActivityGrid();
    }

    async Task RefreshActivityGrid()
    {
        List<ExerciseActivity> activityDates = new List<ExerciseActivity>();
        ActivityIndicators.Clear();

        var previousActivity = await GetPreviousMonth() as List<ExerciseActivity>;
        if (previousActivity != null)
            activityDates.AddRange(previousActivity);

        var monthActivity = await GetMonthActivityDates() as List<ExerciseActivity>;
        activityDates.AddRange(monthActivity);


        foreach (ExerciseActivity activityDate in activityDates)
        {
            activityDate.Date = activityDate.Date.Substring(activityDate.Date.Length - 2);
            ActivityIndicators.Add(activityDate);
        }
    }

    async Task<IEnumerable<ExerciseActivity>> GetPreviousMonth()
    {
        var date = DateTime.Now;
        if (date.Month == 1)
        {
            date = new DateTime(date.Year - 1, 12, 1);
        }

        var endDatePreviousMonth = new DateTime(date.Year, date.Month - 1, DateTime.DaysInMonth(date.Year, date.Month - 1));
        var firstDayOfMonth = new DateTime(date.Year, date.Month, 1).DayOfWeek;

        var offsetDays = (int)firstDayOfMonth - 2;
        if (offsetDays <= 0)
        {
            return Enumerable.Empty<ExerciseActivity>();
        }

        DateTime offsetDate = new DateTime(endDatePreviousMonth.Year, endDatePreviousMonth.Month, endDatePreviousMonth.Day - offsetDays);
        var date1 = offsetDate.ToShortDateString();
        var date2 = endDatePreviousMonth.ToShortDateString();

        return await GetIntervalDates(date1, date2);
    }

    [RelayCommand]
    async Task Present()
    {
        await SetIndicatorStatusAsync(ActivityState.PRESENT);
    }

    [RelayCommand]
    async void RestDay()
    {
        await SetIndicatorStatusAsync(ActivityState.RESTDAY);
    }

    [RelayCommand]
    async void Absent()
    {
        await SetIndicatorStatusAsync(ActivityState.ABSENT);
    }

    private async void ClockedIn(object sender, EventArgs e)
    {
        var profile = await ProfileService.GetProfile();
        DateTime checkInTime = Convert.ToDateTime(profile.LastTime);
        string duration = DateTime.Now.Subtract(checkInTime).ToString(@"hh\:mm\:ss");
        //LastWorkoutDuration.Text = "Last Workout Duration: " + duration;
    }

    //TODO change system
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
