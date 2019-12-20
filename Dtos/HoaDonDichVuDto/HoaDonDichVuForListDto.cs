using MFFMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Dtos.HoaDonDichVuDto
{
    public class HoaDonDichVuForListDto : BaseDto
    {
        public string SoHDDV { get; set; }
        public KhachHang KhachHang { get; set; }
        public NhanVien NhanVien { get; set; }
        public DateTime NgaySuDung { get; set; }
        public DateTime NgayLap { get; set; }
        public double ThanhTien{get;set;}
        public string DaThanhToan{get;set;}
        public string GhiChu { get; set; }
        public string TinhTrang { get; set; }
    }
}
