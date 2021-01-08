using System;
using System.Text;

namespace Raise.Firebase.Utils
{
    public static class Crypto
    {
        private const string _KEY = "_RaIse_Pa$$_uRL@";

        public static string Encryption(string value)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}{1}", value, _KEY)));
        }

        public static string Dencryption(string value)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(value)).Replace(_KEY, "");
        }
    }
}
