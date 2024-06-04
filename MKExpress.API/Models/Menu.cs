using System.ComponentModel.DataAnnotations;

namespace MKExpress.API.Models
{
    public class Menu:BaseModel
    {

        [Required]
        [MaxLength(70)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Code { get; set; }

        [Required]
        [MaxLength(500)]
        public string? Link { get; set; }

        public string? MenuPosition { get; set; }
        public string? Icon { get; set; }
        public string? Title { get; set; }
        public bool? Disable { get; set; }
        public string? Tag { get; set; }
        public int DisplayOrder { get; set; }
    }
}
