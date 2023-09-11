using AutoMapper;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using MKExpress.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Services
{
    public class MasterHolidayService : IMasterHolidayService
    {
        private readonly IMasterHolidayRepository _masterHolidayRepository;
        private readonly IMapper _mapper;
        public MasterHolidayService(IMasterHolidayRepository masterHolidayRepository, IMapper mapper)
        {
            _masterHolidayRepository = masterHolidayRepository;
            _mapper = mapper;
        }
        public async Task<MasterHolidayResponse> Add(MasterHolidayRequest request)
        {
            MasterHoliday masterHoliday = _mapper.Map<MasterHoliday>(request);
            return _mapper.Map<MasterHolidayResponse>(await _masterHolidayRepository.Add(masterHoliday));
        }

        public async Task<int> Delete(int id)
        {
            return await _masterHolidayRepository.Delete(id);
        }

        public async Task<MasterHolidayResponse> Get(int id)
        {
            return _mapper.Map<MasterHolidayResponse>(await _masterHolidayRepository.Get(id));
        }

        public async Task<PagingResponse<MasterHolidayResponse>> GetAll(PagingRequest pagingRequest)
        {
            return _mapper.Map<PagingResponse<MasterHolidayResponse>>(await _masterHolidayRepository.GetAll(pagingRequest));
        }

        public async Task<MasterHolidayResponse> GetHolidayByDate(DateTime holidayDate)
        {
            return _mapper.Map<MasterHolidayResponse>(await _masterHolidayRepository.GetHolidayByDate(holidayDate));
        }

        public async Task<List<MasterHolidayResponse>> GetHolidayByMonthYear(int month, int year)
        {
            return _mapper.Map<List<MasterHolidayResponse>>(await _masterHolidayRepository.GetHolidayByMonthYear(month, year));
        }

        public async Task<bool> IsHoliday(DateTime holidayDate)
        {
            return await _masterHolidayRepository.IsHoliday(holidayDate);
        }

        public async Task<PagingResponse<MasterHolidayResponse>> Search(SearchPagingRequest searchPagingRequest)
        {
            return _mapper.Map<PagingResponse<MasterHolidayResponse>>(await _masterHolidayRepository.Search(searchPagingRequest));
        }

        public async Task<MasterHolidayResponse> Update(MasterHolidayRequest request)
        {
            MasterHoliday masterHoliday = _mapper.Map<MasterHoliday>(request);
            return _mapper.Map<MasterHolidayResponse>(await _masterHolidayRepository.Update(masterHoliday));
        }
    }
}
