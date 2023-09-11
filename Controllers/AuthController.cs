using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MKExpress.API.Constants;
using MKExpress.API.Dto;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Controllers
{

    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [HttpPost(StaticValues.SignUpUserPath)]
        public async Task<ActionResult<UserResponse>> RegisterUser([FromBody] UserRegistrationRequest userRegistrationRequest)
        {
            await _authService.CreateIdentityUser(userRegistrationRequest);
            return StatusCode(StatusCodes.Status201Created,
                await _userService.CreateUser(userRegistrationRequest));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [HttpDelete(StaticValues.DeleteUserPath)]
        public async Task<IdentityResult> DeleteUser([FromQuery] string email)
        {
            return await _authService.DeleteIdentityUser(email);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [HttpPost(StaticValues.LoginPath)]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest loginRequest)
        {
            var loginResponse = await _authService.GenerateToken(loginRequest);
            return StatusCode(StatusCodes.Status200OK, loginResponse);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [HttpPost(StaticValues.RefreshTokenPath)]
        public async Task<ActionResult<LoginResponse>> RefreshToken([FromBody] RefreshTokenRequest refreshTokenRequest)
        {
            var loginResponse = await _authService.RefreshToken(refreshTokenRequest);
            return StatusCode(StatusCodes.Status200OK, loginResponse);
        }

        [HttpPost(StaticValues.UsersResetPasswordTokenPath)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Session>> ResetPasswordToken(
            [FromBody] CustomerResetPasswordRequest customerResetPasswordRequest)
        {
            var session = await _authService.GenerateResetToken(customerResetPasswordRequest);
            return StatusCode(StatusCodes.Status200OK, session);
        }

        [HttpPost(StaticValues.UsersPasswordPath)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IdentityResult>> SetNewPassword(
            [FromBody] CustomerSetNewPassword customerSetNewPassword)
        {
            var resetStatus = await _authService.SetNewPassword(customerSetNewPassword);
            return StatusCode(StatusCodes.Status200OK, resetStatus);
        }

        [HttpPut(StaticValues.UsersPasswordPath)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task ChangeUserPassword(
            [FromBody] ChangeCustomerPasswordRequest changeCustomerPasswordRequest,
            [FromHeader(Name = StaticValues.EmailHeader)]
        string customerEmail
        )
        {
            await _authService.ChangeCustomerPassword(changeCustomerPasswordRequest, customerEmail);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [HttpPost(StaticValues.LogoutUsersPath)]
        public async Task<ActionResult> LogoutUser([FromBody] RefreshTokenRequest refreshTokenRequest)
        {
            await _authService.LogoutCustomer(refreshTokenRequest);
            return StatusCode(StatusCodes.Status200OK);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [HttpPut(StaticValues.UserRolePath)]
        public async Task<ActionResult> AddRoles([FromBody] List<string> roles)
        {
            await _authService.AddRoles(roles);
            return StatusCode(StatusCodes.Status200OK);
        }


        [ProducesResponseType(typeof(List<UserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [HttpGet(StaticValues.UserPath)]
        public async Task<List<UserResponse>> GetUsers()
        {
            return await _userService.GetUsers();
        }
    }
}