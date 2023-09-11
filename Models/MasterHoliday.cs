using MKExpress.API.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class MasterHolidayName : MasterBaseModel
    {
        public int HolidayTypeId { get; set; }

        [ForeignKey("HolidayTypeId")]
        public MasterHolidayType HolidayType { get; set; }
    }
    public class MasterHolidayType : MasterBaseModel
    {
        public List<MasterHolidayName> HolidayNames { get; set; }
    }

    public class MasterHoliday : BaseModel
    {
        public int Year { get; set; }
        public DateTime HolidayDate { get; set; }
        public bool RecurringEveryYear { get; set; }
        public int HolidayNameId { get; set; }
        public string Remark { get; set; }

        [ForeignKey("HolidayNameId")]
        public MasterHolidayName HolidayName { get; set; }
    }
}
