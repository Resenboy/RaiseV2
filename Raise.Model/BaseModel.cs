using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Raise.Model.Models
{
    public class BaseModel : INotifyPropertyChanged
    {
        /// <summary>
        /// [USR_GUID]
        /// </summary>
        public Guid GuidKey { get; set; }

        /// <summary>
        /// [XXX_CREDAT]
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// [XXX_UPDDAT]
        /// </summary>
        public DateTime? UpdateDate { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
