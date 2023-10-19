using MKExpress.API.DTO.Base;
using System.ComponentModel.DataAnnotations;

namespace MKExpress.API.DTO.Request
{
    public class ThirdPartyCourierCompanyRequest:BaseRequest
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Contact { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Url)]
        public string TrackingUrl { get; set; }
        public bool IsActive { get; set; }
    }
}
