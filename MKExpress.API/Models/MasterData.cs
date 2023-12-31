﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MKExpress.API.Models
{
    public class MasterData : BaseModel
    {
        [Key]
        public override Guid Id { get => base.Id; set => base.Id = value; }
        public string MasterDataType { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
        public string Remark { get; set; }
    }
}
