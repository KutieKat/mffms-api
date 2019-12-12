using MFFMS.API.Dtos.DonNhapHangDto;
using MFFMS.API.Helpers;
using MFFMS.API.Helpers.Params;
using MFFMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Data.DonNhapHangRepository
{
    public interface IDonNhapHangRepository
    {
        Task<PagedList<DonNhapHang>> GetAll(DonNhapHangParams userParams);
        Task<DonNhapHang> GetById(int id);
        Task<DonNhapHang> Create(DonNhapHangForCreateDto donNhapHang);
        Task<DonNhapHang> UpdateById(int id, DonNhapHangForUpdateDto donNhapHang);
        Task<DonNhapHang> TemporarilyDeleteById(int id);
        Task<DonNhapHang> RestoreById(int id);
        Task<DonNhapHang> PermanentlyDeleteById(int id);
        int GetTotalPages();
        int GetTotalItems();
        Object GetStatusStatistics(DonNhapHangParams userParams);
    }
}
