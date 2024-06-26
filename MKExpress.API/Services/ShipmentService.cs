﻿using AutoMapper;
using MKExpress.API.Contants;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Enums;
using MKExpress.API.Exceptions;
using MKExpress.API.Extension;
using MKExpress.API.Models;
using MKExpress.API.Repository;

namespace MKExpress.API.Services
{
    public class ShipmentService : IShipmentService
    {
        private readonly IShipmentRepository _repo;
        private readonly IMapper _mapper;
        private readonly IShipmentTrackingRepository _shipmentTrackingRepository;
        private readonly ICommonService _commonService;

        public ShipmentService(IShipmentRepository repo,
            IMapper mapper,
            IShipmentTrackingRepository shipmentTrackingRepository,
            ICommonService commonService)
        {
            _mapper = mapper;
            _repo = repo;
            _shipmentTrackingRepository = shipmentTrackingRepository;
            _commonService = commonService;
        }

        public async Task<bool> AssignForPickup(List<AssignForPickupRequest> requests)
        {
            var req = _mapper.Map<List<AssignShipmentMember>>(requests);
            return await _repo.AssignForPickup(req);
        }
        public async Task<bool> AssignForDelivery(List<AssignForPickupRequest> requests)
        {
            var req = _mapper.Map<List<AssignShipmentMember>>(requests);
            return await _repo.AssignForDelivery(req);
        }

        public async Task<ShipmentResponse> CreateShipment(ShipmentRequest request)
        {
            var shipment = _mapper.Map<Shipment>(request);

            if (await _repo.IsShipmentExists(shipment.ShipmentNumber))
                throw new BusinessRuleViolationException(StaticValues.Error_ShipmentNumberAlreadyExist, string.Format(StaticValues.Message_ShipmentNumberAlreadyExist, shipment.ShipmentNumber));

            return _mapper.Map<ShipmentResponse>(await _repo.CreateShipment(shipment));
        }

        public async Task<PagingResponse<ShipmentResponse>> GetAllShipment(PagingRequest pagingRequest)
        {
            var data = _mapper.Map<PagingResponse<ShipmentResponse>>(await _repo.GetAllShipment(pagingRequest));
            return data;
        }

        public async Task<ShipmentResponse> GetShipment(Guid id)
        {
            return _mapper.Map<ShipmentResponse>(await _repo.GetShipment(id));
        }

        public async Task<List<ShipmentResponse>> GetShipment(string ids)
        {
            try
            {
                var guids = ids.Split(",").Select(x => Guid.Parse(x)).ToList();
                return _mapper.Map<List<ShipmentResponse>>(await _repo.GetShipment(guids));
            }
            catch (ArgumentNullException ane)
            {
                throw new BusinessRuleViolationException(StaticValues.ErrorType_InvalidParameters, StaticValues.Error_InvalidParameters, ane);
            }
            catch (FormatException fe)
            {
                throw new BusinessRuleViolationException(StaticValues.ErrorType_InvalidGUID, StaticValues.Error_InvalidGUID, fe);
            }
        }

        public async Task<List<ShipmentTrackingResponse>> GetTrackingByShipmentId(Guid shipmentId)
        {
            return _mapper.Map<List<ShipmentTrackingResponse>>(await _shipmentTrackingRepository.GetTrackingByShipmentId(shipmentId));
        }

        public async Task<ShipmentValidateResponse> ValidateContainerShipment(List<string> shipmentNo, Guid containerJourneyId)
        {
            var res = await _repo.ValidateShipment(shipmentNo);
            // if (res == null)
            //     throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);
            ShipmentValidateResponse shipmentValidateResponse = new();
            List<ShipmentErrorResponse> shipmentErrors = new();

            if (res.Count == 0)

                shipmentErrors.Add(new ShipmentErrorResponse()
                {
                    ShipmentNo = shipmentNo.FirstOrDefault() ?? "",
                    IsValid = false,
                    Error = StaticValues.Message_ShipmentNumberNotFound
                });

            // var containerJourney = await _masterJourneyService.Get(containerJourneyId);

            shipmentValidateResponse.Shipments = _mapper.Map<List<ShipmentResponse>>(res);
            foreach (Shipment shipment in res)
            {
                //if (shipment.Status.ToLower() != ShipmentStatusEnum.Stored.ToString().ToLower())
                //{
                //    shipmentErrors.Add(new ShipmentErrorResponse()
                //    {
                //        ShipmentNo = shipment.ShipmentNumber,
                //        IsValid = false,
                //        Error = StaticValues.Error_ShipmentStatusShouldBeStored
                //    });
                //}
                //else if(shipment.ShipmentDetail.ShipperCityId!=containerJourney.FromStationId)
                //    shipmentErrors.Add(new ShipmentErrorResponse()
                //    {
                //        ShipmentNo = shipment.ShipmentNumber,
                //        IsValid = false,
                //        Error = StaticValues.Error_ShipmentStatusShouldBeStored
                //    });
                //else if (shipment.ShipmentDetail.ConsigneeCityId != containerJourney.ToStationId)
                //    shipmentErrors.Add(new ShipmentErrorResponse()
                //    {
                //        ShipmentNo = shipment.ShipmentNumber,
                //        IsValid = false,
                //        Error = StaticValues.Error_ShipmentStatusShouldBeStored
                //    });
                //else
                shipmentErrors.Add(new ShipmentErrorResponse()
                {
                    ShipmentNo = shipment.ShipmentNumber,
                    IsValid = true,
                    Error = string.Empty
                });

            }
            shipmentValidateResponse.Errors = shipmentErrors;
            return shipmentValidateResponse;
        }

