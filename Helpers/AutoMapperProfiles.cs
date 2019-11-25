using AutoMapper;
using MFFMS.API.Dtos.TaiKhoanDto;
using MFFMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMapForTaiKhoan();
        }

        private void CreateMapForTaiKhoan()
        {
            CreateMap<TaiKhoan, TaiKhoanForListDto>();
            CreateMap<TaiKhoan, TaiKhoanForViewDto>();
            CreateMap<TaiKhoan, TaiKhoanForCreateDto>();
            CreateMap<TaiKhoan, TaiKhoanForUpdateDto>();
            CreateMap<TaiKhoan, TaiKhoanForLoginDto>();
            CreateMap<TaiKhoanForCreateDto, TaiKhoan>();
            CreateMap<TaiKhoanForUpdateDto, TaiKhoan>();
        }
    }
}
