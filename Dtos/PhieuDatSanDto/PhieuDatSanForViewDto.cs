using MFFMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Dtos.PhieuDatSanDto
{
    public class PhieuDatSanForViewDto : BaseDto
    {
        public string MaPhieuDatSan { get; set; }
        public KhachHang KhachHang { get; set; }
        public NhanVien NhanVien { get; set; }
        public DateTime NgayLap { get; set; }
        public double TongTien { get; set; }
    }
}
