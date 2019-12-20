using MFFMS.API.Dtos;
using MFFMS.API.Dtos.DichVuDto;
using MFFMS.API.Helpers;
using MFFMS.API.Helpers.Params;
using MFFMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Data.DichVuRepository
{
    public interface IDichVuRepository
    {
        Task<PagedList<DichVu>> GetAll(DichVuParams userParams);
        Task<DichVu> GetById(string id);
        Task<DichVu> Create(DichVuForCreateDto dichVu);
        Task<DichVu> UpdateById(string id, DichVuForUpdateDto dichVu);
        Task<DichVu> TemporarilyDeleteById(string id);
        Task<DichVu> RestoreById(string id);
        Task<DichVu> PermanentlyDeleteById(string id);
        int GetTotalPages();
        int GetTotalItems();
        Object GetStatusStatistics(DichVuParams userParams);
        Task<Object> GetGeneralStatistics(DichVuGeneralStatisticsParams userParams);
        ValidationResultDto ValidateBeforeCreate(DichVuForCreateDto dichVu);
        ValidationResultDto ValidateBeforeUpdate(string id, DichVuForUpdateDto dichVu);
    }
}
