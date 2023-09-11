using MKExpress.API.Dto.BaseDto;

namespace MKExpress.API.Models
{
    public class Dropdown : BaseDropdown
    {
        public int ParentId { get; set; }
    }

    public class Dropdown<T> : BaseDropdown
    {
        public T Data { get; set; }
    }
}
