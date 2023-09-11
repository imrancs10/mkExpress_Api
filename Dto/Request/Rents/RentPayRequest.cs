using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Dto.Request.Rents
{
    public class RentPayRequest
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string PaymentMode { get; set; }
        public string ChequeNo { get; set; }
    }
}
