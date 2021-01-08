namespace Raise.Model.Models
{
    /// <summary>
    /// [TCO_FOLLOW] - Feed/Seguidores
    /// </summary>
    public class Feed : BaseModel
    {
        /// <summary>
        /// [FLW_IDENTI]
        /// </summary>
        public long FeedIdenti { get; set; }

        /// <summary>
        /// [FLW_USU_IDENTI]
        /// </summary>
        public long UserIdenti { get; set; }

        /// <summary>
        /// [FLW_USU_USU_IDENTI]
        /// </summary>
        public long FollowerUserIdenti { get; set; }

        User _user;
        /// <summary>
        /// [FLW_USU_IDENTI]
        /// </summary>
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

        User _followerUserPost;
        /// <summary>
        /// [FLW_USU_USU_IDENTI]
        /// </summary>
        public virtual User FollowerUserPost
        {
            get
            {
                if (_followerUserPost == null)
                    _followerUserPost = new User();
                return _followerUserPost;
            }
            set
            {
                _followerUserPost = value;
            }
        }
    }
}
