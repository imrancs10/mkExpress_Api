

namespace MKExpress.API.Dto.Response
{
    public class EmployeeEMIPaymentResponse
    {
        public int Id { get; set; }
        public int AdvancePaymentId { get; set; }
        public decimal Amount { get; set; }
        public int DeductionMonth { get; set; }
        public int DeductionYear { get; set; }
        public string Remark { get; set; }
    }
}
