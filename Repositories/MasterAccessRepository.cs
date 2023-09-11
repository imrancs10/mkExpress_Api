using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MKExpress.API.Constants;
using MKExpress.API.Data;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Exceptions;
using MKExpress.API.Models;
using MKExpress.API.Repositories;
using MKExpress.Web.API.Dto.Request.MasterAccess;
using MKExpress.Web.API.Models;
using MKExpress.Web.API.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.Web.API.Repositories
{
    public class MasterAccessRepository : IMasterAccessRepository
    {
        private readonly MKExpressDbContext _context;
        public MasterAccessRepository(MKExpressDbContext context)
        {
            _context = context;
        }
        public async Task<MasterAccess> Add(MasterAccess masterAccess)
        {
            var oldMasterAccesses = await _context.MasterAccesses.Where(x => x.EmployeeId == masterAccess.EmployeeId && x.RoleId == masterAccess.RoleId && !x.IsDeleted).CountAsync();
            if (oldMasterAccesses > 0)
            {
                throw new BusinessRuleViolationException(StaticValues.RecordAlreadyExistError, StaticValues.RecordAlreadyExistMessage("Master Access"));
            }
            var entity = _context.MasterAccesses.Attach(masterAccess);
            entity.State = EntityState.Added;
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<bool> ChangePassword(MasterAccessChangePasswordRequest masterAccessChangePasswordRequest)
        {
            var oldData = await _context.MasterAccesses
                .Where(x => !x.IsDeleted && x.Id == masterAccessChangePasswordRequest.Id && x.Password == masterAccessChangePasswordRequest.OldPassword)
                .FirstOrDefaultAsync();
            if (oldData == null) return false;

            oldData.Password = masterAccessChangePasswordRequest.NewPassword;
            var entity = _context.Attach(oldData);
            entity.State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;

        }

        public async Task<int> Delete(int Id)
        {
            var oldData = await _context.MasterAccesses
               .Where(x => x.Id == Id)
               .FirstOrDefaultAsync();
            if (oldData == null)
            {
                throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);
            }
            if (oldData.IsDeleted)
            {
                throw new BusinessRuleViolationException(StaticValues.RecordAlreadyDeletedError, StaticValues.RecordAlreadyDeletedMessage);
            }
            oldData.IsDeleted = true;
            var entity = _context.MasterAccesses.Update(oldData);
            entity.State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public async Task<MasterAccess> Get(int Id)
        {
            return await _context.MasterAccesses
                   .Include(x => x.Employee)
                .Include(x => x.UserRole)
                .Include(x => x.MasterAccessDetails)
                .ThenInclude(x => x.MasterMenu)
                .Where(x => x.Id == Id)
                .FirstOrDefaultAsync();
        }

        public async Task<PagingResponse<MasterAccess>> GetAll(PagingRequest pagingRequest)
        {
            var data = await _context.MasterAccesses
                .Include(x => x.Employee)
                .Include(x => x.UserRole)
              .Include(x => x.MasterAccessDetails)
                .ThenInclude(x => x.MasterMenu)
                .Where(x => !x.IsDeleted)
                .OrderBy(x => x.Employee.FirstName)
                .ToListAsync();
            PagingResponse<MasterAccess> pagingResponse = new PagingResponse<MasterAccess>()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = data.Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1)).Take(pagingRequest.PageSize).ToList(),
                TotalRecords = data.Count()
            };
            return pagingResponse;
        }

        public async Task<List<MasterMenu>> GetMenus()
        {
            return await _context.MasterMenus.Where(x => !x.IsDeleted).OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<bool> IsUsernameExist(string username)
        {
            return _context.MasterAccesses.Where(x => x.Username == username && !x.IsDeleted).Any();
        }

        public async Task<MasterAccess> MasterAccessLogin(MasterAccessLoginRequest accessLoginRequest)
        {
            return await _context.MasterAccesses
                 .Include(x => x.UserRole)
                 .Include(x => x.Employee)
                         .Include(x => x.MasterAccessDetails)
                         .ThenInclude(x => x.MasterMenu)
                         .Where(x => !x.IsDeleted && x.Username == accessLoginRequest.UserName && x.Password == accessLoginRequest.Password)
                         .FirstOrDefaultAsync() ?? new MasterAccess();
        }

        public async Task<PagingResponse<MasterAccess>> Search(SearchPagingRequest searchPagingRequest)
        {
            string searchTerm = string.IsNullOrEmpty(searchPagingRequest.SearchTerm) ? string.Empty : searchPagingRequest.SearchTerm.ToLower();
            var data = await _context.MasterAccesses
                .Include(x => x.MasterAccessDetails)
                .ThenInclude(x => x.MasterMenu)
                   .Include(x => x.Employee)
                .Include(x => x.UserRole)
                .Where(x => !x.IsDeleted && (
                        string.IsNullOrEmpty(searchTerm) ||
                        (x.Employee.FirstName + " " + x.Employee.LastName).Contains(searchTerm) ||
                        x.Username.Contains(searchTerm))
                    )
                .OrderBy(x => x.Employee.FirstName)
                    .ToListAsync();
            //GetOrderCountByContactNo
            var filterData = data.Skip(searchPagingRequest.PageSize * (searchPagingRequest.PageNo - 1)).Take(searchPagingRequest.PageSize).ToList();

            PagingResponse<MasterAccess> pagingResponse = new PagingResponse<MasterAccess>()
            {
                PageNo = searchPagingRequest.PageNo,
                PageSize = searchPagingRequest.PageSize,
                Data = filterData,
                TotalRecords = data.Count()
            };
            return pagingResponse;
        }

        public async Task<MasterAccess> Update(MasterAccess entity)
        {
            var oldData = await _context.MasterAccesses
                 .Include(x => x.MasterAccessDetails)
                 .Where(x => !x.IsDeleted && x.Id == entity.Id)
                 .FirstOrDefaultAsync();
            if (oldData == null) return new MasterAccess();
            if (oldData?.MasterAccessDetails?.Count > 0)
            {
                _context.MasterAccessDetail.RemoveRange(oldData.MasterAccessDetails);
                await _context.SaveChangesAsync();
            }
            oldData.EmployeeId = entity.EmployeeId;
            oldData.RoleId = entity.RoleId;
            oldData.MasterAccessDetails = entity.MasterAccessDetails;
            if (entity.Password != null && entity.Password?.Trim() != "")
            {
                oldData.Password = entity.Password;
            }

            EntityEntry<MasterAccess> oldMasterAccess = _context.Attach(oldData);
            oldMasterAccess.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return oldMasterAccess.Entity;
        }
    }
}
