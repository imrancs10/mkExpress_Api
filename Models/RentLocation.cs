using MKExpress.API.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Models
{
    public class RentLocation : BaseModel
    {
        public string LocationName { get; set; }
        public string Address { get; set; }
        public string Remark { get; set; }
        public List<RentDetail> RentDetails { get; set; }
    }

    public class RentDetail : BaseModel
    {
        public int RentLocationId { get; set; }

        [ForeignKey("RentLocationId")]
        public RentLocation RentLocation { get; set; }
        public decimal RentAmount { get; set; }
        public int Installments { get; set; }
        public DateTime FirstInstallmentDate { get; set; }
        public List<RentTransation> RentTransations { get; set; }

    }

    public class RentTransation : BaseModel
    {
        public int RentDetailId { get; set; }
        public string InstallmentName { get; set; }
        public DateTime InstallmentDate { get; set; }
        public decimal InstallmentAmount { get; set; }
        public bool IsPaid { get; set; }
        public DateTime PaidOn { get; set; }
        public int? PaidBy { get; set; }
        public string PaymentMode { get; set; }
        public string ChequeNo { get; set; }

        [ForeignKey("RentDetailId")]
        public RentDetail RentDetail { get; set; }

        [ForeignKey("PaidBy")]
        public Employee PaidByEmp { get; set; }
    }
}
