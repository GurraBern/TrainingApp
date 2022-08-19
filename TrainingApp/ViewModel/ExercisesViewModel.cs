using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using TrainingApp.Model;
using TrainingApp.Services;

namespace TrainingApp.ViewModel;

public partial class ExercisesViewModel : BaseViewModel
{
    public ObservableCollection<Exercise> Exercises { get; } = new();

    public ExercisesViewModel()
    {

    }

    [RelayCommand]
    public async Task GetExercisesAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            var exercises = await ExerciseService.GetExercises();

            if (exercises.Count != 0)
                Exercises.Clear();

            foreach(var exercise in exercises)
            {
                Exercises.Add(exercise);
            }

        }
        catch(Exception ex)
        {
            Debug.WriteLine(ex);
        }
        finally
        {
            IsBusy = false;
        }
    }

}
