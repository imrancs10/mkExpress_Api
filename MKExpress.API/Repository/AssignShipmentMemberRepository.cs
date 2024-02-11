using Microsoft.EntityFrameworkCore;
using MKExpress.API.Data;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Models;
using MKExpress.API.Repository.IRepository;

namespace MKExpress.API.Repository
{
    public class AssignShipmentMemberRepository : IAssignShipmentMemberRepository
    {
        private readonly MKExpressContext _context;

        public AssignShipmentMemberRepository(MKExpressContext context)
        {
            _context = context;
        }
        public async Task<PagingResponse<AssignShipmentMember>> GetCourierRunsheet(PagingRequest pagingRequest, Guid memberId)
        {
            try
            {
                var data = _context.AssignShipmentMembers
                       .Include(x => x.AssignBy)
                       .Include(x => x.Member)
                       .Include(x => x.Shipment)
                       .ThenInclude(x => x.Customer)
                       .Include(x => x.Shipment)
                       .ThenInclude(x => x.ShipmentDetail)
                       .ThenInclude(x => x.ShipperCity)
                       .Include(x => x.Shipment)
                       .ThenInclude(x => x.ShipmentDetail)
                       .ThenInclude(x => x.ConsigneeCity)
                       .Where(x => !x.IsDeleted && x.Shipment.ShipmentDetail != null)
                       .OrderByDescending(x => x.CreatedAt)
                       .AsQueryable();

                PagingResponse<AssignShipmentMember> response = new()
                {
                    PageNo = pagingRequest.PageNo,
                    PageSize = pagingRequest.PageSize,
                    Data = await data.Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1))
                    .Take(pagingRequest.PageSize)
                    .ToListAsync(),
                    TotalRecords = await data.CountAsync()
                };
                return response;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
