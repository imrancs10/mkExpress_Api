using MKExpress.API.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class OrderDetail : CustomerMeasurementBaseModel
    {
        public int? DesignSampleId { get; set; }
        public decimal Price { get; set; }
        public int OrderId { get; set; }
        public string OrderNo { get; set; }
        public string Crystal { get; set; }
        public decimal CrystalPrice { get; set; }
        public string Description { get; set; }
        public string WorkType { get; set; }
        public string OrderStatus { get; set; }
        public string MeasurementStatus { get; set; }
        public decimal SubTotalAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsCancelled { get; set; }
        public string? Note { get; set; }
        public string Status { get; set; }
        public string ModelNo { get; set; }
        public DateTime DeliveredDate { get; set; }
        public DateTime CancelledDate { get; set; }
        public DateTime DeletedDate { get; set; }
        public DateTime OrderDeliveryDate { get; set; }
        [ForeignKey("CreatedBy")]
        public Employee EmployeeCreated { get; set; }
        [ForeignKey("UpdatedBy")]
        public Employee EmployeeUpdated { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        [ForeignKey("DesignSampleId")]
        public DesignSample DesignSample { get; set; }

        public List<CustomerAccountStatement> AccountStatements { get; set; }

        public List<WorkTypeStatus> WorkTypeStatuses { get; set; }
        public List<OrderWorkDescription> OrderWorkDescriptions { get; set; }
        public List<CrystalTrackingOutDetail> CrystalTrackingOutDetails { get; set; }

    }
}
