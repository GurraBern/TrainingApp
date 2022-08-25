using SQLite;

namespace TrainingApp.Services;

public class ExerciseService
{
    static SQLiteAsyncConnection db;

    async Task Init()
    {
        if (db != null)
        {
            return;
        }

        var databasePath = Path.Combine(FileSystem.AppDataDirectory, "Exercises.db");
        db = new SQLiteAsyncConnection(databasePath);
        await db.CreateTableAsync<Exercise>();
    }

    public async Task CreateExercise(Exercise exercise)
    {
        await Init();
        await db.InsertAsync(exercise);
    }

    public async Task<List<Exercise>> GetExercises()
    {
        await Init();
        var exercises = await db.Table<Exercise>().ToListAsync();
        return exercises;
    }
}