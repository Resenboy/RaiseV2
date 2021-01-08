namespace Raise.ViewModels
{
    public class ViewModelLocator_LoginSignup
    {
        private static readonly LoginSignupViewModel _loginSignupViewModel = new LoginSignupViewModel();

        public static LoginSignupViewModel LoginSignupViewModel
        {
            get
            {
                return _loginSignupViewModel;
            }
        }
    }
}
