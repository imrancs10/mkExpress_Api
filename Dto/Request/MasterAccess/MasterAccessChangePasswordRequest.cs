namespace MKExpress.Web.API.Dto.Request.MasterAccess
{
    public class MasterAccessChangePasswordRequest
    {
        public int Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
