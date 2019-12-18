using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Dtos.DichVuDto
{
    public class DichVuForListDto : BaseDto
    {
        public string MaDichVu { get; set; }
        public string TenDichVu { get; set; }
        public double DonGia { get; set; }
        public string DVT { get; set; }
        public string GhiChu { get; set; }
    }
}
