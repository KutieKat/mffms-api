using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MFFMS.API.Models
{
    public class TaiKhoan : BaseModel
    {
        public string MaTaiKhoan { get; set; }
        public string TenDangNhap { get; set; }
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }
        public string PhanQuyen { get; set; }
        public string HoVaTen { get; set; }
        public string GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }

    }
}
