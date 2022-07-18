namespace TrainingApp;

public partial class MainPage : ContentPage
{
	//list width all dateIndicators

	private List<ActivityIndicator> activityIndicators = new List<ActivityIndicator>();
    private int daysOffset = 0;

	public MainPage()
	{
		InitializeComponent();

        FillActivityGrid();
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

    Dictionary<string, int> daysDictionary = new Dictionary<string, int>()
    {

    };

    public void FillActivityGrid()
	{
        int year = System.DateTime.Today.Year;
        int month = System.DateTime.Today.Month;
        int daysInMonth = System.DateTime.DaysInMonth(year, month);


      

        foreach (DaysOfWeek day in Enum.GetValues(typeof(DaysOfWeek))){
            Label dayLabel = new Label();
            dayLabel.Text = day.ToString();
            dayLabel.TextColor = new Color(0,0,0);
            dayLabel.FontSize = 8;

            Thickness margin = dayLabel.Margin;
            margin.Right = 5;
            dayLabel.Margin = margin;

            daysLabels.Add(dayLabel);
        }

        checkIfMonday();


        for (int i = 0; i < daysInMonth; i++)
		{
            ActivityIndicator activityIndicator = new ActivityIndicator();
            activityIndicator.SetActivityStatus(ActivityState.ABSENT);

            // New month begins -> Find 1st monday then take the 



            DateTime date = new DateTime(year, month, (i+1));
            activityIndicator.SetDate(date);


            this.activityIndicators.Add(activityIndicator);
            flexLayout.Add(activityIndicator.GetBoxIndicator());
        }
    }

    private void checkIfMonday()
    {
        int year = DateTime.Today.Year;
        int month = DateTime.Today.Month;

        //First day of week (Friday)
        DateTime firstDayOfMonth = new DateTime(year, month, 1);


        //unavailable


        this.daysOffset = (int)firstDayOfMonth.DayOfWeek-1;

        int steps = daysOffset;

        DateTime previousLastDateTime = new DateTime(year, month - 1, DateTime.DaysInMonth(year, month - 1));

        //Set first days to unavailable
        for (int i = steps; i > 1; i--)
        {
            ActivityIndicator activityIndicator = new ActivityIndicator();
            activityIndicator.SetActivityStatus(ActivityState.PRESENT);


            //Will it work in january first month, change to get previous month

            //Past month end date
            DateTime date = new DateTime(year, month, previousLastDateTime.Day-i);
            activityIndicator.SetDate(date);

            this.activityIndicators.Add(activityIndicator);
            flexLayout.Add(activityIndicator.GetBoxIndicator());

        }
    }

    private void Present_Clicked(object sender, EventArgs e)
    {
        activityIndicators[DateTime.Now.Day + this.daysOffset].SetActivityStatus(ActivityState.PRESENT);
	}

    private void RestDay_Clicked(object sender, EventArgs e)
    {
        activityIndicators[DateTime.Now.Day - 1].SetActivityStatus(ActivityState.RESTDAY);
    }

    private void Present_Absent(object sender, EventArgs e)
    {
        activityIndicators[DateTime.Now.Day - 1].SetActivityStatus(ActivityState.ABSENT);
    }
}

