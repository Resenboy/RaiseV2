using System;
using System.Security.Cryptography;
using System.Text;

namespace Raise.Utils
{
    public static class GuidGenerate
    {
        static string _email;
        public static string E_MAIL
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                USER_ID = SetGuid(_email);
            }
        }

        public static Guid USER_ID { get; private set; }

        public static Guid SetGuid(string key)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(key + "_RaIsE_KeY"));
            return new Guid(data);
        }
    }
}
