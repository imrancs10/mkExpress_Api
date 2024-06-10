using DocumentFormat.OpenXml.Bibliography;
using Microsoft.AspNetCore.Http.HttpResults;
using MKExpress.API.DTO.BaseDto;
using System.Diagnostics.Metrics;

namespace MKExpress.API.DTO.Request
{
    public class SearchShipmentRequest: BasePagingRequest
    {
        public Guid? CustomerId { get; set; }
        public string? Reason { get; set; }
        public string? Status { get; set; }
        public string? Courier { get; set; }
        public Guid? StationId { get; set; }
        public Guid? ConsigneeCityId { get; set; }
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
        public DateTime? DeliveredFrom { get; set; }
        public DateTime? DeliveredTo { get; set; }
        public DateTime? ReceivedFrom { get; set; }
        public DateTime? ReceivedTo { get; set; }
        public DateTime? CodDateFrom { get; set; }
        public DateTime? CodDateTo { get; set; }
        public DateTime? ReturnFrom { get; set; }
        public DateTime? ReturnTo { get; set; }
    }
}
