using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class ShipmentDetail:BaseModel
    {
        public int ShipmentId { get; set; }
        public int FromStoreId { get; set; }
        public int ToStoreId { get; set; }
        public string ShipperName { get; set; }
        public string? ShipperEmail { get; set; }
        public string ShipperPhone { get; set; }
        public string? ShipperSecondPhone { get; set; }
        public string ShipperAddress1 { get; set; }
        public string? ShipperAddress2 { get; set; }
        public string? ShipperAddress3 { get; set; }
        public int ShipperCityId { get; set; }
        public string ConsigneeName { get; set; }
        public string? ConsigneeEmail { get; set; }
        public string ConsigneePhone { get; set; }
        public string? ConsigneeSecondPhone { get; set; }
        public string ConsigneeAddress1 { get; set; }
        public string? ConsigneeAddress2 { get; set; }
        public string? ConsigneeAddress3 { get; set; }
        public int ConsigneeCityId { get; set; }

        public decimal Weight { get; set; }
        public int TotalPieces { get; set; }
        public decimal Dimension { get; set; }
        public decimal Description { get; set; }

        [ForeignKey("ShipmentId")]
        public Shipment Shipment { get; set; }
    }

}
