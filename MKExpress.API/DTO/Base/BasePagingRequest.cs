namespace MKExpress.API.DTO.BaseDto
{
    public class BasePagingRequest
    {
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 100;

    }
}
