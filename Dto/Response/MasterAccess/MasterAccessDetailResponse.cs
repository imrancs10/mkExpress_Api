namespace MKExpress.Web.API.Dto.Response.MasterAccess
{
    public class MasterAccessDetailResponse
    {
        public int AccessId { get; set; }
        public int MasterMenuId { get; set; }
        public int ParentMenuId { get; set; }
        public string MenuName { get; set; }
        public string Url { get; set; }
    }
}
