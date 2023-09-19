using MKExpress.API.DTO.BaseDto;

namespace MKExpress.API.DTO.Request
{
    public class MasterDataRequest : BaseMasterRequest
    {
        public string MasterDataType { get; set; }
        public string Remark { get; set; }
    }
}
