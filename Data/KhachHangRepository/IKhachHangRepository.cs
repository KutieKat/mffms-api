using MFFMS.API.Dtos.KhachHangDto;
using MFFMS.API.Helpers;
using MFFMS.API.Helpers.Params;
using MFFMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Data.KhachHangRepository
{
    public interface IKhachHangRepository
    {
        Task<PagedList<KhachHang>> GetAll(KhachHangParams userParams);
        Task<KhachHang> GetById(string id);
        Task<KhachHang> Create(KhachHangForCreateDto khachHang);
        Task<KhachHang> UpdateById(string id, KhachHangForUpdateDto khachHang);
        Task<KhachHang> TemporarilyDeleteById(string id);
        Task<KhachHang> RestoreById(string id);
        Task<KhachHang> PermanentlyDeleteById(string id);
        int GetTotalPages();
        int GetTotalItems();
        Object GetStatusStatistics(KhachHangParams userParams);
        Task<Object> GetGeneralStatistics(KhachHangStatisticsParams userParams);
    }
}
