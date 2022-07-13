using System;

public class ActivityIndicator
{

//    private Date

    private BoxView boxIndicator;
    private ActivityState activityState;

    public ActivityIndicator()
	{
        initActivityBox();
    }

    private void initActivityBox()
    {
        boxIndicator = new BoxView();
        boxIndicator.Color = Color.FromRgb(225, 255, 255);

        //SetColor
        setIndicatorBoxColor(this.activityState);

        boxIndicator.Opacity = 1;
        boxIndicator.CornerRadius = 2;
        boxIndicator.WidthRequest = 20;
        boxIndicator.HeightRequest = 20;
        boxIndicator.Margin = 1;
        boxIndicator.VerticalOptions = LayoutOptions.Center;
        boxIndicator.HorizontalOptions = LayoutOptions.Center;
    }


    private Color setIndicatorBoxColor(ActivityState state) => state switch
    {
        ActivityState.PRESENT => new Color(220, 220, 220),
        ActivityState.RESTDAY => new Color(50, 50, 220),
        _ => new Color(30, 120, 120)
    };

    public void setActivityStatus(ActivityState activityState)
	{
        this.activityState = activityState;
        boxIndicator.Color = setIndicatorBoxColor(activityState);
    }

    public BoxView getBoxIndicator()
    {
        return this.boxIndicator;
    }
}

