using MKExpress.API.Dto.BaseDto;

namespace MKExpress.API.Dto.Request
{
    public class MasterDataRequest : BaseMasterRequest
    {
        public string MasterDataType { get; set; }
        public string Remark { get; set; }
    }
}
