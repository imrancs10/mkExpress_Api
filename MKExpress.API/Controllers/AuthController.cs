using MKExpress.API.Contants;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MKExpress.API.Controllers
{
    [Route(StaticValues.APIPrefix)]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILoginService _loginService;
        public AuthController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [HttpPost(StaticValues.LoginPath)]
        public async Task<LoginResponse> Login([FromBody] LoginRequest loginRequest)
        {
            return await _loginService.Login(loginRequest);
        }

        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
        [HttpPut(StaticValues.LoginUserRegisterPath)]
        public async Task<UserResponse> RegisterUser([FromBody] UserRequest request)
        {
            return await _loginService.RegisterUser(request);
        }

        [Authorize]
        [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
        [HttpPost(StaticValues.LoginUserChangePasswordPath)]
        public async Task<bool> ChangePassword([FromBody] PasswordChangeRequest request)
        {
            return await _loginService.ChangePassword(request);
        }

        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [HttpGet(StaticValues.LoginUserVerifyEmailPath)]
        public async Task<string> VerifyEmail([FromRoute] string token)
        {
            return await _loginService.VerifyEmail(token);
        }

        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [HttpGet(StaticValues.LoginUserResetPasswordPath)]
        public async Task<string> ResetPassword([FromHeader] string userName)
        {
            return await _loginService.ResetPassword(userName);
        }

        [Authorize]
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

        [Authorize]
        [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
        [HttpPost(StaticValues.UserBlockPath)]
        public async Task<bool> BlockUser([FromRoute] string email)
        {
            return await _loginService.BlockUser(email);
        }

        [Authorize]
        [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
        [HttpPost(StaticValues.UserAssignRolePath)]
        public async Task<bool> AssignRole([FromRoute] string email, [FromRoute] string role)
        {
            return await _loginService.AssignRole(email,role);
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
        [HttpPost(StaticValues.UserResetEmailVerifyCodePath)]
        public async Task<bool> ResetEmailVerificationCode([FromRoute] string email)
        {
            return await _loginService.ResetEmailVerificationCode(email);
        }

    }
}
