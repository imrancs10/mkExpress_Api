using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Services.Interfaces
{
    public interface IMasterHolidayService : ICrudService<MasterHolidayRequest, MasterHolidayResponse>
    {
        Task<bool> IsHoliday(DateTime holidayDate);
        Task<MasterHolidayResponse> GetHolidayByDate(DateTime holidayDate);
        Task<List<MasterHolidayResponse>> GetHolidayByMonthYear(int month, int year);
    }

    public interface IMasterHolidayTypeService : ICrudService<MasterDataTypeRequest, MasterDataTypeResponse>
    {
    }

    public interface IMasterHolidayNameService : ICrudService<MasterHolidayNameRequest, MasterHolidayNameResponse>
    {
    }
}
