using MFFMS.API.Dtos;
using MFFMS.API.Dtos.ChiTietHDDVDto;
using MFFMS.API.Helpers;
using MFFMS.API.Helpers.Params;
using MFFMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Data.ChiTietHDDVRepository
{
    public interface IChiTietHDDVRepository
    {
        Task<PagedList<ChiTietHDDV>> GetAll(ChiTietHDDVParams userParams);
        Task<ChiTietHDDV> GetById(int soHDDV, int maDichVu);
        Task<ChiTietHDDV> Create(ChiTietHDDVForCreateDto chiTietHDDV);
        Task<ChiTietHDDV> UpdateById(int soHDDV, int maDichVu, ChiTietHDDVForUpdateDto chiTietHDDV);
        Task<ChiTietHDDV> TemporarilyDeleteById(int soHDDV, int maDichVu);
        Task<ChiTietHDDV> RestoreById(int soHDDV, int maDichVu);
        Task<ChiTietHDDV> PermanentlyDeleteById(int soHDDV, int maDichVu);
        int GetTotalPages();
        int GetTotalItems();
        Object GetStatusStatistics(ChiTietHDDVParams userParams);
        Task<ICollection<ChiTietHDDV>> CreateMultiple(ICollection<ChiTietHDDVForCreateMultipleDto> danhSachChiTietHDDV);
    }
}
