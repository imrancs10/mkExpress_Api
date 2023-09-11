using MKExpress.API.Models.BaseModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.Web.API.Models
{
    public class MasterAccessDetail:BaseModel
    {
        public int AccessId { get; set; }

        public int MasterMenuId { get; set; }


        [ForeignKey("AccessId")]
        public MasterAccess MasterAccess { get; set; }


        [ForeignKey("MasterMenuId")]
        public MasterMenu MasterMenu { get; set; }
    }
}
