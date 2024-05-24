using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;
using MKExpress.API.Data;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Models;
using MKExpress.API.Repository.IRepository;
using StackExchange.Redis;

namespace MKExpress.API.Repository
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly MKExpressContext _context;
        public UserRoleRepository(MKExpressContext context)
        {
            _context = context;
        }
        public async Task<UserRole> AddRoleAsync(UserRole role)
        {
            if (role == null)
                throw new ArgumentNullException(nameof(role));

            var entity = _context.UserRoles.Add(role);
            if (await _context.SaveChangesAsync() > 0)
                return entity.Entity;
            return role;
        }

        public async Task<bool> DeleteRoleAsync(Guid id)
        {
            var role = await _context.UserRoles
                .Where(x=>x.Id==id &&!x.IsDeleted)
                .FirstOrDefaultAsync() ?? throw new KeyNotFoundException("Role not found");

            role.IsDeleted = true;
            _context.UserRoles.Update(role);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<PagingResponse<UserRole>> GetAllRolesAsync(PagingRequest request)
        {
            var query = _context.UserRoles
                .Where(x=>!x.IsDeleted)
                .OrderBy(x=>x.Name)
                .AsQueryable();

            var totalCount = await query.CountAsync();
            var items = await query
            .Skip((request.PageNo - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return new PagingResponse<UserRole>
            {
                TotalRecords = totalCount,
                Data = items,
                PageNo=request.PageNo,
                PageSize=request.PageSize
            };
        }

        public async Task<UserRole> GetRoleByIdAsync(Guid id)
        {
           return await _context.UserRoles
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<PagingResponse<UserRole>> SearchRolesAsync(SearchPagingRequest request)
        {
            request.SearchTerm = string.IsNullOrEmpty(request.SearchTerm) ? "all" : request.SearchTerm;
            var query = _context.UserRoles
                .Where(x => !x.IsDeleted && (
                request.SearchTerm=="all" || 
                x.Name.Contains(request.SearchTerm) || 
                x.Code.Contains(request.SearchTerm)
                ))
                .OrderBy(x => x.Name)
                .AsQueryable();

            var totalCount = await query.CountAsync();
            var items = await query
            .Skip((request.PageNo - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return new PagingResponse<UserRole>
            {
                TotalRecords = totalCount,
                Data = items,
                PageNo = request.PageNo,
                PageSize = request.PageSize
            };
        }

        public async Task<bool> UpdateRoleAsync(UserRole role)
        {
            if (role == null)
                throw new ArgumentNullException(nameof(role));

            var existingRole = await _context.UserRoles.FirstOrDefaultAsync(r => r.Id == role.Id)??throw new KeyNotFoundException("Role not found");

            _context.UserRoles.Update(existingRole);
           return await _context.SaveChangesAsync()>0;
        }
    }
}
