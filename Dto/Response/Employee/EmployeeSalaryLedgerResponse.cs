namespace MKExpress.Web.API.Dto.Response.Employee
{
    public class EmployeeSalaryLedgerResponse
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public decimal Amount { get; set; }
        public int Qty { get; set; }
    }
}
