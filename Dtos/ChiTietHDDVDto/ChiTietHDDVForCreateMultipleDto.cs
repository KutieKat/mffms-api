using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Dtos.ChiTietHDDVDto
{
    public class ChiTietHDDVForCreateMultipleDto : BaseDto
    {
        [Required]
        public int SoHDDV { get; set; }
        [Required]
        public int MaDichVu { get; set; }
        [Required]
        public int SoLuong { get; set; }

        public override string ToString()
        {
            return "SoHDDV, MaDichVu, SoLuong";
        }
    }


}
