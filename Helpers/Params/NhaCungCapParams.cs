using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Helpers.Params
{
    public class NhaCungCapParams : BaseParams
    {
        public string MaNhaCungCap { get; set; }
        public string TenNhaCungCap { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }
        public string GhiChu { get; set; }
    }
}
