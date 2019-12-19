using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Helpers.Params
{
    public class TaiKhoanParams : BaseParams
    {      
        public string MaTaiKhoan { get; set; }
        public string TenDangNhap { get; set; }
        public string PhanQuyen { get; set; }
        public string HoVaTen { get; set; }
        public string GioiTinh { get; set; }
        public DateTime NgaySinhBatDau { get; set; }
        public DateTime NgaySinhKetThuc {get;set;}
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
    }
}
