using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Dtos.DonNhapHangDto
{
    public class DonNhapHangForUpdateDto : BaseDto
    {
        public string MaNhaCungCap { get; set; }
        public string MaNhanVien { get; set; }
        public DateTime NgayGiaoHang { get; set; }
        public string GhiChu { get; set; }
        public double ThanhTien { get; set; }
        public double DaThanhToan { get; set; }
    }
}
