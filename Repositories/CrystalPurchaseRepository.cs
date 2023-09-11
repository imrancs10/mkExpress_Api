using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public class CrystalPurchaseRepository : ICrystalPurchaseRepository
    {

        private readonly MKExpressDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ICrystalPurchaseEmiRepository _crystalPurchaseEmiRepository;
        private readonly ICrystalStockRepository _crystalStockRepository;
        private readonly IMapper _mapper;
        public CrystalPurchaseRepository(MKExpressDbContext context,
            IConfiguration configuration,
            ICrystalPurchaseEmiRepository crystalPurchaseEmiRepository,
            ICrystalStockRepository crystalStockRepository,
            IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _crystalPurchaseEmiRepository = crystalPurchaseEmiRepository;
            _crystalStockRepository = crystalStockRepository;
            _mapper = mapper;
        }

        public async Task<CrystalPurchase> Add(CrystalPurchase crystalPurchase)
        {
            if (crystalPurchase == null || crystalPurchase.CrystalPurchaseDetails == null || crystalPurchase.CrystalPurchaseDetails.Count == 0)
                throw new BusinessRuleViolationException(StaticValues.InvalidCrystalPurchaseDataError, StaticValues.InvalidCrystalPurchaseDataMessage);
            var trans = _context.Database.BeginTransaction();

            if (crystalPurchase.PaymentMode?.ToLower() != "cheque")
            {
                crystalPurchase.ChequeDate = DateTime.MinValue;
                crystalPurchase.ChequeNo = string.Empty;
            }
            else
            {
                if (crystalPurchase.ChequeDate == DateTime.MinValue)
                    throw new BusinessRuleViolationException(StaticValues.InvalidDataError, StaticValues.ChequeDateIsRequired);
                if (string.IsNullOrEmpty(crystalPurchase.ChequeNo))
                    throw new BusinessRuleViolationException(StaticValues.InvalidDataError, StaticValues.ChequeNumberIsRequired);
            }

            var entity = await _context.CrystalPurchases.AddAsync(crystalPurchase);
            entity.State = EntityState.Added;
            var crysPurchaseResult = await _context.SaveChangesAsync();

            if (crysPurchaseResult > 0)
            {
                var crysStock = _mapper.Map<List<CrystalStock>>(crystalPurchase.CrystalPurchaseDetails);
                crysStock.ForEach(res =>{ res.Id = 0;});
                var stockResult = await _crystalStockRepository.UpdateStock(crysStock);

                if (stockResult <= 0)
                {
                    trans.Rollback();
                    return entity.Entity;
                }
                if (crystalPurchase.Installments > 0)
                {
                    if (crystalPurchase.InstallmentStartDate == DateTime.MinValue)
                        throw new BusinessRuleViolationException(StaticValues.InvalidDataError, StaticValues.InvalidInstallmentStartDate);

                    var installments = new List<CrystalPurchaseInstallment>();
                    for (int i = 1; i <= crystalPurchase.Installments; i++)
                    {
                        installments.Add(new CrystalPurchaseInstallment()
                        {
                            Amount = crystalPurchase.TotalAmount / crystalPurchase.Installments,
                            CrystalPurchaseId = entity.Entity.Id,
                            InstallmentDate = crystalPurchase.InstallmentStartDate.AddMonths(i).Date,
                            InstallmentNo = i,
                            PaymentDate = DateTime.MinValue,
                            PaymentMode = string.Empty
                        });
                    }
                    var emiResult = await _crystalPurchaseEmiRepository.Add(installments);
                    if (emiResult > 0)
                    {
                        trans.Commit();
                        return entity.Entity;
                    }
                }
                else
                {
                    trans.Commit();
                    return entity.Entity;
                }

            }
            trans.Rollback();
            return new CrystalPurchase();
        }

        public async Task<int> Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<CrystalPurchase> Get(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<PagingResponse<CrystalPurchase>> GetAll(PagingRequest pagingRequest)
        {
            var data = await _context.CrystalPurchases
                .Include(x => x.CrystalPurchaseDetails)
                .ThenInclude(x=>x.MasterCrystal)
                .ThenInclude(x => x.Brand)
                .Include(x => x.CrystalPurchaseDetails)
                .ThenInclude(x => x.MasterCrystal)
                .ThenInclude(x => x.Size)
                .Include(x => x.CrystalPurchaseDetails)
                .ThenInclude(x => x.MasterCrystal)
                .ThenInclude(x => x.Shape)

                .Include(x => x.CrystalPurchaseInstallments)
                .Include(x => x.Supplier)
                .Where(x => !x.IsDeleted && !x.IsCancelled && x.InvoiceDate>=pagingRequest.FromDate && x.InvoiceDate<=pagingRequest.ToDate)
                .OrderBy(x => x.PurchaseNo)
                .ToListAsync();
            PagingResponse<CrystalPurchase> pagingResponse = new PagingResponse<CrystalPurchase>()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = data.Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1)).Take(pagingRequest.PageSize).ToList(),
                TotalRecords = data.Count()
            };
            return pagingResponse;
        }

        public async Task<int> GetCrystalPurchaseNo()
        {
            int defaultPurchaseNo = _configuration.GetValue<int>("CrystalPurchaseNoStartFrom");
            var crys = await _context.CrystalPurchases.OrderByDescending(x => x.PurchaseNo).FirstOrDefaultAsync();
            return crys == null ? defaultPurchaseNo : crys.PurchaseNo + 1;
        }

        public async Task<Dictionary<int, int>> GetPurchaseCrystalCounts(DateTime fromDate, DateTime toDate)
        {
            var data = await _context.CrystalPurchaseDetails
                .Include(x => x.CrystalPurchase)
                .Where(x => !x.IsCancelled && !x.IsDeleted &&
                            x.CrystalPurchase.InvoiceDate.Date >= fromDate.Date &&
                            x.CrystalPurchase.InvoiceDate.Date <= toDate.Date)
                .ToListAsync();
            var grp = data.GroupBy(x => x.CrystalId);
            var res =new  Dictionary<int, int>();
            foreach ( var kvp in grp )
            {
                res.Add(kvp.Key, kvp.Sum(c => c.Qty));
            }
            return res;
        }

        public async Task<PagingResponse<CrystalPurchase>> Search(SearchPagingRequest searchPagingRequest)
        {
            var searchWord = string.IsNullOrEmpty(searchPagingRequest.SearchTerm) ? string.Empty : searchPagingRequest.SearchTerm.ToLower();
            var data = await _context.CrystalPurchases
                .Include(x => x.CrystalPurchaseDetails)
                .ThenInclude(x => x.MasterCrystal)
                .ThenInclude(x => x.Brand)
              .Include(x => x.CrystalPurchaseDetails)
                .ThenInclude(x => x.MasterCrystal)
                .ThenInclude(x => x.Size)
                .Include(x => x.CrystalPurchaseDetails)
                .ThenInclude(x => x.MasterCrystal)
                .ThenInclude(x => x.Shape)

                .Include(x => x.CrystalPurchaseInstallments)
                .Include(x => x.Supplier)
               .Where(x => !x.IsDeleted && !x.IsCancelled && (
                searchWord=="" ||
                x.Supplier.CompanyName.Contains(searchWord) ||
                x.Supplier.Contact.Contains(searchWord) ||
                x.Supplier.Address.Contains(searchWord) ||
                x.PurchaseNo.ToString().Contains(searchWord) ||
                x.ChequeNo.Contains(searchWord) ||
                x.InvoiceNo.Contains(searchWord) ||
                x.PaymentMode.Contains(searchWord) ||
                x.Qty.ToString().Equals(searchWord) ||
                x.SubTotalAmount.ToString().Contains(searchWord) ||
                x.TotalAmount.ToString().Contains(searchWord) ||
                x.VatAmount.ToString().Contains(searchWord)
               ))
               .OrderBy(x => x.PurchaseNo)
               .ToListAsync();
            PagingResponse<CrystalPurchase> pagingResponse = new PagingResponse<CrystalPurchase>()
            {
                PageNo = searchPagingRequest.PageNo,
                PageSize = searchPagingRequest.PageSize,
                Data = data.Skip(searchPagingRequest.PageSize * (searchPagingRequest.PageNo - 1)).Take(searchPagingRequest.PageSize).ToList(),
                TotalRecords = data.Count()
            };
            return pagingResponse;
        }

        public async Task<CrystalPurchase> Update(CrystalPurchase entity)
        {
            throw new NotImplementedException();
        }
    }
}
