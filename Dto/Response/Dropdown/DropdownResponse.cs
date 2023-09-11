using MKExpress.API.Dto.BaseDto;

namespace MKExpress.API.Dto.Response
{
    public class DropdownResponse : BaseDropdownResponse
    {
        public int ParentId { get; set; }
    }
    public class DropdownResponse<T> : BaseDropdownResponse where T : class
    {
        public T Data { get; set; }
    }
}
