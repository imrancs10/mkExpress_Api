using MKExpress.API.DTO.Base;
using MKExpress.API.Enums;

namespace MKExpress.API.DTO.Request
{
    public class MemberRequest:BaseRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string Mobile { get; set; }
        public string? Password { get; set; }
        public string? IdNumber { get; set; }
        public GenderEnum Gender { get; set; }
        public Guid RoleId { get; set; }
        public Guid? StationId { get; set; }
    }
}
