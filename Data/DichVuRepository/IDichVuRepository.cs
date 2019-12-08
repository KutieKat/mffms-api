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
        Task<DichVu> GetById(int id);
        Task<DichVu> Create(DichVuForCreateDto dichVu);
        Task<DichVu> UpdateById(int id, DichVuForUpdateDto dichVu);
        Task<DichVu> TemporarilyDeleteById(int id);
        Task<DichVu> RestoreById(int id);
        Task<DichVu> PermanentlyDeleteById(int id);
        int GetTotalPages();
        int GetTotalItems();
        Object GetStatusStatistics(DichVuParams userParams);
        ValidationResultDto ValidateBeforeCreate(DichVuForCreateDto dichVu);
        ValidationResultDto ValidateBeforeUpdate(int id, DichVuForUpdateDto dichVu);
    }
}
