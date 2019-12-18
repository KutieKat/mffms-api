using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Models
{
    public class ChiTietHDDV : BaseModel
    {
        public string SoHDDV { get; set; }
        public HoaDonDichVu HoaDonDichVu { get; set; }
        public string MaDichVu { get; set; }
        public DichVu DichVu { get; set;}
        public double DonGia{get;set;}
        public double ThanhTien{get;set;}
        public int SoLuong { get; set; }
    }
}
