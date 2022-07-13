namespace TrainingApp;

public partial class MainPage : ContentPage
{
	//list width all dateIndicators

	private List<ActivityIndicator> activityIndicators = new List<ActivityIndicator>();

	public MainPage()
	{
		InitializeComponent();

        fillActivityGrid();
    }
    public enum DayOfWeek
    {
        Mon = 0,
        Tue = 1,
        Wed = 2,
        Thu = 3,
        Fri = 4,
        Sat = 5,
        Sun = 6

    };

    public void fillActivityGrid()
	{
        int year = System.DateTime.Today.Year;
        int month = System.DateTime.Today.Month;
        int daysInMonth = System.DateTime.DaysInMonth(year, month);


      

        foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek))){
            Label dayLabel = new Label();
            dayLabel.Text = day.ToString();
            dayLabel.TextColor = new Color(0,0,0);
            dayLabel.FontSize = 8;

            Thickness margin = dayLabel.Margin;
            margin.Right = 5;
            dayLabel.Margin = margin;

            daysLabels.Add(dayLabel);
        }

        for (int i = 0; i < daysInMonth; i++)
		{
            ActivityIndicator activityIndicator = new ActivityIndicator();
            activityIndicator.setActivityStatus(ActivityState.ABSENT);
            DateTime date = new DateTime(year, month, (i+1));
            activityIndicator.setDate(date);


            this.activityIndicators.Add(activityIndicator);
            flexLayout.Add(activityIndicator.getBoxIndicator());
        }
    }

	private void Present_Clicked(object sender, EventArgs e)
	{
        
        activityIndicators[DateTime.Now.Day-1].setActivityStatus(ActivityState.PRESENT);
	}

    private void RestDay_Clicked(object sender, EventArgs e)
    {
        activityIndicators[DateTime.Now.Day - 1].setActivityStatus(ActivityState.RESTDAY);
    }

    private void Present_Absent(object sender, EventArgs e)
    {
        activityIndicators[DateTime.Now.Day - 1].setActivityStatus(ActivityState.ABSENT);
    }
}

