using MFFMS.API.Dtos.PhieuDatSanDto;
using MFFMS.API.Helpers;
using MFFMS.API.Helpers.Params;
using MFFMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Data.PhieuDatSanRepository
{
    public interface IPhieuDatSanRepository
    {
        Task<PagedList<PhieuDatSan>> GetAll(PhieuDatSanParams userParams);
        Task<PhieuDatSan> GetById(int id);
        Task<PhieuDatSan> Create(PhieuDatSanForCreateDto phieuDatSan);
        Task<PhieuDatSan> UpdateById(int id, PhieuDatSanForUpdateDto phieuDatSan);
        Task<PhieuDatSan> TemporarilyDeleteById(int id);
        Task<PhieuDatSan> RestoreById(int id);
        Task<PhieuDatSan> PermanentlyDeleteById(int id);
        int GetTotalPages();
        int GetTotalItems();
        Object GetStatusStatistics(PhieuDatSanParams userParams);
    }
}
