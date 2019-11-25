using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Dtos.TaiKhoanDto
{
    public class TaiKhoanForLoginDto
    {
        [Required]
        public string TenDangNhap { get; set; }

        [Required]
        public string MatKhau { get; set; }
    }
}
