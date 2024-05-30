using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;
using MKExpress.API.Data;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Models;
using MKExpress.API.Repository.IRepository;
using System;
using System.Linq;

namespace MKExpress.API.Repository
{
    public class MenuRepository: IMenuRepository
    {
        private readonly MKExpressContext _context;

        public MenuRepository(MKExpressContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> AddMenuAsync(Menu menu)
        {
            if (menu == null)
                throw new ArgumentNullException(nameof(menu));

            _context.Menus.Add(menu);
           return await _context.SaveChangesAsync()>0;
        }

        public async Task<bool> DeleteMenuAsync(Guid id)
        {
            var menu = await _context.Menus.FindAsync(id);
            if (menu == null)
                throw new KeyNotFoundException("Menu not found");

            menu.IsDeleted = true;
            _context.Menus.Update(menu);
            return await _context.SaveChangesAsync()>0;
        }

        public async Task<bool> UpdateMenuAsync(Menu menu)
        {
            if (menu == null)
                throw new ArgumentNullException(nameof(menu));

            var existingMenu = await _context.Menus.FindAsync(menu.Id) ?? throw new KeyNotFoundException("Menu not found");
            existingMenu.Name = menu.Name;
            existingMenu.Code = menu.Code;
            existingMenu.Link = menu.Link;

            _context.Menus.Update(existingMenu);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Menu> GetMenuByIdAsync(Guid id)
        {
            return await _context.Menus
                .Where(x=>!x.IsDeleted&& x.Id==id)
                .FirstOrDefaultAsync();
        }

        public async Task<PagingResponse<Menu>> GetAllMenusAsync(PagingRequest request)
        {
            var query = _context.Menus.Where(m => !m.IsDeleted);
            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((request.PageNo - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return new PagingResponse<Menu>
            {
                TotalRecords = totalCount,
                Data = items
            };
        }

        public async Task<PagingResponse<Menu>> SearchMenusAsync(SearchPagingRequest request)
        {
            var query = _context.Menus
                .Where(m => !m.IsDeleted && (string.IsNullOrEmpty(request.SearchTerm) || m.Name.Contains(request.SearchTerm) || m.Code.Contains(request.SearchTerm) || m.Link.Contains(request.SearchTerm)));

            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((request.PageNo - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return new PagingResponse<Menu>
            {
                TotalRecords = totalCount,
                Data = items
            };
        }
    }
}
