using MKExpress.API.DTO.BaseDto;
using System;

namespace MKExpress.API.DTO.Request
{
    public class SearchPagingRequest : BasePagingRequest
    {
        public string SearchTerm { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool isAdmin { get; set; }
    }
}
