using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Helpers.Params
{
    public class DonNhapHangParams : BaseParams
    {
        public string MaDonNhapHang { get; set; }
        public string MaNhaCungCap { get; set; }
        public string MaNhanVien { get; set; }
        public string GhiChu { get; set; }
        public double ThanhTienBatDau { get; set; }
        public double ThanhTienKetThuc { get; set; }
        public double DaThanhToanBatDau { get; set; }
        public double DaThanhToanKetThuc { get; set; }
        public string TinhTrang { get; set; }
        public DateTime NgayGiaoHangBatDau { get; set; }
        public DateTime NgayGiaoHangKetThuc { get; set; }
        public DateTime NgayLapBatDau { get; set; }
        public DateTime NgayLapKetThuc { get; set; }
    }
}
