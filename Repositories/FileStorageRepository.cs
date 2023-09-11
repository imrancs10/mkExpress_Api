using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MKExpress.API.Data;
using MKExpress.API.Enums;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories
{
    public class FileStorageRepository : IFileStorageRepository
    {
        private readonly MKExpressDbContext _context;
        public FileStorageRepository(MKExpressDbContext context)
        {
            _context = context;
        }
        public async Task<FileStorage> Add(FileStorage fileStorage)
        {
            var entity = _context.FileStorages.Attach(fileStorage);
            entity.State = EntityState.Added;
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<int> Delete(int storageId)
        {
            FileStorage fileStorage = await _context.FileStorages.Where(fs => fs.Id == storageId).FirstOrDefaultAsync();
            _context.FileStorages.Remove(fileStorage);
            return await _context.SaveChangesAsync();
        }

        public async Task<FileStorage> Get(int storageId)
        {
            return await _context.FileStorages.Where(fs => fs.Id == storageId).FirstOrDefaultAsync();
        }

        public async Task<List<FileStorage>> GetByModuleIds(List<int> moduleIds, ModuleNameEnum moduleName)
        {
            try
            {
                return await _context.FileStorages
                    .Where(fs => moduleIds.Contains(fs.ModuleId) && fs.ModuleName.Equals(moduleName.ToString()))
                    .OrderByDescending(x=>x.CreatedAt)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<FileStorage>> GetByModuleName(ModuleNameEnum moduleName)
        {
            return await _context.FileStorages
                .Where(fs => fs.ModuleName.Equals(moduleName.ToString()))
                .ToListAsync();
        }

        public async Task<FileStorage> Update(FileStorage fileStorage)
        {
            EntityEntry<FileStorage> oldFileStorage = _context.Update(fileStorage);
            oldFileStorage.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return oldFileStorage.Entity;
        }
    }
}
