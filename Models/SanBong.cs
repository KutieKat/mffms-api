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
        public int MaSanBong { get; set; }
        public string TenSanBong { get; set; }
        public string DienTich { get; set; } // Sân 5, sân 7 / sân 11
        public string GhiChu { get; set; }

        public ICollection<ChiTietPhieuDatSan> ChiTietPhieuDatSan { get; set; }
    }
}
