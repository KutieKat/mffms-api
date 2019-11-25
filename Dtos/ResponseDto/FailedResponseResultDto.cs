using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Dtos.ResponseDto
{
    public class FailedResponseResultDto : ResponseResultDto
    {
        public Object Errors { get; set; }
    }
}
