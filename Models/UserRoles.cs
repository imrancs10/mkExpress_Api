
using MKExpress.API.Models.BaseModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class UserRole : BaseModel
    {
       
        public string Name { get; set; }
        public string Code { get; set; }
        public Member Member { get; set; }
    }
}
