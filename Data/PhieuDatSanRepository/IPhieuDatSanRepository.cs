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
        Task<PhieuDatSan> GetById(string id);
        Task<PhieuDatSan> Create(PhieuDatSanForCreateDto phieuDatSan);
        Task<PhieuDatSan> UpdateById(string id, PhieuDatSanForUpdateDto phieuDatSan);
        Task<PhieuDatSan> TemporarilyDeleteById(string id);
        Task<PhieuDatSan> RestoreById(string id);
        Task<PhieuDatSan> PermanentlyDeleteById(string id);
        int GetTotalPages();
        int GetTotalItems();
        Object GetStatusStatistics(PhieuDatSanParams userParams);
        Task<Object> GetGeneralStatistics(PhieuDatSanGeneralStatisticsParams userParams);
    }
}
