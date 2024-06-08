namespace MKExpress.API.DTO.Response
{
    public class DashboardShipmentStatusResponse
    {
        public string? Label { get; set; }
        public List<int>? Data { get; set; }
        public string? BorderColor { get; set; }
        public string? BackgroundColor { get; set; }
    }

    public class DashboardShipmentStatusWiseCountResponse
    {
        public string? Status { get; set; }
        public int? Count { get; set; }
    }
}
