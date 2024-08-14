
using TestingSystem.Model;
using TestingSystem.ViewModel;

namespace TestingSystem.View;

public partial class AddQuestionTestPage : ContentPage
{
	private readonly AddQuestionTestViewModel _viewModel;
	public AddQuestionTestPage(AddQuestionTestViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
        _viewModel = viewModel;


    }

    private void Switch_Toggled(object sender, ToggledEventArgs e)
    {
        var switchSender = sender as Switch;
        if (switchSender is not null && switchSender.BindingContext is AnswerOption answerOption)
            answerOption.Correct = e.Value?Selector.CorrectValue:Selector.NoValueSelected;
    }



    private void Editor_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(_viewModel.Question.Question) &&
            _viewModel.Question.AnswerOptions.Count(x=>!string.IsNullOrEmpty(x.Answer)) == _viewModel.Question.AnswerOptions.Count)
        {
            buttonAdd.IsEnabled = true;
            return;
        }

        buttonAdd.IsEnabled = false;
    }

}