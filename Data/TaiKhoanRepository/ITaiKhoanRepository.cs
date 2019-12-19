using MFFMS.API.Dtos.TaiKhoanDto;
using MFFMS.API.Helpers;
using MFFMS.API.Helpers.Params;
using MFFMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Data
{
    public interface ITaiKhoanRepository
    {
        Task<PagedList<TaiKhoan>> GetAll(TaiKhoanParams userParams);
        Task<TaiKhoan> GetById(string id);
        Task<TaiKhoan> TaoTaiKhoan(TaiKhoan taiKhoan, string matKhau);
        Task<TaiKhoan> DangNhap(string tenDangNhap, string matKhau);
        Task<bool> TaiKhoanTonTai(string tenDangNhap);
        Task<TaiKhoan> UpdateById(string id, TaiKhoanForUpdateDto taiKhoan);
        Task<TaiKhoan> DeleteById(string id);
        int GetTotalPages();
        int GetTotalItems();
        Object GetStatusStatistics(TaiKhoanParams taiKhoanParams);
        Task<TaiKhoan> TemporarilyDeleteById(string id);
        Task<TaiKhoan> RestoreById(string id);
        Task<TaiKhoan> PermanentlyDeleteById(string id);
        Task<TaiKhoan> ChangePassword(string id, TaiKhoanForChangePasswordDto taiKhoan);
        Task<TaiKhoan> ResetPassword(string id);
        Task<TaiKhoan> ValidateHash(string tenDangNhap, byte[] hash);
    }
}
