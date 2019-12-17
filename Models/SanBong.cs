using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Models
{
    public class SanBong : BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string MaSanBong { get; set; }
        public string TenSanBong { get; set; }
        public double ChieuDai { get; set; }
        public double ChieuRong { get; set; }
        public double DienTich { get; set; }
        public string GhiChu { get; set; }

        public ICollection<ChiTietPhieuDatSan> ChiTietPhieuDatSan { get; set; }
    }
}
