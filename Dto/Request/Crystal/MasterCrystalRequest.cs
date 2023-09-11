using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Dto.Request
{
    public class MasterCrystalRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AlertQty { get; set; }
        public int BrandId { get; set; }
        public int SizeId { get; set; }
        public int ShapeId { get; set; }
        public int QtyPerPacket { get; set; }
        public string Barcode { get; set; }
        public int CrystalId { get; set; }
        public bool IsArtical { get; set; }
    }
}
