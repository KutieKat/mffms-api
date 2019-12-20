using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Models
{
    public class KhachHang : BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]     
        public string MaKhachHang { get; set; }
        public string TenKhachHang { get; set; }
        public string GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }
        public string GhiChu { get; set; }

        public ICollection<PhieuDatSan> PhieuDatSan { get; set; }
        public ICollection<HoaDonDichVu> HoaDonDichVu { get; set; }
    }
}
