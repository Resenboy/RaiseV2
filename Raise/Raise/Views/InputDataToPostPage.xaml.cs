using Octane.Xamarin.Forms.VideoPlayer;
using Raise.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Raise.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InputDataToPostPage : ContentPage
    {
        private NewPostActivityViewModel newPostActivityViewModel;
        public InputDataToPostPage()
        {
            InitializeComponent();
        }
        public InputDataToPostPage(NewPostActivityViewModel newPostActivityView) : this()
        {
            BindingContext = newPostActivityViewModel = newPostActivityView;

            if (newPostActivityViewModel.IsPhoto)
            {
                ReceivedFileImage.Source = ImageSource.FromStream(() => newPostActivityViewModel.FileStream);
                ReceivedFileVideo.IsVisible = false;
            }
            else
            {
                ReceivedFileVideo.Source = VideoSource.FromStream(() => newPostActivityViewModel.FileStream, "MP3");
                ReceivedFileImage.IsVisible = false;
            }
        }

        private void PinchGestureRecognizer_PinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {

        }

        private void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
        {

        }
    }
}