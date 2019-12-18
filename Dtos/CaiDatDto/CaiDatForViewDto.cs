using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Dtos.CaiDatDto
{
    public class CaiDatForViewDto:BaseDto
    {
         public string MaCaiDat { get; set; }
        public string TenSanBong { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai {get;set;}
        public string Fax {get;set;}
        public string DiaChiTrenPhieu {get; set;}
    }
}