        public async Task<ShipmentResponse> ValidateThirdPartyShipment(string shipmentNo)
        {
            if (string.IsNullOrEmpty(shipmentNo))
                throw new BusinessRuleViolationException(StaticValues.Error_InvalidShipmentNo, StaticValues.Message_InvalidShipmentNo);

            var shipments = await _repo.ValidateShipment(new List<string>() { shipmentNo });

            if (shipments == null || shipments.Count == 0)
                throw new BusinessRuleViolationException(StaticValues.Error_ShipmentNumberNotFound, StaticValues.Message_ShipmentNumberNotFound);

            var shipment = shipments.FirstOrDefault();
            bool isValidStatus = Enum.TryParse(shipment?.Status, out ShipmentStatusEnum currentStatus);

            if (!isValidStatus)
                throw new BusinessRuleViolationException(StaticValues.Error_InvalidShipmentStatus, StaticValues.Message_InvalidShipmentStatus);

            if (!_commonService.ValidateThirdPartyShipmentStatus(currentStatus))
                throw new BusinessRuleViolationException(StaticValues.Error_InvalidShipmentStatusForThirdParty, StaticValues.Message_InvalidShipmentStatusForThirdParty);

            return _mapper.Map<ShipmentResponse>(shipment);
        }

        //Mobile API

        //Deliver API
        //Get Assigned Shipment
        public async Task<List<ShipmentResponse>> GetShipments(string userId, ShipmentStatusEnum shipment, ShipmentStatusEnum shipmentStatus)
        {
            try
            {
                return _mapper.Map<List<ShipmentResponse>>(await _repo.GetShipmentByUser(userId, shipment, shipmentStatus));
            }
            catch (ArgumentNullException ane)
            {
                throw new BusinessRuleViolationException(StaticValues.ErrorType_InvalidParameters, StaticValues.Error_InvalidParameters, ane);
            }
            catch (FormatException fe)
            {
                throw new BusinessRuleViolationException(StaticValues.ErrorType_InvalidGUID, StaticValues.Error_InvalidGUID, fe);
            }
        }

        public async Task<ShipmentResponse?> ValidateShipmentStatus(string shipmentNo, string status)
        {
            if (!Enum.TryParse(status.Replace(" ", ""), true, out ShipmentStatusEnum result))
                return null;
            var res = await _repo.ValidateShipmentStatus(shipmentNo, result) ?? throw new BusinessRuleViolationException(StaticValues.ErrorType_RecordNotFound, StaticValues.Error_RecordNotFound);

            if (res.Status == result.ToFormatString())
                return _mapper.Map<ShipmentResponse?>(res);
            else
                throw new BusinessRuleViolationException(StaticValues.Error_InvalidShipmentStatus, StaticValues.Message_InvalidShipmentStatus);
        }

        public async Task<bool> HoldShipment(List<Guid> requests)
        {
            if(requests.Count<1)
                throw new BusinessRuleViolationException(StaticValues.ErrorType_InvalidParameters, StaticValues.Error_InvalidParameters);
            return await _repo.HoldShipment(requests);
        }

        public async Task<PagingResponse<ShipmentResponse>> SearchShipment(SearchShipmentRequest requests)
        {
            if (requests.CreatedFrom != null && requests.CreatedTo == null)
            {
                requests.CreatedTo = DateTime.Now.Date;
            }
            if (requests.CreatedFrom == null)
                requests.CreatedTo = null;

            if (requests.CodDateFrom != null && requests.CodDateTo == null)
            {
                requests.CodDateTo = DateTime.Now.Date;
            }
            if (requests.CodDateFrom == null)
                requests.CodDateTo = null;

            if (requests.ReceivedFrom != null && requests.ReceivedTo == null)
            {
                requests.ReceivedTo = DateTime.Now.Date;
            }
            if (requests.ReceivedFrom == null)
                requests.ReceivedTo = null;

            if (requests.DeliveredFrom != null && requests.DeliveredTo == null)
            {
                requests.DeliveredTo = DateTime.Now.Date;
            }
            if (requests.DeliveredFrom == null)
                requests.DeliveredTo = null;

            if (requests.ReturnFrom != null && requests.ReturnTo == null)
            {
                requests.ReturnTo = DateTime.Now.Date;
            }
            if (requests.ReturnFrom == null)
                requests.ReturnTo = null;
            return _mapper.Map<PagingResponse<ShipmentResponse>>(await _repo.SearchShipment(requests));
        }
    }
}
