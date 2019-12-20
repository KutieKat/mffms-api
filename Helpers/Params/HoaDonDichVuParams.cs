using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MFFMS.API.Models;

namespace MFFMS.API.Helpers.Params
{
    public class HoaDonDichVuParams : BaseParams
    {
        public string SoHDDV { get; set; }
        public string MaKhachHang { get; set; }
        public string MaNhanVien { get; set; }
        public DateTime NgaySuDungBatDau { get; set; }
        public DateTime NgaySuDungKetThuc {get;set;}
        public DateTime NgayLapBatDau { get; set; }
        public DateTime NgayLapKetThuc {get;set;}
        public string GhiChu { get; set; }
        public double ThanhTienBatDau {get;set;}
        public double ThanhTienKetThuc {get;set;}
        public double DaThanhToanBatDau{get;set;}
        public double DaThanhToanKetThuc { get; set; }
        public string TinhTrang { get; set; }
    }
}
