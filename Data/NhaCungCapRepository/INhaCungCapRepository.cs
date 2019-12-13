using MFFMS.API.Dtos;
using MFFMS.API.Dtos.NhaCungCapDto;
using MFFMS.API.Helpers;
using MFFMS.API.Helpers.Params;
using MFFMS.API.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MFFMS.API.Data.NhaCungCapRepository
{
    public interface INhaCungCapRepository
    {
        Task<PagedList<NhaCungCap>> GetAll(NhaCungCapParams userParams);
        Task<NhaCungCap> GetById(int id);
        Task<NhaCungCap> Create(NhaCungCapForCreateDto nhaCungCap);
        Task<NhaCungCap> UpdateById(int id, NhaCungCapForUpdateDto nhaCungCap);
        Task<NhaCungCap> TemporarilyDeleteById(int id);
        Task<NhaCungCap> RestoreById(int id);
        Task<NhaCungCap> PermanentlyDeleteById(int id);

        int GetTotalPages();
        int GetTotalItems();

        Object GetStatusStatistics(NhaCungCapParams userParams);
        Task<Object> GetGeneralStatistics(NhaCungCapStatisticsParams userParams);
        
        ValidationResultDto ValidateBeforeCreate(NhaCungCapForCreateDto nhaCungCap);
        ValidationResultDto ValidateBeforeUpdate(int id, NhaCungCapForUpdateDto nhaCungCap);

    }
}