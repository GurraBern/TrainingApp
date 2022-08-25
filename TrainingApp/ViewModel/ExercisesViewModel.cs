using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using TrainingApp.Model;
using TrainingApp.Services;
using TrainingApp.View;

namespace TrainingApp.ViewModel;

public partial class ExercisesViewModel : BaseViewModel
{
    ExerciseService exerciseService;

    public ObservableCollection<Exercise> Exercises { get; } = new();

    public ExercisesViewModel(ExerciseService exerciseService)
    {
        this.exerciseService = exerciseService;
        new Action(async () => await GetExercisesAsync()).Invoke();
    }

    [RelayCommand]
    async Task Add()
    {
        var route = $"{nameof(AddExercisePage)}";
        await Shell.Current.GoToAsync(route);
    }


    //TODO get All exercises on route back
    public async Task GetExercisesAsync()
    {

        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            var exercises = await exerciseService.GetExercises();

            if (exercises.Count != 0)
                Exercises.Clear();

            foreach (var exercise in exercises)
            {
                Exercises.Add(exercise);
            }

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
        finally
        {
            IsBusy = false;
        }
    }
}
