using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class UserActivity:BaseModel
    {
        public Guid UserId { get; set; }
        public string ActivityType { get; set; }
        public string Activity { get; set; }
        public string? Remark { get; set; }

        [ForeignKey("UserId")]  
        public User User { get; set; }
    }
}
