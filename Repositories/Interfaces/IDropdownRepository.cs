using MKExpress.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories.Interfaces
{
    public interface IDropdownRepository
    {
        Task<List<Dropdown<Employee>>> Employee(string searchTerm);
        Task<List<Dropdown>> JobTitle();
        Task<List<Dropdown>> Customers(string searchTerm);
        Task<List<Dropdown>> CustomerOrders();
        Task<List<Dropdown>> Products();
        //Task<List<Dropdown<Supplier>>> Suppliers();
        Task<List<Dropdown>> DesignCategory();
        Task<List<Dropdown>> OrderDetailNos(bool excludeDelivered);
        Task<List<Dropdown>> WorkTypes();
    }
}
