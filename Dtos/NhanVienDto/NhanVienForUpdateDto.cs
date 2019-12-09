using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Dtos.NhanVienDto
{
    public class NhanVienForUpdateDto : BaseDto
    {
        [Required]
        public string TenNhanVien { get; set; }
        [Required]
        public string GioiTinh { get; set; }
        [Required]
        public DateTime NgaySinh { get; set; }
        [Required]
        public string DiaChi { get; set; }
        [Required]
        public string ChucVu { get; set; }
        [Required]
        public string SoDienThoai { get; set; }
        [Required]
        public string SoCMND { get; set; }
        [Required]
        public double Luong { get; set; }
        public string GhiChu { get; set; }
    }
}
