using MKExpress.API.DTO.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.DTO.Request
{
    public class ContainerRequest:BaseRequest
    {
        public int ContainerNo { get; set; }
        public string ContainerType { get; set; }
    }
}
