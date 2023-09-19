using MKExpress.API.DTO.Base;

namespace MKExpress.API.DTO.Response.Image
{
    public class ImageStoreResponse:BaseResponse
    {
        public string FilePath { get; set; }
        public string ThumbPath { get; set; }
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public string Remark { get; set; }
        public string ImageType { get; set; }
    }
    public class ImageStoreWithNameResponse:ImageStoreResponse {

        public string EnName { get; set; }
        public string HiName { get; set; }
        public string TeName { get; set; }
        public string TaName { get; set; }
    }
}
