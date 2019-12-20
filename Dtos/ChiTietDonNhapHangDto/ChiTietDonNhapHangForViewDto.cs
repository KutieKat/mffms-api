using MFFMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Dtos.ChiTietDonNhapHangDto
{
    public class ChiTietDonNhapHangForViewDto : BaseDto
    {
        public DonNhapHang DonNhapHang { get; set; }
        public TaiSanThietBi TaiSanThietBi { get; set; }
        public int SoLuong { get; set; }
        public double DonGia { get; set; }
        public string DVT { get; set; }
        public double ThanhTien { get; set; }
    }
}
