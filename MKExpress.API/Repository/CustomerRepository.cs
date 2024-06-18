using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MKExpress.API.Common;
using MKExpress.API.Contants;
using MKExpress.API.Data;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Exceptions;
using MKExpress.API.Extension;
using MKExpress.API.Models;
using MKExpress.API.Repository;
using MKExpress.API.Services;

namespace MKExpress.API.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly MKExpressContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IAppSettingRepository _appSettingRepository;
        private readonly ICommonService _commonService;
        public CustomerRepository(MKExpressContext context,IUserRepository userRepository,IAppSettingRepository appSettingRepository, IUserRoleRepository userRoleRepository,ICommonService commonService)
        {
            _context = context;
            _userRepository = userRepository;
            _appSettingRepository = appSettingRepository;
            _userRoleRepository = userRoleRepository;
            _commonService = commonService;
        }
        public async Task<Customer> Add(Customer customer)
        {
            var oldCustomer = await _context.Customers.Where(x =>!x.IsDeleted && (x.ContactNo == customer.ContactNo ||x.Email==customer.Email)).FirstOrDefaultAsync();
            if (oldCustomer!=null)
            {
                if(oldCustomer.ContactNo==customer.ContactNo)
                throw new BusinessRuleViolationException(StaticValues.ErrorType_CustomerAlreadyExist, StaticValues.Error_CustomerAlreadyExist(customer.ContactNo));
                if (oldCustomer.Email == customer.Email)
                    throw new BusinessRuleViolationException(StaticValues.ErrorType_CustomerAlreadyExist, StaticValues.Error_CustomerAlreadyExist(customer.Email));
            }
            var trans=await _context.Database.BeginTransactionAsync();
            var entity = _context.Customers.Attach(customer);
            entity.State = EntityState.Added;
            if(await _context.SaveChangesAsync() > 0)
            {
                var _defaultPassword = await _appSettingRepository.GetAppSettingValueByKey<string>("defaultPassword");
                var customerAdminRole = await _userRoleRepository.GetRoleByCode("customeradmin");
                if (string.IsNullOrEmpty(_defaultPassword) || customerAdminRole==null)
                {
                    trans.Rollback();
                    throw new BusinessRuleViolationException(StaticValues.ErrorType_InvalidConfigData, StaticValues.ErrorType_InvalidConfigData);
                }
                var user = new User()
                {
                    CreatedAt = DateTime.Now,
                    Email = customer.Email,
                    FirstName = customer.Name,
                    LastName = string.Empty,
                    Gender = Enums.GenderEnum.IPreferNotToSay,
                    IsBlocked = false,
                    IsCustomer = true,
                    IsEmailVerified = true,
                    IsLocked = false,
                    IsDeleted = false,
                    IsTcAccepted = true,
                    Password = _commonService.GeneratePasswordHash(_defaultPassword?.EncodeBase64()??string.Empty),
                    UserName = customer.Email,
                    RoleId=customerAdminRole.Id,
                    Mobile=customer.ContactNo  
                };
               var savedUser=await _userRepository.Add(user);
                if(Guid.TryParse(savedUser.Id.ToString(), out Guid Output))
                {
                   await trans.CommitAsync();
                    return entity.Entity;
                }
            }
            await trans.RollbackAsync();
            return default;
        }

        public async Task<int> Delete(Guid customerId)
        {
            Customer customer = await _context.Customers
                .Where(customer => customer.Id == customerId)
                .FirstOrDefaultAsync()?? throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);
            
            if (customer.IsDeleted)
            {
                throw new BusinessRuleViolationException(StaticValues.RecordAlreadyDeletedError, StaticValues.RecordAlreadyDeletedMessage);
            }
            customer.IsDeleted = true;
            var entity = _context.Customers.Update(customer);
            entity.State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public async Task<Customer> Get(Guid customerId)
        {
            return await _context.Customers
                .Include(x => x.City)
                .Where(customer => customer.Id == customerId)
                .FirstOrDefaultAsync() ?? throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage); ;
        }

        public async Task<Customer> Update(Customer customer)
        {
            EntityEntry<Customer> oldCustomer = _context.Update(customer);
            oldCustomer.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return oldCustomer.Entity;
        }

        public async Task<PagingResponse<Customer>> GetAll(PagingRequest pagingRequest)
        {  
            var data = _context.Customers
              .Include(x => x.City)
             .Where(x => !x.IsDeleted)
                .OrderBy(x => x.Name)
                .AsQueryable();
            PagingResponse<Customer> pagingResponse = new ()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data =await data.Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1)).Take(pagingRequest.PageSize).ToListAsync(),
                TotalRecords =await data.CountAsync()
            };
            return pagingResponse;
        }

        public async Task<PagingResponse<Customer>> Search(SearchPagingRequest searchPagingRequest)
        {
           string searchTerm = string.IsNullOrEmpty(searchPagingRequest.SearchTerm) ? string.Empty : searchPagingRequest.SearchTerm.ToLower();
            var data = _context.Customers
                .Include(x=>x.City)
                .Where(customer => !customer.IsDeleted &&(
                        string.IsNullOrEmpty(searchTerm) ||
                        customer.Name.Contains(searchTerm) ||
                         customer.Email.Contains(searchTerm) ||
                           customer.Address.Contains(searchTerm) ||
                             customer.ZipCode.Contains(searchTerm) ||
                               customer.City.Value.Contains(searchTerm) ||
                        customer.MaxDeliveryAttempt.ToString().Contains(searchTerm))
                    )
                .OrderBy(x => x.Name)
                    .AsQueryable();

            var filterData =await data.Skip(searchPagingRequest.PageSize * (searchPagingRequest.PageNo - 1)).Take(searchPagingRequest.PageSize).ToListAsync();

            PagingResponse<Customer> pagingResponse = new ()
            {
                PageNo = searchPagingRequest.PageNo,
                PageSize = searchPagingRequest.PageSize,
                Data = filterData,
                TotalRecords =await data.CountAsync()
            };
            return pagingResponse;
        }

        public async Task<List<Customer>> GetCustomers(string contactNo)
        {

            return await _context.Customers
                 .Include(x => x.City)
               .Where(x => !x.IsDeleted && x.ContactNo == contactNo)
               .OrderBy(x => x.Name)
               .ToListAsync();
        }

        public async Task<List<DropdownResponse>> GetCustomersDropdown()
        {
            return await _context.Customers
                .Where(x=>!x.IsDeleted)
                .OrderBy(x =>x.Name)
                .Select(x=>new DropdownResponse() { Id=x.Id,Value=$"{x.Name}"})
                .ToListAsync();
        }

        public async Task<bool> ResetPassword(Guid customerId)
        {
            var oldCustomer = await _context.Customers
                .Where(customer => customer.Id == customerId)
                .FirstOrDefaultAsync() ?? throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);

            var oldUser = await _context.Users
              .Where(user => user.Email == oldCustomer.Email)
              .FirstOrDefaultAsync() ?? throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);

            var _defaultPassword = await _appSettingRepository.GetAppSettingValueByKey<string>("defaultPassword");
            _defaultPassword = string.IsNullOrEmpty(_defaultPassword) ? "Admin@12345" : _defaultPassword;

            oldUser.Password = _commonService.GeneratePasswordHash(_defaultPassword.EncodeBase64());
            _context.Update(oldUser);
            return await _context.SaveChangesAsync() > 0;

        }

        public async Task<bool> BlockUnblockCustomer(Guid customerId, bool isBlocked)
        {
            var oldCustomer = await _context.Customers
                .Where(customer => customer.Id == customerId)
                .FirstOrDefaultAsync() ?? throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);

            var oldUser = await _context.Users
              .Where(user => user.Email == oldCustomer.Email)
              .FirstOrDefaultAsync() ?? throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);

            oldCustomer.IsBlocked = isBlocked;
            oldUser.IsBlocked = isBlocked;
            var trans = _context.Database.BeginTransaction();
            _context.Attach(oldUser);
            _context.Attach(oldCustomer);
            if (await _context.SaveChangesAsync() > 0)
            {
                trans.Commit();
                return true;
            }
            trans.Rollback();
            return false;   
        }
    }
}
