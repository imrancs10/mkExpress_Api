﻿namespace MKExpress.API.Models
{
    public class Customer:BaseModel
    {
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public int MaxDeliveryAttempt { get; set; }
        public bool Confirmed { get; set; }
        public string PreferredPickupTime { get; set; }
    }
}
