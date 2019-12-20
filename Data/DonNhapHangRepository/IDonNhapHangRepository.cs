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
        Task<DonNhapHang> GetById(string id);
        Task<DonNhapHang> Create(DonNhapHangForCreateDto donNhapHang);
        Task<DonNhapHang> UpdateById(string id, DonNhapHangForUpdateDto donNhapHang);
        Task<DonNhapHang> TemporarilyDeleteById(string id);
        Task<DonNhapHang> RestoreById(string id);
        Task<DonNhapHang> PermanentlyDeleteById(string id);
        int GetTotalPages();
        int GetTotalItems();
        Object GetStatusStatistics(DonNhapHangParams userParams);
        Task<Object> GetGeneralStatistics(DonNhapHangGeneralStatisticsParams userParams);
    }
}
