using Raise.Enums;
using Raise.Model.App;
using Raise.Model.Models;
using Raise.Services;
using Raise.Utils;
using Raise.Views;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Raise.ViewModels
{
    public class LoginSignupViewModel : BaseViewModel
    {
        UserClient _userClient;
        AccountClient _accountClient;

        ApiResponse<User> _apiResponse;
        AppCustomConfigurationViewModel _appCustomConfigurationViewModel;

        public LoginSignupViewModel()
        {
            InitialTypePages = GetPages();

            User = new User();

            _userClient = new UserClient();
            _accountClient = new AccountClient();

            _apiResponse = new ApiResponse<User>();
            _appCustomConfigurationViewModel = DependencyService.Get<AppCustomConfigurationViewModel>();

            LoginCommand = new Command<User>(async (loginParms) => await LoginAction(loginParms));
            CreateUserCommand = new Command<User>(async (loginParms) => await CreateUserAction(loginParms));
        }

        //OBJETO DE INTERACAO NA TELA (BINDING)
        public User User { get; set; }

        //COMMANDOS CHAMADOS PELO USUARIO
        public ICommand LoginCommand { get; set; }
        public ICommand CreateUserCommand { get; set; }

        //LISTA DE PAGINAS DO TAB CUSTOM
        public IList<InitialTypePage> InitialTypePages { get; private set; }

        private async Task LoginAction(User user)
        {
            user.GuidKey = GuidGenerate.USER_ID;
            _apiResponse = _userClient.Login(user);
            if (_apiResponse.Data == null)
            {
                await App.Current.MainPage.DisplayAlert("Falha", _apiResponse.Message, "OK");
                return;
            }

            if (user.RememberMe)
            {
                var userCustomConfig = new UserCustomConfigurationApp();
                userCustomConfig.GuidKey = _apiResponse.Data.GuidKey.ToString();
                userCustomConfig.IsFirstAccess = false;
                userCustomConfig.LastPasswordOnThisPhone = _apiResponse.Data.Password;
                _appCustomConfigurationViewModel.SaveUserCustomConfigAsync(userCustomConfig);
            }

            // verifica se existe uma conta, se não existir, chama a página para realizar a criação da conta
            var _apiAccountResponse = _accountClient.GetByObj(new Account() { UserIdenti = _apiResponse.Data.UserIdenti });
            if (_apiAccountResponse.Data == null)
                await CreateAccount();
            else
                await App.Current.MainPage.Navigation.PopModalAsync();
        }

        private async Task CreateUserAction(User user)
        {
            if (!user.AcceptedTerms)
            {
                await Shell.Current.DisplayAlert("Atenção", "Favor aceitar o termo de conduta.", "OK");
                return;
            }

            _apiResponse = _userClient.Add(user);
            if (!_apiResponse.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Falha", _apiResponse.Message, "OK");
                return;
            }

            var userCustomConfig = new UserCustomConfigurationApp();
            userCustomConfig.GuidKey = _apiResponse.Data.GuidKey.ToString();
            userCustomConfig.IsFirstAccess = false;
            userCustomConfig.IsLoggedIn = true;
            userCustomConfig.LastPasswordOnThisPhone = _apiResponse.Data.Password;
            _appCustomConfigurationViewModel.SaveUserCustomConfigAsync(userCustomConfig);

            await CreateAccount();
        }

        private IList<InitialTypePage> GetPages()
        {
            return new List<InitialTypePage>
            {
                new InitialTypePage { Name = "Login", Type = StartPageType.Login },
                new InitialTypePage { Name = "Signup", Type = StartPageType.Signup }
            };
        }

        async Task CreateAccount() 
        {
            await Shell.Current.Navigation.PushModalAsync(new AccountPage());
        }
    }
}
