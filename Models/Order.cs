using MKExpress.API.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class Order : BaseModel
    {
        [Column("OrderId")]
        [Key]
        public override int Id { get => base.Id; set => base.Id = value; }
        public string OrderNo { get; set; }
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime OrderDeliveryDate { get; set; }
        public string City { get; set; }
        public decimal VAT { get; set; }
        public decimal VATAmount { get; set; }
        public decimal AdvanceVATAmount { get; set; }
        public decimal SubTotalAmount { get; set; }
        public decimal CancelledAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal AdvanceAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public string PaymentMode { get; set; }
        public string CustomerRefName { get; set; }
        public bool IsCancelled { get; set; }
        public string Note { get; set; }
        public decimal Profit { get; set; }
        public string Status { get; set; }
        public int? Qty { get; set; }
        public DateTime CancelledDate { get; set; }
        public DateTime DeletedDate { get; set; }
        public int TaxInvoiceNo { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

        [ForeignKey("CreatedBy")]
        public Employee EmployeeCreated { get; set; }
        [ForeignKey("UpdatedBy")]
        public Employee EmployeeUpdated { get; set; }
        public CancelledOrder CancelledOrder { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public List<CustomerAccountStatement> AccountStatements { get; set; }
    }
}
