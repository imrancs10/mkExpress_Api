using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Dto.Response
{
    public class CrystalTrackingOutResponse
    {
        public int Id { get; set; }
        public int OrderDetailId { get; set; }
        public int EmployeeId { get; set; }
        public string KandooraNo { get; set; }
        public string OrderNo { get; set; }
        public string EmployeeName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Note { get; set; }
        public List<CrystalTrackingOutDetailResponse> CrystalTrackingOutDetails { get; set; }
    }
}
