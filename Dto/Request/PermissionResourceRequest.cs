namespace MKExpress.API.Dto.Request
{
    public class PermissionResourceRequest
    {
        public int Id { get; set; }
        public int ResourceTypeId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
