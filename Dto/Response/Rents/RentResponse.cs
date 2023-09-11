using System;
using System.Collections.Generic;

namespace MKExpress.API.Dto.Response.Rents
{
    public class RentLocationResponse
    {
        public int Id { get; set; }
        public string LocationName { get; set; }
        public string Address { get; set; }
        public string Remark { get; set; }
    }

    public class RentDetailResponse
    {
        public int Id { get; set; }
        public int RentLocationId { get; set; }
        public string RentLocation { get; set; }
        public decimal RentAmount { get; set; }
        public int Installments { get; set; }
        public DateTime FirstInstallmentDate { get; set; }
        public List<RentTransactionResponse> RentTransactions { get; set; }
    }

    public class RentTransactionResponse
    {
        public int Id { get; set; }
        public int RentDetailId { get; set; }
        public string InstallmentName { get; set; }
        public DateTime InstallmentDate { get; set; }
        public decimal InstallmentAmount { get; set; }
        public bool IsPaid { get; set; }
        public DateTime PaidOn { get; set; }
        public string PaidBy { get; set; }
        public string PaymentMode { get; set; }
        public string ChequeNo { get; set; }
        public string RentLocation { get; set; }
    }
}
