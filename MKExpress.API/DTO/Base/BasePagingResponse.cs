using System.Collections.Generic;

namespace SalehGarib.API.Dto.BaseDto
{
    public class BasePagingResponse<T> where T : class
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public List<T> Data { get; set; }
        public int TotalRecords { get; set; }
    }
}
