using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Helpers.Params
{
    public class PhieuDatSanParams : BaseParams
    {
        public string MaPhieuDatSan { get; set; }
        public string MaKhachHang { get; set; }
        public string MaNhanVien { get; set; }
        public DateTime NgayLapBatDau { get; set; }
        public DateTime NgayLapKetThuc {get;set;}
        public double TongTienBatDau { get; set; }
        public double TongTienKetThuc {get;set;}
    }
}
