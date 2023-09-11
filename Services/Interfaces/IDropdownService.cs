using MKExpress.API.Dto.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Services.Interfaces
{
    public interface IDropdownService
    {
        Task<List<DropdownResponse<EmployeeResponse>>> Employee(string searchTerm);
        Task<List<DropdownResponse>> JobTitle();
        Task<List<DropdownResponse>> Products();
        Task<List<DropdownResponse<SupplierResponse>>> Suppliers();
        Task<List<DropdownResponse>> Customers(string searchTerm);
        Task<List<DropdownResponse>> CustomerOrders();
        Task<List<DropdownResponse>> DesignCategory();
        Task<List<DropdownResponse>> OrderDetailNos(bool excludeDelivered);
        Task<List<DropdownResponse>> WorkTypes();
    }
}
