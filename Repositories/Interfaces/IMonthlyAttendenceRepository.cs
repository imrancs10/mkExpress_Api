using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories.Interfaces
{
    public interface IMonthlyAttendenceRepository : ICrudRepository<MonthlyAttendence>
    {
        Task<int> DeleteByEmpIdMonthYear(int employeeId, int month, int year);
        Task<MonthlyAttendence> GetByEmpIdMonthYear(int employeeId, int month, int year);
        Task<PagingResponse<MonthlyAttendence>> GetByMonthYear(PagingRequest pagingRequest, int month, int year);
        Task<PagingResponse<MonthlyAttendence>> GetByEmpId(int employeeId, PagingRequest pagingRequest);
        Task<int> AddUpdateDailyAttendence(List<MonthlyAttendence> dailyAttendence, int day);
        Task<List<MonthlyAttendence>> GetDailyAttendence(DateTime attendenceDate);
        Task<int> PayMonthlySalary(int id,DateTime paidOn,decimal salary);
    }
}
