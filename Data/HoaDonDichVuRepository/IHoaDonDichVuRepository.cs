using MFFMS.API.Dtos;
using MFFMS.API.Dtos.HoaDonDichVuDto;
using MFFMS.API.Helpers;
using MFFMS.API.Helpers.Params;
using MFFMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Data.HoaDonDichVuRepository
{
    public interface IHoaDonDichVuRepository
    {
        Task<PagedList<HoaDonDichVu>> GetAll(HoaDonDichVuParams userParams);
        Task<HoaDonDichVu> GetById(string id);
        Task<HoaDonDichVu> Create(HoaDonDichVuForCreateDto hoaDonDichVu);
        Task<HoaDonDichVu> UpdateById(string id, HoaDonDichVuForUpdateDto hoaDonDichVu);
        Task<HoaDonDichVu> TemporarilyDeleteById(string id);
        Task<HoaDonDichVu> RestoreById(string id);
        Task<HoaDonDichVu> PermanentlyDeleteById(string id);
        int GetTotalPages();
        int GetTotalItems();
        //Task<Object> GetGeneralStatisticsParams(HoaDonDichVuStatisticsParams userParams);
        Object GetStatusStatistics(HoaDonDichVuParams userParams);
        Task<Object> GetGeneralStatistics(HoaDonDichVuStatisticsParams userParams);
    }
}
