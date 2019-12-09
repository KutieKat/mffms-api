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
        public int MaDichVu { get; set; }
        [Required]
        public DateTime NgaySuDung { get; set; }
        [Required]
        public DateTime NgayLap { get; set; }
        public string GhiChu { get; set; }
    }
}
