using MFFMS.API.Dtos;
using MFFMS.API.Dtos.SanBongDto;
using MFFMS.API.Helpers;
using MFFMS.API.Helpers.Params;
using MFFMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Data.SanBongRepository
{
    public interface ISanBongRepository
    {
        Task<PagedList<SanBong>> GetAll(SanBongParams userParams);
        Task<SanBong> GetById(string id);
        Task<SanBong> Create(SanBongForCreateDto sanBong);
        Task<SanBong> UpdateById(string id, SanBongForUpdateDto sanBong);
        Task<SanBong> TemporarilyDeleteById(string id);
        Task<SanBong> RestoreById(string id);
        Task<SanBong> PermanentlyDeleteById(string id);
        int GetTotalPages();
        int GetTotalItems();
        Object GetStatusStatistics(SanBongParams userParams);
        Task<Object> GetGeneralStatistics(SanBongStatisticsParams userParams);
        ValidationResultDto ValidateBeforeCreate(SanBongForCreateDto sanBong);
        ValidationResultDto ValidateBeforeUpdate(string id, SanBongForUpdateDto sanBong);
    }
}
