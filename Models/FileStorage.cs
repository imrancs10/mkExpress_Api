using MKExpress.API.Models.BaseModels;

namespace MKExpress.API.Models
{
    public class FileStorage : BaseModel
    {
        public string FilePath { get; set; }
        public string ThumbPath { get; set; }
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public string Remark { get; set; }
    }
}
