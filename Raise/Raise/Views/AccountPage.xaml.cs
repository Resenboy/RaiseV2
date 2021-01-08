using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Raise.ViewModels;

namespace Raise.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountPage : ContentPage
    {
        AccountViewModel _accountViewModel;
        public AccountPage()
        {
            InitializeComponent();
        }

        public AccountPage(string facebookProfile) 
            : this()
        {
            BindingContext = _accountViewModel = new AccountViewModel(facebookProfile);
        }
    }
}