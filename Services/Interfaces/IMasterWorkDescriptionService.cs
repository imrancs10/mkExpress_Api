using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Services.Interfaces
{
    public interface IMasterWorkDescriptionService:ICrudService<MasterDataTypeRequest, MasterDataTypeResponse>
    {
        Task<List<MasterDataTypeResponse>> GetByWorkTypes(string worktype);
        Task<int> SaveOrderWorkDescription(List<OrderWorkDescriptionRequest> orderWorkDescriptions);
        Task<List<OrderWorkDescriptionResponse>> GetOrderWorkDescription(int orderDetailId);
    }
}
