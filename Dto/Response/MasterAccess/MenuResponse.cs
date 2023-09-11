namespace MKExpress.Web.API.Dto.Response.MasterAccess
{
    public class MenuResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }
        public int ParentId { get; set; }
        public bool Disable { get; set; }
        public string Url { get; set; }
    }
}
