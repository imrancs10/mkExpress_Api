namespace MKExpress.API.Dto.Request
{
    public class EachKandooraExpenseRequest
    {
        public int Id { get; set; }
        public int KandooraHeadId { get; set; }
        public decimal Amount { get; set; }
    }
}
