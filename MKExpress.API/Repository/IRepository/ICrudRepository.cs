using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories.Interfaces
{
    public interface ICrudRepository<T> where T : class
    {
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<int> Delete(Guid Id);
        Task<T> Get(Guid Id);
        Task<PagingResponse<T>> GetAll(PagingRequest pagingRequest);
        Task<PagingResponse<T>> Search(SearchPagingRequest searchPagingRequest);
    }
}
