using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Dtos.DonNhapHangDto
{
    public class DonNhapHangForUpdateDto : BaseDto
    {
        [Required]
        public string MaNhaCungCap { get; set; }
        [Required]
        public string MaNhanVien { get; set; }
        public DateTime NgayGiaoHang { get; set; }
        public DateTime NgayLap { get; set; }
        public string GhiChu { get; set; }
        public double ThanhTien { get; set; }
        public double DaThanhToan { get; set; }
    }
}
