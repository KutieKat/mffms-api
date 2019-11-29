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
        public int MaPhieuDatSan { get; set; }
        public string MaKhachHang { get; set; }
        public KhachHang KhachHang { get; set; }
        public string MaNhanVien { get; set; }
        public NhanVien NhanVien { get; set; }
        public DateTime NgayLap { get; set; }

        public ICollection<ChiTietPhieuDatSan> ChiTietPhieuDatSan { get; set; }
    }
}
