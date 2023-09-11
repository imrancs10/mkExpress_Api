using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Dto.Response
{
    public class OrderAlertResponse
    {
        public int OrderId { get; set; }
        public int OrderDetailId { get; set; }
        public int OrderQty { get; set; }
        public string OrderNo { get; set; }
        public string Status { get; set; }
        public string Salesman { get; set; }
        public decimal SubTotalAmount { get; set; }
        public string KandooraNo { get; set; }
        public string Grade { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Design { get; set; }
        public string Cutting { get; set; }
        public string MEmb { get; set; }
        public string HFix { get; set; }
        public string HEmb { get; set; }
        public string Apliq { get; set; }
        public string Stitch { get; set; }
    }
}
