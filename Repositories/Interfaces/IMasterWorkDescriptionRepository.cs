using MKExpress.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories.Interfaces
{
    public interface IMasterWorkDescriptionRepository:ICrudRepository<MasterWorkDescription>
    {
        Task<List<MasterWorkDescription>> GetByWorkTypes(string worktype);
        Task<int> SaveOrderWorkDescription(List<OrderWorkDescription> orderWorkDescriptions);
        Task<List<OrderWorkDescription>> GetOrderWorkDescription(int orderDetailId);

    }
}
