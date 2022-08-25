using TrainingApp.View;

namespace TrainingApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(AddExercisePage), typeof(AddExercisePage));
	}
}
