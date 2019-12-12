using MFFMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Dtos.DonNhapHangDto
{
    public class DonNhapHangForViewDto : BaseDto
    {
        public int MaDonNhapHang { get; set; }
        public NhaCungCap NhaCungCap { get; set; }
        public NhanVien NhanVien { get; set; }
        public DateTime NgayGiaoHang { get; set; }
        public string NoiNhanHang { get; set; }
    }
}
