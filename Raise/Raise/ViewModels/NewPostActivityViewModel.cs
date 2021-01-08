using Octane.Xamarin.Forms.VideoPlayer;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Raise.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Raise.ViewModels
{
    public class NewPostActivityViewModel : BaseViewModel
    {
        public MediaFile mediaFile;
        public Stream FileStream { get; set; }

        public bool IsPhoto { get; set; }

        //Commands
        public ICommand PickPhotoCommand { get; set; }
        public ICommand PickVideoCommand { get; set; }
        public ICommand TakePhotoCommand { get; set; }
        public ICommand MakeVideoCommand { get; set; }

        public NewPostActivityViewModel()
        {
            PickPhotoCommand = new Command(() => PickPhotoAction());
            TakePhotoCommand = new Command(() => TakePhotoAction());
            PickVideoCommand = new Command(() => PickVideoAction());
            MakeVideoCommand = new Command(() => MakeVideoAction());
        }

        private async void PickPhotoAction()
        {
            await CrossMedia.Current.Initialize();
            try
            {
                var a = CrossMedia.Current;

                if (a.IsPickPhotoSupported)
                {
                    mediaFile = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                    {
                        PhotoSize = PhotoSize.Medium,
                        RotateImage = true
                    });
                }

                if (mediaFile == null)
                    return;

                FileStream = mediaFile.GetStream();
                IsPhoto = true;
                //await StoreImages(file.GetStream());
                GoToNextPage();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async void TakePhotoAction()
        {
            await CrossMedia.Current.Initialize();
            try
            {
                var a = CrossMedia.Current;

                if (a.IsCameraAvailable && a.IsTakePhotoSupported)
                {
                    mediaFile = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                    {

                        PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                        AllowCropping = true
                    });
                }

                if (mediaFile == null)
                    return;

                FileStream = mediaFile.GetStream();
                IsPhoto = true;
                //PostImage.Source = VideoSource.FromStream(() => file.GetStream(), "mp4");
                //await StoreImages(file.GetStream());
                GoToNextPage();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async void PickVideoAction()
        {
            await CrossMedia.Current.Initialize();
            try
            {
                var a = CrossMedia.Current;


                if (a.IsCameraAvailable && a.IsPickVideoSupported)
                {
                    mediaFile = await Plugin.Media.CrossMedia.Current.PickVideoAsync();
                }

                if (mediaFile == null)
                    return;

                FileStream = mediaFile.GetStream();
                IsPhoto = false;
                //PostImage.Source = VideoSource.FromStream(() => file.GetStream(), "mp4");
                //await StoreImages(file.GetStream());
                GoToNextPage();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async void MakeVideoAction()
        {
            await CrossMedia.Current.Initialize();
            try
            {
                var a = CrossMedia.Current;

                if (a.IsCameraAvailable && a.IsTakeVideoSupported)
                {
                    mediaFile = await Plugin.Media.CrossMedia.Current.TakeVideoAsync(new StoreVideoOptions
                    {
                        Directory = "Sample",
                        Name = Title + ".mp4"
                    });
                }

                await Task.Delay(3000);

                if (mediaFile == null)
                    return;

                FileStream = mediaFile.GetStream();
                IsPhoto = false;
                //PostImage.Source = VideoSource.FromStream(() => file.GetStream(), "mp4");
                //await StoreImages(file.GetStream());
                GoToNextPage();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void GoToNextPage()
        {
            Shell.Current.Navigation.PushAsync(new InputDataToPostPage(this));
        }
    }
}
