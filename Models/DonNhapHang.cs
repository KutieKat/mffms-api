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
        public int MaDonNhapHang { get; set; }
        public int MaNhaCungCap { get; set; }
        public NhaCungCap NhaCungCap { get; set; }
        public string MaNhanVien { get; set; }
        public NhanVien NhanVien { get; set; }
        public DateTime NgayGiaoHang { get; set; }
        public string NoiNhanHang { get; set; }

        public ICollection<ChiTietDonNhapHang> ChiTietDonNhapHang { get; set; }

    }
}
