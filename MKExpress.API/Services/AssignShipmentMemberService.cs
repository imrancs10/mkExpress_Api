using AutoMapper;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Repository.IRepository;
using MKExpress.API.Services.IServices;

namespace MKExpress.API.Services
{
    public class AssignShipmentMemberService : IAssignShipmentMemberService
    {
        private readonly IAssignShipmentMemberRepository _repository;
        private readonly IMapper _mapper;

        public AssignShipmentMemberService(IAssignShipmentMemberRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PagingResponse<AssignShipmentMemberResponse>> GetCourierRunsheet(PagingRequest pagingRequest, Guid memberId)
        {
           return _mapper.Map<PagingResponse<AssignShipmentMemberResponse>>(await _repository.GetCourierRunsheet(pagingRequest, memberId));
        }
    }
}
