using System;

namespace MKExpress.API.Dto.Response
{
    public class MasterHolidayResponse
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public DateTime HolidayDate { get; set; }
        public bool RecurringEveryYear { get; set; }
        public int HolidayNameId { get; set; }
        public string Remark { get; set; }
        public string HolidayType { get; set; }
        public string HolidayName { get; set; }
    }
}
