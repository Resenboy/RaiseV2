using Raise.Enums;
using Raise.Model.App;
using Raise.Model.Models;
using Raise.Views;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Raise.ViewModels
{
    public class ActivityPageViewModel : BaseViewModel
    {
        AppCustomConfigurationViewModel _configurationViewModel;
        UserCustomConfigurationApp _loadedConfigs;

        private ObservableCollection<Post> _userPosts;
        public ObservableCollection<Post> UserPosts
        {
            get
            {
                return _userPosts;
            }
            set
            {
                OnPropertyChanged("UserPosts");
                _userPosts = value;
            }
        }
        public ICommand OpenActivityDetailCommand { get; }
        public ICommand SavePersonCommand { get; }
        public ICommand RefreshCommand { get; }
        public ActivityPageViewModel()
        {
            _configurationViewModel = DependencyService.Get<AppCustomConfigurationViewModel>();

            FillUserPosts_old();

            OpenActivityDetailCommand = new Command<Post>(async (x) => await ExecuteOpenActivityDetailPage(x));
            SavePersonCommand = new Command<Post>(async (x) => await SavePersonAction(x));
            RefreshCommand = new Command(async () => await RefreshCommandAction());
        }
        private async Task RefreshCommandAction()
        {
            _loadedConfigs = _configurationViewModel.LoadUserCustomConfigurationApp();

            FillUserPosts_old();
            IsBusy = false;
        }


        private async Task SavePersonAction(Post x)
        {
            //await _firebaseHelper.AddPerson(x.Name, new Random().Next(1, 999999));
            //await Shell.Current.DisplayAlert("Mensagem", "Nova pessoa adicionada com sucesso!", "OK");
        }

        private async Task ExecuteOpenActivityDetailPage(Post x)
        {
            var page = new ActivityDetailPage();
            page.BindingContext = x;
            await AppShell.Current.Navigation.PushAsync(page);
        }

        private void FillUserPosts_old()
        {
            UserPosts = new ObservableCollection<Post>();
            var contadorTeste = 0;

            string[] randomUrls = new string[] {
                "https://i.pinimg.com/564x/ce/50/53/ce50532e4fc5a18035c854d5babfe9fe.jpg",
                "https://i.pinimg.com/564x/af/ef/dd/afefddd94f8a14018d875b6c436d6675.jpg",
                "https://i.pinimg.com/564x/e8/44/cc/e844cca330436c649d24b28cc22d85bc.jpg",
                "https://i.pinimg.com/564x/54/b5/01/54b50107288b9e4df513f0a6293607a3.jpg",
                "https://i.pinimg.com/564x/7e/61/43/7e6143260212fa01af47785ace6edfef.jpg",
                "https://i.pinimg.com/564x/80/29/b5/8029b536868412263927ecb699e47d93.jpg",
                "https://i.pinimg.com/564x/c9/60/58/c96058849f63851ef447e10dcf3b498a.jpg",
                "https://i.pinimg.com/564x/9f/ed/ae/9fedae67312c50001522120a9b298374.jpg",
                "https://i.pinimg.com/564x/f4/3d/87/f43d879da8ecc18d4b9b88cb7a0d8379.jpg" };

            while (contadorTeste <= 200)
            {
                var post = new Post();
                post.UrlPostImage = randomUrls[new Random().Next(0, randomUrls.Length)];
                post.User = new User { Name = "Alvin Resende" };
                post.Description = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.";
                post.Date = DateTime.Now;
                //post.Hashtags = "#PUBG #COD #FIFA20",
                //post.Comments = 788,
                //post.Likes = 1569,
                //post.Place = "Ribeirão Preto - BR",
                //post.Participants = "@resenboy",
                //post.Activity = "Gamer Full",
                //post.ProfileType = ProfileType.Gamer

                UserPosts.Add(post);
                contadorTeste++;
            }
        }

        private void FillUserPosts()
        {
            if (_loadedConfigs == null || (_loadedConfigs != null && _loadedConfigs.GuidKey == null))
                return;

            //var _apiResponse = new ApiResponse();
            //var feedInfo = new Feed() { UserId = GuidGenerate.USER_ID };
            //_apiResponse = new FeedClient().GetAsync(feedInfo);

            //var feeds = _apiResponse.Data as List<Feed>;
            //if (feeds == null)
            //    return;

            UserPosts = new ObservableCollection<Post>();

            //foreach (var item in feeds)
            //{
            //    UserPosts.Add(new UserPost
            //    {
            //        UrlPostImage = item.UrlPostImage,
            //        Name = "Alvin Resende",
            //        PostDescription = item.PostDescription,
            //        Hashtags = "#PUBG #COD #FIFA20",
            //        Comments = 788,
            //        Likes = 1569,
            //        Place = "Ribeirão Preto - BR",
            //        PostDate = item.PostDate,
            //        Participants = "@resenboy",
            //        Activity = "Gamer Full",
            //        ProfileType = ProfileType.Gamer
            //    });
            //}

            var contadorTeste = 0;

            string[] randomUrls = new string[] {
                "https://i.pinimg.com/564x/ce/50/53/ce50532e4fc5a18035c854d5babfe9fe.jpg",
                "https://i.pinimg.com/564x/af/ef/dd/afefddd94f8a14018d875b6c436d6675.jpg",
                "https://i.pinimg.com/564x/e8/44/cc/e844cca330436c649d24b28cc22d85bc.jpg",
                "https://i.pinimg.com/564x/54/b5/01/54b50107288b9e4df513f0a6293607a3.jpg",
                "https://i.pinimg.com/564x/7e/61/43/7e6143260212fa01af47785ace6edfef.jpg",
                "https://i.pinimg.com/564x/80/29/b5/8029b536868412263927ecb699e47d93.jpg",
                "https://i.pinimg.com/564x/c9/60/58/c96058849f63851ef447e10dcf3b498a.jpg",
                "https://i.pinimg.com/564x/9f/ed/ae/9fedae67312c50001522120a9b298374.jpg",
                "https://i.pinimg.com/564x/f4/3d/87/f43d879da8ecc18d4b9b88cb7a0d8379.jpg" };

            while (contadorTeste <= 10)
            {
                var post = new Post();
                post.UrlPostImage = randomUrls[new Random().Next(0, randomUrls.Length)];
                post.User.Name = "Alvin Resende";
                post.Description = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.";
                post.Date = DateTime.Now;
                //post.Hashtags = "#PUBG #COD #FIFA20",
                //post.Comments = 788,
                //post.Likes = 1569,
                //post.Place = "Ribeirão Preto - BR",
                //post.Participants = "@resenboy",
                //post.Activity = "Gamer Full",
                //post.ProfileType = ProfileType.Gamer

                UserPosts.Add(post);
                contadorTeste++;
            }
        }
    }
}
