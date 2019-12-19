using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Dtos.TaiKhoanDto
{
    public class TaiKhoanForUpdateDto : BaseDto
    {
        [Required]
        public string PhanQuyen { get; set; }
        public string TenDangNhap { get; set; }
        public string HoVaTen { get; set; }
        public string GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }

    }
}
