using MKExpress.API.Models.BaseModels;

namespace MKExpress.Web.API.Models
{
    public class MasterMenu:BaseModel
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }
        public int? ParentId { get; set; }
        public bool Disable { get; set; }
        public string Url { get; set; }
    }
}
