using MFFMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Dtos.ChiTietPhieuDatSanDto
{
    public class ChiTietPhieuDatSanForListDto : BaseDto
    {
        public int MaChiTietPDS { get; set; }
        public PhieuDatSan PhieuDatSan { get; set; }
        public SanBong SanBong { get; set; }
        public double ThoiGianBatDau { get; set; }
        public double ThoiGianKetThuc { get; set; }
        public DateTime NgayDat { get; set; }
        public double TienCoc { get; set; }
        public double ThanhTien { get; set; }  

    }
}
