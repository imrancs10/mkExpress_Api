using MKExpress.API.Models.BaseModels;
using System;

namespace MKExpress.API.Models
{
    public class CuttingMaster : BaseModel
    {
        public string MasterName { get; set; }

        public string OrderNumber { get; set; }

        public DateTime? CutttingDate { get; set; }

        public string Status { get; set; }

        public string Size { get; set; }

        public string Type { get; set; }

        public decimal? Qantity { get; set; }

        public string Length { get; set; }

        public string Shoulder { get; set; }

        public string Sleeves { get; set; }

        public string Chest { get; set; }

        public string Neck { get; set; }

        public string Bottom { get; set; }

        public string Loosing { get; set; }

        public string F_Fabric { get; set; }

        public string OrderPic { get; set; }
    }

}
