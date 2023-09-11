using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Dto.Response
{
    public class KandooraExpenseResponse
    {
        public DateTime OrderDate { get; set; }
        public string OrderNo { get; set; }
        public string Status { get; set; }
        public string Salesman { get; set; }
        public string CustomerName { get; set; }
        public string ModalNo { get; set; }
        public decimal Amount { get; set; }
        public decimal Design { get; set; }
        public decimal CrystalUsed { get; set; }
        public decimal Cutting { get; set; }
        public decimal MEmb { get; set; }
        public decimal HFix { get; set; }
        public decimal HEmb { get; set; }
        public decimal Apliq { get; set; }
        public decimal Stitch { get; set; }
        public decimal FixAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Profit { get; set; }
        public decimal ProfitPercentage { get; set; }
    }
}
