using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Models
{
    public abstract class BaseModel
    {
        public DateTime ThoiGianTao { get; set; }
        public DateTime ThoiGianCapNhat { get; set; }
        public int TrangThai { get; set; } = 1;
        public int DaXoa { get; set; } = 0;
    }
}
