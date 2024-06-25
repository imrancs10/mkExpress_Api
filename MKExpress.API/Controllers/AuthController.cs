using MKExpress.API.Contants;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MKExpress.API.Controllers
{
    [Route(StaticValues.APIPrefix)]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IFileUploadService _uploadService;
        public AuthController(ILoginService loginService, IFileUploadService uploadService)
        {
            _loginService = loginService;
            _uploadService = uploadService;
        }

        [AllowAnonymous]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [HttpPost(StaticValues.LoginPath)]
        public async Task<LoginResponse> Login([FromBody] LoginRequest loginRequest)
        {
            return await _loginService.Login(loginRequest);
        }

        [AllowAnonymous]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
        [HttpPut(StaticValues.LoginUserRegisterPath)]
        public async Task<UserResponse> RegisterUser([FromBody] UserRequest request)
        {
            return await _loginService.RegisterUser(request);
        }

        
        [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
        [HttpPost(StaticValues.LoginUserChangePasswordPath)]
        public async Task<bool> ChangePassword([FromBody] PasswordChangeRequest request)
        {
            return await _loginService.ChangePassword(request);
        }

        [AllowAnonymous]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [HttpGet(StaticValues.LoginUserVerifyEmailPath)]
        public async Task<string> VerifyEmail([FromRoute] string token)
        {
            return await _loginService.VerifyEmail(token);
        }

        [AllowAnonymous]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [HttpGet(StaticValues.LoginUserResetPasswordPath)]
        public async Task<string> ResetPassword([FromHeader] string userName)
        {
            return await _loginService.ResetPassword(userName);
        }

       
        [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
        [HttpPost(StaticValues.LoginUserUpdateProfilePath)]
        public async Task<bool> UpdateProfile([FromBody] UserRequest request)
        {
            return await _loginService.UpdateProfile(request);
        }

        [HttpPost(StaticValues.LoginUserDeleteProfilePath)]
        public async Task<bool> DeleteUser([FromRoute] string email)
        {
            return await _loginService.DeleteUser(email);
        }

        
        [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
        [HttpPost(StaticValues.UserBlockPath)]
        public async Task<bool> BlockUser([FromRoute] string email)
        {
            return await _loginService.BlockUser(email);
        }

        
        [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
        [HttpPost(StaticValues.UserAssignRolePath)]
        public async Task<bool> AssignRole([FromRoute] string email, [FromRoute] Guid roleId)
        {
            return await _loginService.AssignRole(email,roleId);
        }

        [AllowAnonymous]
        [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
        [HttpPost(StaticValues.UserResetEmailVerifyCodePath)]
        public async Task<bool> ResetEmailVerificationCode([FromRoute] string email)
        {
            return await _loginService.ResetEmailVerificationCode(email);
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [HttpPost(StaticValues.UserUpdateProfileImagePath)]
        public async Task<string> ResetEmailVerificationCode([FromForm] IFormFile file)
        {
            return await _uploadService.UploadUserProfileImage(file);
        }

    }
}
