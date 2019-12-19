using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MFFMS.API.Dtos.TaiKhoanDto;
using MFFMS.API.Helpers;
using MFFMS.API.Helpers.Params;
using MFFMS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace MFFMS.API.Data
{
    public class TaiKhoanRepository : ITaiKhoanRepository
    {
        private readonly DataContext _context;
        private int _totalItems;
        private int _totalPages;

        public TaiKhoanRepository(DataContext context)
        {
            _context = context;
            _totalItems = 0;
            _totalPages = 0;
        }
        private string GenerateId()
        {
            int count = _context.DanhSachTaiKhoan.Count() + 1;
            string tempId = count.ToString();
            string currentYear = DateTime.Now.ToString("yy");
 
            while (tempId.Length < 4)
            {
                tempId = "0" + tempId;
            }
 
            tempId = "TK" + currentYear + tempId;
 
            return tempId;
        }

        public async Task<TaiKhoan> ChangePassword(string id, TaiKhoanForChangePasswordDto taiKhoan)
        {
            var result = await _context.DanhSachTaiKhoan.FirstOrDefaultAsync(x => x.MaTaiKhoan == id);

            if (result == null)
                return null;

            if (!KiemTraHash(taiKhoan.MatKhauCu, result.Hash, result.Salt))
                return null;

            if (taiKhoan.MatKhauMoi != taiKhoan.XacNhanMatKhauMoi)
                return null;

            byte[] hash, salt;
            TaoHash(taiKhoan.MatKhauMoi, out hash, out salt);

            var taiKhoanToUpdate = new TaiKhoan
            {
                MaTaiKhoan = id,
                Hash = hash,
                Salt = salt,
                TenDangNhap = result.TenDangNhap,
                GioiTinh = result.GioiTinh,
                HoVaTen = result.HoVaTen,
                NgaySinh = result.NgaySinh,
                SoDienThoai = result.SoDienThoai,
                Email = result.Email,
                DiaChi = result.DiaChi,
                PhanQuyen = result.PhanQuyen,
                ThoiGianCapNhat = DateTime.Now,
                TrangThai = result.TrangThai
            };

            _context.Entry(result).State = EntityState.Detached;
            _context.DanhSachTaiKhoan.Update(taiKhoanToUpdate);

            await _context.SaveChangesAsync();
            return taiKhoanToUpdate;
        }

        public async Task<TaiKhoan> ResetPassword(string id)
        {
            var result = await _context.DanhSachTaiKhoan.FirstOrDefaultAsync(x => x.MaTaiKhoan == id);

            if (result == null)
                return null;

            byte[] hash, salt;
            TaoHash("123456", out hash, out salt);

            var taiKhoanToUpdate = new TaiKhoan
            {
                MaTaiKhoan = id,
                Hash = hash,
                Salt = salt,
                TenDangNhap = result.TenDangNhap,
                GioiTinh = result.GioiTinh,
                HoVaTen = result.HoVaTen,
                NgaySinh = result.NgaySinh,
                SoDienThoai = result.SoDienThoai,
                Email = result.Email,
                DiaChi = result.DiaChi,
                PhanQuyen = result.PhanQuyen,
                ThoiGianCapNhat = DateTime.Now,
                TrangThai = result.TrangThai
            };

            _context.Entry(result).State = EntityState.Detached;
            _context.DanhSachTaiKhoan.Update(taiKhoanToUpdate);
            await _context.SaveChangesAsync();
            return taiKhoanToUpdate;
        }

        public async Task<TaiKhoan> DangNhap(string tenDangNhap, string matKhau)
        {
            var taiKhoan = await _context.DanhSachTaiKhoan.FirstOrDefaultAsync(x => x.TenDangNhap == tenDangNhap);

            if (taiKhoan == null)
                return null;

            if (!KiemTraHash(matKhau, taiKhoan.Hash, taiKhoan.Salt))
                return null;

            return taiKhoan;
        }

        public async Task<TaiKhoan> DeleteById(string id)
        {
            var taiKhoanToDelete = await _context.DanhSachTaiKhoan.FirstOrDefaultAsync(x => x.MaTaiKhoan == id);

            _context.DanhSachTaiKhoan.Remove(taiKhoanToDelete);
            await _context.SaveChangesAsync();
            return taiKhoanToDelete;
        }

        public async Task<PagedList<TaiKhoan>> GetAll(TaiKhoanParams userParams)
        {
            var result = _context.DanhSachTaiKhoan.AsQueryable();
            var sortField = userParams.SortField;
            var sortOrder = userParams.SortOrder;
            var keyword = userParams.Keyword;

            var maTaiKhoan = userParams.MaTaiKhoan;
            var tenDangNhap = userParams.TenDangNhap;
            var phanQuyen = userParams.PhanQuyen;
            var hoVaTen = userParams.HoVaTen;
            var gioiTinh = userParams.GioiTinh;
            var ngaySinhBatDau = userParams.NgaySinhBatDau;
            var ngaySinhKetThuc = userParams.NgaySinhKetThuc;
            var email = userParams.Email;
            var diaChi = userParams.DiaChi;
            var soDienThoai = userParams.SoDienThoai;

            var thoiGianTaoBatDau = userParams.ThoiGianTaoBatDau;
            var thoiGianTaoKetThuc = userParams.ThoiGianTaoKetThuc;
            var thoiGianCapNhatBatDau = userParams.ThoiGianCapNhatBatDau;
            var thoiGianCapNhatKetThuc = userParams.ThoiGianCapNhatKetThuc;
            var trangThai = userParams.TrangThai;
            var daXoa = userParams.DaXoa;

           // Tai khoan
           if (!string.IsNullOrEmpty(maTaiKhoan))
            {
                result = result.Where(x => x.MaTaiKhoan.ToLower().Contains(maTaiKhoan.ToLower()));
            }

            if (!string.IsNullOrEmpty(tenDangNhap))
            {
                result = result.Where(x => x.TenDangNhap.ToLower().Contains(tenDangNhap.ToLower()));
            }

            if (!string.IsNullOrEmpty(hoVaTen))
            {
                result = result.Where(x => x.HoVaTen.ToLower().Contains(hoVaTen.ToLower()));
            }

            if (!string.IsNullOrEmpty(gioiTinh))
            {
                result = result.Where(x => x.GioiTinh.ToLower().Contains(gioiTinh.ToLower()));
            }

            if (!string.IsNullOrEmpty(phanQuyen))
            {
                result = result.Where(x => x.PhanQuyen.ToLower().Contains(phanQuyen.ToLower()));
            }

            if (ngaySinhBatDau.GetHashCode() != 0 && ngaySinhKetThuc.GetHashCode() != 0)
            {
                result = result.Where(x => x.NgaySinh >= ngaySinhBatDau && x.NgaySinh <= ngaySinhKetThuc);
            }

            if (!string.IsNullOrEmpty(email))
            {
                result = result.Where(x => x.Email.ToLower().Contains(email.ToLower()));
            }

            if (!string.IsNullOrEmpty(diaChi))
            {
                result = result.Where(x => x.DiaChi.ToLower().Contains(diaChi.ToLower()));
            }

            if (!string.IsNullOrEmpty(soDienThoai))
            {
                result = result.Where(x => x.SoDienThoai.ToLower().Contains(soDienThoai.ToLower()));
            }

            // Base 
            if (thoiGianTaoBatDau.GetHashCode() != 0 && thoiGianTaoKetThuc.GetHashCode() != 0)
            {
                result = result.Where(x => x.ThoiGianTao >= thoiGianTaoBatDau && x.ThoiGianTao <= thoiGianTaoKetThuc);
            }

            if (thoiGianCapNhatBatDau.GetHashCode() != 0 && thoiGianCapNhatKetThuc.GetHashCode() != 0)
            {
                result = result.Where(x => x.ThoiGianCapNhat >= thoiGianCapNhatBatDau && x.ThoiGianCapNhat <= thoiGianCapNhatKetThuc);
            }

            if (trangThai == -1 || trangThai == 1)
            {
                result = result.Where(x => x.TrangThai == trangThai);
            }
            
            if(daXoa == 1 || daXoa == 0)
            {
                result = result.Where(x => x.DaXoa == daXoa);
            }

            if (!string.IsNullOrEmpty(sortField) && !string.IsNullOrEmpty(sortOrder))
            {
                switch (sortField)
                {
                    case "MaTaiKhoan":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.MaTaiKhoan);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.MaTaiKhoan);
                        }
                        break;

                    case "TenDangNhap":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.TenDangNhap);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.TenDangNhap);
                        }
                        break;

                    case "PhanQuyen":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.PhanQuyen);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.PhanQuyen);
                        }
                        break;

                    case "ThoiGianTao":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.ThoiGianTao);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.ThoiGianTao);
                        }
                        break;

                    case "ThoiGianCapNhat":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.ThoiGianCapNhat);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.ThoiGianCapNhat);
                        }
                        break;

                    case "TrangThai":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.TrangThai);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.TrangThai);
                        }
                        break;

                    default:
                        result = result.OrderByDescending(x => x.ThoiGianTao);
                        break;
                }
            }

            return await PagedList<TaiKhoan>.CreateAsync(result, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<TaiKhoan> GetById(string id)
        {
            var result = await _context.DanhSachTaiKhoan.FirstOrDefaultAsync(x => x.MaTaiKhoan == id);
            return result;
        }

        public object GetStatusStatistics(TaiKhoanParams userParams)
        {
            var result = _context.DanhSachTaiKhoan.AsQueryable();
            var sortField = userParams.SortField;
            var sortOrder = userParams.SortOrder;
            var keyword = userParams.Keyword;
            var phanQuyen = userParams.PhanQuyen;
            var thoiGianTaoBatDau = userParams.ThoiGianTaoBatDau;
            var thoiGianTaoKetThuc = userParams.ThoiGianTaoKetThuc;
            var thoiGianCapNhatBatDau = userParams.ThoiGianCapNhatBatDau;
            var thoiGianCapNhatKetThuc = userParams.ThoiGianCapNhatKetThuc;
            var trangThai = userParams.TrangThai;

            if (!string.IsNullOrEmpty(keyword))
            {
                result = result.Where(x => x.TenDangNhap.ToLower().Contains(keyword.ToLower()) || x.MaTaiKhoan.ToString() == keyword);
            }

            if (!string.IsNullOrEmpty(phanQuyen))
            {
                result = result.Where(x => x.PhanQuyen.ToLower().Contains(phanQuyen.ToLower()));
            }

            if (thoiGianTaoBatDau.GetHashCode() != 0 && thoiGianTaoKetThuc.GetHashCode() != 0)
            {
                result = result.Where(x => x.ThoiGianTao >= thoiGianTaoBatDau && x.ThoiGianTao <= thoiGianTaoKetThuc);
            }

            if (thoiGianCapNhatBatDau.GetHashCode() != 0 && thoiGianCapNhatKetThuc.GetHashCode() != 0)
            {
                result = result.Where(x => x.ThoiGianCapNhat >= thoiGianCapNhatBatDau && x.ThoiGianCapNhat <= thoiGianCapNhatKetThuc);
            }

            if (trangThai == -1 || trangThai == 1)
            {
                result = result.Where(x => x.TrangThai == trangThai);
            }

            if (!string.IsNullOrEmpty(sortField) && !string.IsNullOrEmpty(sortOrder))
            {
                switch (sortField)
                {
                    case "MaTaiKhoan":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.MaTaiKhoan);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.MaTaiKhoan);
                        }
                        break;

                    case "TenDangNhap":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.TenDangNhap);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.TenDangNhap);
                        }
                        break;

                    case "PhanQuyen":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.PhanQuyen);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.PhanQuyen);
                        }
                        break;

                    case "ThoiGianTao":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.ThoiGianTao);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.ThoiGianTao);
                        }
                        break;

                    case "ThoiGianCapNhat":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.ThoiGianCapNhat);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.ThoiGianCapNhat);
                        }
                        break;

                    case "TrangThai":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.TrangThai);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.TrangThai);
                        }
                        break;

                    default:
                        result = result.OrderByDescending(x => x.ThoiGianTao);
                        break;
                }
            }

            var all = result.Count();
            var active = result.Count(x => x.DaXoa == 0);
            var inactive = result.Count(x => x.DaXoa == 1);

            return new
            {
                All = all,
                Active = active,
                Inactive = inactive
            };
        }

        public int GetTotalItems()
        {
            return _totalItems;
        }

        public int GetTotalPages()
        {
            return _totalPages;
        }

        public async Task<TaiKhoan> PermanentlyDeleteById(string id)
        {
            var taiKhoanToDelete = await _context.DanhSachTaiKhoan.FirstOrDefaultAsync(x => x.MaTaiKhoan == id);

            _context.DanhSachTaiKhoan.Remove(taiKhoanToDelete);
            await _context.SaveChangesAsync();

            return taiKhoanToDelete;
        }

        public async Task<TaiKhoan> RestoreById(string id)
        {
            var taiKhoanToRestoreById = await _context.DanhSachTaiKhoan.FirstOrDefaultAsync(x => x.MaTaiKhoan == id);

            taiKhoanToRestoreById.DaXoa = 0;
            taiKhoanToRestoreById.ThoiGianCapNhat = DateTime.Now;

            _context.DanhSachTaiKhoan.Update(taiKhoanToRestoreById);
            await _context.SaveChangesAsync();

            return taiKhoanToRestoreById;
        }

        public async Task<bool> TaiKhoanTonTai(string tenDangNhap)
        {
            var result = await _context.DanhSachTaiKhoan.FirstOrDefaultAsync(x => x.TenDangNhap == tenDangNhap);

            if (result != null)
                return true;

            return false;
        }

        public async Task<TaiKhoan> TaoTaiKhoan(TaiKhoan taiKhoan, string matKhau)
        {
            byte[] hash, salt;
            TaoHash(matKhau, out hash, out salt);

            taiKhoan.Hash = hash;
            taiKhoan.Salt = salt;
            taiKhoan.ThoiGianTao = DateTime.Now;
            taiKhoan.ThoiGianCapNhat = DateTime.Now;
            taiKhoan.TrangThai = 1;

            await _context.DanhSachTaiKhoan.AddAsync(taiKhoan);
            await _context.SaveChangesAsync();

            return taiKhoan;
        }

        public async Task<TaiKhoan> TemporarilyDeleteById(string id)
        {
            var taiKhoanToTemporarilyDeleteById = await _context.DanhSachTaiKhoan.FirstOrDefaultAsync(x => x.MaTaiKhoan == id);

            taiKhoanToTemporarilyDeleteById.DaXoa = 1;
            taiKhoanToTemporarilyDeleteById.ThoiGianCapNhat = DateTime.Now;

            _context.DanhSachTaiKhoan.Update(taiKhoanToTemporarilyDeleteById);
            await _context.SaveChangesAsync();

            return taiKhoanToTemporarilyDeleteById;
        }

        public async Task<TaiKhoan> UpdateById(string id, TaiKhoanForUpdateDto taiKhoan)
        {
            var taiKhoanCu = await _context.DanhSachTaiKhoan.FirstOrDefaultAsync(x => x.MaTaiKhoan == id);

            var taiKhoanToUpdate = new TaiKhoan
            {
                MaTaiKhoan = id,
                TenDangNhap = taiKhoan.TenDangNhap,
                HoVaTen = taiKhoan.HoVaTen,
                Hash = taiKhoanCu.Hash,
                Salt = taiKhoanCu.Salt,
                NgaySinh = taiKhoan.NgaySinh,
                GioiTinh = taiKhoan.GioiTinh,
                SoDienThoai = taiKhoan.SoDienThoai,
                Email = taiKhoan.Email,
                DiaChi = taiKhoan.DiaChi,
                PhanQuyen = taiKhoan.PhanQuyen,
                ThoiGianCapNhat = DateTime.Now,
                TrangThai = taiKhoan.TrangThai
            };

            _context.Entry(taiKhoanCu).State = EntityState.Detached;
            _context.DanhSachTaiKhoan.Update(taiKhoanToUpdate);
            await _context.SaveChangesAsync();
            return taiKhoanToUpdate;
        }

        public async Task<TaiKhoan> ValidateHash(string tenDangNhap, byte[] hash)
        {
            var result = await _context.DanhSachTaiKhoan.FirstOrDefaultAsync(x => x.TenDangNhap == tenDangNhap && x.Hash == hash);
            return result;
        }

        private bool KiemTraHash(string matKhau, byte[] hash, byte[] salt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(salt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(matKhau));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != hash[i]) return false;
                }
            }

            return true;
        }

        private void TaoHash(string matKhau, out byte[] hash, out byte[] salt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                salt = hmac.Key;
                hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(matKhau));
            }
        }
    }
}
