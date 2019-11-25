using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Dtos.TaiKhoanDto
{
    public class TaiKhoanForChangePasswordDto
    {
        public string MatKhauCu { get; set; }
        public string MatKhauMoi { get; set; }
        public string XacNhanMatKhauMoi { get; set; }
    }
}
