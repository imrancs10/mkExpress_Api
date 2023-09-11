namespace MKExpress.API.Dto.Response
{
    public class UpdateMeasurementRequest
    {
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
        public string Description { get; set; }
        public int OrderDetailId { get; set; }
        public int customerId { get; set; }
        public string MeasurementCustomerName { get; set; }
    }
}
