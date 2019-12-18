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
        Task<ChiTietHDDV> GetById(string soHDDV, string maDichVu);
        Task<ChiTietHDDV> Create(ChiTietHDDVForCreateDto chiTietHDDV);
        Task<ChiTietHDDV> UpdateById(string soHDDV, string maDichVu, ChiTietHDDVForUpdateDto chiTietHDDV);
        Task<ChiTietHDDV> TemporarilyDeleteById(string soHDDV, string maDichVu);
        Task<ChiTietHDDV> RestoreById(string soHDDV, string maDichVu);
        Task<ChiTietHDDV> PermanentlyDeleteById(string soHDDV, string maDichVu);
        int GetTotalPages();
        int GetTotalItems();
        Object GetStatusStatistics(ChiTietHDDVParams userParams);
        Task<ICollection<ChiTietHDDV>> CreateMultiple(ICollection<ChiTietHDDVForCreateMultipleDto> danhSachChiTietHDDV);
    }
}
