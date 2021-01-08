using Raise.Model.App;
using Raise.Model.Models;
using Raise.Services;
using Raise.ViewModels;
using Raise.Views;
using RestSharp;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Raise
{
    public partial class AppShell : Shell
    {
        AppCustomConfigurationViewModel configurationViewModel;
        UserCustomConfigurationApp _loadedConfigs;

        public AppShell()
        {
            InitializeComponent();

            BindingContext = configurationViewModel = DependencyService.Get<AppCustomConfigurationViewModel>(DependencyFetchTarget.GlobalInstance);
            _loadedConfigs = configurationViewModel.LoadUserCustomConfigurationApp();

            StartApp();
        }

        private void StartApp()
        {
            if (Current == null)
                Application.Current.MainPage = this;

            if (_loadedConfigs.GuidKey == null || _loadedConfigs.IsFirstAccess)
            {
                ShowWalkthroughPage();
            }
            else
            {
                if (_loadedConfigs.RememberMe)
                {
                    ShowProfilePage(_loadedConfigs.GuidKey);
                }
                else
                {
                    ShowLoginPage();
                }
            }
        }

        // chamada pelo facebook
        public async static Task NavigateToProfile(string profile)
        {
            await Current.Navigation.PushModalAsync(new AccountPage(profile), true);
        }

        public void ShowProfilePage(string guidKey)
        {
            if (_loadedConfigs.IsLoggedIn && Guid.Parse(guidKey) != Guid.Empty)
            {
                Current.Navigation.PopModalAsync();
            }
            else
            {
                var _apiResponse = new UserClient().GetByObj(new User() { GuidKey = Guid.Parse(guidKey) });
                if (_apiResponse.IsSuccess)
                {
                    Current.Navigation.PopModalAsync();
                }
                else
                {
                    Current.DisplayAlert("Falha", _apiResponse.Message, "OK");
                }
            }
        }
        public void ShowLoginPage()
        {
            _loadedConfigs.IsLoggedIn = false;
            _loadedConfigs.GuidKey = Guid.Empty.ToString();
            Current.Navigation.PopModalAsync();
            Current.Navigation.PushModalAsync(new StartPage(), false);
        }
        public void ShowWalkthroughPage()
        {
            _loadedConfigs.IsLoggedIn = false;
            _loadedConfigs.IsFirstAccess = true;
            _loadedConfigs.GuidKey = Guid.Empty.ToString();
            Current.Navigation.PushModalAsync(new StartupPage(), true);
        }
    }
}
