using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MKExpress.API.Contants;
using MKExpress.API.Data;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Exceptions;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories
{
    public class MasterDataRepository : IMasterDataRepository
    {
        private readonly MKExpressContext _context;

        public MasterDataRepository(MKExpressContext context)
        {
            _context = context;
        }

        public async Task<MasterData> Add(MasterData masterData)
        {
            var oldData = await _context.MasterDatas.Where(x => !x.IsDeleted &&
              x.MasterDataType == masterData.MasterDataType &&
              x.Value == masterData.Value).CountAsync();
            if (oldData > 0)
            {
                throw new BusinessRuleViolationException(StaticValues.RecordAlreadyExistError, StaticValues.RecordAlreadyExistMessage($"{masterData.MasterDataType} and {masterData.Value}"));
            }
            var entity = _context.MasterDatas.Attach(masterData);
            entity.State = EntityState.Added;
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<MasterData> Update(MasterData MasterData)
        {
            EntityEntry<MasterData> oldJobTitle = _context.Update(MasterData);
            oldJobTitle.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return oldJobTitle.Entity;
        }

        public async Task<int> Delete(Guid MasterDataId)
        {
            MasterData masterData = await _context.MasterDatas
                .Where(mjt => mjt.Id == MasterDataId)
                .FirstOrDefaultAsync();
            if (masterData == null)
            {
                throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);
            }
            if (masterData.IsDeleted)
            {
                throw new BusinessRuleViolationException(StaticValues.RecordAlreadyDeletedError, StaticValues.RecordAlreadyDeletedMessage);
            }
            masterData.IsDeleted = true;
            var entity = _context.MasterDatas.Update(masterData);
            entity.State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public async Task<MasterData> Get(Guid MasterDataId)
        {
            return await _context.MasterDatas
                .Where(mjt => mjt.Id == MasterDataId && !mjt.IsDeleted)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<PagingResponse<MasterData>> GetAll(PagingRequest pagingRequest)
        {
            var dataCount= await _context.MasterDatas.Where(x=>!x.IsDeleted).CountAsync();
            var data = await _context.MasterDatas
                .Where(x => !x.IsDeleted)
                .Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1))
                .Take(pagingRequest.PageSize)
                .OrderBy(x => x.MasterDataType)
                .ThenBy(x => x.Value)
                .ToListAsync();

            PagingResponse<MasterData> pagingResponse = new PagingResponse<MasterData>()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = data,
                TotalRecords = dataCount
            };
            return pagingResponse;
        }

        public async Task<PagingResponse<MasterData>> Search(SearchPagingRequest searchPagingRequest)
        {
            string searchTerm = string.IsNullOrEmpty(searchPagingRequest.SearchTerm) ? string.Empty : searchPagingRequest.SearchTerm.ToLower();
            var data = await _context.MasterDatas
                .Where(mdc => !mdc.IsDeleted &&
                    (
                    searchTerm == string.Empty ||
                    mdc.Value.Contains(searchTerm) ||
                    mdc.MasterDataType.Contains(searchTerm) ||
                     mdc.Remark.Contains(searchTerm)
                     )
                )
                .OrderBy(x => x.MasterDataType)
                .ThenBy(x => x.Value)
                .ToListAsync();
            PagingResponse<MasterData> pagingResponse = new PagingResponse<MasterData>()
            {
                PageNo = searchPagingRequest.PageNo,
                PageSize = searchPagingRequest.PageSize,
                Data = data
                    .Skip(searchPagingRequest.PageSize * (searchPagingRequest.PageNo - 1))
                    .Take(searchPagingRequest.PageSize)
                    .ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<List<MasterData>> GetByMasterDataType(string masterDataType)
        {
            return await _context.MasterDatas
                  .Where(x => x.MasterDataType.Equals(masterDataType) && !x.IsDeleted)
                  .OrderBy(x => x.MasterDataType)
                  .ThenBy(x => x.Value)
                  .ToListAsync();
        }

        public async Task<List<MasterData>> GetByMasterDataTypes(List<string> masterDataTypes)
        {
            return await _context.MasterDatas
                .Where(x => masterDataTypes.Contains(x.MasterDataType) && !x.IsDeleted)
                .OrderBy(x => x.MasterDataType)
                .ThenBy(x => x.Code)
                .ThenBy(x => x.Value)
                .ToListAsync();
        }
    }
}