using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories.Interfaces
{
    public interface ICrudRepository<T> where T : class
    {
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<int> Delete(int Id);
        Task<T> Get(int Id);
        Task<PagingResponse<T>> GetAll(PagingRequest pagingRequest);
        Task<PagingResponse<T>> Search(SearchPagingRequest searchPagingRequest);
    }
}
