namespace MKExpress.API.DTO.Response
{
    public class MenuResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Link { get; set; }

        public string? MenuPosition { get; set; }
        public string? Icon { get; set; }
        public string? Title { get; set; }
        public bool? Disable { get; set; }
        public string? Tag { get; set; }
    }
}
