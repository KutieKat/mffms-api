using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Dtos.ResponseDto
{
    public abstract class BaseResponseDto
    {
        public string Status { get; protected set; }
        public string Message { get; set; }
        public ResponseResultDto Result { get; set; }
    }
}
