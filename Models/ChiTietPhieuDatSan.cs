using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Models
{
    public class ChiTietPhieuDatSan : BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaChiTietPDS { get; set; }
        public int MaPhieuDatSan { get; set; }
        public PhieuDatSan PhieuDatSan { get; set; }
        public int MaSanBong { get; set; }
        public SanBong SanBong { get; set; }
        public double ThoiGianBatDau { get; set; }
        public double ThoiGianKetThuc { get; set; }
        public DateTime NgayDat { get; set; }
        public decimal TienCoc { get; set; }
    }
}
