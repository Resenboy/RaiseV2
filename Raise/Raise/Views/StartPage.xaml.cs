using Raise.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Raise.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        LoginSignupViewModel _viewModel;
        public StartPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = ViewModelLocator_LoginSignup.LoginSignupViewModel;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (sender is StackLayout stack)
            {
                if (stack == StackLogin)
                    CarouselPager.Position = 0;
                if (stack == StackSignup)
                    CarouselPager.Position = 1;
            }
        }
    }
}