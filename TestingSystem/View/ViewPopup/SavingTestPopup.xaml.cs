using CommunityToolkit.Maui.Views;
using TestingSystem.ViewModel;
namespace TestingSystem.View.ViewPopup;

public sealed partial class SavingTestPopup : Popup
{
    public SavingTestPopup(SavingTestViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;

    }
}