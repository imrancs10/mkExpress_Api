using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Request.Rents;
using MKExpress.API.Dto.Response;
using MKExpress.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories.Interfaces
{
    public interface IRentDetailRepository:ICrudRepository<RentDetail>
    {
        Task<List<RentTransation>> GetRentTransations(int rentDetails=0);
        Task<PagingResponse<RentTransation>> GetDueRents(bool isPaid,PagingRequest pagingRequest);
        Task<int> PayDeuRents(RentPayRequest rentPayRequest, int paidBy);
        Task<PagingResponse<RentTransation>> SearchDeuRents(bool isPaid, SearchPagingRequest pagingRequest);
    }
}
