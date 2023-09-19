namespace MKExpress.API.DTO.Response.Common
{
    public class PagingResponse<T> where T : class
    {
        public int PageNo { get; set; }
        public int TotalCount { get; set; } = 0;
        public int PageSize { get; set; }
        public List<T> Data { get; set; }

    }
}
