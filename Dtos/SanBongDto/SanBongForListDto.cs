using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Dtos.SanBongDto
{
    public class SanBongForListDto : BaseDto
    {
        public int MaSanBong { get; set; }
        public string TenSanBong { get; set; }
        public string DienTich { get; set; }
        public string GhiChu { get; set; }
    }
}
