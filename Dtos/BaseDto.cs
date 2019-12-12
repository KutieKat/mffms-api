using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Dtos
{
    public class BaseDto
    {
        public DateTime ThoiGianTao { get; set; }
        public DateTime ThoiGianCapNhat { get; set; }
        public int TrangThai { get; set; }
        public int DaXoa { get; set; }
    }
}
