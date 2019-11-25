
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Dtos.TaiKhoanDto
{
    public class TaiKhoanForListDto : BaseDto
    {
        public int MaTaiKhoan { get; set; }
        public string TenDangNhap { get; set; }
        public string PhanQuyen { get; set; }

    }
}
