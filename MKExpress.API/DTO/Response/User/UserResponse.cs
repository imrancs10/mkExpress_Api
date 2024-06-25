using MKExpress.API.DTO.Base;

namespace MKExpress.API.DTO.Response
{
    public class UserResponse: BaseResponse
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public bool IsLocked { get; set; }
        public bool IsBlocked { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Gender { get; set; }
        public string Mobile { get; set; }
        public string Role { get; set; }
        public Guid RoleId { get; set; }
        public Guid? MemberId { get; set; }
        public string RoleCode { get; set; }
        public bool IsTcAccepted { get; set; }
        public bool IsEmailVerified { get; set; }
        public List<UserRoleMenuMapperResponse>? Permissions { get; set; }
        public string? ProfileImagePath { get; set; }
    }
}
