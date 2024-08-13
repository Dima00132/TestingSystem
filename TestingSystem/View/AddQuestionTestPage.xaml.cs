using TestingSystem.ViewModel;

namespace TestingSystem.View;

public partial class AddQuestionTestPage : ContentPage
{
	public AddQuestionTestPage(AddQuestionTestViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}