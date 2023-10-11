using MKExpress.API.DTO.Base;
using MKExpress.API.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.DTO.Request
{
    public class ContainerRequest:BaseRequest
    { 
        public Guid ContainerTypeId { get; set; }
        public Guid JourneyId { get; set; }
        public List<ContainerDetailRequest> ContainerDetails { get; set; }
    }
}
