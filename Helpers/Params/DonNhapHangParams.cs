using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Helpers.Params
{
    public class DonNhapHangParams : BaseParams
    {
       
        public DateTime NgayGiaoHangBatDau { get; set; }
        public DateTime NgayGiaoHangKetThuc { get; set; }
    }
}
