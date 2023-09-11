using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
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
    public class MasterCrystalRepository : IMasterCrystalRepository
    {
        private readonly MKExpressDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ICrystalStockRepository _crystalStockRepository;
        public MasterCrystalRepository(MKExpressDbContext context, IConfiguration configuration, ICrystalStockRepository crystalStockRepository)
        {
            _context = context;
            _configuration = configuration;
            _crystalStockRepository = crystalStockRepository;
        }
        public async Task<MasterCrystal> Add(MasterCrystal entity)
        {
            var oldCrystal = await _context.MasterCrystals.Where(x => x.Name == entity.Name && x.SizeId == entity.SizeId && x.ShapeId == entity.ShapeId && x.BrandId == entity.BrandId).CountAsync();
            if (oldCrystal > 0)
            {
                throw new BusinessRuleViolationException(StaticValues.DataAlreadyExistError, StaticValues.DataAlreadyExistMessage);
            }
            var trans = _context.Database.BeginTransaction();
            var res = _context.MasterCrystals.Add(entity);
            res.State = EntityState.Added;
            if (await _context.SaveChangesAsync() > 0)
            {
                if (await _crystalStockRepository.AddNewCrystalInStockWithZeroQty(res.Entity.Id))
                {
                    trans.Commit();
                    return res.Entity;
                }
            }
            trans.Rollback();
            return default;
        }

        public async Task<int> Delete(int Id)
        {
            MasterCrystal masterCrystal = await _context.MasterCrystals
              .Where(mcrys => mcrys.Id == Id)
              .FirstOrDefaultAsync();
            if (masterCrystal == null)
            {
                throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);
            }
            if (masterCrystal.IsDeleted)
            {
                throw new BusinessRuleViolationException(StaticValues.RecordAlreadyDeletedError, StaticValues.RecordAlreadyDeletedMessage);
            }
            if (await IsCrystalInUse(Id))
            {
                throw new BusinessRuleViolationException(StaticValues.DataIsInUseDeleteError, StaticValues.DataIsInUseDeleteMessage);
            }
            masterCrystal.IsDeleted = true;
            var entity = _context.MasterCrystals.Update(masterCrystal);
            entity.State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public async Task<MasterCrystal> Get(int Id)
        {
            return await _context.MasterCrystals.Where(mcrys => mcrys.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<PagingResponse<MasterCrystal>> GetAll(PagingRequest pagingRequest)
        {
            var data = await _context.MasterCrystals
                .Include(x => x.Brand)
                .Include(x => x.Size)
                .Include(x => x.Shape)
                .Where(x => !x.IsDeleted)
                .OrderBy(x => x.Name)
                .ToListAsync();
            PagingResponse<MasterCrystal> pagingResponse = new PagingResponse<MasterCrystal>()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = data.Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1)).Take(pagingRequest.PageSize).ToList(),
                TotalRecords = data.Count()
            };
            return pagingResponse;
        }

        public async Task<int> GetNextCrystalNo()
        {
            int defaultOrderNo = _configuration.GetValue<int>("CrystalIdStartFrom");
            var crys = await _context.MasterCrystals.OrderByDescending(x => x.CrystalId).FirstOrDefaultAsync();
            return crys == null ? defaultOrderNo : crys.CrystalId + 1;
        }

        public async Task<PagingResponse<MasterCrystal>> Search(SearchPagingRequest searchPagingRequest)
        {
            string searchTerm = string.IsNullOrEmpty(searchPagingRequest.SearchTerm) ? string.Empty : searchPagingRequest.SearchTerm.ToLower();
            var data = await _context.MasterCrystals
                 .Include(x => x.Brand)
                .Include(x => x.Size)
                .Include(x => x.Shape)
                .Where(mcrys => !mcrys.IsDeleted &&
                       (searchTerm.Equals(string.Empty) ||
                        mcrys.Name.Contains(searchTerm) ||
                       mcrys.Brand.Value.Contains(searchTerm) ||
                        mcrys.Shape.Value.Contains(searchTerm) ||
                        mcrys.Size.Value.Contains(searchTerm))
                    )
                .OrderBy(x => x.Name)
                    .ToListAsync();
            PagingResponse<MasterCrystal> pagingResponse = new PagingResponse<MasterCrystal>()
            {
                PageNo = searchPagingRequest.PageNo,
                PageSize = searchPagingRequest.PageSize,
                Data = data.Skip(searchPagingRequest.PageSize * (searchPagingRequest.PageNo - 1)).Take(searchPagingRequest.PageSize).ToList(),
                TotalRecords = data.Count()
            };
            return pagingResponse;
        }

        public async Task<MasterCrystal> Update(MasterCrystal entity)
        {
            var oldCrystal = await _context.MasterCrystals.Where(x => x.Id != entity.Id && x.Name == entity.Name && x.SizeId == entity.SizeId && x.ShapeId == entity.ShapeId && x.BrandId == entity.BrandId).CountAsync();
            if (oldCrystal > 0)
            {
                throw new BusinessRuleViolationException(StaticValues.DataAlreadyExistError, StaticValues.DataAlreadyExistMessage);
            }
            EntityEntry<MasterCrystal> oldCustomer = _context.Update(entity);
            oldCustomer.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return oldCustomer.Entity;
        }

        private async Task<bool> IsCrystalInUse(int crystalId)
        {
            return await _context.CrystalStocks.Where(x => !x.IsDeleted && x.CrystalId == crystalId).CountAsync() > 0;
        }
    }
}
