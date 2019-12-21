using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Dtos.PhieuDatSanDto
{
    public class PhieuDatSanForUpdateDto : BaseDto
    {
        [Required]
        public string MaKhachHang { get; set; }
        [Required]
        public string MaNhanVien { get; set; }
        [Required]
        public double TongTien { get; set; }
        public DateTime NgayLap { get; set; }
    }
}
