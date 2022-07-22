using System;

public class ActivityIndicator
{
    private Activity _model;
    private BoxView _boxIndicator;

    public ActivityIndicator(Activity model)
	{
        this._model = model;
        InitActivityBox();
    }


    //TODO make in xaml only
    private void InitActivityBox()
    {
        this._boxIndicator = new BoxView();
        SetIndicatorBoxColor(this._model.ActivityState);
        _boxIndicator.Opacity = 1;
        _boxIndicator.CornerRadius = 2;
        _boxIndicator.WidthRequest = 20;
        _boxIndicator.HeightRequest = 20;
        _boxIndicator.Margin = 1;
        _boxIndicator.VerticalOptions = LayoutOptions.Center;
        _boxIndicator.HorizontalOptions = LayoutOptions.Center;
    }


    //TODO choose color palette
    private Color SetIndicatorBoxColor(ActivityState state) => state switch
    {
        ActivityState.PRESENT => new Color(116, 255, 112),
        ActivityState.RESTDAY => new Color(255, 203, 76),
        _ => new Color(0,0,0,0.1f)
    };

    public void SetActivityStatus(ActivityState activityState)
	{
        _model.ActivityState = activityState;
        _boxIndicator.Color = SetIndicatorBoxColor(activityState);
    }

    public void SetDate(DateTime date)
    {
        _model.Date = date.ToShortDateString();
    }

    public string GetDate()
    {
        return _model.Date;
    }

    public BoxView GetBoxIndicator()
    {
        return _boxIndicator;
    }
}

