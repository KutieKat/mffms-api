using AutoMapper;
using MFFMS.API.Dtos.DichVuDto;
using MFFMS.API.Dtos.HoaDonDichVuDto;
using MFFMS.API.Dtos.KhachHangDto;
using MFFMS.API.Dtos.NhanVienDto;
using MFFMS.API.Dtos.SanBongDto;
using MFFMS.API.Dtos.TaiKhoanDto;
using MFFMS.API.Dtos.CaiDatDto;
using MFFMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MFFMS.API.Dtos.ChiTietHDDVDto;
using MFFMS.API.Dtos.DonNhapHangDto;
using MFFMS.API.Dtos.ChiTietDonNhapHangDto;
using MFFMS.API.Dtos.PhieuDatSanDto;
using MFFMS.API.Dtos.ChiTietPhieuDatSanDto;

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
            CreateMapForDichVu();
            CreateMapForHoaDonDichVu();
            CreateMapForCaiDat();
            CreateMapForChiTietHoaDonDichVu();
            CreateMapForDonNhapHang();
            CreateMapForChiTietDonNhapHang();
            CreateMapForPhieuDatSan();
            CreateMapForChiTietPhieuDatSan();
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

        
        private void CreateMapForDichVu()
        {
            CreateMap<DichVu, DichVuForCreateDto>();
            CreateMap<DichVu, DichVuForUpdateDto>();
            CreateMap<DichVu, DichVuForViewDto>();
            CreateMap<DichVu, DichVuForListDto>();
            CreateMap<DichVuForCreateDto, DichVu>();
            CreateMap<DichVuForUpdateDto, DichVu>();
        }

        private void CreateMapForHoaDonDichVu()
        {
            CreateMap<HoaDonDichVu, HoaDonDichVuForCreateDto>();
            CreateMap<HoaDonDichVu, HoaDonDichVuForUpdateDto>();
            CreateMap<HoaDonDichVu, HoaDonDichVuForViewDto>();
            CreateMap<HoaDonDichVu, HoaDonDichVuForListDto>();
            CreateMap<HoaDonDichVuForCreateDto, HoaDonDichVu>();
            CreateMap<HoaDonDichVuForUpdateDto, HoaDonDichVu>();
        }

        private void CreateMapForCaiDat()
        {           
            CreateMap<CaiDat, CaiDatForViewDto>();
            CreateMap<CaiDat, CaiDatForUpdateDto>();
            CreateMap<CaiDatForUpdateDto, CaiDat>();  
        }

        private void CreateMapForChiTietHoaDonDichVu()
        {
            CreateMap<ChiTietHDDV, ChiTietHDDVForCreateDto>();
            CreateMap<ChiTietHDDV, ChiTietHDDVForUpdateDto>();
            CreateMap<ChiTietHDDV, ChiTietHDDVForViewDto>();
            CreateMap<ChiTietHDDV, ChiTietHDDVForListDto>();
            CreateMap<ChiTietHDDVForCreateDto, ChiTietHDDV>();
            CreateMap<ChiTietHDDVForUpdateDto, ChiTietHDDV>();
        }

        private void CreateMapForDonNhapHang()
        {
            CreateMap<DonNhapHang, DonNhapHangForCreateDto>();
            CreateMap<DonNhapHang, DonNhapHangForUpdateDto>();
            CreateMap<DonNhapHang, DonNhapHangForViewDto>();
            CreateMap<DonNhapHang, DonNhapHangForListDto>();
            CreateMap<DonNhapHangForCreateDto, DonNhapHang>();
            CreateMap<DonNhapHangForUpdateDto, DonNhapHang>();
        }

        private void CreateMapForChiTietDonNhapHang()
        {
            CreateMap<ChiTietDonNhapHang, ChiTietDonNhapHangForCreateDto>();
            CreateMap<ChiTietDonNhapHang, ChiTietDonNhapHangForUpdateDto>();
            CreateMap<ChiTietDonNhapHang, ChiTietDonNhapHangForViewDto>();
            CreateMap<ChiTietDonNhapHang, ChiTietDonNhapHangForListDto>();
            CreateMap<ChiTietDonNhapHangForCreateDto, ChiTietDonNhapHang>();
            CreateMap<ChiTietDonNhapHangForUpdateDto, ChiTietDonNhapHang>();
        }

        private void CreateMapForPhieuDatSan()
        {
            CreateMap<PhieuDatSan, PhieuDatSanForCreateDto>();
            CreateMap<PhieuDatSan, PhieuDatSanForUpdateDto>();
            CreateMap<PhieuDatSan, PhieuDatSanForViewDto>();
            CreateMap<PhieuDatSan, PhieuDatSanForListDto>();
            CreateMap<PhieuDatSanForCreateDto, PhieuDatSan>();
            CreateMap<PhieuDatSanForUpdateDto, PhieuDatSan>();
        }

        private void CreateMapForChiTietPhieuDatSan()
        {
            CreateMap<ChiTietPhieuDatSan, ChiTietPhieuDatSanForCreateDto>();
            CreateMap<ChiTietPhieuDatSan, ChiTietPhieuDatSanForUpdateDto>();
            CreateMap<ChiTietPhieuDatSan, ChiTietPhieuDatSanForViewDto>();
            CreateMap<ChiTietPhieuDatSan, ChiTietPhieuDatSanForListDto>();
            CreateMap<ChiTietPhieuDatSanForCreateDto, ChiTietPhieuDatSan>();
            CreateMap<ChiTietPhieuDatSanForUpdateDto, ChiTietPhieuDatSan>();
        }


    }
}
