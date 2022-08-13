using SQLite;
using TrainingApp.Model;

namespace TrainingApp.Services;

public class ExerciseService
{
    static SQLiteAsyncConnection db;

    static async Task Init()
    {
        if (db != null)
        {
            return;
        }

        var databasePath = Path.Combine(FileSystem.AppDataDirectory, "Exercises.db");
        db = new SQLiteAsyncConnection(databasePath);
        await db.CreateTableAsync<Exercise>();

        await CreateExercise();

    }

    static async Task CreateExercise()
    {
        var exercise = new Exercise();
        exercise.Name = "Dumbell Curls";
        exercise.Description = "Move the handles up and down";
        exercise.Equipment = "Dumbell";
        exercise.PrimaryMuscle = "Chest";
        exercise.SecondaryMuscle = "Triceps";


        await db.InsertAsync(exercise);
    }
}