using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using TestingSystem.Navigation;
using TestingSystem.Service;
using TestingSystem.Service.Interface;
using TestingSystem.View;

using TestingSystem.View.ViewPopup;
using TestingSystem.ViewModel;

namespace TestingSystem
{
    public static class ServicesExtensions
    {
        public static MauiAppBuilder ConfigureServices(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<AddQuestionTestViewModel>().AddTransient<AddQuestionTestPage>();
            builder.Services.AddTransient<PassingTestViewModel>().AddTransient<PassingTestPage>();
            builder.Services.AddSingleton<IPopupService, PopupService>()
                .AddTransientPopup<SavingTestPopup, SavingTestViewModel>();
            builder.Services.AddSingleton<ILocalDbService, LocalDbService>();
            builder.Services.AddSingleton<INavigationService, NavigationService>();
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<MainPage>();
            return builder;
        }
    }
}
