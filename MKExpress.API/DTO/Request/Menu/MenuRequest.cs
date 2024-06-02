﻿using System.ComponentModel.DataAnnotations;

namespace MKExpress.API.DTO.Request
{
    public class MenuRequest
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Code { get; set; }

        [Required]
        [MaxLength(200)]
        public string? Link { get; set; }

        public string? MenuPosition { get; set; }
        public string? Icon { get; set; }
        public string? Title { get; set; }
        public bool? Disable { get; set; }
        public string? Tag { get; set; }
    }
}
