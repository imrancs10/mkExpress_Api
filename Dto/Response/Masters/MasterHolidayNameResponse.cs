using MKExpress.API.Dto.BaseDto;

namespace MKExpress.API.Dto.Response
{
    public class MasterHolidayNameResponse : BaseMasterResponse
    {
        public int HolidayTypeId { get; set; }
        public string HolidayType { get; set; }
    }
}
