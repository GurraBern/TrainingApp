using Microsoft.Maui.Platform;


namespace TrainingApp;

public partial class ExercisesView : ContentPage
{
    public ExercisesView(ExercisesView viewModel)
	{
        InitializeComponent();
        BindingContext = viewModel;
    }
}
