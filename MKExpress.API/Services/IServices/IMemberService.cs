using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;

namespace MKExpress.API.Services
{
    public interface IMemberService:ICrudService<MemberRequest,MemberResponse>
    {
        Task<bool> ActiveDeactivate(Guid memberId);
        Task<bool> ChangeStation(Guid memberId, Guid stationId);
        Task<bool> ChangePassword(PasswordChangeRequest changeRequest);
        Task<bool> ResetPassword(string userId);
        Task<bool> ChangeRole(Guid userId, Guid roleId);
        Task<List<MemberResponse>> GetMemberByRole(string role);
    }
}
