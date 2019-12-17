using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Dtos.ChiTietPhieuDatSanDto
{
    public class ChiTietPhieuDatSanForUpdateDto
    {
        [Required]
        public int MaPhieuDatSan { get; set; }
        [Required]
        public string MaSanBong { get; set; }
        [Required]
        public double ThoiGianBatDau { get; set; }
        [Required]
        public double ThoiGianKetThuc { get; set; }
        [Required]
        public DateTime NgayDat { get; set; }
        [Required]
        public decimal TienCoc { get; set; }
        [Required]
        public decimal ThanhTien { get; set; }
    }
}
