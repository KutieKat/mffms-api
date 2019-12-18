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
        public string NoiNhanHang { get; set; }
        public DateTime NgayGiaoHangBatDau { get; set; }
        public DateTime NgayGiaoHangKetThuc { get; set; }
    }
}
