using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using Microsoft.Extensions.Logging;
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
            //builder.Services.AddTransient<EditorEventViewModel>().AddTransient<EditorEventPage>();
            //builder.Services.AddTransient<StatisticsViewModel>().AddTransient<StatisticsPage>();
            //builder.Services.AddTransient<MailingViewModel>().AddTransient<MailingPage>();
            //builder.Services.AddSingleton<IMailAccount, MailAccount>();
            builder.Services.AddSingleton<IPopupService, CommunityToolkit.Maui.PopupService>()
                .AddTransientPopup<SavingTestPopup, SavingTestViewModel>();

           
            //builder.Services.AddTransient<AddScheduledEventViewModel>().AddTransient<AddScheduledEventPage>();
            //builder.Services.AddTransient<GuestVerificationTableViewModel>().AddTransient<GuestVerificationTablePage>();
            //builder.Services.AddTransient<SettingsPage>().AddTransient<SettingsViewModel>();
            //builder.Services.AddSingleton<IDataService, DataService>();
            builder.Services.AddSingleton<ILocalDbService, LocalDbService>();
            builder.Services.AddSingleton<INavigationService, NavigationService>();
            //builder.Services.AddTransient<ListOfEventsPage>().AddTransient<ListOfEventsViewModel>();
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<MainPage>();
            return builder;
        }
    }
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureServices()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
