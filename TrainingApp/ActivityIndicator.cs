using System;

public class ActivityIndicator
{
    private DateTime timeOfDate;
    private BoxView boxIndicator;
    private ActivityState activityState;

    public ActivityIndicator()
	{
        InitActivityBox();
    }

    private void InitActivityBox()
    {
        this.boxIndicator = new BoxView();
        SetIndicatorBoxColor(this.activityState);
        boxIndicator.Opacity = 1;
        boxIndicator.CornerRadius = 2;
        boxIndicator.WidthRequest = 20;
        boxIndicator.HeightRequest = 20;
        boxIndicator.Margin = 1;
        boxIndicator.VerticalOptions = LayoutOptions.Center;
        boxIndicator.HorizontalOptions = LayoutOptions.Center;
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
        this.activityState = activityState;
        this.boxIndicator.Color = SetIndicatorBoxColor(activityState);
    }

    public void SetDate(DateTime date)
    {
        this.timeOfDate = date;
    }

    public DateTime GetDate()
    {
        return this.timeOfDate;
    }

    public BoxView GetBoxIndicator()
    {
        return this.boxIndicator;
    }
}

