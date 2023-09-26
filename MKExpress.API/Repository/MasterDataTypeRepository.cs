using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MKExpress.API.Contants;
using MKExpress.API.Data;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Exceptions;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using MKExpress.API.Utility;
using System.Linq;
using System.Threading.Tasks;

namespace SalehGarib.API.Repositories
{
    public class MasterDataTypeRepository : IMasterDataTypeRepository
    {
        private readonly MKExpressContext _context;
        public MasterDataTypeRepository(MKExpressContext context)
        {
            _context = context;
        }
        public async Task<MasterDataType> Add(MasterDataType masterDataType)
        {
            if(masterDataType.Value.Contains(","))
            {
                var valList=masterDataType.Value.Split(',');
                var list=new List<MasterDataType>();
                foreach(var val in valList)
                {
                    var code =val.CreateMasterCode();
                    if(list.Count(x=>x.Code.Equals(code))==0)
                    {
                        if (!string.IsNullOrEmpty(code))
                        {
                            list.Add(new MasterDataType()
                            {
                                Code = code,
                                Value = val.Trim(),
                                Id = Guid.NewGuid()
                            });
                        }
                    }
                }

                var codeList=list.Select(x=>x.Code).ToList();
                var existedData=await _context.MasterDataTypes.Where(x=>!x.IsDeleted && codeList.Contains(x.Code)).Select(x=>x.Value).ToListAsync();
                if (existedData.Count > 0)
                {
                    throw new BusinessRuleViolationException(StaticValues.RecordAlreadyExistError, StaticValues.RecordAlreadyExistMessage($"{string.Join(",",existedData)}"));
                }
                _context.MasterDataTypes.AddRange(list);
                if(await _context.SaveChangesAsync()>0)
                return list.FirstOrDefault();
                return new MasterDataType();

            }
            var oldData = await _context.MasterDataTypes.Where(x => !x.IsDeleted &&
                 x.Value == masterDataType.Value).CountAsync();
            if (oldData > 0)
            {
                throw new BusinessRuleViolationException(StaticValues.RecordAlreadyExistError, StaticValues.RecordAlreadyExistMessage($"{masterDataType.Value}"));
            }

            var entity = _context.MasterDataTypes.Attach(masterDataType);
            entity.State = EntityState.Added;
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<int> Delete(Guid id)
        {
            MasterDataType masterDataType = await _context.MasterDataTypes
                .Where(mdt => mdt.Id == id)
                .FirstOrDefaultAsync();
            if (masterDataType == null)
            {
                throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);
            }
            masterDataType.IsDeleted = true;
            var entity = _context.MasterDataTypes.Update(masterDataType);
            entity.State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public async Task<MasterDataType> Get(Guid id)
        {
            return await _context.MasterDataTypes.Where(mdt => mdt.Id == id).FirstOrDefaultAsync();
        }

        public async Task<PagingResponse<MasterDataType>> GetAll(PagingRequest pagingRequest)
        {
            var dataCount = await _context.MasterDataTypes
                .Where(x => !x.IsDeleted).CountAsync();
            var data = await _context.MasterDataTypes
                .Where(x => !x.IsDeleted)
                .Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1)).Take(pagingRequest.PageSize)
                .OrderBy(x => x.Value)
                .ToListAsync();
            PagingResponse<MasterDataType> pagingResponse = new PagingResponse<MasterDataType>()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = data,
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<bool> IsMasterDataTypeExist(string masterDataType)
        {
            return await _context.MasterDataTypes.Where(x => !x.IsDeleted && x.Value.Trim().Equals(masterDataType.Trim()) || x.Code.Trim().Equals(masterDataType.Trim())).CountAsync() > 0;
        }

        public async Task<PagingResponse<MasterDataType>> Search(SearchPagingRequest searchPagingRequest)
        {
            string searchTerm = string.IsNullOrEmpty(searchPagingRequest.SearchTerm) ? string.Empty : searchPagingRequest.SearchTerm.ToLower();
            var dataCount = await _context.MasterDataTypes
                .Where(mdt => !mdt.IsDeleted &&
                        searchTerm.Equals(string.Empty) ||
                        searchTerm.Equals(mdt.Value)
                    ).CountAsync();

            var data = await _context.MasterDataTypes
                .Where(mdt => !mdt.IsDeleted &&
                        searchTerm.Equals(string.Empty) ||
                        searchTerm.Equals(mdt.Value)
                    )
                .Skip(searchPagingRequest.PageSize * (searchPagingRequest.PageNo - 1))
                .Take(searchPagingRequest.PageSize)
                .OrderBy(x => x.Value)
                .ToListAsync();
            PagingResponse<MasterDataType> pagingResponse = new PagingResponse<MasterDataType>()
            {
                PageNo = searchPagingRequest.PageNo,
                PageSize = searchPagingRequest.PageSize,
                Data = data,
                TotalRecords = dataCount
            };
            return pagingResponse;
        }

        public async Task<MasterDataType> Update(MasterDataType entity)
        {
            EntityEntry<MasterDataType> oldCustomer = _context.Update(entity);
            oldCustomer.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return oldCustomer.Entity;
        }
    }
}
