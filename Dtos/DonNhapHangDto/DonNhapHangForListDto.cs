using MFFMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Dtos.DonNhapHangDto
{
    public class DonNhapHangForListDto : BaseDto
    {
        public string MaDonNhapHang { get; set; }
        public NhaCungCap NhaCungCap { get; set; }
        public NhanVien NhanVien { get; set; }
        public DateTime NgayGiaoHang { get; set; }
        public DateTime NgayLap { get; set; }
        public string GhiChu { get; set; }
        public double ThanhTien { get; set; }
        public double DaThanhToan { get; set; }
        public string TinhTrang { get; set; }
    }
}
