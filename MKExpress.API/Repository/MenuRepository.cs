using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;
using MKExpress.API.Contants;
using MKExpress.API.Data;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Exceptions;
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

            if(await _context.Menus.Where(x=>!x.IsDeleted && ((x.Name==menu.Name && x.MenuPosition==menu.MenuPosition) || (x.Link == menu.Link && x.MenuPosition == menu.MenuPosition))).AnyAsync())
                throw new BusinessRuleViolationException(StaticValues.RecordAlreadyExistError, StaticValues.RecordAlreadyExistMessage($"{menu.Name} or {menu.Link} with {menu.MenuPosition}"));
            _context.Menus.Add(menu);
           return await _context.SaveChangesAsync()>0;
        }

        public async Task<bool> DeleteMenuAsync(Guid id)
        {
            var menu = await _context.Menus.FindAsync(id) ?? throw new BusinessRuleViolationException(StaticValues.ErrorType_RecordNotFound, StaticValues.Error_RecordNotFound);
            menu.IsDeleted = true;
            _context.Menus.Update(menu);
            return await _context.SaveChangesAsync()>0;
        }

        public async Task<bool> UpdateMenuAsync(Menu menu)
        {
            if (menu == null)
                throw new ArgumentNullException(nameof(menu));

            var existingMenu = await _context.Menus.FindAsync(menu.Id) ?? throw new BusinessRuleViolationException(StaticValues.ErrorType_RecordNotFound,StaticValues.Error_RecordNotFound);
            existingMenu.Name = menu.Name;
            existingMenu.Code = menu.Code;
            existingMenu.Link = menu.Link;
            existingMenu.Title = menu.Title;
            existingMenu.Icon = menu.Icon;
            existingMenu.Disable = menu.Disable;
            existingMenu.MenuPosition= menu.MenuPosition;
            existingMenu.Tag = menu.Tag;
            existingMenu.DisplayOrder = menu.DisplayOrder;
            existingMenu.Title = menu.Title;

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
            var query = _context.Menus.Where(m => !m.IsDeleted)
                .OrderBy(x=>x.MenuPosition)
                .ThenBy(x=>x.DisplayOrder);
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
                .Where(m => !m.IsDeleted && (string.IsNullOrEmpty(request.SearchTerm) || 
                m.Name.Contains(request.SearchTerm) ||
                m.Code.Contains(request.SearchTerm) ||
                m.Link.Contains(request.SearchTerm)))
                .OrderBy(x => x.MenuPosition)
                .ThenBy(x => x.DisplayOrder); ;

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
