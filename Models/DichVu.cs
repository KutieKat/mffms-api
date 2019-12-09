using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Models
{
    public class DichVu : BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaDichVu { get; set; }
        public string TenDichVu { get; set; }
        public decimal DonGia { get; set; }
        public string DVT { get; set; }
        public string GhiChu { get; set; }

        public ICollection<HoaDonDichVu> HoaDonDichVu { get; set; }
        public ICollection<ChiTietHDDV> ChiTietHDDV { get; set; }
    }
}
