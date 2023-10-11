using MKExpress.API.DTO.Base;
using MKExpress.API.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.DTO.Response
{
    public class ContainerDetailResponse
    {
        public Guid ContainerId { get; set; }
        public Guid ShipmentId { get; set; }
        public ShipmentResponse Shipment { get; set; }
    }
}
