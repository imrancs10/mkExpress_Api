namespace MKExpress.API.DTO.Response
{
    public class PagingResponse<T> where T : class
    {
        public int PageNo { get; set; }
        public int TotalRecords { get; set; } = 0;
        public int PageSize { get; set; }
        public List<T> Data { get; set; }

    }
}
