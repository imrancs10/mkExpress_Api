namespace MKExpress.API.Dto.Response
{
    public class CustomerPreviousAmountStatementResponse
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string OrderNo { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? AdvanceAmount { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? BalanceAmount { get; set; }
    }
}
