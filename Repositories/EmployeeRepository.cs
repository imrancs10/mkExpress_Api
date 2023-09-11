using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Ocsp;
using MKExpress.API.Constants;
using MKExpress.API.Data;
using MKExpress.API.Dto.Request.Employee;
using MKExpress.API.Dto.Response;
using MKExpress.API.Dto.Response.Employee;
using MKExpress.API.Exceptions;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using MKExpress.Web.API.Dto.Response.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MKExpressDbContext _context;
        private readonly IProductStockRepository _productStockRepository;
        private readonly IDropdownRepository _dropdownRepository;
        private readonly IConfiguration _configuration;
        private readonly ICrystalTrackingOutRepository _crystalTrackingOutRepository;
        private readonly decimal CrystalPerPackLabourCharge = 17;
        private readonly int CrystalQtyPerPack = 1440;
        public EmployeeRepository(MKExpressDbContext context, IProductStockRepository productStockRepository, IConfiguration configuration, ICrystalTrackingOutRepository crystalTrackingOutRepository, IDropdownRepository dropdownRepository)
        {
            _context = context;
            _productStockRepository = productStockRepository;
            _configuration = configuration;
            CrystalPerPackLabourCharge = decimal.Parse(_configuration.GetSection("CrystalPerPackLabourCharge").Value);
            CrystalQtyPerPack = int.Parse(_configuration.GetSection("CrystalQtyPerPack").Value);
            _crystalTrackingOutRepository = crystalTrackingOutRepository;
            _dropdownRepository = dropdownRepository;
        }
        public async Task<Employee> Add(Employee employee)
        {
            employee.IsActive = true;
            var entity = _context.Employees.Attach(employee);
            entity.State = EntityState.Added;
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<int> Delete(int employeeId)
        {
            Employee employee = await _context.Employees
                .Where(emp => emp.Id == employeeId)
                .FirstOrDefaultAsync();
            if (employee == null)
            {
                throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);
            }
            if (employee.IsDeleted)
            {
                throw new BusinessRuleViolationException(StaticValues.RecordAlreadyDeletedError, StaticValues.RecordAlreadyDeletedMessage);
            }
            employee.IsDeleted = true;
            var entity = _context.Employees.Update(employee);
            entity.State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public async Task<Employee> Get(int employeeId)
        {
            return await _context.Employees
                .Include(x => x.MasterJobTitle)
                .Include(x => x.UserRole)
                .Where(emp => emp.Id == employeeId && !emp.IsDeleted && emp.IsActive)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<Employee> Update(Employee employee)
        {
            EntityEntry<Employee> oldEmp = _context.Update(employee);
            oldEmp.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return oldEmp.Entity;
        }

        public async Task<PagingResponse<Employee>> GetAll(EmployeePagingRequest pagingRequest)
        {
            pagingRequest.Type = string.IsNullOrEmpty(pagingRequest.Type) ? "" : pagingRequest.Type.ToLower();
            bool empType = pagingRequest.Type == "staff" ? true : false;
            string jobTitle = string.IsNullOrEmpty(pagingRequest.Title) ? "" : pagingRequest.Title.ToLower();
            var data = await _context.Employees
                .Include(x => x.MasterJobTitle)
                 .Include(x => x.UserRole)
                .Where(x => !x.IsDeleted && x.IsActive && (pagingRequest.Type == "" || x.IsFixedEmployee == empType) && (jobTitle == "null" || x.MasterJobTitle.Code.ToLower().Contains(jobTitle)))
                .OrderBy(x => x.FirstName)
                .ToListAsync();

            PagingResponse<Employee> pagingResponse = new PagingResponse<Employee>()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = data.Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1)).Take(pagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<PagingResponse<Employee>> Search(EmployeeSearchPagingRequest searchPagingRequest)
        {
            string searchTerm = string.IsNullOrEmpty(searchPagingRequest.SearchTerm) ? string.Empty : searchPagingRequest.SearchTerm.ToLower();
            searchPagingRequest.Type = string.IsNullOrEmpty(searchPagingRequest.Type) ? "" : searchPagingRequest.Type.ToLower();
            bool empType = searchPagingRequest.Type.ToLower() == "staff";

            string jobTitle = string.IsNullOrEmpty(searchPagingRequest.Title) || searchPagingRequest.Title == "null" ? "" : searchPagingRequest.Title.ToLower();
            var data = await _context.Employees
                .Include(x => x.MasterJobTitle)
                 .Include(x => x.UserRole)
                .Where(emp => !emp.IsDeleted && emp.IsActive && (searchPagingRequest.Type == "" || emp.IsFixedEmployee == empType) &&
                        emp.MasterJobTitle.Code.Contains(jobTitle) &&
                        (searchTerm.Equals(string.Empty) ||
                        emp.FirstName.Contains(searchTerm) ||
                        emp.LastName.Contains(searchTerm) ||
                        emp.Country.Contains(searchTerm) ||
                        emp.BasicSalary.ToString().Contains(searchTerm) ||
                        emp.Contact.Contains(searchTerm) ||
                        emp.Address.Contains(searchTerm) ||
                        emp.WorkPermitID.Contains(searchTerm) ||
                        emp.WorkPEDate.ToString().Contains(searchTerm) ||
                        emp.Salary.ToString().Contains(searchTerm) ||
                        emp.PassportNumber.Contains(searchTerm) ||
                        emp.PassportExpiryDate.ToString().Contains(searchTerm) ||
                        emp.MedicalExpiryDate.ToString().Contains(searchTerm) ||
                        emp.HireDate.ToString().Contains(searchTerm) ||
                        emp.Accomodation.ToString().Contains(searchTerm) ||
                        emp.Contact.Contains(searchTerm))
                    )
                .OrderBy(x => x.FirstName)
                    .ToListAsync();
            PagingResponse<Employee> pagingResponse = new PagingResponse<Employee>()
            {
                PageNo = searchPagingRequest.PageNo,
                PageSize = searchPagingRequest.PageSize,
                Data = data.Skip(searchPagingRequest.PageSize * (searchPagingRequest.PageNo - 1)).Take(searchPagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<List<Employee>> GetEmployeeByJobIds(List<int> jobIds)
        {
            return await _context.Employees
                .Where(x => jobIds.Contains(x.JobTitleId) && !x.IsDeleted && x.IsActive)
                .ToListAsync();
        }

        public async Task<List<ExpireDocResponse>> GetExpireDocuments(int empId)
        {
            var emp = await Get(empId);
            if (emp == null)
                return new List<ExpireDocResponse>();
            List<ExpireDocResponse> result = new List<ExpireDocResponse>();
            if (emp.EmiratesIdExpire <= DateTime.Now)
            {
                result.Add(new ExpireDocResponse()
                {
                    DocumentName = DocumentNameEnum.EmiratesId.ToString(),
                    DocumentNumber = emp.EmiratesId,
                    ExpireAt = emp.EmiratesIdExpire,
                    Status = DocExpiryStatusEnum.Expired.ToString()
                });
            }
            else if (emp.EmiratesIdExpire.AddMonths(1) <= DateTime.Now)
            {
                result.Add(new ExpireDocResponse()
                {
                    DocumentName = DocumentNameEnum.EmiratesId.ToString(),
                    DocumentNumber = emp.EmiratesId,
                    ExpireAt = emp.EmiratesIdExpire,
                    Status = DocExpiryStatusEnum.ExpiryInMonth.ToString()
                });
            }
            if (emp.MedicalExpiryDate != null)
            {
                if (emp.MedicalExpiryDate <= DateTime.Now)
                {
                    result.Add(new ExpireDocResponse()
                    {
                        DocumentName = DocumentNameEnum.MedicalCard.ToString(),
                        DocumentNumber = "NA",
                        ExpireAt = emp.MedicalExpiryDate,
                        Status = DocExpiryStatusEnum.Expired.ToString()
                    });
                }
                else if (((DateTime)emp.MedicalExpiryDate).AddMonths(1) <= DateTime.Now)
                {
                    result.Add(new ExpireDocResponse()
                    {
                        DocumentName = DocumentNameEnum.MedicalCard.ToString(),
                        DocumentNumber = "NA",
                        ExpireAt = emp.MedicalExpiryDate,
                        Status = DocExpiryStatusEnum.ExpiryInMonth.ToString()
                    });
                }
            }
            if (emp.PassportExpiryDate != null)
            {
                if (emp.PassportExpiryDate <= DateTime.Now)
                {
                    result.Add(new ExpireDocResponse()
                    {
                        DocumentName = DocumentNameEnum.Passport.ToString(),
                        DocumentNumber = emp.PassportNumber,
                        ExpireAt = emp.PassportExpiryDate,
                        Status = DocExpiryStatusEnum.Expired.ToString()
                    });
                }
                else if (((DateTime)emp.PassportExpiryDate).AddMonths(1) <= DateTime.Now)
                {
                    result.Add(new ExpireDocResponse()
                    {
                        DocumentName = DocumentNameEnum.Passport.ToString(),
                        DocumentNumber = emp.PassportNumber,
                        ExpireAt = emp.PassportExpiryDate,
                        Status = DocExpiryStatusEnum.ExpiryInMonth.ToString()
                    });
                }
            }
            if (emp.ResidentPDExpire != null)
            {
                if (emp.ResidentPDExpire <= DateTime.Now)
                {
                    result.Add(new ExpireDocResponse()
                    {
                        DocumentName = DocumentNameEnum.ResidentId.ToString(),
                        DocumentNumber = "NA",
                        ExpireAt = emp.ResidentPDExpire,
                        Status = DocExpiryStatusEnum.Expired.ToString()
                    });
                }
                else if (((DateTime)emp.ResidentPDExpire).AddMonths(1) <= DateTime.Now)
                {
                    result.Add(new ExpireDocResponse()
                    {
                        DocumentName = DocumentNameEnum.ResidentId.ToString(),
                        DocumentNumber = "NA",
                        ExpireAt = emp.ResidentPDExpire,
                        Status = DocExpiryStatusEnum.ExpiryInMonth.ToString()
                    });
                }
            }
            if (emp.WorkPEDate != null)
            {
                if (emp.WorkPEDate <= DateTime.Now)
                {
                    result.Add(new ExpireDocResponse()
                    {
                        DocumentName = DocumentNameEnum.WorkPermit.ToString(),
                        DocumentNumber = emp.WorkPermitID,
                        ExpireAt = emp.WorkPEDate,
                        Status = DocExpiryStatusEnum.Expired.ToString()
                    });
                }
                else if (((DateTime)emp.WorkPEDate).AddMonths(1) <= DateTime.Now)
                {
                    result.Add(new ExpireDocResponse()
                    {
                        DocumentName = DocumentNameEnum.WorkPermit.ToString(),
                        DocumentNumber = emp.WorkPermitID,
                        ExpireAt = emp.WorkPEDate,
                        Status = DocExpiryStatusEnum.ExpiryInMonth.ToString()
                    });
                }
            }
            return result;
        }

        public async Task<List<Employee>> GetAllActiveDeactiveEmployee(bool allFixed)
        {
            return await _context.Employees
                .Where(x => !x.IsDeleted && (!allFixed || x.IsFixedEmployee == allFixed))
                .Include(x => x.MasterJobTitle)
                 .Include(x => x.UserRole)
                .OrderBy(x => x.FirstName)
                .ToListAsync();
        }

        public async Task<bool> ActiveDeactiveEmployee(int empId, bool isActive)
        {
            var emp = await _context.Employees.Where(x => !x.IsDeleted && x.Id == empId).FirstOrDefaultAsync();
            if (emp == null)
                return false;
            emp.IsActive = isActive;
            await Update(emp);
            return true;
        }

        public async Task<List<Employee>> GetAllActiveDeactiveEmployeeSearch(string searchTearm, bool allFixed = false)
        {
            string searchTerm = string.IsNullOrEmpty(searchTearm) ? string.Empty : searchTearm.ToLower();
            return await _context.Employees
                .Include(x => x.MasterJobTitle)
                 .Include(x => x.UserRole)
                .Where(emp => !emp.IsDeleted && (!allFixed || emp.IsFixedEmployee == allFixed) &&
                        (searchTerm.Equals(string.Empty) ||
                        emp.FirstName.Contains(searchTerm) ||
                        emp.LastName.Contains(searchTerm) ||
                        emp.Country.Contains(searchTerm) ||
                        emp.BasicSalary.ToString().Contains(searchTerm) ||
                        emp.Contact.Contains(searchTerm) ||
                        emp.Address.Contains(searchTerm) ||
                        emp.WorkPermitID.Contains(searchTerm) ||
                        emp.WorkPEDate.ToString().Contains(searchTerm) ||
                        emp.Salary.ToString().Contains(searchTerm) ||
                        emp.PassportNumber.Contains(searchTerm) ||
                        emp.PassportExpiryDate.ToString().Contains(searchTerm) ||
                        emp.MedicalExpiryDate.ToString().Contains(searchTerm) ||
                        emp.HireDate.ToString().Contains(searchTerm) ||
                        emp.MasterJobTitle.Value.Contains(searchTerm) ||
                        emp.Accomodation.ToString().Contains(searchTerm) ||
                        emp.Contact.Contains(searchTerm))
                    )
                .OrderBy(x => x.FirstName)
                    .ToListAsync();
        }

        public async Task<List<EmployeeSalarySlipResponse>> GetEmployeeSalarySlip(int empId, int month, int year)
        {
            var crystalUsedData = await _crystalTrackingOutRepository.GetOrderUsedCrystalsByEmployee(empId, month, year);
            var crystalUsedDataDic = crystalUsedData.ToDictionary(x => x.OrderDetailId, y => y);
            var data = await _context.WorkTypeStatuses
                .Include(x => x.CompletedByEmployee)
                .ThenInclude(x => x.MasterJobTitle)
                .Include(x => x.OrderDetail)
                .Where(x => !x.IsDeleted &&
                            x.CompletedBy == empId &&
                            x.CompletedOn.Month == month &&
                            x.CompletedOn.Year == year)

                .ToListAsync();
            var res = new List<EmployeeSalarySlipResponse>();
            res.AddRange(data.Select(x => new EmployeeSalarySlipResponse()
            {
                Date = x.CompletedOn,
                EmployeeName = x.CompletedByEmployee.FirstName + " " + x.CompletedByEmployee.LastName,
                EmployeeId = (int)x.CompletedBy,
                KandooraNo = x.OrderDetail.OrderNo,
                VoucherNo = x.VoucherNo,
                OrderPrice = x.OrderDetail.Price,
                Note = x.Note,
                Extra = x.Extra??0,
                JobTitle = x.CompletedByEmployee.MasterJobTitle.Value,
                ContactNo = x.CompletedByEmployee.Contact,
                Qty = 1,
                Amount = x.Price
            })
                .OrderBy(x => x.Date)
                .ToList()
                );
            res.AddRange(crystalUsedData.Select(x => new EmployeeSalarySlipResponse()
            {
                Date = x.ReleaseDate,
                EmployeeName = x.Employee.FirstName + " " + x.Employee.LastName,
                EmployeeId = (int)x.EmployeeId,
                KandooraNo = x.OrderDetail.OrderNo,
                VoucherNo = string.Empty,
                OrderPrice = x.OrderDetail.Price,
                Note = string.Empty,
                Extra = (x.CrystalTrackingOutDetails.Where(y => y.IsAlterWork).Sum(y => y.CrystalLabourCharge + y.ArticalLabourCharge)),
                JobTitle = x.Employee.MasterJobTitle.Value,
                ContactNo = x.Employee.Contact,
                Qty = x.CrystalTrackingOutDetails.Sum(y => y.ReleasePacketQty),
                Amount = (x.CrystalTrackingOutDetails.Where(y => !y.IsAlterWork).Sum(y => y.CrystalLabourCharge + y.ArticalLabourCharge))
            })
                .OrderBy(x => x.Date)
                .ToList()
                ); ;
            return res;
        }

        public async Task<List<EmployeeSalaryLedgerResponse>> GetEmployeeSalaryLedger(int jobTitleId, int month, int year)
        {
            var workType = await _dropdownRepository.JobTitle();
            var hotFixId = workType.Where(x => x.Value.ToLower().Contains("hot")).Select(x => x.Id).FirstOrDefault();
            List<CrystalTrackingOut> crystalUsedData = null;
           IEnumerable<IGrouping<int, CrystalTrackingOut>> crystalUsedDataDic = null;
            if (hotFixId == jobTitleId)
            {
                crystalUsedData = await _crystalTrackingOutRepository.GetOrderUsedCrystalsByEmployee(0, month, year);
                crystalUsedDataDic = crystalUsedData.AsEnumerable().GroupBy(x => x.EmployeeId);
            }
            var data = await _context.WorkTypeStatuses
                .Include(x => x.CompletedByEmployee)
                .ThenInclude(x => x.MasterJobTitle)
                .Include(x => x.OrderDetail)
            .Where(x => !x.IsDeleted &&
                            x.CompletedByEmployee.MasterJobTitle.Id == jobTitleId &&
                            x.CompletedOn.Month == month &&
                            x.CompletedOn.Year == year)
            .ToListAsync();

            var dataGrp = data
            .GroupBy(x => x.CompletedBy);
            var res = new List<EmployeeSalaryLedgerResponse>();
            foreach (var item in dataGrp)
            {
                res.Add(new EmployeeSalaryLedgerResponse()
                {
                    EmployeeName = item.FirstOrDefault().CompletedByEmployee.FirstName + " " + item.FirstOrDefault().CompletedByEmployee.LastName,
                    EmployeeId = item.FirstOrDefault().CompletedBy ?? 0,
                    Qty = item.Count(),
                    Amount = item.Sum(x => x.Price+x.Extra) ?? 0
                });
            }
            if (crystalUsedDataDic != null)
            {
                foreach (var item in crystalUsedDataDic)
                {
                    res.Add(new EmployeeSalaryLedgerResponse()
                    {
                        EmployeeName = item.FirstOrDefault().Employee.FirstName.Trim() + " " + item.FirstOrDefault().Employee.LastName?.Trim(),
                        EmployeeId = item.Key,
                        Qty = item.Count(),
                        Amount = (item.Sum(x => x.CrystalTrackingOutDetails.Where(y => !y.IsAlterWork).Sum(y => y.CrystalLabourCharge + y.ArticalLabourCharge)))
                    });
                }
            }
            return res.OrderBy(x=>x.EmployeeName).ToList();
        }
    }
}
