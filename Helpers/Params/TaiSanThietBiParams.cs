using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Helpers.Params
{
    public class TaiSanThietBiParams : BaseParams
    {
        public string MaTSTB { get; set; }
        public string MaNhaCungCap { get; set; }
        public string TenTSTB { get; set; }
        public string TinhTrang { get; set; }
        public string ThongTinBaoHanh { get; set; }
    }
}
