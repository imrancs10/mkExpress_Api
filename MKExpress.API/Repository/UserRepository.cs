using Microsoft.EntityFrameworkCore;
using MKExpress.API.Common;
using MKExpress.API.Contants;
using MKExpress.API.Data;
using MKExpress.API.DTO.Request;
using MKExpress.API.Exceptions;
using MKExpress.API.Middleware;
using MKExpress.API.Models;

namespace MKExpress.API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MKExpressContext _context;

        public UserRepository(MKExpressContext context)
        {
            _context = context;
        }
        public async Task<bool> ActiveDeactivate(string email)
        {
            var oldData = await _context.Users.Where(x => !x.IsDeleted && x.Email == email).FirstOrDefaultAsync()
                                ?? throw new BusinessRuleViolationException(StaticValues.ErrorType_RecordNotFound, StaticValues.ErrorType_RecordNotFound);

            oldData.IsBlocked = !oldData.IsBlocked;
            _context.Attach(oldData);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<User> Add(User request)
        {
            var oldData = await _context.Users.Where(x => !x.IsDeleted && x.Email == request.Email).FirstOrDefaultAsync();
            if (oldData != null) throw new BusinessRuleViolationException(StaticValues.EmailAlreadyExist_Error, StaticValues.EmailAlreadyExist_Message);

            var entity = _context.Users.Add(request);
            if (await _context.SaveChangesAsync() > 0)
            {
                return entity.Entity;
            }
            return default(User);
        }

        public async Task<bool> ChangePassword(PasswordChangeRequest changeRequest)
        {
            if (changeRequest.NewPassword != changeRequest.ConfirmNewPassword)
                throw new BusinessRuleViolationException(StaticValues.NewAndConfirmPasswordNotSame_Error, StaticValues.NewAndConfirmPasswordNotSame_Message);

            var _oldPasswordHash = PasswordHasher.GenerateHash(changeRequest.OldPassword);

            var oldData = await _context.Users.Where(x => !x.IsDeleted && x.Id == JwtMiddleware.GetUserId() && x.Password == _oldPasswordHash).FirstOrDefaultAsync()
                               ?? throw new BusinessRuleViolationException(StaticValues.ErrorType_RecordNotFound, StaticValues.ErrorType_RecordNotFound);

            oldData.Password = PasswordHasher.GenerateHash(changeRequest.NewPassword); ;
            _context.Attach(oldData);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteUser(string userName)
        {
            var oldData = await _context.Users.Where(x => !x.IsDeleted && x.UserName == userName).FirstOrDefaultAsync() ?? throw new BusinessRuleViolationException(StaticValues.ErrorType_RecordNotFound, StaticValues.ErrorType_RecordNotFound);
            oldData.IsDeleted = true;
            _context.Attach(oldData);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ResetPassword(string userId)
        {

            var oldData = await _context.Users.Where(x => !x.IsDeleted && x.Email == userId).FirstOrDefaultAsync()
                               ?? throw new BusinessRuleViolationException(StaticValues.ErrorType_RecordNotFound, StaticValues.ErrorType_RecordNotFound);

            oldData.IsResetCodeInitiated = true;
            oldData.PasswordResetCode = $"{Guid.NewGuid()}-{Guid.NewGuid()}";
            oldData.PasswordResetCodeExpireOn = DateTime.Now.AddHours(2);
            _context.Attach(oldData);

            if (await _context.SaveChangesAsync() > 0)
            {
                //string body = await _mailService.GetMailTemplete(Constants.EmailTemplateEnum.ResetPassword);
                //MailRequest mailRequest = new()
                //{
                //    Subject = "IMK Express-Password Reset",
                //    ToEmail = oldData.Email,
                //    Body = body
                //};
                //await _mailService.SendEmailAsync(mailRequest);
                return true;
            }
            return false;
        }

        public async Task<string?> UpdateProfileImagePath(string path)
        {
            var userId = JwtMiddleware.GetUserId();
            string oldImagePath;
            var user = await _context.Users.Where(x => !x.IsDeleted && x.Id == userId).FirstOrDefaultAsync();
            if (user == null)
                return string.Empty;

            oldImagePath = user?.ProfileImagePath ?? string.Empty;
            user.ProfileImagePath = path;

            _context.Update(user);
            if (await _context.SaveChangesAsync() > 0)
                return oldImagePath;
            return "ImageNotSaved";

        }
    }
}
