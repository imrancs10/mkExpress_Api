using MKExpress.API.Models.BaseModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class CustomerMeasurement : CustomerMeasurementBaseModel
    {
        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
    }
}
