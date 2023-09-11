using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using System.Threading.Tasks;

namespace MKExpress.API.Services.Interfaces
{
    public interface IProductService : ICrudService<ProductRequest, ProductResponse>
    {
        Task<int> GetInvoiceNo();
    }
}
