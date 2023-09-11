using System;
using System.ComponentModel.DataAnnotations;

namespace MKExpress.API.Models.BaseModels
{

    public class BaseModel
    {
        [Key] public virtual int Id { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}