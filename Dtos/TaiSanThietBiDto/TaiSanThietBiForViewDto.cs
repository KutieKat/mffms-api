using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MFFMS.API.Models;

namespace MFFMS.API.Dtos.TaiSanThietBiDto
{
    public class TaiSanThietBiForViewDto:BaseDto
    {
        public string MaTSTB { get; set; }
        public string MaNhaCungCap { get; set; }
        public NhaCungCap NhaCungCap { get; set; }
        public string TenTSTB { get; set; }
        public string TinhTrang { get; set; }
        public string ThongTinBaoHanh { get; set; }


    }

}