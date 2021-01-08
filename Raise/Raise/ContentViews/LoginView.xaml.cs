using Raise.ViewModels;
using Raise.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Raise.ContentViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentView
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = ViewModelLocator_LoginSignup.LoginSignupViewModel;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new FacebookLogin());
        }
    }
}