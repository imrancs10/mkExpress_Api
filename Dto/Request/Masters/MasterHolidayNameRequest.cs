using MKExpress.API.Dto.BaseDto;

namespace MKExpress.API.Dto.Request
{
    public class MasterHolidayNameRequest : BaseMasterRequest
    {
        public int HolidayTypeId { get; set; }
    }
}
