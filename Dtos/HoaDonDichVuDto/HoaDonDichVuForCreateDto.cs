using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Dtos.HoaDonDichVuDto
{
    public class HoaDonDichVuForCreateDto : BaseDto
    {
        public string MaKhachHang { get; set; }
        [Required]
        public string MaDichVu { get; set; }
        [Required]
        public DateTime NgaySuDung { get; set; }
        [Required]
        public DateTime NgayLap { get; set; }
        [Required]
        public double ThanhTien{get;set;}
        [Required]
        public string DaThanhToan{get;set;}
        public string GhiChu { get; set; }
    }
}
