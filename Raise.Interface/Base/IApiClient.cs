using System.Collections.Generic;
using Raise.Model.Models;
using Raise.Utils;

namespace Raise.Interface.Base
{
    public interface IApiClient<T> where T : BaseModel
    {
        ApiResponse<T> GetAll();
        ApiResponse<T> GetByObj(T obj);
        ApiResponse<T> Add(T obj);
        ApiResponse<T> Update(T obj);
        ApiResponse<T> Delete(long id);
    }
}
