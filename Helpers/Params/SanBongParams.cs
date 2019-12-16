using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Helpers.Params
{
    public class SanBongParams : BaseParams
    {
        public string MaSanBong { get; set; }
        public string TenSanBong { get; set; }
        public double ChieuDaiBatDau { get; set;}
        public double ChieuDaiKetThuc {get;set;}
        public double ChieuRongBatDau {get;set;}
        public double ChieuRongKetThuc { get; set; }
        public double DienTichBatDau { get; set; }
        public double DienTichKetThuc {get;set;}
        public string GhiChu { get; set; }

    }
}
