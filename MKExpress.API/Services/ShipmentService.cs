using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using MKExpress.API.Contants;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Exceptions;
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
        private readonly IMasterJourneyService _masterJourneyService;

        public ShipmentService(IShipmentRepository repo, IMapper mapper, IShipmentTrackingRepository shipmentTrackingRepository, IMasterJourneyService masterJourneyService)
        {
            _mapper = mapper;
            _repo = repo;
            _shipmentTrackingRepository = shipmentTrackingRepository;
            _masterJourneyService = masterJourneyService;
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
           // if (res == null || res.Count == 0)
           //     throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);

           // var containerJourney = await _masterJourneyService.Get(containerJourneyId);
            ShipmentValidateResponse shipmentValidateResponse = new();
            List<ShipmentErrorResponse> shipmentErrors = new();
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
    }
}
