namespace MKExpress.API.DTO.Request
{
    public class UserRoleMenuMapperRequest
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public Guid MenuId { get; set; }
    }
}
