#nullable enable
using Microsoft.AspNetCore.Identity;
using MKExpress.API.Dto;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Services.Interfaces
{

    public interface IAuthService
    {
        Task<IdentityResult?> CreateIdentityUser(UserRegistrationRequest userRegistrationRequest);
        Task<IdentityResult?> DeleteIdentityUser(string email);

        Task<LoginResponse> GenerateToken(LoginRequest loginRequest);
        Task<LoginResponse> RefreshToken(RefreshTokenRequest refreshTokenRequest);
        Task<Session> GenerateResetToken(CustomerResetPasswordRequest customerResetPasswordRequest);
        Task<IdentityResult> SetNewPassword(CustomerSetNewPassword customerSetNewPassword);

        Task ChangeCustomerPassword(ChangeCustomerPasswordRequest changeCustomerPasswordRequest,
            string customerEmail);

        Task CheckIfUserExists(string email);
        Task LogoutCustomer(RefreshTokenRequest refreshTokenRequest);
        Task AddRoles(List<string> roles);
    }
}