using MKExpress.API.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Models
{
    public class MasterWorkDescription:MasterBaseModel
    {
        public List<OrderWorkDescription> OrderWorkDescriptions { get; set; }
    }
}
