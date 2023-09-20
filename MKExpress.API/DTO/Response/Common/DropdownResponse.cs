using MKExpress.API.DTO.Base;
using MKExpress.API.DTO.BaseDto;

namespace MKExpress.API.DTO.Response.Common
{
    public class DropdownResponse:BaseResponse
    {
        public string EnValue { get; set; }
        public string HiValue { get; set; }
        public string TaValue { get; set; }
        public string TeValue { get; set; }
        public int ParentId { get; set; }
        public int DisplayOrder { get; set; }
    }
}
