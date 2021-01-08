using System;

namespace Raise.Model.Models
{
    /// <summary>
    /// [TCO_POST] - Posts do usuário
    /// </summary>
    public class Post : BaseModel
    {
        /// <summary>
        /// [POS_IDENTI]
        /// </summary>
        public long PostIdenti { get; set; }

        /// <summary>
        /// [POS_USU_IDENTI]
        /// </summary>
        public long UserIdenti { get; set; }

        /// <summary>
        /// Guid para controle no firestorage
        /// </summary>
        public Guid PostGuid { get; set; }

        public byte[] PostImage { get; set; }

        /// <summary>
        /// [POS_IMAGE]
        /// </summary>
        public string UrlPostImage { get; set; }

        /// <summary>
        /// [POS_DATE]
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// [POS_DESCRI]
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// [POS_USU_IDENTI]
        /// </summary>
        public virtual User User { get; set; }

        //public string Hashtags { get; set; }

        //public string Activity { get; set; }

        //public string Participants { get; set; }

        //public ProfileType ProfileType { get; set; }

        //public long Likes { get; set; }

        //public long Comments { get; set; }

        //public string Place { get; set; }
    }
}
