namespace MKExpress.API.DTO.Response
{
    public class UserRoleMenuMapperResponse
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public Guid MenuId { get; set; }

        public UserRoleResponse? UserRole { get; set; }

        public MenuResponse? Menu { get; set; }
    }
}
