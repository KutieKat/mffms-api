using MFFMS.API.Dtos.CaiDatDto;
using MFFMS.API.Models;
using System.Threading.Tasks;

namespace MFFMS.API.Data.CaiDatRepository
{
    public interface ICaiDatRepository
    {
        Task<CaiDat> GetAll();
        Task<CaiDat> UpdateById(string id, CaiDatForUpdateDto caiDat);
        Task<CaiDat> Restore();
    }
}