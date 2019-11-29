﻿using AutoMapper;
using MFFMS.API.Dtos.KhachHangDto;
using MFFMS.API.Dtos.NhanVienDto;
using MFFMS.API.Dtos.SanBongDto;
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
            CreateMapForKhachHang();
            CreateMapForNhanVien();
            CreateMapForSanBong();
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

        private void CreateMapForKhachHang()
        {
            CreateMap<KhachHang, KhachHangForListDto>();
            CreateMap<KhachHang, KhachHangForViewDto>();
            CreateMap<KhachHang, KhachHangForCreateDto>();
            CreateMap<KhachHang, KhachHangForUpdateDto>();
            CreateMap<KhachHangForCreateDto, KhachHang>();
            CreateMap<KhachHangForUpdateDto, KhachHang>();
        }

        private void CreateMapForNhanVien()
        {
            CreateMap<NhanVien, NhanVienForListDto>();
            CreateMap<NhanVien, NhanVienForViewDto>();
            CreateMap<NhanVien, NhanVienForCreateDto>();
            CreateMap<NhanVien, NhanVienForUpdateDto>();
            CreateMap<NhanVienForCreateDto, NhanVien>();
            CreateMap<NhanVienForUpdateDto, NhanVien>();
        }


        private void CreateMapForSanBong()
        {
            CreateMap<SanBong, SanBongForListDto>();
            CreateMap<SanBong, SanBongForViewDto>();
            CreateMap<SanBong, SanBongForCreateDto>();
            CreateMap<SanBong, SanBongForUpdateDto>();
            CreateMap<SanBongForCreateDto, SanBong>();
            CreateMap<SanBongForUpdateDto, SanBong>();
        }
    }
}
