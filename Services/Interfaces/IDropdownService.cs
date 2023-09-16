using MKExpress.API.Dto.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Services.Interfaces
{
    public interface IDropdownService
    {
        Task<List<DropdownResponse>> JobTitle();
        Task<List<DropdownResponse>> Customers(string searchTerm);
    }
}
