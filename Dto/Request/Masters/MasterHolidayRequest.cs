using System;

namespace MKExpress.API.Dto.Request
{
    public class MasterHolidayRequest
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public DateTime HolidayDate { get; set; }
        public bool RecurringEveryYear { get; set; }
        public int HolidayNameId { get; set; }
        public string Remark { get; set; }
    }
}
