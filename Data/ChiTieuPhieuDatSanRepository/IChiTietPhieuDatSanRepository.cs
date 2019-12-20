using MFFMS.API.Dtos;
using MFFMS.API.Dtos.ChiTietPhieuDatSanDto;
using MFFMS.API.Helpers;
using MFFMS.API.Helpers.Params;
using MFFMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Data.ChiTieuPhieuDatSanRepository
{
    public interface IChiTietPhieuDatSanRepository
    {
        Task<PagedList<ChiTietPhieuDatSan>> GetAll(ChiTietPhieuDatSanParams userParams);
        Task<ChiTietPhieuDatSan> GetById(int id);
        Task<ChiTietPhieuDatSan> Create(ChiTietPhieuDatSanForCreateDto chiTietPhieuDatSan);
        Task<ChiTietPhieuDatSan> UpdateById(int id, ChiTietPhieuDatSanForUpdateDto chiTietPhieuDatSan);
        Task<ChiTietPhieuDatSan> TemporarilyDeleteById(int id);
        Task<ChiTietPhieuDatSan> RestoreById(int id);
        Task<ChiTietPhieuDatSan> PermanentlyDeleteById(int id);
        int GetTotalPages();
        int GetTotalItems();
        Object GetStatusStatistics(ChiTietPhieuDatSanParams userParams);
        ValidationResultDto ValidateBeforeCreate(ChiTietPhieuDatSanForCreateDto chiTietPhieuDatSan);
        ValidationResultDto ValidateBeforeCreateMultiple(ICollection<ChiTietPhieuDatSanForCreateMultipleDto> danhSachChiTietPhieuDatSan);
        ValidationResultDto ValidateBeforeUpdate(int id, ChiTietPhieuDatSanForUpdateDto chiTietPhieuDatSan);
        Task<ICollection<ChiTietPhieuDatSan>> CreateMultiple(ICollection<ChiTietPhieuDatSanForCreateMultipleDto> danhSachChiTietPhieuDatSan);
    }
}
