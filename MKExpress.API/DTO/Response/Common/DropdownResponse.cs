namespace MKExpress.API.DTO.Response.Common
{
    public class DropdownResponse
    {
        public int Id { get; set; }
        public string EnValue { get; set; }
        public string HiValue { get; set; }
        public string TaValue { get; set; }
        public string TeValue { get; set; }
        public int ParentId { get; set; }
        public int DisplayOrder { get; set; }
    }
}
