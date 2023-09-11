using MKExpress.API.Dto.BaseDto;
using System;
using System.Collections.Generic;

namespace MKExpress.API.Dto.Response
{
    public class OrderDetailResponse : BaseResponseModel
    {
        public int Id { get; set; }
        public int DesignSampleId { get; set; }
        public decimal Price { get; set; }
        public int OrderId { get; set; }
        public string OrderNo { get; set; }
        public string Size { get; set; }
        public string Chest { get; set; }
        public string SleeveLoose { get; set; }
        public string Deep { get; set; }
        public string BackDown { get; set; }
        public string Bottom { get; set; }
        public string Length { get; set; }
        public string Hipps { get; set; }
        public string Sleeve { get; set; }
        public string Shoulder { get; set; }
        public string Neck { get; set; }
        public string Waist { get; set; }
        public string Extra { get; set; }
        public string Cuff { get; set; }
        public string Crystal { get; set; }
        public decimal CrystalPrice { get; set; }
        public string Description { get; set; }
        public string WorkType { get; set; }
        public string OrderStatus { get; set; }
        public string MeasurementStatus { get; set; }
        public string DesignCategory { get; set; }
        public string DesignModel { get; set; }
        public bool IsCancelled { get; set; }
        public bool IsDeleted { get; set; }
        public decimal SubTotalAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDeliveryDate { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Note { get; set; }
        public List<string> WorkTypes { get; set; }
        public string CustomerName { get; set; }
        public string Contact { get; set; }
        public string MainOrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public string MeasurementCustomerName { get; set; }
        public DateTime DeliveredDate { get; set; }
        public DateTime CancelledDate { get; set; }
        public DateTime DeletedDate { get; set; }
        public string Salesman { get; set; }
        public string Contact1 { get; set; }
        public int OrderQty { get; set; }
    }
}
