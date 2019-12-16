using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Models
{
    public class NhaCungCap : BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string MaNhaCungCap { get; set; }
        public string TenNhaCungCap { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }
        public string GhiChu { get; set; }

        public ICollection<TaiSanThietBi> TaiSanThietBi { get; set; }
        public ICollection<DonNhapHang> DonNhapHang { get; set; }
    }
}