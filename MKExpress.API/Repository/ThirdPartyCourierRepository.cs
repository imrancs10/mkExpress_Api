using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Models;
using MKExpress.API.Repository.IRepository;

namespace MKExpress.API.Repository
{
    public class ThirdPartyCourierRepository : IThirdPartyCourierRepository
    {
        public async Task<ThirdPartyCourierCompany> Add(ThirdPartyCourierCompany entity)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<ThirdPartyCourierCompany> Get(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<PagingResponse<ThirdPartyCourierCompany>> GetAll(PagingRequest pagingRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ThirdPartyShipment>> GetShipments(Guid thirdPartyId)
        {
            throw new NotImplementedException();
        }

        public async Task<PagingResponse<ThirdPartyCourierCompany>> Search(SearchPagingRequest searchPagingRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<ThirdPartyCourierCompany> Update(ThirdPartyCourierCompany entity)
        {
            throw new NotImplementedException();
        }
    }
}
