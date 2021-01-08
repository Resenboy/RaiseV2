using Plugin.Media;
using Plugin.Media.Abstractions;
using Raise.Enums;
using Raise.Model.App;
using Raise.Model.Models;
using Raise.Services;
using Raise.Utils;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Raise.ViewModels
{
    public class AccountViewModel : BaseViewModel
    {
        ApiResponse<Account> _apiResponse;

        AccountClient _accountClient;

        MediaFile _globalImage;
        MediaProvider _mediaProvider;

        AppCustomConfigurationViewModel _appCustomConfigurationViewModel;
        UserCustomConfigurationApp _loadedConfigs;

        public Account Account { get; set; }

        public AccountViewModel()
        {
            _apiResponse = new ApiResponse<Account>();

            _accountClient = new AccountClient();

            _mediaProvider = new MediaProvider();
            _appCustomConfigurationViewModel = DependencyService.Get<AppCustomConfigurationViewModel>();
            _loadedConfigs = _appCustomConfigurationViewModel.LoadUserCustomConfigurationApp();

            TakePictureCommand = new Command<Account>(async (accountParms) => await TakePictureAction());
            LoadPictureCommand = new Command<Account>(async (accountParms) => await LoadPictureAction());
            SaveCommand = new Command<Account>(async (accountParms) => await SaveAction());

            LoadAccount();
        }

        public AccountViewModel(string facebookProfile)
            : this()
        {
            LoadProfileInformation(facebookProfile);
        }

        public ICommand TakePictureCommand { get; set; }
        public ICommand LoadPictureCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        void LoadAccount() 
        {
            Account = _accountClient.GetByObj(new Account() { GuidKey = GuidGenerate.USER_ID }).Data;
            if (Account == null)
                Account = new Account();
        }

        private async Task TakePictureAction()
        {
            try
            {
                await CrossMedia.Current.Initialize();
                _globalImage = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    Directory = "Download",
                    Name = "profile.jpg",

                    PhotoSize = PhotoSize.Custom,
                    CustomPhotoSize = 90,
                    CompressionQuality = 92,
                    SaveToAlbum = true
                });

                if (_globalImage == null)
                    return;

                Account.ProfileImg = _mediaProvider.GetImage(new RecoveryImageParms { ImageSourceType = ImageSourceType.FromStream, Stream = _globalImage.GetStream() });
            }
            catch (Exception)
            {
                await Shell.Current.DisplayAlert("Erro", "Falha ao tirar foto", "OK");
            }
        }

        private async Task LoadPictureAction()
        {
            try
            {
                await CrossMedia.Current.Initialize();
                _globalImage = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    PhotoSize = PhotoSize.Medium
                });

                if (_globalImage == null)
                    return;

                Account.ProfileImg = _mediaProvider.GetImage(new RecoveryImageParms { Stream = _globalImage.GetStream() });
            }
            catch (Exception)
            {
                await Shell.Current.DisplayAlert("Erro", "Erro ao abrir imagem", "OK");
            }
        }

        private async Task SaveAction()
        {
            if (Account.AccountIdenti == 0)
            {
                _apiResponse = _accountClient.Add(Account);
            }
            else
            {
                _apiResponse = _accountClient.Update(Account);
            }

            if (!_apiResponse.IsSuccess)
            {
                await Shell.Current.DisplayAlert("Erro", _apiResponse.Message, "OK");
                return;
            }

            var userCustomConfig = new UserCustomConfigurationApp();
            userCustomConfig.IsLoggedIn = true;
            userCustomConfig.IsFirstAccess = false;
            userCustomConfig.GuidKey = _apiResponse.Data.GuidKey.ToString();
            userCustomConfig.LastUserNameOnThisPhone = _apiResponse.Data.User.Name;
            _appCustomConfigurationViewModel.SaveUserCustomConfigAsync(userCustomConfig);
        }

        void LoadProfileInformation(string profile)
        {
            //preencher informacoes basicas obtidas atraves do FACEBOOK;
            if (!string.IsNullOrEmpty(profile))
            {
                Account.User.Name = profile.Split('|')[0];
                Account.ProfileImg = _mediaProvider.GetImage(new RecoveryImageParms { Uri = new Uri(profile.Split('|')[1]) });
                return;
            }
        }
    }
}
