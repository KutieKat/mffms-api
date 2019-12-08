﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Dtos.DichVuDto
{
    public class DichVuForUpdateDto : BaseDto
    {
        [Required]
        public string TenDichVu { get; set; }
        [Required]
        public decimal DonGia { get; set; }
        [Required]
        public string DVT { get; set; }
        public string GhiChu { get; set; }
    }
}
