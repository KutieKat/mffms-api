using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Models
{
    public class ChiTietDonNhapHang : BaseModel
    {
        public string MaDonNhapHang { get; set; }
        public DonNhapHang DonNhapHang { get; set; }
        public string MaTSTB { get; set; }
        public TaiSanThietBi TaiSanThietBi {get;set;}
        public int SoLuong { get; set; }
        public double DonGia { get; set; }
        public string DVT { get; set; }
        public double ThanhTien { get; set; }
    }
}
