using AutoMapper;
using MKExpress.API.Constants;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Exceptions;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using MKExpress.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Services
{

    public class MonthlyAttendenceService : IMonthlyAttendenceService
    {
        private readonly IMonthlyAttendenceRepository _monthlyAttendenceRepository;
        private readonly IMapper _mapper;
        public MonthlyAttendenceService(IMonthlyAttendenceRepository monthlyAttendenceRepository, IMapper mapper)
        {
            _monthlyAttendenceRepository = monthlyAttendenceRepository;
            _mapper = mapper;
        }
        public async Task<MonthlyAttendenceResponse> Add(MonthlyAttendenceRequest monthlyAttendenceRequest)
        {
            if (await _monthlyAttendenceRepository.DeleteByEmpIdMonthYear(
                monthlyAttendenceRequest.EmployeeId,
                monthlyAttendenceRequest.Month,
                monthlyAttendenceRequest.Year) == 0)
                return default;

            MonthlyAttendence monthlyAttendence = _mapper.Map<MonthlyAttendence>(monthlyAttendenceRequest);
            return _mapper.Map<MonthlyAttendenceResponse>(await _monthlyAttendenceRepository.Add(monthlyAttendence));
        }

        public async Task<int> AddUpdateDailyAttendence(List<MonthlyAttendenceRequest> dailyAttendence)
        {
            if (dailyAttendence.Count == 0)
                return default;
            List<MonthlyAttendence> monthlyAttendences = _mapper.Map<List<MonthlyAttendence>>(dailyAttendence);
            return await _monthlyAttendenceRepository.AddUpdateDailyAttendence(monthlyAttendences, dailyAttendence.First().Day);
        }

        public async Task<int> Delete(int monthlyAttendenceId)
        {
            return await _monthlyAttendenceRepository.Delete(monthlyAttendenceId);
        }

        public async Task<int> DeleteByEmpIdMonthYear(int employeeId, int month, int year)
        {
            return await _monthlyAttendenceRepository.DeleteByEmpIdMonthYear(employeeId, month, year);
        }

        public async Task<MonthlyAttendenceResponse> Get(int monthlyAttendenceId)
        {
            return _mapper.Map<MonthlyAttendenceResponse>(await _monthlyAttendenceRepository.Get(monthlyAttendenceId));
        }

        public async Task<PagingResponse<MonthlyAttendenceResponse>> GetAll(PagingRequest pagingRequest)
        {
            var result = _mapper.Map<PagingResponse<MonthlyAttendenceResponse>>(await _monthlyAttendenceRepository.GetAll(pagingRequest));

            foreach (var item in result.Data)
            {
                item.EmployeeAdvancePayments = null;
                foreach (var advancePayment in item.Employee.EmployeeAdvancePayments)
                {
                    advancePayment.Employee = null;
                    if (advancePayment.EMI == 0 && advancePayment.AdvanceDate.Month == item.Month && advancePayment.AdvanceDate.Year == item.Year) {
                        item.Advance = advancePayment.Amount;
                    }
                    if(advancePayment.EMI > 0 && advancePayment.AdvanceDate.Month == item.Month && advancePayment.AdvanceDate.Year == item.Year)
                    {
                        item.Advance = advancePayment.EmployeeEMIPayments.Where(x => x.DeductionMonth == item.Month && x.DeductionYear == item.Year).FirstOrDefault()?.Amount ?? 0;
                    }
                }
            }
            return result;
        }

        public async Task<PagingResponse<MonthlyAttendenceResponse>> GetByEmpId(int employeeId, PagingRequest pagingRequest)
        {
            return _mapper.Map<PagingResponse<MonthlyAttendenceResponse>>(await _monthlyAttendenceRepository.GetByEmpId(employeeId, pagingRequest));
        }

        public async Task<MonthlyAttendenceResponse> GetByEmpIdMonthYear(int employeeId, int month, int year)
        {
            var result = await _monthlyAttendenceRepository.GetByEmpIdMonthYear(employeeId, month, year);
            if (result == null)
                return new MonthlyAttendenceResponse()
                {
                    Advance = 0,
                    EmployeeId = employeeId,
                    Id = 0,
                    Year = year,
                    Month = month
                };
           return _mapper.Map<MonthlyAttendenceResponse>(result);
        }

        public async Task<PagingResponse<MonthlyAttendenceResponse>> GetByMonthYear(PagingRequest pagingRequest, int month, int year)
        {
            var result= _mapper.Map<PagingResponse<MonthlyAttendenceResponse>>(await _monthlyAttendenceRepository.GetByMonthYear(pagingRequest, month, year));
            foreach (var item in result.Data)
            {
                item.EmployeeAdvancePayments = null;
                foreach (var advancePayment in item.Employee.EmployeeAdvancePayments)
                {
                    advancePayment.Employee = null;
                    if (advancePayment.EMI == 0 && advancePayment.AdvanceDate.Month == item.Month && advancePayment.AdvanceDate.Year == item.Year)
                    {
                        item.Advance = advancePayment.Amount;
                    }
                    if (advancePayment.EMI > 0 && advancePayment.AdvanceDate.Month == item.Month && advancePayment.AdvanceDate.Year == item.Year)
                    {
                        item.Advance = advancePayment.EmployeeEMIPayments.Where(x => x.DeductionMonth == item.Month && x.DeductionYear == item.Year).FirstOrDefault()?.Amount ?? 0;
                    }
                }
            }
            return result;
        }

        public async Task<List<MonthlyAttendenceResponse>> GetDailyAttendence(DateTime attendenceDate)
        {
            return _mapper.Map<List<MonthlyAttendenceResponse>>(await _monthlyAttendenceRepository.GetDailyAttendence(attendenceDate));
        }

        public async Task<PagingResponse<MonthlyAttendenceResponse>> Search(SearchPagingRequest searchPagingRequest)
        {
            return _mapper.Map<PagingResponse<MonthlyAttendenceResponse>>(await _monthlyAttendenceRepository.Search(searchPagingRequest));
        }

        public async Task<MonthlyAttendenceResponse> Update(MonthlyAttendenceRequest monthlyAttendenceRequest)
        {
            MonthlyAttendence monthlyAttendence = _mapper.Map<MonthlyAttendence>(monthlyAttendenceRequest);
            return _mapper.Map<MonthlyAttendenceResponse>(await _monthlyAttendenceRepository.Update(monthlyAttendence));
        }
    }
}
