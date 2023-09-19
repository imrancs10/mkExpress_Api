using MKExpress.API.Dto.BaseDto;

namespace MKExpress.API.Dto.Response
{
    public class MasterDataResponse : BaseMasterResponse
    {
        public string MasterDataTypeCode { get; set; }
        public string MasterDataTypeValue { get; set; }
        public string Remark { get; set; }
    }
}
