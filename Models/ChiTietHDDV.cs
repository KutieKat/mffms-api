using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Models
{
    public class ChiTietHDDV : BaseModel
    {
        public int SoHDDV { get; set; }
        public HoaDonDichVu HoaDonDichVu { get; set; }
        public int MaDichVu { get; set; }
        public DichVu DichVu {get;set;}
        public int SoLuong { get; set; }
    }
}
