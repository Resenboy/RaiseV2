using Raise.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace Raise.Utils
{
    public class RecoveryImageParms
    {
        public ImageSourceType ImageSourceType { get; set; }
        public string File { get; set; }
        public string Resource { get; set; }
        public Stream Stream { get; set; }
        public Uri Uri { get; set; }
    }

    public class MediaProvider
    {
        public ImageSource GetImage(RecoveryImageParms recoveryImageParms)
        {
            //if (_globalImage == null)
            //    return null;

            //if (Account.ProfileImg == null)
            //    Account.ProfileImg = new Image();

            ImageSource imageSource = null;

            switch (recoveryImageParms.ImageSourceType)
            {
                case ImageSourceType.FromFile:
                    imageSource = ImageSource.FromFile(recoveryImageParms.File);
                    break;
                case ImageSourceType.FromResource:
                    imageSource = ImageSource.FromResource(recoveryImageParms.Resource);
                    break;
                case ImageSourceType.FromStream:
                    imageSource = ImageSource.FromStream(() => recoveryImageParms.Stream);
                    break;
                case ImageSourceType.FromURL:
                    imageSource = ImageSource.FromUri(recoveryImageParms.Uri);
                    break;
                default:
                    break;
            }
            return imageSource;
        }
    }
}
