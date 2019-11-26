using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Models
{
    public class GiaGioThue : BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaGio { get; set; }
        public string ChiTiet { get; set; }
        public decimal DonGia { get; set; }
        public ICollection<ChiTietGioThue> ChiTietGioThue { get; set; }
    }
}
