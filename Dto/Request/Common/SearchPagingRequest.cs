using MKExpress.API.Dto.BaseDto;
using System;

namespace MKExpress.API.Dto.Request
{
    public class SearchPagingRequest : BasePagingRequest
    {
        public string SearchTerm { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool isAdmin { get; set; }
    }
}
