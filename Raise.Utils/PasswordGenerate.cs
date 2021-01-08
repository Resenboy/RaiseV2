using System;
using System.Text;

namespace Raise.Utils
{
    public static class PasswordGenerate
    {
        private const string _KEY = "_RaIse_Pa$$_";

        public static string Encryption(string password)
        {
            if (IsCryption(password))
                return password;

            return Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}{1}", password, _KEY)));
        }

        public static string Dencryption(string password)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(password)).Replace(_KEY, "");
        }

        public static bool IsCryption(string password) 
        {
            if (string.IsNullOrEmpty(password) || 
                password.Length % 4 != 0 || 
                password.Contains(" ") || 
                password.Contains("\t") || 
                password.Contains("\r") || 
                password.Contains("\n"))
                return false;

            try
            {
                Convert.FromBase64String(password);
                return true;
            }
            catch { }
            return false;
        }
    }
}
