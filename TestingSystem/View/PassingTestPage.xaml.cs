using TestingSystem.ViewModel;

namespace TestingSystem.View;

public partial class PassingTestPage : ContentPage
{
	public PassingTestPage(PassingTestViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}