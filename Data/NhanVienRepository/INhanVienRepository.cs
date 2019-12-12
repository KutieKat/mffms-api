using MFFMS.API.Dtos;
using MFFMS.API.Dtos.NhanVienDto;
using MFFMS.API.Helpers;
using MFFMS.API.Helpers.Params;
using MFFMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Data.NhanVienRepository
{
    public interface INhanVienRepository
    {
        Task<PagedList<NhanVien>> GetAll(NhanVienParams userParams);
        Task<NhanVien> GetById(string id);
        Task<NhanVien> Create(NhanVienForCreateDto nhanVien);
        Task<NhanVien> UpdateById(string id, NhanVienForUpdateDto nhanVien);
        Task<NhanVien> TemporarilyDeleteById(string id);
        Task<NhanVien> RestoreById(string id);
        Task<NhanVien> PermanentlyDeleteById(string id);
        int GetTotalPages();
        int GetTotalItems();
        Object GetStatusStatistics(NhanVienParams userParams);
        ValidationResultDto ValidateBeforeCreate(NhanVienForCreateDto nhanVien);
        ValidationResultDto ValidateBeforeUpdate(string id, NhanVienForUpdateDto nhanVien);
        Task<Object> GetGeneralStatistics(NhanVienStatisticsParams userParams);
    }
}
