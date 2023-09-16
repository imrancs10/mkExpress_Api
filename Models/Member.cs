using MKExpress.API.Models.BaseModels;
using static System.Collections.Specialized.BitVector32;
using System.Numerics;
using System.Reflection;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class Member:BaseModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int RoleId { get; set; }
        public int StationId { get; set; }
        public string PersonalPhone { get; set; }
        public int IdNumber { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey("RoleId")]
        public UserRole Role { get; set; }

        [ForeignKey("StationId")]
        public Station Station { get; set; }
    }
}
