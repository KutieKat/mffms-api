using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Dtos.ChiTietHDDVDto
{
    public class ChiTietHDDVForUpdateDto : BaseDto
    {
        [Required]
        public int SoLuong { get; set; }
        [Required]
        public double DonGia{get;set;}
        [Required]
        public double ThanhTien{get;set;}
    }
}
