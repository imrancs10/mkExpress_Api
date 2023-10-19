using MKExpress.API.DTO.Base;
using MKExpress.API.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.DTO.Response
{
    public class ThirdPartyShipmentResponse:BaseResponse
    {
        public Guid ThirdPartyCourierCompanyId { get; set; }
        public Guid ShipmentId { get; set; }
        public DateTime AssignAt { get; set; }
        public Guid AssignById { get; set; }
        public string? ThirdPartyShipmentNo { get; set; }
        public ThirdPartyCourierResponse ThirdPartyCourierCompany { get; set; }
        public ShipmentResponse Shipment { get; set; }
        public string AssignBy { get; set; }
    }
}
