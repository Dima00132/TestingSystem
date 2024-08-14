using TestingSystem.Model;
using TestingSystem.ViewModel;

namespace TestingSystem.View;

public partial class PassingTestPage : ContentPage
{
	private readonly PassingTestViewModel _viewModel;
    public PassingTestPage(PassingTestViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = viewModel;
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		if (_viewModel.StackQuestionTests.Count != 0)
			return;
        TestProgressDisplay.IsVisible = false;
		TestResultDisplay.IsVisible = true;
    }


    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
     
        var checkBoxSender = sender as CheckBox;
        if (checkBoxSender is not null && checkBoxSender.BindingContext is AnswerOption answerOption)
            answerOption.Selected = e.Value ? Selector.CorrectValue : Selector.NoValueSelected;
    }
}