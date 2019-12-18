using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MFFMS.API.Dtos.CaiDatDto
{
    public class CaiDatForUpdateDto : BaseDto
    {
        [Required]
        public string TenSanBong { get; set; }
        [Required]
        public string DiaChi { get; set; }
        [Required]
        public string SoDienThoai {get;set;}
        [Required]
        public string Fax {get;set;}
        [Required]
        public string DiaChiTrenPhieu{get;set;}

    }
}