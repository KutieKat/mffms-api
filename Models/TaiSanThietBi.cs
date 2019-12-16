using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Models
{
    public class TaiSanThietBi:BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string MaTSTB { get; set; }
        public string MaNhaCungCap { get; set; }
        public NhaCungCap NhaCungCap { get; set; }
        public string TenTSTB { get; set; }
        public string TinhTrang { get; set; }
        public string ThongTinBaoHanh { get; set; }
       public ICollection<ChiTietDonNhapHang> ChiTietDonNhapHang { get; set; }
    }
}
