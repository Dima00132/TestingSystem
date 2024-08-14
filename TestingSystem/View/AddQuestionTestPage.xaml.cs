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
            answerOption.Correct = e.Value?AnswerChoice.Correct:AnswerChoice.NotSelected;
    }
}