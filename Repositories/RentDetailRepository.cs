using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MKExpress.API.Constants;
using MKExpress.API.Data;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Request.Rents;
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
    public class RentDetailRepository : IRentDetailRepository
    {
        private readonly MKExpressDbContext _context;
        private readonly IExpenseRepository _expenseRepository;
        private readonly IExpenseNameRepository _expenseName;
        public RentDetailRepository(MKExpressDbContext context, IExpenseRepository expenseRepository, IExpenseNameRepository expenseName)
        {
            _context = context;
            _expenseRepository = expenseRepository;
            _expenseName = expenseName;
        }
        public async Task<RentDetail> Add(RentDetail rentDetail)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var entity = _context.RentDetails.Attach(rentDetail);
                entity.State = EntityState.Added;
                if (await _context.SaveChangesAsync() > 0)
                {

                    List<RentTransation> rentTransations = new List<RentTransation>();
                    int emis = rentDetail.Installments <= 12 ? 12 : rentDetail.Installments;
                    for (int i = 0; i < rentDetail.Installments; i++)
                    {
                        rentTransations.Add(new RentTransation()
                        {
                            InstallmentAmount = rentDetail.RentAmount / rentDetail.Installments,
                            InstallmentDate = rentDetail.FirstInstallmentDate.AddMonths((emis / rentDetail.Installments) * i),
                            InstallmentName = $"{i + 1} Installment",
                            IsPaid = false,
                            RentDetailId = entity.Entity.Id,
                            Id = 0
                        });
                    }
                    await _context.RentTransations.AddRangeAsync(rentTransations);
                    if (await _context.SaveChangesAsync() > 0)
                        trans.Commit();
                    else
                        trans.Rollback();
                }
                return entity.Entity;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw new BusinessRuleViolationException(StaticValues.AddRecordError, StaticValues.AddRecordErrorMessage + "##" + ex.Message);
            }
        }

        public async Task<int> Delete(int Id)
        {
            if (await IsRentPaid(Id))
            {
                throw new BusinessRuleViolationException(StaticValues.RentUpdateDeleteError, StaticValues.RentUpdateDeleteErrorMessage);
            }
            RentDetail rentDetail = await _context.RentDetails
               .Where(mht => mht.Id == Id)
               .FirstOrDefaultAsync();
            if (rentDetail == null)
            {
                throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);
            }
            if (rentDetail.IsDeleted)
            {
                throw new BusinessRuleViolationException(StaticValues.RentLocationAlreadyExistError, StaticValues.RentLocationAlreadyExistMessage);
            }
            rentDetail.IsDeleted = true;
            var entity = _context.RentDetails.Update(rentDetail);
            entity.State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public async Task<RentDetail> Get(int Id)
        {
            return await _context.RentDetails
                .Include(x => x.RentLocation)
                .Include(x => x.RentTransations)
                .Where(rentDetail => rentDetail.Id == Id && !rentDetail.IsDeleted)
                .FirstOrDefaultAsync();
        }

        public async Task<PagingResponse<RentDetail>> GetAll(PagingRequest pagingRequest)
        {
            var data = await _context.RentDetails
                .Include(x => x.RentLocation)
                .Include(x => x.RentTransations)
               .Where(x => !x.IsDeleted)
               .OrderByDescending(x => x.CreatedAt)
               .ToListAsync();
            PagingResponse<RentDetail> pagingResponse = new PagingResponse<RentDetail>()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = data.Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1)).Take(pagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<PagingResponse<RentTransation>> GetDueRents(bool isPaid, PagingRequest pagingRequest)
        {
            var currentMonth = DateTime.Parse($"{DateTime.Now.Year}-{ DateTime.Now.Month}-01");
            var data = await _context.RentTransations
                 .Include(x => x.RentDetail)
                 .ThenInclude(x => x.RentLocation)
                .Where(x => !x.IsDeleted && x.IsPaid == isPaid && x.InstallmentDate >= currentMonth)
                .OrderBy(x => x.InstallmentDate)
                .ToListAsync();
            PagingResponse<RentTransation> pagingResponse = new PagingResponse<RentTransation>()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = data.Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1)).Take(pagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<List<RentTransation>> GetRentTransations(int rentDetailId = 0)
        {
            return await _context.RentTransations.Where(x => rentDetailId == 0 || x.RentDetailId == rentDetailId).OrderBy(x => x.InstallmentDate).ToListAsync();
        }

        public async Task<int> PayDeuRents(RentPayRequest rentPayRequest, int paidBy)
        {
            var data = await _context.RentTransations.Where(x => x.Id == rentPayRequest.Id && !x.IsDeleted).FirstOrDefaultAsync();
            if (data == null)
            {
                throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);
            }
            if (data.IsPaid)
            {
                throw new BusinessRuleViolationException(StaticValues.RentAlreadyPaidError, StaticValues.RentAlreadyPaidErrorMessage);
            }
            data.IsPaid = true;
            data.PaidBy = paidBy;
            data.PaidOn = DateTime.Now;
            data.PaymentMode = rentPayRequest.PaymentMode;
            data.ChequeNo = rentPayRequest.ChequeNo;
            var entity = _context.RentTransations.Attach(data);
            entity.State = EntityState.Modified;
            var trans = await _context.Database.BeginTransactionAsync();
            if (await _context.SaveChangesAsync() > 0)
            {
                var expenseNo = await _expenseRepository.GetExpenseNo();
                int expenseNameId = 0;
                var expenseName = await _expenseName.GetAll(new PagingRequest() { PageNo = 1, PageSize = 1000 });
                expenseNameId = expenseName.Data.Where(x => x.Value.ToLower().Contains("rent") && x.ExpenseType.Value.ToLower().Contains("rent")).FirstOrDefault().Id;
                if (expenseNameId < 1)
                {
                    await trans.RollbackAsync();
                    throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.RentExpenseNameIdNotFound);
                }
                Expense expense = new Expense()
                {
                    Amount = data.InstallmentAmount,
                    Name = "Rent Payment",
                    PaymentMode = data.PaymentMode,
                    ExpenseNo = expenseNo,
                    CompanyId = rentPayRequest.CompanyId,
                    ExpenseNameId = expenseNameId
                };
                var result = await _expenseRepository.Add(expense);
                if (result.Id > 0)
                {
                    await trans.CommitAsync();
                    return result.Id;
                }
                return 0;
            }
            return 0;
        }

        public async Task<PagingResponse<RentDetail>> Search(SearchPagingRequest searchPagingRequest)
        {
            string searchTerm = string.IsNullOrEmpty(searchPagingRequest.SearchTerm) ? string.Empty : searchPagingRequest.SearchTerm.ToLower();
            var data = await _context.RentDetails
                .Where(mht => !mht.IsDeleted &&
                        (searchTerm.Contains(string.Empty) ||
                        mht.RentAmount.ToString().Equals(searchTerm) ||
                         mht.Installments.ToString().Equals(searchTerm) ||
                        mht.RentLocation.LocationName.Contains(searchTerm))
                    )
                .OrderByDescending(x => x.CreatedAt)
                    .ToListAsync();
            PagingResponse<RentDetail> pagingResponse = new PagingResponse<RentDetail>()
            {
                PageNo = searchPagingRequest.PageNo,
                PageSize = searchPagingRequest.PageSize,
                Data = data.Skip(searchPagingRequest.PageSize * (searchPagingRequest.PageNo - 1)).Take(searchPagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<PagingResponse<RentTransation>> SearchDeuRents(bool isPaid, SearchPagingRequest pagingRequest)
        {
            string searchTerm = string.IsNullOrEmpty(pagingRequest.SearchTerm) ? string.Empty : pagingRequest.SearchTerm.ToLower();
            var data = await _context.RentTransations
                 .Include(x => x.RentDetail)
                 .ThenInclude(x => x.RentLocation)
                .Where(x => !x.IsDeleted && x.IsPaid == isPaid && x.InstallmentDate >= DateTime.Today && (
                searchTerm.Contains(string.Empty) ||
                        x.InstallmentDate.ToString().Contains(searchTerm) ||
                         x.InstallmentName.ToString().Contains(searchTerm) ||
                        (x.PaidByEmp.FirstName + " " + x.PaidByEmp.LastName).Contains(searchTerm) ||
                        x.PaymentMode.ToString().Contains(searchTerm) ||
                        x.RentDetail.RentLocation.LocationName.ToString().Contains(searchTerm)
                ))
                .OrderBy(x => x.InstallmentDate)
                .ToListAsync();
            PagingResponse<RentTransation> pagingResponse = new PagingResponse<RentTransation>()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = data.Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1)).Take(pagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<RentDetail> Update(RentDetail entity)
        {
            if (await IsRentPaid(entity.Id))
            {
                throw new BusinessRuleViolationException(StaticValues.RentUpdateDeleteError, StaticValues.RentUpdateDeleteErrorMessage);
            }
            var oldType = await _context.RentDetails.Where(x => x.Id == entity.Id).FirstOrDefaultAsync();
            if (oldType != null)
            {
                var oldData = _context.RentTransations.Where(x => x.RentDetailId == entity.Id).ToList();
                _context.Remove(oldType);
                _context.RemoveRange(oldData);
                _context.SaveChanges();
            }
            return await Add(entity);
        }

        private async Task<bool> IsRentPaid(int rentDetailId)
        {
            return await _context.RentTransations.Where(x => x.IsPaid && x.RentDetailId == rentDetailId && !x.IsDeleted).CountAsync() > 0;
        }
    }
}
