namespace MKExpress.API.Dto.Response.Dashboard
{
    public class ExpenseDashboardResponse
    {
        public int Suppliers { get; set; }
        public int Stocks { get; set; }
        public int Purchases { get; set; }
        public int Rent { get; set; }
        public decimal ExpCash { get; set; }
        public decimal ExpVisa { get; set; }
        public decimal ExpCheque { get; set; }
        public decimal Expenses { get; set; }
    }
}
