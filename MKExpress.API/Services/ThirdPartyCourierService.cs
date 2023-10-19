using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Models;
using MKExpress.API.Repository.IRepository;
using MKExpress.API.Services.IServices;

namespace MKExpress.API.Services
{
    public class ThirdPartyCourierService : IThirdPartyCourierService
    {
        private readonly IThirdPartyCourierRepository _repository;
        private readonly IMapper _mapper;
        public ThirdPartyCourierService(IThirdPartyCourierRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ThirdPartyCourierResponse> Add(ThirdPartyCourierCompanyRequest thirdPartyCourierCompanyRequest)
        {
            var request = _mapper.Map<ThirdPartyCourierCompany>(thirdPartyCourierCompanyRequest);
            return _mapper.Map<ThirdPartyCourierResponse>(await _repository.Add(request));
        }

        public async Task<int> Delete(Guid id)
        {
           return await _repository.Delete(id);
        }

        public async Task<ThirdPartyCourierResponse> Get(Guid id)
        {
            return _mapper.Map<ThirdPartyCourierResponse>(await _repository.Get(id));
        }

        public async Task<PagingResponse<ThirdPartyCourierResponse>> GetAll(PagingRequest pagingRequest)
        {
            return _mapper.Map<PagingResponse<ThirdPartyCourierResponse>>(await _repository.GetAll(pagingRequest));
        }

        public async Task<List<ShipmentResponse>> GetShipments(Guid thirdPartyId)
        {
            return _mapper.Map<List<ShipmentResponse>>(await _repository.GetShipments(thirdPartyId));
        }

        public async Task<PagingResponse<ThirdPartyCourierResponse>> Search(SearchPagingRequest pagingRequest)
        {
            return _mapper.Map<PagingResponse<ThirdPartyCourierResponse>>(await _repository.Search(pagingRequest));
        }

        public async Task<ThirdPartyCourierResponse> Update(ThirdPartyCourierCompanyRequest thirdPartyCourierCompanyRequest)
        {
            var request = _mapper.Map<ThirdPartyCourierCompany>(thirdPartyCourierCompanyRequest);
            return _mapper.Map<ThirdPartyCourierResponse>(await _repository.Update(request));
        }
    }
}
