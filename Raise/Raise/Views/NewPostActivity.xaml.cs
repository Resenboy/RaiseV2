using Octane.Xamarin.Forms.VideoPlayer;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Raise.ViewModels;
using System;
using System.Diagnostics;
using System.Reactive.Subjects;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Raise.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewPostActivity : ContentPage
    {
        MediaFile file;
        public NewPostActivity()
        {
            InitializeComponent();
            BindingContext = new NewPostActivityViewModel();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }

        //private async void TakePhoto_TapGesture(object sender, EventArgs e)
        //{
        //    await CrossMedia.Current.Initialize();
        //    try
        //    {
        //        var a = CrossMedia.Current;

        //        if (a.IsCameraAvailable && a.IsTakePhotoSupported)
        //        {
        //            file = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
        //            {

        //                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
        //                AllowCropping = true
        //            });
        //        }

        //        if (file == null)
        //            return;

        //        //PostImage.Source = VideoSource.FromStream(() => file.GetStream(), "mp4");
        //        //await StoreImages(file.GetStream());
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex.Message);
        //    }
        //}

        //private async void LoadPhoto_TapGesture(object sender, EventArgs e)
        //{
        //    await CrossMedia.Current.Initialize();
        //    try
        //    {
        //        var a = CrossMedia.Current;

        //        if (a.IsPickPhotoSupported)
        //        {
        //            file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
        //            {
        //                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
        //                RotateImage = true
        //            });
        //        }

        //        if (file == null)
        //            return;

        //        //PostImage.Source = VideoSource.FromStream(() => file.GetStream(), "mp4");
        //        //await StoreImages(file.GetStream());
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex.Message);
        //    }
        //}

        //private async void LoadVideo_TapGesture(object sender, EventArgs e)
        //{
        //    await CrossMedia.Current.Initialize();
        //    try
        //    {
        //        var a = CrossMedia.Current;

                
        //        if (a.IsCameraAvailable && a.IsPickVideoSupported)
        //        {
        //            file = await Plugin.Media.CrossMedia.Current.PickVideoAsync();
        //        }

        //        if (file == null)
        //            return;

        //        //PostImage.Source = VideoSource.FromStream(() => file.GetStream(), "mp4");
        //        //await StoreImages(file.GetStream());
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex.Message);
        //    }
        //}

        //private async void MakeVideo_TapGesture(object sender, EventArgs e)
        //{
        //    await CrossMedia.Current.Initialize();
        //    try
        //    {
        //        var a = CrossMedia.Current;

        //        if (a.IsCameraAvailable && a.IsTakeVideoSupported)
        //        {
        //            file = await Plugin.Media.CrossMedia.Current.TakeVideoAsync(new StoreVideoOptions
        //            {
        //                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
        //                AllowCropping = true
        //            });
        //        }

        //        if (file == null)
        //            return;

        //        //PostImage.Source = VideoSource.FromStream(() => file.GetStream(), "mp4");
        //        //await StoreImages(file.GetStream());
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex.Message);
        //    }
        //}
    }
}