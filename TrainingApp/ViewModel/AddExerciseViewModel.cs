using MvvmHelpers.Commands;
using TrainingApp.Services;

namespace TrainingApp.ViewModel;

[QueryProperty(nameof(Name), nameof(Name))]
public class AddExerciseViewModel : BaseViewModel
{

    string name, description, primaryMuscle, secondaryMuscle, equipment;
    public string Name { get => name; set => SetProperty(ref name, value); }
    public string Description { get => description; set => SetProperty(ref description, value); }
    public string PrimaryMuscle { get => primaryMuscle; set => SetProperty(ref primaryMuscle, value); }
    public string SecondaryMuscle { get => secondaryMuscle; set => SetProperty(ref secondaryMuscle, value); }
    public string Equipment { get => equipment; set => SetProperty(ref equipment, value); }

    public AsyncCommand SaveCommand { get; }

    ExerciseService exerciseService;
    //ICoffeeService coffeeService;

    public AddExerciseViewModel(ExerciseService exerciseService)
    {
        Title = "Add Exercise";
        SaveCommand = new AsyncCommand(Save);
        //coffeeService = DependencyService.Get<ICoffeeService>();

        this.exerciseService = exerciseService;
    }

    async Task Save()
    {
        if (string.IsNullOrWhiteSpace(name))
            return;
        
        Exercise exercise = new Exercise()
        {
            Name = name,
            Description = description,
            Equipment = equipment,
            PrimaryMuscle = primaryMuscle,
            SecondaryMuscle = secondaryMuscle
        };

        await exerciseService.CreateExercise(exercise);
        await Shell.Current.GoToAsync("..");
    }
}
