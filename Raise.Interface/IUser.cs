using Raise.Interface.Base;
using Raise.Model.Models;
using Raise.Utils;

namespace Raise.Interface
{
    public interface IUser
    {
        ApiResponse<User> Login(User user);
    }
}
