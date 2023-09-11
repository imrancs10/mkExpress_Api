using Microsoft.EntityFrameworkCore;
using MKExpress.API.Data;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories
{

    public class UserRepository : IUserRepository
    {
        private readonly MKExpressDbContext _context;

        public UserRepository(MKExpressDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUser(User user)
        {
            user.Name = $"{user.FirstName} {user.LastName}";
            user.UserName = user.Email;
            var entity = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public Task<User> UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUser(string email = null)
        {
            email = string.IsNullOrEmpty(email) ? "" : email;
            var data= await _context.Users.Where(user => user.Email.ToUpper().Equals(email.ToUpper()) && !user.IsDeleted).FirstOrDefaultAsync();
            return data;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.Where(user => !user.IsDeleted).ToListAsync();
        }

        public async Task<int> AddRoles(List<string> roles)
        {
            if (roles.Count == 0) return 0;
            List<UserRole> userRoles = new List<UserRole>();
            roles.ForEach(x =>
            {
                userRoles.Add(new UserRole()
                {
                    Code = x.ToUpper(),
                    Name = x
                });
            });

            await _context.AddRangeAsync(userRoles);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteUser(int empId)
        {
            var user = await _context.Users.Where(x => x.EmployeeId == empId).FirstOrDefaultAsync();
            if (user == null)
                return default;
            user.IsDeleted = true;
           var entity= _context.Users.Attach(user);
            entity.State = EntityState.Modified;
           return await _context.SaveChangesAsync();

        }
    }
}