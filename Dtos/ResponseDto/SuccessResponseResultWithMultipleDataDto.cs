using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Dtos.ResponseDto
{
    public class SuccessResponseResultWithMultipleDataDto : ResponseResultDto
    {
        public Object Data { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Object StatusStatistics { get; set; }
    }
}
