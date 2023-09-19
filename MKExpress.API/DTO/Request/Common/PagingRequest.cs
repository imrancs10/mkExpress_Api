using MKExpress.API.DTO.BaseDto;
using System;

namespace MKExpress.API.DTO.Request
{
    public class PagingRequest : BasePagingRequest
    {
        public virtual DateTime FromDate { get; set; } = DateTime.Now.AddYears(-10).Date;
        public virtual DateTime ToDate { get; set; } = DateTime.Now.Date;
    }
}
