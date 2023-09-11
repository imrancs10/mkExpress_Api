using MKExpress.API.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Models
{
    public class OrderWorkDescription:BaseModel
    {
        public int OrderDetailId { get; set; }
        public int WorkDescriptionId { get; set; }
        public string NewModel { get; set; }
        public string SamePrint { get; set; }
        public string LikeModel { get; set; }

        [ForeignKey("OrderDetailId")]
        public OrderDetail OrderDetail { get; set; }

        [ForeignKey("WorkDescriptionId")]
        public MasterWorkDescription MasterWorkDescription { get; set; }

    }
}
