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
        public int SoHDDV { get; set; }
        public string MaKhachHang { get; set; }
        public KhachHang KhachHang { get; set; }
        public int MaDichVu { get; set; }
        public DichVu DichVu { get; set; }
        public DateTime NgaySuDung { get; set; }
        public DateTime NgayLap { get; set; }
        public string GhiChu { get; set; }
        public double ThanhTien {get;set;}
        public string DaThanhToan{get;set;}
        public ICollection<ChiTietHDDV> ChiTietHDDV { get; set; }

    }
}
