using Microsoft.Maui.Graphics.Text;
using TrainingApp.Model;
using static System.Net.Mime.MediaTypeNames;

public class ActivityIndicator
{
    private ExerciseActivity _model;

    public ActivityIndicator(ExerciseActivity model)
    {
        this._model = model;
    }

    private Color SetIndicatorBoxColor(ActivityState state) => state switch
    {
        ActivityState.PRESENT => new Color(116, 255, 112),
        ActivityState.RESTDAY => new Color(255, 203, 76),
        _ => new Color(0, 0, 0, 0.1f)
    };

    //public void SetActivityStatus(ActivityState activityState)
    //{
    //    _model.ActivityState = activityState;
    //}

    //public void SetDate(DateTime date)
    //{
    //    _model.Date = date.ToShortDateString();
    //}
    //private string SplitToDay(string date)
    //{
    //    var splitString = date.Split("-");

    //    string dayString = splitString[splitString.Count() - 1];

    //    return dayString;
    //}

    //public string GetDate()
    //{
    //    return _model.Date;
    //}


}

