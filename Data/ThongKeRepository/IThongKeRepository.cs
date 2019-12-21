using MFFMS.API.Dtos.CaiDatDto;
using MFFMS.API.Helpers.Params;
using MFFMS.API.Models;
using System.Threading.Tasks;

namespace MFFMS.API.Data.ThongKeRepository
{
    public interface IThongKeRepository
    {
        //Task<object> GetAll(ThongKeParams userParams);
        Task<object> TongSoLuotDatSan(ThongKeParams userParams);
        Task<object> TongTienDichVu(ThongKeParams userParams);
        Task<object> TongTienDatSan(ThongKeParams userParams);
        Task<object> TongTienNhapHang(ThongKeParams userParams);
    }
}