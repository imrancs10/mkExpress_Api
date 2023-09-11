using MKExpress.API.Models.BaseModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class DesignSample : BaseModel
    {
        [Required]
        public string Model { get; set; }
        public string DesignerName { get; set; }
        public string Shape { get; set; }
        public int Size { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public MasterDesignCategory MasterDesignCategory { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }

    }
}
