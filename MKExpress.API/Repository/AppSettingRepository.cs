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

        public Task<PagingResponse<AppSetting>> GetAll(PagingRequest pagingRequest)
        {
            throw new NotImplementedException();
        }

        public Task<PagingResponse<AppSetting>> Search(SearchPagingRequest searchPagingRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<AppSetting> Update(AppSetting entity)
        {
            var oldData = await _context.AppSettings
                 .Where(x => !x.IsDeleted && x.Id == entity.Id)
                 .FirstOrDefaultAsync() ??
                 throw new BusinessRuleViolationException(StaticValues.ErrorType_RecordNotFound, StaticValues.Error_RecordNotFound);

           oldData.Key = entity.Key;
            oldData.Value = entity.Value;
            oldData.Group= entity.Group;
           var res= _context.Attach(oldData);
           await _context.SaveChangesAsync();
            return res.Entity;
        }
    }
}
