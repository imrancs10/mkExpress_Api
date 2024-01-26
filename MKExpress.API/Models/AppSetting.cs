namespace MKExpress.API.Models
{
    public class AppSetting:BaseModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string DataType { get; set; }
        public int Group { get; set; }
        public bool AllowDelete{ get; set; }
        public bool AllowUpdate { get; set; }
    }
}
