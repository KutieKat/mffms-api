using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Dtos.ChiTietDonNhapHangDto
{
    public class ChiTietDonNhapHangForCreateMultiple : BaseDto
    {
        [Required]
        public string MaDonNhapHang { get; set; }
        [Required]
        public string MaTSTB { get; set; }
        [Required]
        public int SoLuong { get; set; }
        [Required]
        public double DonGia { get; set; }
        [Required]
        public string DVT { get; set; }
        public double ThanhTien { get; set; }

        public override string ToString()
        {
            return "MaDonNhapHang, MaTSTB, SoLuong, DonGia, DVT";
        }
    }
}
