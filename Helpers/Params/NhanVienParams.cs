using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Helpers.Params
{
    public class NhanVienParams : BaseParams
    {
        public string MaNhanVien { get; set; }
        public string TenNhanVien { get; set; }
        public string GioiTinh { get; set; }
        public DateTime NgaySinhBatDau { get; set; }
        public DateTime NgaySinhKetThuc { get; set; }
        public string ChucVu { get; set; }
        public string SoDienThoai { get; set; }
        public string SoCMND { get; set; }
        public double LuongBatDau { get; set; }
        public double LuongKetThuc { get; set; }
        public string GhiChu { get; set; }
        public string DiaChi { get; set; }
    }
}
