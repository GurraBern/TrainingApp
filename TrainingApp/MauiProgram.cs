using TrainingApp.Services;
using TrainingApp.View;
using TrainingApp.ViewModel;

namespace TrainingApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<DateIndicatorService>();
		builder.Services.AddSingleton<ExerciseService>();
		builder.Services.AddSingleton<ProfileService>();

		builder.Services.AddSingleton<MainPageViewModel>();
        builder.Services.AddSingleton<MainPage>();

        builder.Services.AddSingleton<ExercisesViewModel>();
		builder.Services.AddSingleton<ExercisesView>();

        builder.Services.AddSingleton<AddExerciseViewModel>();
        builder.Services.AddSingleton<AddExercisePage>();

        return builder.Build();
	}
}
