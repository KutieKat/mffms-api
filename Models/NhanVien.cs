using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Models
{
    public class NhanVien : BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string MaNhanVien { get; set; }
        public string TenNhanVien { get; set; }
        public string GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string ChucVu { get; set; }
        public string SoDienThoai { get; set; }
        public string SoCMND { get; set; }
        public double Luong { get; set; }
        public string GhiChu { get; set; }
        public string DiaChi { get; set; }

        public ICollection<PhieuDatSan> PhieuDatSan { get; set; }
        public ICollection<DonNhapHang> DonNhapHang { get; set; }
        public ICollection<HoaDonDichVu> HoaDonDichVu { get; set; }
    }
}
