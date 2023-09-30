using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;

namespace MKExpress.API.Repository.IRepository
{
    public interface IMemberRepository
    {
        Task<Member> Add(Member entity,string password);
        Task<Member> Update(Member entity);
        Task<int> Delete(Guid Id);
        Task<Member> Get(Guid Id);
        Task<PagingResponse<Member>> GetAll(PagingRequest pagingRequest);
        Task<PagingResponse<Member>> Search(SearchPagingRequest searchPagingRequest);
        Task<bool> ActiveDeactivate(Guid memberId);
        Task<bool> ChangeStation(Guid memberId,Guid stationId);
        Task<bool> ChangePassword(PasswordChangeRequest changeRequest);
        Task<bool> ResetPassword(string userId);
    }
}
