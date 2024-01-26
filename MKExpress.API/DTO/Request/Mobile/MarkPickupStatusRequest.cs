using Microsoft.AspNetCore.Http;

namespace MKExpress.API.DTO.Request
{
    public class MarkPickupStatusRequest
    {
        public Guid MemberId { get; set; }
        public Guid ShipmentId { get; set; }
        public string Reason { get; set; }
        public List<IFormFile> PodImages { get; set; }
    }
}
