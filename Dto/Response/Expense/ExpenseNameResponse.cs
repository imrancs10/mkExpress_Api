namespace MKExpress.API.Dto.Response.Expense
{
    public class ExpenseNameResponse
    {
        public int Id { get; set; }
        public int ExpenseTypeId { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
        public string ExpenseType { get; set; }
    }
}
