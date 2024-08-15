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
    private void Editor_TextChangedQuestion(object sender, TextChangedEventArgs e)
    {
        CheckingTextChanges(_viewModel.Question.Question);
    }

    private void CheckingTextChanges(string text)
    {
        ChangeIsEnabledButtonAdd(!string.IsNullOrEmpty(text) && _viewModel.IsAnswerOptionsAreFilledIn);
    }


    private void Editor_TextChanged(object sender, TextChangedEventArgs e)
    {
        CheckingTextChanges(e.NewTextValue);
    }

    private void ChangeIsEnabledButtonAdd(bool isEnabled)
    {
        buttonAdd.IsEnabled = isEnabled;
    }
    private void MenuFlyoutItem_Clicked(object sender, EventArgs e)
    {
        ChangeIsEnabledButtonAdd(_viewModel.CountAnswerOptions >= 2 & _viewModel.IsAnswerOptionsAreFilledIn);
    }
}
