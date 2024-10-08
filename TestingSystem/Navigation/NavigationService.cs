﻿using TestingSystem.ViewModel;
using TestingSystem.ViewModel.Base;
using System.Diagnostics;
using TestingSystem.View;



namespace TestingSystem.Navigation
{
    public sealed class NavigationService : INavigationService
    {

        private readonly Dictionary<Type, Type> viewModelView = new()
        {
            {typeof(MainViewModel),typeof(MainPage)},
            {typeof(AddQuestionTestViewModel),typeof(AddQuestionTestPage)},
            {typeof(PassingTestViewModel),typeof(PassingTestPage)},
        };



        private readonly IServiceProvider _services;
        public INavigation Navigation
        {
            get
            {
                INavigation navigation = Application.Current?.MainPage?.Navigation;
                if (navigation is not null)
                    return navigation;
                else
                {
                    if (Debugger.IsAttached)
                        Debugger.Break();
                    throw new Exception();
                }
            }
        } 
        public bool IsAnimated { get; set; }
        public NavigationService(IServiceProvider services)=> _services = services;

        public Task NavigateToMainPageAsync(object parameter = null)
                    => NavigateToPageAsync<MainPage>(parameter);
        public Task NavigateByPageAsync<T>(object parameter = null, object parameterSecond = null) where T : Page
                => NavigateToPageAsync<T>(parameter, parameterSecond);
   


        public Task NavigateByViewModelAsync<T>(object parameter = null) where T : ViewModelBase
        {

            if (viewModelView.ContainsKey(typeof(T)))
            {
                var typePage = viewModelView[typeof(T)];
                return NavigateToPageAsync(typePage, parameter);
            }
            else
                throw new KeyNotFoundException($"Не найден тип в словаре {viewModelView}");   
        }


        private async Task NavigateToPageAsync(Type typePage,object parameter = null) 
        {
            if(ResolvePage(typePage) is Page toPage)
                await InitializecircutPageAsync(toPage, parameter);
        }

        private async Task NavigateToPageAsync<T>(object parameter = null, object parameterSecond = null) where T : Page
        {
            try
            {
                if (ResolvePage<T>() is T toPage)
                    await InitializecircutPageAsync(toPage, parameter, parameterSecond);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message,ex);

            }
         
        }

        private async Task InitializecircutPageAsync(Page toPage, object parameter = null, object parameterSecond = null)
        {
            if (toPage is not null)
            {
                toPage.NavigatedTo += Page_NavigatedToAsync;
                var toViewModel = GetPageViewModelBase(toPage);
                if (toViewModel is not null)
                    await toViewModel.OnNavigatingToAsync(parameter, parameterSecond);
                await Navigation.PushAsync(toPage, IsAnimated);
                toPage.NavigatedFrom += Page_NavigatedFromAsync;
            }
            else
                throw new InvalidOperationException($"Unable to resolve type");
        }

        private async void Page_NavigatedFromAsync(object sender, NavigatedFromEventArgs e)
        {
            bool isForwardNavigation = Navigation.NavigationStack.Count > 1
                && Navigation.NavigationStack[^2] == sender;
            if (sender is Page thisPage)
            {
                if (!isForwardNavigation)
                {
                    thisPage.NavigatedTo -= Page_NavigatedToAsync;
                    thisPage.NavigatedFrom -= Page_NavigatedFromAsync;
                }
                await CallNavigatedFromAsync(thisPage, isForwardNavigation);
            }
        }
        private Task CallNavigatedFromAsync(Page p, bool isForward)
        {
            var fromViewModel = GetPageViewModelBase(p);
            if (fromViewModel is not null)
                return fromViewModel.OnNavigatedFromAsync(isForward);
            return Task.CompletedTask;
        }
        private ViewModelBase GetPageViewModelBase(Page toPage)
        { 
            if(toPage?.BindingContext is ViewModelBase viewModel)
                return viewModel;
            throw new NullReferenceException($"Не найден BindingContext в Page {toPage?.GetType().FullName}");
        }
        private async void Page_NavigatedToAsync(object sender, NavigatedToEventArgs e)
        { 
            if(sender is Page toPage)
             await CallNavigatedTo(toPage); 
        }
        private  Task CallNavigatedTo(Page page)
        {
            var fromViewModel = GetPageViewModelBase(page);
            if (fromViewModel is not null)
                return fromViewModel.OnNavigatedToAsync();
            return Task.CompletedTask;
        }


        private T ResolvePage<T>() where T : Page
          => _services.GetService<T>();

        private object ResolvePage(Type type)
        {
            var service = _services.GetService(type);
            if (service is not null)
               return service;
            throw new NullReferenceException($"Не найден сервер в {_services.GetType().FullName}");
        }

        public void NavigateBack()
        {
            if (Navigation.NavigationStack.Count > 1)
            {
               Navigation.PopAsync(IsAnimated);
            }
        }

    }
}
