using SQLite;

namespace TrainingApp.Model;

public class Exercise
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string PrimaryMuscle { get; set; }
    public string SecondaryMuscle { get; set; }
    public string Equipment { get; set; }
}

