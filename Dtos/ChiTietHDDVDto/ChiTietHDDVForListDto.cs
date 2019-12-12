using MFFMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Dtos.ChiTietHDDVDto
{
    public class ChiTietHDDVForListDto : BaseDto
    {
        public HoaDonDichVu HoaDonDichVu { get; set; }
        public DichVu DichVu { get; set; }
        public int SoLuong { get; set; }
    }
}
