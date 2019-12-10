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
        Task<TaiSanThietBi> GetById(int id);
        Task<TaiSanThietBi> Create(TaiSanThietBiForCreateDto taiSanThietBi);
        Task<TaiSanThietBi> UpdateById(int id, TaiSanThietBiForUpdateDto taiSanThietBi);
        Task<TaiSanThietBi> TemporarilyDeleteById(int id);
        Task<TaiSanThietBi> RestoreById(int id);
        Task<TaiSanThietBi> PermanentlyDeleteById(int id);
        int GetTotalPages();
        int GetTotalItems();
        Object GetStatusStatistics(TaiSanThietBiParams userParams);
        ValidationResultDto ValidateBeforeCreate(TaiSanThietBiForCreateDto nhaCungCap);
        ValidationResultDto ValidateBeforeUpdate(int id, TaiSanThietBiForUpdateDto nhaCungCap);

    }
}