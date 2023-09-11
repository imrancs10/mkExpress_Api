namespace MKExpress.API.Dto.Request
{
    public class ChangeCustomerPasswordRequest
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}