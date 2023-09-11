using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Request.Rents;
using MKExpress.API.Dto.Response;
using MKExpress.API.Dto.Response.Rents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Services.Interfaces
{
    public interface IRentLocationService:ICrudService<RentLocationRequest, RentLocationResponse>
    {

    }

    public interface IRentDetailService : ICrudService<RentDetailRequest, RentDetailResponse>
    {
        Task<List<RentTransactionResponse>> GetRentTransations(int rentDetailId = 0);
        Task<PagingResponse<RentTransactionResponse>> GetDueRents(bool isPaid, PagingRequest pagingRequest);
        Task<int> PayDeuRents(RentPayRequest rentPayRequest, int userId);
        Task<PagingResponse<RentTransactionResponse>> SearchDeuRents(bool isPaid, SearchPagingRequest pagingRequest);
    }
}
