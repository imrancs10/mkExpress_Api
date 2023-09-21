﻿using MKExpress.API.Constants;
using System.ComponentModel.DataAnnotations;

namespace MKExpress.API.DTO.Request
{
    public class CustomerRequest
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string ContactNo { get; set; }
        public int MaxDeliveryAttempt { get; set; }
        public bool Confirmed { get; set; }
        public string PreferredPickupTime { get; set; }
    }
}
