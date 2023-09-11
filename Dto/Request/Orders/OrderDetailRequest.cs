using MKExpress.API.Models.BaseModels;
using System;
using System.Collections.Generic;

namespace MKExpress.API.Dto.Request
{
    public class OrderDetailRequest : CustomerMeasurementBaseModel
    {
        public int? DesignSampleId { get; set; }
        public decimal Price { get; set; }
        public int OrderId { get; set; }
        public string OrderNo { get; set; }
        public string Crystal { get; set; }
        public decimal CrystalPrice { get; set; }
        public string Description { get; set; }
        public string WorkType { get; set; }
        public List<int> WorkTypes { get; set; }
        public string OrderStatus { get; set; }
        public string MeasurementStatus { get; set; }
        public string CustomerName { get; set; }
        public decimal SubTotalAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDeliveryDate { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
    }
}
