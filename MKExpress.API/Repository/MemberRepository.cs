using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.EntityFrameworkCore;
using MKExpress.API.Common;
using MKExpress.API.Contants;
using MKExpress.API.Data;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Exceptions;
using MKExpress.API.Models;
using MKExpress.API.Repository.IRepository;
using MKExpress.API.Services.Interfaces;

namespace MKExpress.API.Repository
{
    public class MemberRepository : IMemberRepository
    {
        private readonly MKExpressContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IMasterDataService _masterDataService;

        public MemberRepository(MKExpressContext context, IUserRepository userRepository, IMapper mapper,IMasterDataService masterDataService)
        {
            _context = context;
            _userRepository = userRepository;
            _mapper = mapper;
            _masterDataService = masterDataService; 
        }

        public async Task<bool> ActiveDeactivate(Guid memberId)
        {
            var oldData = await _context.Members.Where(x => !x.IsDeleted && x.Id == memberId).FirstOrDefaultAsync()
                                ?? throw new BusinessRuleViolationException(StaticValues.ErrorType_RecordNotFound, StaticValues.ErrorType_RecordNotFound);
            var trans = _context.Database.BeginTransaction();
            oldData.IsActive = !oldData.IsActive;
            _context.Attach(oldData);
            if (await _context.SaveChangesAsync() > 0)
            {
                if (await _userRepository.ActiveDeactivate(oldData.Email))
                {
                    trans.Commit();
                    return true;
                }
            }
            trans.Rollback();
            return false;
        }

        public async Task<Member> Add(Member request, string password)
        {
            var oldData = await _context.Members.Where(x => x.Email == request.Email).FirstOrDefaultAsync();
            if (oldData != null) throw new BusinessRuleViolationException(StaticValues.EmailAlreadyExist_Error, StaticValues.EmailAlreadyExist_Message);
            var trans = _context.Database.BeginTransaction();
            request.IsActive = true;
            var entity = _context.Members.Add(request);
            if (await _context.SaveChangesAsync() > 0)
            {
                var role=await _masterDataService.Get(request.RoleId);
                if(role==null)
                {
                    trans.Rollback();
                    return default(Member);
                }
                var user = _mapper.Map<User>(request);
                user.Password=PasswordHasher.GenerateHash(password);
                user.RoleId = Guid.NewGuid();
                var res = await _userRepository.Add(user);
                if (res?.Id is Guid)
                {
                    trans.Commit();
                    return entity.Entity;
                }
            }

            trans.Rollback();
            return default(Member);

        }

        public async Task<bool> ChangePassword(PasswordChangeRequest changeRequest)
        {
            return await _userRepository.ChangePassword(changeRequest);
        }

        public async Task<bool> ChangeStation(Guid memberId, Guid stationId)
        {
            var oldData = await _context.Members.Where(x => !x.IsDeleted && x.Id == memberId).FirstOrDefaultAsync();
            if (oldData == null) throw new BusinessRuleViolationException(StaticValues.ErrorType_RecordNotFound, StaticValues.Error_RecordNotFound);

            oldData.StationId = stationId;
            return await _context.SaveChangesAsync()>0;
        }

        public async Task<bool> ChangeRole(Guid userId, Guid roleId)
        {
            var oldData = await _context.Members.Where(x => x.Id == userId && !x.IsDeleted).FirstOrDefaultAsync()??throw new BusinessRuleViolationException(StaticValues.ErrorType_RecordNotFound, StaticValues.Error_RecordNotFound);
            
            oldData.RoleId = roleId;
            _context.Attach(oldData);
            return await _context.SaveChangesAsync()>0;
        }

        public async Task<int> Delete(Guid Id)
        {
            var oldData = await _context.Members.Where(x => !x.IsDeleted && x.Id == Id).FirstOrDefaultAsync();
            if (oldData == null) throw new BusinessRuleViolationException(StaticValues.EmailAlreadyExist_Error, StaticValues.EmailAlreadyExist_Message);

            oldData.IsDeleted=true; 
            return await _context.SaveChangesAsync();

        }

        public async Task<Member> Get(Guid Id)
        {
            return await _context.Members
                .Include(x => x.Role)
                .Include(x => x.Station)
                   .Where(x => !x.IsDeleted && x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<PagingResponse<Member>> GetAll(PagingRequest pagingRequest)
        {
            var data = _context.Members
                .Include(x => x.Role)
                .Include(x => x.Station)
               .Where(x => !x.IsDeleted)
               .OrderBy(x => x.FirstName)
               .AsQueryable();
            PagingResponse<Member> response = new()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = await data.Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1))
                .Take(pagingRequest.PageSize)
                .ToListAsync(),
                TotalRecords = await data.CountAsync()
            };
            return response;
        }

        public async Task<bool> ResetPassword(string userId)
        {
           return await _userRepository.ResetPassword(userId);
        }

        public async Task<PagingResponse<Member>> Search(SearchPagingRequest pagingRequest)
        {
            string searchTerm = string.IsNullOrEmpty(pagingRequest.SearchTerm) ? string.Empty : pagingRequest.SearchTerm.ToLower();
            var data = _context.Members
                .Include(x => x.Role)
                .Include(x => x.Station)
                   .Where(x => !x.IsDeleted && (
                   x.FirstName.Contains(searchTerm) ||
                   x.LastName.Contains(searchTerm) ||
                   x.IdNumber.Contains(searchTerm) ||
                   x.Mobile.Contains(searchTerm) ||
                   x.Phone.Contains(searchTerm) ||
                   x.Role.Value.Contains(searchTerm) ||
                   x.Station.Value.Contains(searchTerm)
               ))
               .OrderBy(x => x.FirstName)
               .AsQueryable();
            PagingResponse<Member> response = new()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = await data.Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1))
                .Take(pagingRequest.PageSize)
                .ToListAsync(),
                TotalRecords = await data.CountAsync()
            };
            return response;
        }

        public async Task<Member> Update(Member request)
        {
            var oldData = await _context.Members.Where(x => !x.IsDeleted && x.Id == request.Id).FirstOrDefaultAsync();
            if (oldData == null) throw new BusinessRuleViolationException(StaticValues.ErrorType_RecordNotFound, StaticValues.ErrorType_RecordNotFound);

            oldData.FirstName= request.FirstName;
            oldData.LastName= request.LastName;
            oldData.Phone= request.Phone;   
            oldData.Role= request.Role;
            oldData.IdNumber= request.IdNumber;
            oldData.Gender= request.Gender;
            oldData.Mobile= request.Mobile;
            var trans = _context.Database.BeginTransaction();
            _context.Attach(oldData);
            if (await _context.SaveChangesAsync() > 0)
            {
                if (await _userRepository.DeleteUser(oldData.Email))
                {
                    trans.Commit();
                    return oldData;
                }
            }
            trans.Rollback();
            return default;

        }

        public async Task<List<Member>> GetMemberByRole(string role)
        {
            return await _context
                .Members
                .Include(x=>x.Role)
                .Where(x=>!x.IsDeleted && x.Role.Value.ToLower()==role.ToLower())
                .OrderBy(x=>x.FirstName)
                .ToListAsync();
        }
    }
}
