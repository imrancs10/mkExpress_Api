namespace MKExpress.API.Dto.Request.Expense
{
    public class ExpenseNameRequest
    {
        public int Id { get; set; }
        public int ExpenseTypeId { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
    }
}
