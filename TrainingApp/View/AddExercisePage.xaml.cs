namespace TrainingApp.View;

public partial class AddExercisePage : ContentPage
{
    public AddExercisePage(AddExerciseViewModel viewModel)
	{
        InitializeComponent();
        BindingContext = viewModel;
    }
}
