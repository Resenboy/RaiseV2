using Raise.Interface;
using Raise.Interface.Base;
using Raise.Model.Models;

namespace Raise.MobileAppService.Interfaces
{
    public interface IPostRepository : IRepositoryBase<Post>, IPost
    {
    }
}
