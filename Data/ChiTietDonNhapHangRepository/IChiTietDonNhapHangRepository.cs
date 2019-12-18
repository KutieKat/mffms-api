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
        Task<ChiTietDonNhapHang> GetById(string maDonNhapHang, string maTSTB);
        Task<ChiTietDonNhapHang> Create(ChiTietDonNhapHangForCreateDto chiTietDonNhapHang);
        Task<ChiTietDonNhapHang> UpdateById(string maDonNhapHang, string maTSTB, ChiTietDonNhapHangForUpdateDto chiTietDonNhapHang);
        Task<ChiTietDonNhapHang> TemporarilyDeleteById(string maDonNhapHang, string maTSTB);
        Task<ChiTietDonNhapHang> RestoreById(string maDonNhapHang, string maTSTB);
        Task<ChiTietDonNhapHang> PermanentlyDeleteById(string maDonNhapHang, string maTSTB);
        int GetTotalPages();
        int GetTotalItems();
        Object GetStatusStatistics(ChiTietDonNhapHangParams userParams);
        Task<ICollection<ChiTietDonNhapHang>> CreateMultiple(ICollection<ChiTietDonNhapHangForCreateMultiple> danhSachChiTietDonNhapHang);
    }
}
