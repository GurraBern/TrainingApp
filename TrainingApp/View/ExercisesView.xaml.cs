﻿namespace TrainingApp.View;

public partial class ExercisesView : ContentPage
{
    public ExercisesView(ExercisesViewModel viewModel)
	{
        InitializeComponent();
        BindingContext = viewModel;
    }
}
