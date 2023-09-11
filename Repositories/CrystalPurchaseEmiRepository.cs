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
    public class CrystalPurchaseEmiRepository : ICrystalPurchaseEmiRepository
    {
        private readonly MKExpressDbContext _context;
        public CrystalPurchaseEmiRepository(MKExpressDbContext context)
        {
            _context = context;
        }

        public async Task<int> Add(IList<CrystalPurchaseInstallment> crystalPurchaseInstallments)
        {
            if (crystalPurchaseInstallments.Count == 0)
                throw new BusinessRuleViolationException(StaticValues.InvalidDataError, StaticValues.InvalidDataMessage);
            await _context.CrystalPurchaseInstallments.AddRangeAsync(crystalPurchaseInstallments);
            return await _context.SaveChangesAsync();
        }

        public Task<int> Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<CrystalPurchaseInstallment> Get(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<PagingResponse<CrystalPurchaseInstallment>> GetAll(PagingRequest pagingRequest)
        {
            throw new NotImplementedException();
        }

        public Task<PagingResponse<CrystalPurchaseInstallment>> Search(SearchPagingRequest searchPagingRequest)
        {
            throw new NotImplementedException();
        }

        public Task<CrystalPurchaseInstallment> Update(CrystalPurchaseInstallment entity)
        {
            throw new NotImplementedException();
        }
    }
}
