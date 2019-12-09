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
        Task<SanBong> GetById(int id);
        Task<SanBong> Create(SanBongForCreateDto sanBong);
        Task<SanBong> UpdateById(int id, SanBongForUpdateDto sanBong);
        Task<SanBong> TemporarilyDeleteById(int id);
        Task<SanBong> RestoreById(int id);
        Task<SanBong> PermanentlyDeleteById(int id);
        int GetTotalPages();
        int GetTotalItems();
        Object GetStatusStatistics(SanBongParams userParams);
        ValidationResultDto ValidateBeforeCreate(SanBongForCreateDto sanBong);
        ValidationResultDto ValidateBeforeUpdate(int id, SanBongForUpdateDto sanBong);
    }
}
