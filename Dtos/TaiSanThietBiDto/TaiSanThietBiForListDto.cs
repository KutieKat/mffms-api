using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MFFMS.API.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Dtos.TaiSanThietBiDto
{
    public class TaiSanThietBiForListDto : BaseDto
    {
        public string MaTSTB { get; set; }
        public string MaNhaCungCap { get; set; }
        public NhaCungCap NhaCungCap { get; set; }
        public string TenTSTB { get; set; }
        public string TinhTrang { get; set; }
        public string ThongTinBaoHanh { get; set; }
    }
}