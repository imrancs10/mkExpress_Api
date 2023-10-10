using MKExpress.API.DTO.Base;

namespace MKExpress.API.DTO.Response
{
    public class ContainerDetailResponse:BaseResponse
    {
        public Guid ContainerId { get; set; }
        public Guid ShipmentId { get; set; }
    }
}
