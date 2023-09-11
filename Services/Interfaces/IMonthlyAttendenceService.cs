using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Services.Interfaces
{
    public interface IMonthlyAttendenceService : ICrudService<MonthlyAttendenceRequest, MonthlyAttendenceResponse>
    {
        Task<int> DeleteByEmpIdMonthYear(int employeeId, int month, int year);
        Task<MonthlyAttendenceResponse> GetByEmpIdMonthYear(int employeeId, int month, int year);
        Task<PagingResponse<MonthlyAttendenceResponse>> GetByEmpId(int employeeId, PagingRequest pagingRequest);
        Task<int> AddUpdateDailyAttendence(List<MonthlyAttendenceRequest> dailyAttendence);
        Task<List<MonthlyAttendenceResponse>> GetDailyAttendence(DateTime attendenceDate);
        Task<PagingResponse<MonthlyAttendenceResponse>> GetByMonthYear(PagingRequest pagingRequest, int month, int year);
    }
}
