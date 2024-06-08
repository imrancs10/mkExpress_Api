using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using System.Threading.Tasks;

namespace MKExpress.API.Services
{
    public interface ICrudService<TRequest, TResponse> where TRequest : class where TResponse : class
    {
        Task<TResponse> Add(TRequest request);
        Task<TResponse> Update(TRequest request);
        Task<int> Delete(Guid id);
        Task<TResponse> Get(Guid id);
        Task<PagingResponse<TResponse>> GetAll(PagingRequest pagingRequest);
        Task<PagingResponse<TResponse>> Search(SearchPagingRequest searchPagingRequest);
    }
}
