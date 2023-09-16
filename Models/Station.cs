using MKExpress.API.Models.BaseModels;

namespace MKExpress.API.Models
{
    public class Station:BaseModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public Member Member { get; set; }
    }
}
