using AutoMapper;
using MKExpress.API.Contants;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Exceptions;
using MKExpress.API.Models;
using MKExpress.API.Repository;

namespace MKExpress.API.Services
{
    public class LoginService(IUserRoleMenuMapperRepository roleMenuMapperRepository, ILoginRepository loginRepository, IMapper mapper, IConfiguration configuration, ICommonService commonService) : ILoginService
    {
        private readonly ILoginRepository _loginRepository = loginRepository;
        private readonly IUserRoleMenuMapperRepository _roleMenuMapperRepository = roleMenuMapperRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IConfiguration _configuration = configuration;
        private readonly ICommonService _commonService = commonService;

        public async Task<bool> AssignRole(string email, Guid roleId)
        {
            return await _loginRepository.AssignRole(email, roleId);
        }

        public async Task<bool> BlockUser(string email)
        {
            return await _loginRepository.BlockUser(email);
        }

        public async Task<bool> ChangePassword(PasswordChangeRequest request)
        {
            request.OldPassword =_commonService.GeneratePasswordHash(request.OldPassword);
            request.NewPassword = _commonService.GeneratePasswordHash(request.NewPassword);
            request.ConfirmNewPassword = _commonService.GeneratePasswordHash(request.ConfirmNewPassword);
            return await _loginRepository.ChangePassword(request);
        }

        public async Task<bool> DeleteUser(string email)
        {
            return await _loginRepository.DeleteUser(email);
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            if (request == null)
            {
                throw new BusinessRuleViolationException(StaticValues.ErrorType_NoDataSupplied, StaticValues.ErrorType_NoDataSupplied);
            }

            if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
            {
                throw new BusinessRuleViolationException(StaticValues.ErrorType_InvalidDataSupplied, StaticValues.ErrorType_InvalidDataSupplied);
            }
           
            request.Password=_commonService.GeneratePasswordHash(request.Password);
            LoginResponse response = new()
            {
                UserResponse = _mapper.Map<UserResponse>(await _loginRepository.Login(request))
            };

            if (response?.UserResponse?.Id == null)
            {
                throw new UnauthorizedException();
            }
            var permissions = await _roleMenuMapperRepository.GetByRoleId(response.UserResponse.RoleId);

            if (permissions?.Count<1)
            {
                throw new BusinessRuleViolationException(StaticValues.Error_PermissionNotMapped,StaticValues.Message_PermissionNotMapped);
            }
            response.UserResponse.Permissions = _mapper.Map<List<UserRoleMenuMapperResponse>>(permissions);
            response.AccessToken = Utility.Utility.GenerateAccessToken(response);
            response.IsAuthenticated = true;
            return response;
        }

        public async Task<UserResponse> RegisterUser(UserRequest request)
        {
            if (await _loginRepository.IsUserExist(request.Email))
            {
                throw new BusinessRuleViolationException(StaticValues.ErrorType_AlreadyExist, StaticValues.Error_EmailAlreadyRegistered);
            }
            User user = _mapper.Map<User>(request);
            user.IsEmailVerified = _configuration.GetValue<int>("EnableEmailVerification", 0) == 0;
            user.Password = _commonService.GeneratePasswordHash(request?.Password);
            user.EmailVerificationCode = Guid.NewGuid().ToString() + Guid.NewGuid().ToString();
            user.EmailVerificationCodeExpireOn = DateTime.Now.AddHours(48);
            var res = _mapper.Map<UserResponse>(await _loginRepository.RegisterUser(user));
            if (res.Id != null)
            {
                //var emailBody = await _mailService.GetMailTemplete(Constants.EmailTemplateEnum.EmailVerification);

                //MailRequest mailRequest = new()
                //{
                //    ToEmail = request.Email,
                //    Body = emailBody,
                //    Subject = "Email verification | MK Express"
                //};
                // _mailService.SendEmailAsync(mailRequest);
            }
            return res;
        }

        public async Task<bool> ResetEmailVerificationCode(string email)
        {
            return await _loginRepository.ResetEmailVerificationCode(email);
        }

        public async Task<string> ResetPassword(string userName)
        {
            var result = await _loginRepository.ResetPassword(userName);
            if (result)
            {

            }
            return ValidationMessage.ResetPasswordEmailSentSuccess;
        }

        public async Task<bool> UpdateProfile(UserRequest request)
        {
            User user = _mapper.Map<User>(request);
            return await _loginRepository.UpdateProfile(user);
        }

        public async Task<string> VerifyEmail(string token)
        {
            var result = await _loginRepository.VerifyEmail(token);
            return result ? ValidationMessage.EmailVerificationSuccess : ValidationMessage.EmailVerificationFail;
        }
    }
}
