using Raise.Model.Models;
using Raise.Utils;
using System.Collections.Generic;

namespace Raise.Interface.Base
{
    public interface IRepositoryBase<T> where T : BaseModel
    {
        public ApiResponse<List<T>> GetAll();

        public ApiResponse<T> GetByObj(T obj);

        public ApiResponse<T> Add(T obj);

        public ApiResponse<T> Update(T obj);

        public ApiResponse<T> Delete(long id);

    }
}
