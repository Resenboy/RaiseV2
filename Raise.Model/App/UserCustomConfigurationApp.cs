using SQLite;

namespace Raise.Model.App
{
    public class UserCustomConfigurationApp
    {
        [PrimaryKey]
        public string GuidKey { get; set; }
        public bool IsLoggedIn { get; set; }
        public bool IsFirstAccess { get; set; }
        public bool RememberMe { get; set; }

        public string PrimaryColorHex { get; set; }
        public string SecondaryColorHex { get; set; }

        public string LastUserNameOnThisPhone { get; set; }
        public string LastPasswordOnThisPhone { get; set; }
    }
}
