namespace TrainingApp;

public partial class MainPage : ContentPage
{
	//list width all dateIndicators

	List<ActivityIndicator> activityIndicators = new List<ActivityIndicator>();

	public MainPage()
	{
		InitializeComponent();

		test();
    }

	public void test()
	{
		//Better to have activtyIndicators that can add them selves to flexlayout
		for (int i = 0; i < 28; i++)
		{
            ActivityIndicator a = new ActivityIndicator();
			activityIndicators.Add(a);
            flexLayout.Add(a.getBoxIndicator());
        }
    }

	private void Present_Clicked(object sender, EventArgs e)
	{
		activityIndicators[0].setActivityStatus(ActivityState.PRESENT);
	}

    private void RestDay_Clicked(object sender, EventArgs e)
    {
        activityIndicators[0].setActivityStatus(ActivityState.RESTDAY);
    }

    private void Present_Absent(object sender, EventArgs e)
    {
        activityIndicators[0].setActivityStatus(ActivityState.ABSENT);
    }
}

