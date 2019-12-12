using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Dtos.PhieuDatSanDto
{
    public class PhieuDatSanForCreateDto : BaseDto
    {
        [Required]
        public string MaKhachHang { get; set; }
        [Required]
        public string MaNhanVien { get; set; }
        [Required]
        public decimal TongTien { get; set; }
    }
}
