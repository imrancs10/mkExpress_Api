using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class Customer:BaseModel
    {
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public Guid CityId { get; set; }
        public string ZipCode { get; set; }
        public int MaxDeliveryAttempt { get; set; }
        public bool Confirmed { get; set; }
        public string PreferredPickupTime { get; set; }

        [ForeignKey("CityId")]
        public MasterData City { get; set; }
    }
}
