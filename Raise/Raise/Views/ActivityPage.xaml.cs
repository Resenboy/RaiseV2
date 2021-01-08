using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Raise.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivityPage : ContentPage
    {
        bool isTapped = false;
        public ActivityPage()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            if (isTapped)
            {
                ((Label)sender).MaxLines = 2;
                isTapped = false;
            }
            else
            {
                ((Label)sender).MaxLines = 100;
                isTapped = true;
            }
        }
    }
}