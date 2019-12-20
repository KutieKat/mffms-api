using MFFMS.API.Dtos;
using MFFMS.API.Dtos.TaiSanThietBiDto;
using MFFMS.API.Helpers;
using MFFMS.API.Helpers.Params;
using MFFMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Data.TaiSanThietBiRepository
{
    public interface ITaiSanThietBiRepository
    {
        Task<PagedList<TaiSanThietBi>> GetAll(TaiSanThietBiParams userParams);
        Task<TaiSanThietBi> GetById(string id);
        Task<TaiSanThietBi> Create(TaiSanThietBiForCreateDto taiSanThietBi);
        Task<TaiSanThietBi> UpdateById(string id, TaiSanThietBiForUpdateDto taiSanThietBi);
        Task<TaiSanThietBi> TemporarilyDeleteById(string id);
        Task<TaiSanThietBi> RestoreById(string id);
        Task<TaiSanThietBi> PermanentlyDeleteById(string id);
        int GetTotalPages();
        int GetTotalItems();
        Object GetStatusStatistics(TaiSanThietBiParams userParams);
        Task<Object> GetGeneralStatistics(TSTBGeneralStatisticsParams userParams);
        ValidationResultDto ValidateBeforeCreate(TaiSanThietBiForCreateDto nhaCungCap);
        ValidationResultDto ValidateBeforeUpdate(string id, TaiSanThietBiForUpdateDto nhaCungCap);

    }
}