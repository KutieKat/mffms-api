using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Models
{
    public class HoaDonDichVu : BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string SoHDDV { get; set; }
        public string MaKhachHang { get; set; }
        public KhachHang KhachHang { get; set; }
        public string MaNhanVien { get; set; }
        public NhanVien NhanVien { get; set; }
        public DateTime NgaySuDung { get; set; }
        public DateTime NgayLap { get; set; }
        public string GhiChu { get; set; }
        public double ThanhTien {get;set;}
        public double DaThanhToan{get;set;}
        public string TinhTrang { get; set; }
        public ICollection<ChiTietHDDV> ChiTietHDDV { get; set; }

    }
}
