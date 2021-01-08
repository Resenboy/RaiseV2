using Raise.ContentViews;
using Raise.Enums;
using Raise.Model.App;
using Xamarin.Forms;

namespace Raise.Templates
{
    public class LoginSignupDataTemplateSelector : DataTemplateSelector
    {
        private readonly DataTemplate _login;
        private readonly DataTemplate _signup;

        public LoginSignupDataTemplateSelector()
        {
            _login = new DataTemplate(typeof(LoginView));
            _signup = new DataTemplate(typeof(SignupView));
        }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is InitialTypePage startPage)
            {
                if (startPage.Type == StartPageType.Login)
                    return _login;
                if (startPage.Type == StartPageType.Signup)
                    return _signup;
            }
            return new DataTemplate(typeof(ContentView));
        }
    }
}
