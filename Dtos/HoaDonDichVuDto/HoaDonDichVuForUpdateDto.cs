using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Dtos.HoaDonDichVuDto
{
    public class HoaDonDichVuForUpdateDto : BaseDto
    {
        [Required]
        public string MaKhachHang { get; set; }
        [Required]
        public string MaNhanVien { get; set; }
        public DateTime NgaySuDung { get; set; }
        public DateTime NgayLap { get; set; }
        public double ThanhTien {get;set;}
        public double DaThanhToan{get;set;}
        public string GhiChu { get; set; }
    }
}
