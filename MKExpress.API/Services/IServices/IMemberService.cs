using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Services.Interfaces;

namespace MKExpress.API.Services.IServices
{
    public interface IMemberService:ICrudService<MemberRequest,MemberResponse>
    {
        Task<bool> ActiveDeactivate(Guid memberId);
        Task<bool> ChangeStation(Guid memberId, Guid stationId);
        Task<bool> ChangePassword(PasswordChangeRequest changeRequest);
        Task<bool> ResetPassword(string userId);
    }
}
