namespace MKExpress.API.Dto.Response
{
    public class AccountSummaryReportResponse
    {
        public decimal TotalBookingAmount { get; set; }
        public decimal TotalBookingCashAmount { get; set; }
        public decimal TotalBookingVisaAmount { get; set; }
        public decimal TotalAdvanceCashAmount { get; set; }
        public decimal TotalAdvanceVisaAmount { get; set; }
        public decimal TotalAdvanceAmount { get; set; }
        public decimal TotalDeliveryCashAmount { get; set; }
        public decimal TotalDeliveryVisaAmount { get; set; }
        public decimal TotalDeliveryAmount { get; set; }
        public decimal TotalCancelledAmount { get; set; }
        public decimal TotalDeletedAmount { get; set; }
        public decimal TotalRefundAmount { get; set; }
        public decimal TotalBookingVatAmount { get; set; }
        public decimal TotalAdvanceVatAmount { get; set; }
        public decimal TotalBalanceVatAmount { get; set; }
    }
}
