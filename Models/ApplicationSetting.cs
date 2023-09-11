using MKExpress.API.Models.BaseModels;
using System;

namespace MKExpress.Web.API.Models
{
    public class ApplicationSetting:BaseModel
    {
        public string SettingKey { get; set; }
        public string DisplayName { get; set; }
        public string SettingValue { get; set; }
        public string DataType { get; set; } = "string";
        public DateTime EffectFromDate { get; set; }
    }
}
