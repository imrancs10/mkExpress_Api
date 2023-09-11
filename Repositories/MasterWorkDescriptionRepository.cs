using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MKExpress.API.Constants;
using MKExpress.API.Data;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Exceptions;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories
{
    public class MasterWorkDescriptionRepository : IMasterWorkDescriptionRepository
    {
        private readonly MKExpressDbContext _context;
        public MasterWorkDescriptionRepository(MKExpressDbContext context)
        {
            _context = context;
        }
        public async Task<MasterWorkDescription> Add(MasterWorkDescription masterWorkDescription)
        {
            var oldData = await _context.MasterWorkDescriptions.Where(x => x.Code == masterWorkDescription.Code && x.Value == masterWorkDescription.Value).CountAsync();
            if (oldData > 0)
            {
                throw new BusinessRuleViolationException(StaticValues.DataAlreadyExistError, StaticValues.MasterDataTypeAlreadyExistMessage);
            }
            var typeList = masterWorkDescription.Value.Split(",").ToList();
            var masterWorkDescriptions = new List<MasterWorkDescription>();
            foreach (var item in typeList)
            {
                masterWorkDescriptions.Add(new MasterWorkDescription
                {
                    Code = masterWorkDescription.Code,
                    Value = item.Trim(),
                    Id = 0,
                    IsDeleted = false
                });
            }
            _context.MasterWorkDescriptions.AttachRange(masterWorkDescriptions);
            await _context.SaveChangesAsync();
            return masterWorkDescriptions.First();
        }

        public async Task<int> Delete(int Id)
        {
            MasterWorkDescription masterWorkDescription = await _context.MasterWorkDescriptions
                .Where(mwd => mwd.Id == Id)
                .FirstOrDefaultAsync();
            if (masterWorkDescription == null)
            {
                throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);
            }
            if (masterWorkDescription.IsDeleted)
            {
                throw new BusinessRuleViolationException(StaticValues.RecordAlreadyDeletedError, StaticValues.RecordAlreadyDeletedMessage);
            }
            masterWorkDescription.IsDeleted = true;
            var entity = _context.MasterWorkDescriptions.Update(masterWorkDescription);
            entity.State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public async Task<MasterWorkDescription> Get(int Id)
        {
            return await _context.MasterWorkDescriptions.Where(mwd => mwd.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<PagingResponse<MasterWorkDescription>> GetAll(PagingRequest pagingRequest)
        {
            var data = await _context.MasterWorkDescriptions
                .Where(x => !x.IsDeleted)
                .OrderBy(x => x.Code)
                .ToListAsync();
            PagingResponse<MasterWorkDescription> pagingResponse = new PagingResponse<MasterWorkDescription>()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = data.Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1)).Take(pagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<List<MasterWorkDescription>> GetByWorkTypes(string worktype)
        {
            if (worktype == null)
                return new List<MasterWorkDescription>();
            return await _context.MasterWorkDescriptions.Where(x => !x.IsDeleted && worktype.Contains(x.Code)).OrderBy(x => x.Code).ToListAsync();
        }

        public async Task<int> SaveOrderWorkDescription(List<OrderWorkDescription> orderWorkDescriptions)
        {
            if (orderWorkDescriptions.Count() == 0)
                return 0;
            int orderDetailId = orderWorkDescriptions.First().OrderDetailId;
            var oldData = await _context.OrderWorkDescriptions.Where(x => !x.IsDeleted && x.OrderDetailId == orderDetailId).ToListAsync();
            if (oldData.Count() > 0)
            {
                _context.RemoveRange(oldData);
                await _context.SaveChangesAsync();
            }
            orderWorkDescriptions.ForEach(res =>
            {
                res.Id = 0;
            });

            _context.AddRange(orderWorkDescriptions);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<OrderWorkDescription>> GetOrderWorkDescription(int orderDetailId)
        {
            return await _context.OrderWorkDescriptions.Where(x => !x.IsDeleted && x.OrderDetailId == orderDetailId).ToListAsync();
        }

        public async Task<PagingResponse<MasterWorkDescription>> Search(SearchPagingRequest searchPagingRequest)
        {
            string searchTerm = string.IsNullOrEmpty(searchPagingRequest.SearchTerm) ? string.Empty : searchPagingRequest.SearchTerm.ToLower();
            var data = await _context.MasterWorkDescriptions
                .Where(x => !x.IsDeleted &&
                        (searchTerm.Equals(string.Empty) ||
                        x.Value.Contains(searchTerm))
                    )
                .OrderBy(x => x.Code)
                    .ToListAsync();
            PagingResponse<MasterWorkDescription> pagingResponse = new PagingResponse<MasterWorkDescription>()
            {
                PageNo = searchPagingRequest.PageNo,
                PageSize = searchPagingRequest.PageSize,
                Data = data.Skip(searchPagingRequest.PageSize * (searchPagingRequest.PageNo - 1)).Take(searchPagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<MasterWorkDescription> Update(MasterWorkDescription masterWorkDescription)
        {
            var oldData = await _context.MasterWorkDescriptions.Where(x => x.Code == masterWorkDescription.Code && x.Value == masterWorkDescription.Value).CountAsync();
            if (oldData > 0)
            {
                throw new BusinessRuleViolationException(StaticValues.DataAlreadyExistError, StaticValues.MasterDataTypeAlreadyExistMessage);
            }
            EntityEntry<MasterWorkDescription> oldMasterWorkDescription = _context.Update(masterWorkDescription);
            oldMasterWorkDescription.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return oldMasterWorkDescription.Entity;
        }
    }
}
