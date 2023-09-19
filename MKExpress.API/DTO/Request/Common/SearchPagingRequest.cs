using MKExpress.API.DTO.Request.Common;

namespace MKExpress.API.DTO.Request
{
    public class SearchPagingRequest:PagingRequest
    {
        public string SearchTerm { get; set; }
    }
}
