using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Models
{
    public class ChiTietDonNhapHang : BaseModel
    {
        public int MaDonNhapHang { get; set; }
        public DonNhapHang DonNhapHang { get; set; }
        public int MaTSTB { get; set; }
        public TaiSanThietBi TaiSanThietBi {get;set;}
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public string DVT { get; set; }
    }
}
