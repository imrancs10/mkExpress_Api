#nullable enable
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MKExpress.API.Config;
using MKExpress.API.Constants;
using MKExpress.API.Dto;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Exceptions;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using MKExpress.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MKExpress.API.Services
{

    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly TokenConfig _tokenConfig;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;

        public AuthService(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            TokenConfig tokenConfig, RoleManager<IdentityRole> roleManager, IUserService userService, IUserRepository userRepository, IMapper mapper)
        {
            _tokenConfig = tokenConfig;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userService = userService;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IdentityResult?> CreateIdentityUser(UserRegistrationRequest userRegistrationRequest)
        {
            // check if user exists

            var userExists = await _userManager.FindByNameAsync(userRegistrationRequest.Email);
            if (userExists != null)
                //throw new BusinessRuleViolationException(
                //    StaticValues.UserWithSameEmailExists,
                //    StaticValues.ErrorUserRegister
                //);
                return default;

            IdentityUser iUser = new IdentityUser()
            {
                Email = userRegistrationRequest.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = userRegistrationRequest.Email
            };

            // create identity user - asp_net_users
            var result = await _userManager.CreateAsync(iUser, userRegistrationRequest.Password);
            await _userService.CreateUser(userRegistrationRequest);
            if (!result.Succeeded)
                throw new BusinessRuleViolationException(
                    StaticValues.RegistrationFailed,
                    result.Errors.Aggregate("", (acc, item) => string.Concat(acc, item.Description))
                );

            // assign given role
            await _userManager.AddToRoleAsync(iUser, userRegistrationRequest.Role);

            return result;
        }

        public async Task<LoginResponse> GenerateToken(LoginRequest loginRequest)
        {
            try
            {
                var iUser = await GetIdentityUser(loginRequest.Username);
                var result = await _signInManager.PasswordSignInAsync(
                    loginRequest.Username,
                    loginRequest.Password,
                    false,
                    false
                );
                if (!result.Succeeded) throw new UnauthorizedException(StaticValues.ErrorIncorrectCredentials);

                var user = _mapper.Map<User>(await _userService.GetUser(loginRequest.Username));
                return await GetAuthTokens(user, iUser);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<LoginResponse> RefreshToken(RefreshTokenRequest refreshTokenRequest)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var decodedAccessToken = handler.ReadJwtToken(refreshTokenRequest.AccessToken);
            var email = decodedAccessToken.Claims.First(claim => claim.Type == StaticValues.EmailClaim).Value;
            var iUser = await GetIdentityUser(email);
            var user = _mapper.Map<User>(await _userService.GetUser(email));
            return await GetAuthTokens(user, iUser);
        }

        public async Task<Session> GenerateResetToken(CustomerResetPasswordRequest customerResetPasswordRequest)
        {
            var token = Guid.NewGuid().ToString();
            var user = await GetIdentityUser(customerResetPasswordRequest.Email);
            CustomerSession session = new CustomerSession()
            {
                Email = user.Email,
                OtpVerified = false,
            };
            return new Session
            {
                SessionId = token
            };
        }

        public async Task<IdentityResult> SetNewPassword(CustomerSetNewPassword customerSetNewPassword)
        {
            var email = string.Empty;
            var user = await GetIdentityUser(email);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, customerSetNewPassword.Password);
            if (!result.Succeeded) throw new UnauthorizedException(result.ToString());

            /*Customer? customer = await _customerRepository.GetCustomerByEmail(session.Email);
            customer.PasswordResetDate = DateTime.UtcNow.Date;
            await _customerRepository.UpdateCustomer(customer);
            _cache.RemoveAsync(customerSetNewPassword.Token);*/
            return result;
        }

        public async Task ChangeCustomerPassword(
            ChangeCustomerPasswordRequest changeCustomerPasswordRequest,
            string customerEmail
        )
        {
            var user = await GetIdentityUser(customerEmail);
            var identityResult = await _userManager.ChangePasswordAsync(
                user,
                changeCustomerPasswordRequest.CurrentPassword,
                changeCustomerPasswordRequest.NewPassword
            );
            if (!identityResult.Succeeded)
                throw new BusinessRuleViolationException(
                    StaticValues.ChangedPasswordFailed,
                    StaticValues.ErrorChangePassword,
                    identityResult.Errors
                );

            /*Customer? customer = await _customerRepository.GetCustomerByEmail(customerEmail);
            customer.PasswordResetDate = DateTime.UtcNow.Date;
            await _customerRepository.UpdateCustomer(customer);*/
        }

        public async Task CheckIfUserExists(string email)
        {
            // check if user exists
            var userExists = await _userManager.FindByNameAsync(email);
            if (userExists != null)
                throw new BusinessRuleViolationException(
                    StaticValues.UserWithSameEmailExists,
                    StaticValues.ErrorUserRegister
                );
        }

        public async Task LogoutCustomer(RefreshTokenRequest refreshTokenRequest)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var decodedAccessToken = handler.ReadJwtToken(refreshTokenRequest.AccessToken);
            var email = decodedAccessToken.Claims.First(claim => claim.Type == StaticValues.EmailClaim).Value;
            /*Account account = await GetUserAccount(email);
            await _cache.RemoveAsync($"{account.Id}_{StaticValues.RefreshTokenKey}");*/

            await _signInManager.SignOutAsync();
            //Log.Information("User with account Id - {AccountId} logged out.", account.Id);
        }

        public async Task AddRoles(List<string> roles)
        {
            var notExistedRoles = new List<string>();
            foreach (var role in roles)
            {
                if (await _roleManager.RoleExistsAsync(role)) continue;
                notExistedRoles.Add(role);
                await _roleManager.CreateAsync(new IdentityRole { Name = role });
            }
            await _userRepository.AddRoles(notExistedRoles);
        }

        private async Task<IdentityUser> GetIdentityUser(string email)
        {
            var user = await _userManager.FindByNameAsync(email);
            if (user == null)
                throw new UnauthorizedException(StaticValues.ErrorIncorrectCredentials);

            return user;
        }

        private async Task<LoginResponse> GetAuthTokens(User user, IdentityUser iUser)
        {
            IList<string>? userRoles = await _userManager.GetRolesAsync(iUser);
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
            new Claim(StaticValues.EmailClaim, iUser.UserName),
            new Claim(StaticValues.UserIdClaim,user.EmployeeId==null?user.Id.ToString():user.EmployeeId.ToString()),
            new Claim(StaticValues.UserFirstnameClaim, user.FirstName),
            new Claim(StaticValues.UserLastnameClaim, user.LastName),
            new Claim(StaticValues.UserFullnameClaim, user.Name),
            new Claim(StaticValues.UserUsernameClaim, user.UserName),
            new Claim(StaticValues.UserRoleClaim, string.Join(",",userRoles))
        });
            foreach (var userRole in userRoles) claimsIdentity.AddClaim(new Claim(StaticValues.UserTypeClaim, userRole));

            var token = _tokenConfig.GetJwtToken(claimsIdentity);
            try
            {
                var refreshToken = TokenConfig.GetRefreshToken();
                LoginResponse loginResponse = new LoginResponse()
                {
                    AccessToken = token,
                    RefreshToken = refreshToken
                };
                return loginResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IdentityResult?> DeleteIdentityUser(string email)
        {
            var userExists = await _userManager.FindByNameAsync(email);
            if (userExists != null)
            {
                return await _userManager.DeleteAsync(userExists);
            }
            return default;
        }
    }
}