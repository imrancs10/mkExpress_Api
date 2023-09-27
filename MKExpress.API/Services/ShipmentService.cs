using AutoMapper;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Models;
using MKExpress.API.Repository.IRepository;
using MKExpress.API.Services.IServices;

namespace MKExpress.API.Services
{
    public class ShipmentService : IShipmentService
    {
        private readonly IShipmentRepository _repo;
        private readonly IMapper _mapper;

        public ShipmentService(IShipmentRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }
        public async Task<ShipmentResponse> CreateShipment(ShipmentRequest request)
        {
            var shipment = _mapper.Map<Shipment>(request);
            return _mapper.Map<ShipmentResponse>(await _repo.CreateShipment(shipment));
        }
    }
}
