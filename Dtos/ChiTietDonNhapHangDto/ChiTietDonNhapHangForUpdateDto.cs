using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Dtos.ChiTietDonNhapHangDto
{
    public class ChiTietDonNhapHangForUpdateDto : BaseDto
    {
        [Required]
        public int SoLuong { get; set; }
        [Required]
        public double DonGia { get; set; }
        [Required]
        public string DVT { get; set; }
        public double ThanhTien { get; set; }
    }
}
