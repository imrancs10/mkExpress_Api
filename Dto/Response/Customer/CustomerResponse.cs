namespace MKExpress.API.Dto.Response
{
    public class CustomerResponse
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Contact1 { get; set; }
        public string Contact2 { get; set; }
        public string Branch { get; set; }
        public string POBox { get; set; }
        public int TotalOrders { get; set; }
        public string TRN { get; set; }
        public string LastSalesMan { get; set; }
    }
}
