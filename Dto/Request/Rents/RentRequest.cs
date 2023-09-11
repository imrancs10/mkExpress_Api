using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Dto.Request.Rents
{
    public class RentLocationRequest
    {
        public int Id { get; set; }
        public string LocationName { get; set; }
        public string Address { get; set; }
        public string Remark { get; set; }
    }
    public class RentDetailRequest
    {
        public int Id { get; set; }
        public int RentLocationId { get; set; }
        public decimal RentAmount { get; set; }
        public int Installments { get; set; }
        public DateTime FirstInstallmentDate { get; set; }
    }
}
