using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.EntityFrameworkCore;
using MKExpress.API.Contants;
using MKExpress.API.Data;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Exceptions;
using MKExpress.API.Models;
using MKExpress.API.Repository.IRepository;

namespace MKExpress.API.Repository
{
    public class AppSettingRepository : IAppSettingRepository
    {
        private readonly MKExpressContext _context;
        public AppSettingRepository(MKExpressContext context)
        {
            _context = context;
        }
        public async Task<AppSetting> Add(AppSetting request)
        {
            request.Key = request.Key?.Trim()?.ToLower();
            var entity = _context.Add(request);
            entity.State = EntityState.Added;
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<int> Delete(Guid Id)
        {
            var oldData = await _context.AppSettings
                .Where(x => !x.IsDeleted && x.Id == Id)
                .FirstOrDefaultAsync()??
                throw new BusinessRuleViolationException(StaticValues.ErrorType_RecordNotFound,StaticValues.Error_RecordNotFound);

            if(!oldData.AllowDelete)
                throw new BusinessRuleViolationException(StaticValues.ErrorType_DeleteNotAllowed, StaticValues.Message_DeleteNotAllowed);

            oldData.IsDeleted = true;
            _context.Attach(oldData);
            return await _context.SaveChangesAsync();

        }

        public async Task<AppSetting> Get(Guid Id)
        {
            return await _context.AppSettings
                 .Where(x => !x.IsDeleted && x.Id == Id)
                 .FirstOrDefaultAsync()??new();
        }

        public async Task<PagingResponse<AppSetting>> GetAll(PagingRequest pagingRequest)
        {
            var data = _context.AppSettings
                .Include(x=>x.AppSettingGroup)
           .Where(x => !x.IsDeleted)
              .OrderBy(x => x.AppSettingGroup.Name)
              .AsQueryable();
            PagingResponse<AppSetting> pagingResponse = new()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = await data.Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1)).Take(pagingRequest.PageSize).ToListAsync(),
                TotalRecords = await data.CountAsync()
            };
            return pagingResponse;
        }
        public async Task<List<AppSettingGroup>> GetAllAppSettingGroup()
        {
            return await _context.AppSettingGroups
           .Where(x => !x.IsDeleted)
              .ToListAsync();
        }

        public async Task<T> GetAppSettingValueByKey<T>(string key)
        {
            var data = await _context.AppSettings
                .Where(x => !x.IsDeleted && x.Key == key.ToLower().Trim())
                .FirstOrDefaultAsync()??throw new BusinessRuleViolationException(StaticValues.DataNotFoundError,StaticValues.Message_AppSettingKeyNotFound);

           return (T)Convert.ChangeType(data.Value, typeof(T));

        }

        public async Task<PagingResponse<AppSetting>> Search(SearchPagingRequest searchPagingRequest)
        {
            var searchTerm = string.IsNullOrEmpty(searchPagingRequest.SearchTerm) ? string.Empty : searchPagingRequest.SearchTerm;
            var data = _context.AppSettings
          .Where(x => !x.IsDeleted && (x.Key.Contains(searchTerm) || x.Value.Contains(searchTerm)) || x.AppSettingGroup.Name.Contains(searchTerm))
             .OrderBy(x => x.AppSettingGroup.Name)
             .AsQueryable();
            PagingResponse<AppSetting> pagingResponse = new()
            {
                PageNo = searchPagingRequest.PageNo,
                PageSize = searchPagingRequest.PageSize,
                Data = await data.Skip(searchPagingRequest.PageSize * (searchPagingRequest.PageNo - 1)).Take(searchPagingRequest.PageSize).ToListAsync(),
                TotalRecords = await data.CountAsync()
            };
            return pagingResponse;
        }

        public async Task<AppSetting> Update(AppSetting entity)
        {
            var oldData = await _context.AppSettings
                 .Where(x => !x.IsDeleted && x.Id == entity.Id)
                 .FirstOrDefaultAsync() ??
                 throw new BusinessRuleViolationException(StaticValues.ErrorType_RecordNotFound, StaticValues.Error_RecordNotFound);

            if (!oldData.AllowUpdate)
                throw new BusinessRuleViolationException(StaticValues.ErrorType_UpdateNotAllowed, StaticValues.Message_UpdateNotAllowed);

            oldData.DataType = entity.DataType;
            oldData.Value = entity.Value;
            oldData.GroupId= entity.GroupId;

           var res= _context.Attach(oldData);
           await _context.SaveChangesAsync();
            return res.Entity;
        }
    }
}
