using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Dtos.KhachHangDto
{
    public class KhachHangForListDto : BaseDto
    {
        public string MaKhachHang { get; set; }
        public string TenKhachHang { get; set; }
        public string GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }
        public string GhiChu { get; set; }
    }
}
