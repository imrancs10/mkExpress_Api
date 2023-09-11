using MKExpress.API.Dto.BaseDto;
using System;

namespace MKExpress.API.Dto.Request
{
    public class PagingRequest : BasePagingRequest
    {
        public virtual DateTime FromDate { get; set; } = DateTime.Now.AddYears(-10).Date;
        public virtual DateTime ToDate { get; set; } = DateTime.Now.Date;
    }
}
