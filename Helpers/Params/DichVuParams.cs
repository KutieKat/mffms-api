using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Helpers.Params
{
    public class DichVuParams : BaseParams
    {
        public string MaDichVu { get; set; }
        public string TenDichVu { get; set; }
        public double DonGiaBatDau { get; set; }
        public double DonGiaKetThuc {get;set;}
        public string DVT { get; set; }
        public string GhiChu { get; set; } 
    }
}
