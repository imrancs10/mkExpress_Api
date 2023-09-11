using MKExpress.API.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Models
{
    public class CrystalTrackingOut:BaseModel
    {
        public int OrderDetailId { get; set; }
        public int EmployeeId { get; set; }
        public string Note { get; set; }
        public DateTime ReleaseDate { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

        [ForeignKey("OrderDetailId")]
        public OrderDetail OrderDetail { get; set; }

        public List<CrystalTrackingOutDetail> CrystalTrackingOutDetails { get; set; }
    }
}
