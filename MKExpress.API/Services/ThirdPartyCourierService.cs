using AutoMapper;
using MKExpress.API.Contants;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Exceptions;
using MKExpress.API.Middleware;
using MKExpress.API.Models;
using MKExpress.API.Repository;

namespace MKExpress.API.Services
{
    public class ThirdPartyCourierService : IThirdPartyCourierService
    {
        private readonly IThirdPartyCourierRepository _repository;
        private readonly IMapper _mapper;
        public ThirdPartyCourierService(IThirdPartyCourierRepository repository, IMapper mapper)
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

        public async Task<List<ShipmentResponse>> GetShipments(Guid thirdPartyId, DateTime fromDate, DateTime toDate)
        {
            return _mapper.Map<List<ShipmentResponse>>(await _repository.GetShipments(thirdPartyId, fromDate, toDate));
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
        public async Task<bool> AddShipmentToThirdParty(List<ThirdPartyShipmentRequest> request)
        {
            if (request == null)
                throw new BusinessRuleViolationException(StaticValues.ErrorType_InvalidParameters, StaticValues.Error_InvalidParameters);

            var shipmentIds = request.Select(x => x.ShipmentId).ToList();

            var alreadyAddedShipment = await _repository.IsShipmentAddedInThirdParty(shipmentIds);
            if (alreadyAddedShipment.Any())
            {
                throw new BusinessRuleViolationException(StaticValues.Error_SomeShipmentsAlreadyAddedToThirdParty,
                   "These shipment already added to third party courier : "+ string.Join(", ", alreadyAddedShipment.Select(x => x.Shipment.ShipmentNumber)));
            }

            request.ForEach(res =>
            {
                res.AssignById = JwtMiddleware.GetUserId();
                res.AssignAt = DateTime.UtcNow;
            });

            var thirdPartyShipment = _mapper.Map<List<ThirdPartyShipment>>(request);
            return await _repository.AddShipmentToThirdParty(thirdPartyShipment);
        }
    }
}
