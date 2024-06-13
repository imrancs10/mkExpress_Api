﻿using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class ShipmentDetail:BaseModel
    {
        public Guid ShipmentId { get; set; }
        public Guid FromStoreId { get; set; }
        public Guid ToStoreId { get; set; }
        public string? ShipperName { get; set; }
        public string? ShipperEmail { get; set; }
        public string? ShipperPhone { get; set; }
        public string? ShipperSecondPhone { get; set; }
        public string? ShipperAddress1 { get; set; }
        public string? ShipperAddress2 { get; set; }
        public string? ShipperAddress3 { get; set; }
        public Guid ShipperCityId { get; set; }
        public string? ConsigneeName { get; set; }
        public string? ConsigneeEmail { get; set; }
        public string? ConsigneePhone { get; set; }
        public string? ConsigneeSecondPhone { get; set; }
        public string? ConsigneeAddress1 { get; set; }
        public string? ConsigneeAddress2 { get; set; }
        public string? ConsigneeAddress3 { get; set; }
        public Guid ConsigneeCityId { get; set; }

        public decimal? Weight { get; set; }
        public int? TotalPieces { get; set; }
        public string? Dimension { get; set; }
        public string? Description { get; set; }

        [ForeignKey("ShipmentId")]
        public Shipment? Shipment { get; set; }

        [ForeignKey("FromStoreId")]
        public MasterData? FromStore { get; set; }
        [ForeignKey("ToStoreId")]
        public MasterData? ToStore { get; set; }
        [ForeignKey("ConsigneeCityId")]
        public MasterData? ConsigneeCity { get; set; }
        [ForeignKey("ShipperCityId")]
        public MasterData? ShipperCity { get; set; }
    }

}
