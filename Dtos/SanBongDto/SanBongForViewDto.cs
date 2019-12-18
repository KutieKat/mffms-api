using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Dtos.SanBongDto
{
    public class SanBongForViewDto : BaseDto
    {
        public string MaSanBong { get; set; }
        public string TenSanBong { get; set; }
        public double ChieuDai { get; set; }
        public double ChieuRong { get; set; }
        public double DienTich { get; set; }
        public string GhiChu { get; set; }
    }
}
