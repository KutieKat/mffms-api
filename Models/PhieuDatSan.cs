using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Models
{
    public class PhieuDatSan : BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string MaPhieuDatSan { get; set; }
        public string MaKhachHang { get; set; }
        public KhachHang KhachHang { get; set; }
        public int MaSanBong { get; set; }
        public SanBong SanBong { get; set; }
        public string MaNhanVien { get; set; }
        public string NhanVien { get; set; }
        public DateTime NgayDatSan { get; set; }
        public DateTime NgayLap { get; set; }

        public ICollection<ChiTietGioThue> ChiTietGioThue { get; set; }
    }
}
