using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class AppSetting:BaseModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string DataType { get; set; }
        public int GroupId { get; set; }
        public bool AllowDelete{ get; set; }
        public bool AllowUpdate { get; set; }

        [ForeignKey("GroupId")]
        public AppSettingGroup AppSettingGroup { get; set; }

    }
}
