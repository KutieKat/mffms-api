using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Dtos.SanBongDto
{
    public class SanBongForUpdateDto : BaseDto
    {
        [Required]
        public string TenSanBong { get; set; }
        [Required]
        public string DienTich { get; set; }
        public string GhiChu { get; set; }
    }
}
