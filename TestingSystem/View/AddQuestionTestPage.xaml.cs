using TestingSystem.Model;
using TestingSystem.ViewModel;
namespace TestingSystem.View;

public partial class AddQuestionTestPage : ContentPage
{
	private readonly AddQuestionTestViewModel _viewModel;
    private const int MIN_COUNT = 2;
	public AddQuestionTestPage(AddQuestionTestViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
        _viewModel = viewModel;
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
        
        buttonAdd.IsEnabled = isEnabled ;
    }
    private void MenuFlyoutItem_Clicked(object sender, EventArgs e)
    {
        ChangeIsEnabledButtonAdd(_viewModel.CountAnswerOptions >= MIN_COUNT & _viewModel.IsAnswerOptionsAreFilledIn);
    }

    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var switchSender = sender as CheckBox;
        if (switchSender is not null && switchSender.BindingContext is AnswerOption answerOption)
        {
            answerOption.Correct = e.Value ? Selector.CorrectValue : Selector.NoValueSelected;
            ChangeIsEnabledButtonAdd(_viewModel.IsAnswerOptionsAreFilledIn);
        }
    }
}
