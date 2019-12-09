using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Dtos.DichVuDto
{
    public class DichVuForViewDto : BaseDto
    {
        public int MaDichVu { get; set; }
        public string TenDichVu { get; set; }
        public decimal DonGia { get; set; }
        public string DVT { get; set; }
        public string GhiChu { get; set; }
    }
}
