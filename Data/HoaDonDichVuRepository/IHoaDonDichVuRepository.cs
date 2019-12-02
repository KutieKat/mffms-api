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
        Task<HoaDonDichVu> GetById(int id);
        Task<HoaDonDichVu> Create(HoaDonDichVuForCreateDto hoaDonDichVu);
        Task<HoaDonDichVu> UpdateById(int id, HoaDonDichVuForUpdateDto hoaDonDichVu);
        Task<HoaDonDichVu> TemporarilyDeleteById(int id);
        Task<HoaDonDichVu> RestoreById(int id);
        Task<HoaDonDichVu> PermanentlyDeleteById(int id);
        int GetTotalPages();
        int GetTotalItems();
        Object GetStatusStatistics(HoaDonDichVuParams userParams);
    }
}
