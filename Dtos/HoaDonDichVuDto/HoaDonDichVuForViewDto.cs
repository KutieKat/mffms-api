using MFFMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Dtos.HoaDonDichVuDto
{
    public class HoaDonDichVuForViewDto : BaseDto
    {
        public int SoHDDV { get; set; }
        public KhachHang KhachHang { get; set; }
        public DichVu DichVu { get; set; }
        public DateTime NgaySuDung { get; set; }
        public DateTime NgayLap { get; set; }
        public string GhiChu { get; set; }
    }
}
