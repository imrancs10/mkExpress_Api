using System.ComponentModel.DataAnnotations;

namespace MKExpress.API.Models
{
    public class ThirdPartyCourierCompany : BaseModel
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string? Contact { get; set; }
        public string Email { get; set; }
        public string? TrackingUrl { get; set; }
        public bool IsActive { get; set; }
    }
}
