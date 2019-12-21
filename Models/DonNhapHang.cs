using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Models
{
    public class DonNhapHang : BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string MaDonNhapHang { get; set; }
        public string MaNhaCungCap { get; set; }
        public NhaCungCap NhaCungCap { get; set; }
        public string MaNhanVien { get; set; }
        public NhanVien NhanVien { get; set; }
        public DateTime NgayGiaoHang { get; set; }
        public DateTime NgayLap { get; set; }
        public string GhiChu { get; set; }
        public double ThanhTien { get; set; }
        public double DaThanhToan { get; set; }
        public string TinhTrang { get; set; }
        public ICollection<ChiTietDonNhapHang> ChiTietDonNhapHang { get; set; }

    }
}
