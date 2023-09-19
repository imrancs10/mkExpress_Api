namespace MKExpress.API.DTO.Request.Common
{
    public class PagingRequest
    {
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
