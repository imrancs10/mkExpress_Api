using AutoMapper;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Models;
using MKExpress.API.Repository;
using System;

namespace MKExpress.API.Services
{
    public class SystemActionService : ISystemActionService
    {
        private readonly ISystemActionRepository _actionRepository;
        private readonly IMapper _mapper;
        public SystemActionService(ISystemActionRepository actionRepository,IMapper mapper)
        {
            _actionRepository = actionRepository;
            _mapper = mapper;
        }
        public async Task<PagingResponse<SystemActionResponse>> GetSystemActions(SystemActionRequest request)
        {
            request ??= new SystemActionRequest()
                {
                    PageNo = 1,
                    PageSize = 20,
                    ActionFrom = DateTime.Now.AddMonths(-1),
                    ActionTo = DateTime.Now
                };

            if (request.ActionFrom == null || request.ActionTo == null)
            {
                request.ActionFrom = DateTime.Now.AddMonths(-1);
                request.ActionTo = DateTime.Now;
            }
            return _mapper.Map<PagingResponse<SystemActionResponse>>(await _actionRepository.GetSystemActions(request));
        }
    }
}
