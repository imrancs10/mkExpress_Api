﻿using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
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
        private readonly IShipmentTrackingRepository _shipmentTrackingRepository;

        public ShipmentService(IShipmentRepository repo, IMapper mapper, IShipmentTrackingRepository shipmentTrackingRepository)
        {
            _mapper = mapper;
            _repo = repo;
            _shipmentTrackingRepository = shipmentTrackingRepository;
        }
        public async Task<ShipmentResponse> CreateShipment(ShipmentRequest request)
        {
            var shipment = _mapper.Map<Shipment>(request);
            return _mapper.Map<ShipmentResponse>(await _repo.CreateShipment(shipment));
        }

        public async Task<PagingResponse<ShipmentResponse>> GetAllShipment(PagingRequest pagingRequest)
        {
            return _mapper.Map<PagingResponse<ShipmentResponse>>(await _repo.GetAllShipment(pagingRequest));
        }

        public async Task<ShipmentResponse> GetShipment(Guid id)
        {
            return _mapper.Map<ShipmentResponse>(await _repo.GetShipment(id));
        }

        public async Task<List<ShipmentTrackingResponse>> GetTrackingByShipmentId(Guid shipmentId)
        {
            return _mapper.Map<List<ShipmentTrackingResponse>>(await _shipmentTrackingRepository.GetTrackingByShipmentId(shipmentId));
        }
    }
}
