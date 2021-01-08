using Raise.Utils;
using System.Collections.Generic;

namespace Raise.Model.Models
{
    /// <summary>
    /// [TCO_USER] - Usuário
    /// </summary>
    public class User : BaseModel
    {
        public User()
        {
            Posts = new HashSet<Post>();
            Feed = new HashSet<Feed>();
        }

        /// <summary>
        /// [USU_IDENTI]
        /// </summary>
        public long UserIdenti { get; set; }

        string _email;
        /// <summary>
        /// [USR_EMAIL]
        /// </summary>
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                GuidGenerate.E_MAIL = _email;
                GuidKey = GuidGenerate.USER_ID;
            }
        }

        string _password;
        /// <summary>
        /// [USR_PASWRD]
        /// </summary>
        public string Password
        {
            get
            {
                if (!PasswordGenerate.IsCryption(_password))
                    return string.IsNullOrEmpty(_password) ? "" : PasswordGenerate.Encryption(_password);
                else
                    return _password;
            }
            set
            {
                _password = value;
            }
        }

        /// <summary>
        /// [USR_NAME]
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// [USR_ACPTER]
        /// </summary>
        public bool AcceptedTerms { get; set; }

        /// <summary>
        /// [USR_FCBUSR]
        /// </summary>
        public bool IsFacebookUser { get; set; }

        /// <summary>
        /// [USR_REMEMB]
        /// </summary>
        public bool RememberMe { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<Feed> Feed { get; set; }
    }
}
