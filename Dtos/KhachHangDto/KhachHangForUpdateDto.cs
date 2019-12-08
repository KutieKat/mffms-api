using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Dtos.KhachHangDto
{
    public class KhachHangForUpdateDto : BaseDto
    {
        [Required]
        public string TenKhachHang { get; set; }
        [Required]
        public string GioiTinh { get; set; }
        [Required]
        public DateTime NgaySinh { get; set; }
        [Required]
        public string SoDienThoai { get; set; }
        [Required]
        public string DiaChi { get; set; }
        public string GhiChu { get; set; }
    }
}
