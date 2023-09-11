using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Dto.Response
{
    public class OrderWorkDescriptionResponse
    {
        public int Id { get; set; }
        public int OrderDetailId { get; set; }
        public int WorkDescriptionId { get; set; }
        public string NewModel { get; set; }
        public string SamePrint { get; set; }
        public string LikeModel { get; set; }
    }
}
