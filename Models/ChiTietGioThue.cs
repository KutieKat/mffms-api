using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Models
{
    public class ChiTietGioThue : BaseModel
    {
        public string MaPhieuDatSan { get; set; }
        public PhieuDatSan PhieuDatSan { get; set; }
        public int MaGio { get; set; }
        public GiaGioThue GiaGioThue { get; set; }
        public decimal TienCoc { get; set; }
    }
}
