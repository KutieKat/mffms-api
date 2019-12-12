using MFFMS.API.Dtos.ChiTietDonNhapHangDto;
using MFFMS.API.Helpers;
using MFFMS.API.Helpers.Params;
using MFFMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Data.ChiTietDonNhapHangRepository
{
    public interface IChiTietDonNhapHangRepository
    {
        Task<PagedList<ChiTietDonNhapHang>> GetAll(ChiTietDonNhapHangParams userParams);
        Task<ChiTietDonNhapHang> GetById(int maDonNhapHang, int maTSTB);
        Task<ChiTietDonNhapHang> Create(ChiTietDonNhapHangForCreateDto chiTietDonNhapHang);
        Task<ChiTietDonNhapHang> UpdateById(int maDonNhapHang, int maTSTB, ChiTietDonNhapHangForUpdateDto chiTietDonNhapHang);
        Task<ChiTietDonNhapHang> TemporarilyDeleteById(int maDonNhapHang, int maTSTB);
        Task<ChiTietDonNhapHang> RestoreById(int maDonNhapHang, int maTSTB);
        Task<ChiTietDonNhapHang> PermanentlyDeleteById(int maDonNhapHang, int maTSTB);
        int GetTotalPages();
        int GetTotalItems();
        Object GetStatusStatistics(ChiTietDonNhapHangParams userParams);
        Task<ICollection<ChiTietDonNhapHang>> CreateMultiple(ICollection<ChiTietDonNhapHangForCreateMultiple> danhSachChiTietDonNhapHang);
    }
}
