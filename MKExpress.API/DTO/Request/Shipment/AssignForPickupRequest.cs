using MKExpress.API.DTO.Base;

namespace MKExpress.API.DTO.Request
{
    public class AssignForPickupRequest : BaseRequest
    {
        public Guid MemberId { get; set; }
        public Guid ShipmentId { get; set; }
    }
}
