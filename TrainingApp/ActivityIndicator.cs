using System;

public class ActivityIndicator
{
    private ActivityIndicatorModel _model;
    private BoxView _boxIndicator;

    public ActivityIndicator(ActivityIndicatorModel model)
	{
        this._model = model;
        InitActivityBox();
    }

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
        ActivityState.PRESENT => new Color(152, 255, 79),
        ActivityState.RESTDAY => new Color(255, 180, 80),
        _ => new Color(0,0,0,0.3f)
    };

    public void SetActivityStatus(ActivityState activityState)
	{
        this._model.ActivityState = activityState;
        this._boxIndicator.Color = SetIndicatorBoxColor(activityState);
    }

    public void SetDate(DateTime date)
    {
        this._model.Date = date;
    }

    public DateTime GetDate()
    {
        return this._model.Date;
    }

    public BoxView GetBoxIndicator()
    {
        return this._boxIndicator;
    }
}

