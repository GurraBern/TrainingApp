using System;

public class ActivityIndicator
{
    private DateTime timeOfDate;
    private BoxView boxIndicator;
    private ActivityState activityState;

    public ActivityIndicator()
	{
        initActivityBox();
    }

    private BoxView GetBoxIndicator()
    {
        return this.boxIndicator;
    }

    private void initActivityBox()
    {
        this.boxIndicator = new BoxView();
        setIndicatorBoxColor(this.activityState);
        boxIndicator.Opacity = 1;
        boxIndicator.CornerRadius = 2;
        boxIndicator.WidthRequest = 20;
        boxIndicator.HeightRequest = 20;
        boxIndicator.Margin = 1;
        boxIndicator.VerticalOptions = LayoutOptions.Center;
        boxIndicator.HorizontalOptions = LayoutOptions.Center;
    }


    //TODO choose color palette
    private Color setIndicatorBoxColor(ActivityState state) => state switch
    {
        ActivityState.PRESENT => new Color(220, 220, 220),
        ActivityState.RESTDAY => new Color(50, 50, 220),
        _ => new Color(30, 120, 120)
    };

    public void setActivityStatus(ActivityState activityState)
	{
        this.activityState = activityState;
        this.boxIndicator.Color = setIndicatorBoxColor(activityState);
    }

    public void setDate(DateTime date)
    {
        this.timeOfDate = date;
    }

    public DateTime getDate()
    {
        return this.timeOfDate;
    }

    public BoxView getBoxIndicator()
    {
        return this.boxIndicator;
    }
}

