using MKExpress.API.Dto.BaseDto;
using MKExpress.API.Dto.Response.Customer;
using System;
using System.Collections.Generic;

namespace MKExpress.API.Dto.Response
{
    public class OrderResponse : BaseResponseModel
    {
        public int Id { get; set; }
        public string OrderNo { get; set; }
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime OrderDeliveryDate { get; set; }
        public string City { get; set; }
        public decimal VAT { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal AdvanceAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public decimal SubTotalAmount { get; set; }
        public decimal CancelledAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public string PaymentMode { get; set; }
        public string CustomerRefName { get; set; }
        public string CustomerName { get; set; }
        public string Contact1 { get; set; }
        public string Salesman { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
        public bool IsCancelled { get; set; }
        public int? Qty { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CancelledDate { get; set; }
        public DateTime DeletedDate { get; set; }
        public List<OrderDetailResponse> OrderDetails { get; set; }
        public List<CustomerAccountStatementResponse> AccountStatements { get; set; }
        public int TaxInvoiceNo { get; set; }
        public string CustomerTRN { get; set; }
    }
}
