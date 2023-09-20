using MKExpress.API.DTO.Base;
using MKExpress.API.Enums;

namespace MKExpress.API.DTO.Request
{
    public class UserRequest:BaseRequest
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GenderEnum Gender { get; set; }
        public string Mobile { get; set; }
        public bool IsTcAccepted { get; set; }
    }
}
