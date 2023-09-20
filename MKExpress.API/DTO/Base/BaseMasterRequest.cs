namespace MKExpress.API.DTO.BaseDto
{
    public class BaseMasterRequest
    {
        public Guid? Id { get; set; }=Guid.NewGuid();
        public string Code { get; set; }
        public string Value { get; set; }
    }
}
