namespace MKExpress.API.Dto.Request
{
    public class UserPermissionRequest
    {
        public int Id { get; set; }
        public int UserRoleId { get; set; }
        public int PermissionResourceId { get; set; }
    }
}
