using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Dtos.DonNhapHangDto
{
    public class DonNhapHangForCreateDto : BaseDto
    {
        [Required]
        public int MaNhaCungCap { get; set; }
        [Required]
        public string MaNhanVien { get; set; }
        [Required]
        public DateTime NgayGiaoHang { get; set; }
        [Required]
        public string NoiNhanHang { get; set; }
    }
}
