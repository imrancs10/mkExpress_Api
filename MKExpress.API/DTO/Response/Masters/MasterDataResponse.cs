using MKExpress.API.DTO.BaseDto;

namespace MKExpress.API.DTO.Response
{
    public class MasterDataResponse : BaseMasterResponse
    {
        public string MasterDataTypeCode { get; set; }
        public string MasterDataTypeValue { get; set; }
        public string Remark { get; set; }
    }
}
