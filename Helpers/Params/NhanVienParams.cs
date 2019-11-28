using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Helpers.Params
{
    public class NhanVienParams : BaseParams
    {
        public string ChucVu { get; set; }
        public DateTime NgaySinhBatDau { get; set; }
        public DateTime NgaySinhKetThuc { get; set; }
    }
}
