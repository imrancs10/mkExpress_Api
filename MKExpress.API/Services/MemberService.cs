using AutoMapper;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Models;
using MKExpress.API.Repository;

namespace MKExpress.API.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public MemberService(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }
        public async Task<bool> ActiveDeactivate(Guid memberId)
        {
            return await _memberRepository.ActiveDeactivate(memberId);
        }

        public async Task<MemberResponse> Add(MemberRequest request)
        {
            Member member=_mapper.Map<Member>(request);
            return _mapper.Map<MemberResponse>(await _memberRepository.Add(member));
        }

        public async Task<bool> ChangePassword(PasswordChangeRequest changeRequest)
        {
            return await _memberRepository.ChangePassword(changeRequest);
        }

        public async Task<bool> ChangeRole(Guid userId, Guid roleId)
        {
            return await _memberRepository.ChangeRole(userId, roleId);
        }

        public async Task<bool> ChangeStation(Guid memberId, Guid stationId)
        {
            return await _memberRepository.ChangeStation(memberId,stationId);
        }

        public async Task<int> Delete(Guid id)
        {
            return await _memberRepository.Delete(id);
        }

        public async Task<MemberResponse> Get(Guid id)
        {
            return _mapper.Map<MemberResponse>(await _memberRepository.Get(id));
        }

        public async Task<PagingResponse<MemberResponse>> GetAll(PagingRequest pagingRequest)
        {
            return _mapper.Map<PagingResponse<MemberResponse>>(await _memberRepository.GetAll(pagingRequest));
        }

        public async Task<List<MemberResponse>> GetMemberByRole(string role)
        {
            return _mapper.Map<List<MemberResponse>>(await _memberRepository.GetMemberByRole(role));
        }

        public async Task<bool> ResetPassword(string userId)
        {
            return await _memberRepository.ResetPassword(userId);
        }

        public async Task<PagingResponse<MemberResponse>> Search(SearchPagingRequest searchPagingRequest)
        {
            return _mapper.Map<PagingResponse<MemberResponse>>(await _memberRepository.Search(searchPagingRequest));
        }

        public async Task<MemberResponse> Update(MemberRequest request)
        {
            Member member = _mapper.Map<Member>(request);
            return _mapper.Map<MemberResponse>(await _memberRepository.Update(member));
        }
    }
}
