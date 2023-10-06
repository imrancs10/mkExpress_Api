using MKExpress.API.DTO.Base;

namespace MKExpress.API.DTO.Request
{
    public class ContainerDetailRequest:BaseRequest
    {
        public Guid ContainerId { get; set; }
        public Guid ShipmentId { get; set; }
    }
}
