using MKExpress.API.Constants;
using System.ComponentModel.DataAnnotations;

namespace MKExpress.API.Dto.Request
{
    public class CustomerRequest
    {
        public int Id { get; set; }

        [Required(ErrorMessage = StaticValues.FirstNameRequired)]
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        [Required(ErrorMessage = StaticValues.ContactRequired)]
        public string Contact1 { get; set; }
        public string Contact2 { get; set; }
        public string Branch { get; set; }
        public string POBox { get; set; }
        public string TRN { get; set; }
    }
}
