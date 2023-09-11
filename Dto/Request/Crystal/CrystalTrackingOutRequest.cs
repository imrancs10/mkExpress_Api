using MKExpress.API.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.Web.API.Dto.Request
{
    public class CrystalTrackingOutRequest
    {
        public int Id { get; set; }
        public int OrderDetailId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Note { get; set; }

        public List<CrystalTrackingOutDetailRequest> CrystalTrackingOutDetails { get; set; }
    }
}
