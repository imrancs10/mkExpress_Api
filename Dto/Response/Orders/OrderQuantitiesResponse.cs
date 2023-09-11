namespace MKExpress.API.Dto.Response.Orders
{
    public class OrderQuantitiesResponse
    {
        public int BookingQty { get; set; }
        public int OrderQty { get; set; }
        public int ActiveQty { get; set; }
        public int ProcessingQty { get; set; }
        public int CompletedQty { get; set; }
        public int CancelledQty { get; set; }
        public int DeletedQty { get; set; }
        public int DeliveredQty { get; set; }
        public int AlertQty { get; set; }
    }
}
