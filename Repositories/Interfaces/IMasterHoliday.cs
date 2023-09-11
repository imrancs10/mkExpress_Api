using MKExpress.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories.Interfaces
{
    public interface IMasterHolidayTypeRepository : ICrudRepository<MasterHolidayType>
    {
    }
    public interface IMasterHolidayNameRepository : ICrudRepository<MasterHolidayName>
    {
    }
    public interface IMasterHolidayRepository : ICrudRepository<MasterHoliday>
    {
        Task<bool> IsHoliday(DateTime holidayDate);
        Task<MasterHoliday> GetHolidayByDate(DateTime holidayDate);
        Task<List<MasterHoliday>> GetHolidayByMonthYear(int month, int year);
    }
}
