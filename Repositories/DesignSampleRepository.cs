using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MKExpress.API.Data;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories
{
    public class DesignSampleRepository : IDesignSampleRepository
    {
        private readonly MKExpressDbContext _context;
        public DesignSampleRepository(MKExpressDbContext context)
        {
            _context = context;
        }
        public async Task<DesignSample> Add(DesignSample designSample)
        {
            var entity = _context.DesignSamples.Attach(designSample);
            entity.State = EntityState.Added;
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<int> AddQuantity(int quantity, int designSampleId)
        {
            DesignSample designSample = await _context.DesignSamples.Where(ds => ds.Id == designSampleId).FirstOrDefaultAsync();
            if (designSample != null)
            {
                designSample.Quantity += quantity;
                EntityEntry<DesignSample> oldSample = _context.Update(designSample);
                oldSample.State = EntityState.Modified;
                return await _context.SaveChangesAsync();
            }
            return default;
        }

        public async Task<int> AddQuantity(Dictionary<int, int> sampleQuantity)
        {
            var designSampleIds = sampleQuantity.Keys.ToList();
            List<DesignSample> designSamples = await _context.DesignSamples.Where(ds => designSampleIds.Contains(ds.Id)).ToListAsync();
            if (designSamples.Count > 0)
            {
                foreach (var designSample in designSamples)
                {
                    designSample.Quantity += sampleQuantity[designSample.Id];
                }

                _context.UpdateRange(designSamples);
                return await _context.SaveChangesAsync();
            }
            return default;
        }

        public async Task<int> Delete(int designSampleId)
        {
            DesignSample designSample = await _context.DesignSamples.Where(ds => ds.Id == designSampleId).FirstOrDefaultAsync();
            _context.DesignSamples.Remove(designSample);
            return await _context.SaveChangesAsync();
        }

        public async Task<DesignSample> Get(int designSampleId)
        {
            return await _context.DesignSamples.Include(x => x.MasterDesignCategory).Where(ds => ds.Id == designSampleId).FirstOrDefaultAsync();
        }

        public async Task<PagingResponse<DesignSample>> GetAll(PagingRequest pagingRequest)
        {
            var data = await _context.DesignSamples.Include(x => x.MasterDesignCategory).ToListAsync();
            PagingResponse<DesignSample> pagingResponse = new PagingResponse<DesignSample>()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = data.Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1)).Take(pagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<List<DesignSample>> GetByCategory(int categotyId)
        {
            return await _context.DesignSamples.Include(x => x.MasterDesignCategory).Where(x => x.CategoryId == categotyId).OrderBy(x => x.Model).ToListAsync();
        }

        public async Task<PagingResponse<DesignSample>> Search(SearchPagingRequest searchPagingRequest)
        {
            string searchTerm = string.IsNullOrEmpty(searchPagingRequest.SearchTerm) ? string.Empty : searchPagingRequest.SearchTerm.ToLower();
            var data = await _context.DesignSamples.Include(x => x.MasterDesignCategory)
                .Where(ds =>
                        searchTerm.Equals(string.Empty) ||
                        ds.Quantity.ToString().Contains(searchTerm) ||
                        ds.Shape.Contains(searchTerm) ||
                         ds.Size.ToString().Contains(searchTerm) ||
                          ds.Model.Contains(searchTerm) ||
                           ds.MasterDesignCategory.Value.Contains(searchTerm) ||
                           ds.DesignerName.Contains(searchTerm)
                    )
                    .ToListAsync();
            PagingResponse<DesignSample> pagingResponse = new PagingResponse<DesignSample>()
            {
                PageNo = searchPagingRequest.PageNo,
                PageSize = searchPagingRequest.PageSize,
                Data = data.Skip(searchPagingRequest.PageSize * (searchPagingRequest.PageNo - 1)).Take(searchPagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<DesignSample> Update(DesignSample designSample)
        {
            EntityEntry<DesignSample> oldCustomer = _context.Update(designSample);
            oldCustomer.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return oldCustomer.Entity;
        }
    }
}
