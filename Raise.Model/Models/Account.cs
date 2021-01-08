using System;
using Raise.Enums;
using Xamarin.Forms;

namespace Raise.Model.Models
{
    /// <summary>
    /// [TCO_ACCOUN] - Conta de usuário
    /// </summary>
    public class Account : BaseModel
    {
        /// <summary>
        /// [ACC_IDENTI]
        /// </summary>
        public long AccountIdenti { get; set; }

        /// <summary>
        /// [ACC_USU_IDENTI]
        /// </summary>
        public long UserIdenti { get; set; }

        /// <summary>
        /// [ACC_CITY]
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// [ACC_STATE]
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// [ACC_BTHDAY]
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// [ACC_INIDAT]
        /// </summary>
        public DateTime StartingDate { get; set; }

        /// <summary>
        /// [ACC_GAMER]
        /// </summary>
        private string gamerTag;
        public string GamerTag
        {
            get { return gamerTag; }
            set
            {
                gamerTag = value;
                OnPropertyChanged();
            }
        }

        public byte[] ProfileImage { get; set; }

        private ImageSource profileImg;

        public ImageSource ProfileImg
        {
            get
            {
                return profileImg;
            }
            set
            {
                if (profileImg != value)
                {
                    profileImg = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// [ACC_IMAGE]
        /// </summary>
        public string UrlProfileImage { get; set; }

        private Games favorityGame;

        public Games FavorityGame
        {
            get
            {
                return favorityGame;
            }
            set
            {
                favorityGame = value;
                OnPropertyChanged("FavorityGame");
            }
        }

        /// <summary>
        /// [ACC_USU_IDENTI]
        /// </summary>
        User _user;
        public virtual User User
        {
            get
            {
                if (_user == null)
                    _user = new User();
                return _user;
            }
            set
            {
                _user = value;
            }
        }
    }
}
