namespace MKExpress.API.Dto.Response
{
    public class PermissionResourceResponse
    {
        public int Id { get; set; }
        public int ResourceTypeId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ResourceTypeName { get; set; }
    }
}
