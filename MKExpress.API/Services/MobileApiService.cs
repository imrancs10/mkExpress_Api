using AutoMapper;
using MKExpress.API.Contants;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Enums;
using MKExpress.API.Repository;

namespace MKExpress.API.Services
{
    public class MobileApiService : IMobileApiService
    {
        private readonly IMobileApiRepository _mobileApiRepository;
        private readonly IMapper _mapper;
        public MobileApiService(IMobileApiRepository mobileApiRepository,IMapper mapper)
        {
            _mapper = mapper;
            _mobileApiRepository = mobileApiRepository;
        }
        public async Task<List<ShipmentResponse>> GetShipmentByMember(ShipmentStatusEnum shipmentStatus)
        {
           return _mapper.Map<List<ShipmentResponse>>(await _mobileApiRepository.GetShipmentByMember(shipmentStatus));
        }

        public async Task<bool> MarkPickupDone(Guid shipmentId)
        {
          return  await _mobileApiRepository.MarkPickupDone(shipmentId);
        }

        public async Task<bool> MarkPickupFailed(MarkPickupStatusRequest request)
        {
            return await _mobileApiRepository.MarkPickupFailed(request);
        }

        public async Task<bool> MarkPickupReschedule(MarkPickupStatusRequest request)
        {            
            return await _mobileApiRepository.MarkPickupReschedule(request);
        }

        public async Task<bool> MarkReadyForPickup(Guid shipmentId)
        {
            return await _mobileApiRepository.MarkReadyForPickup(shipmentId);
        }
    }
}
