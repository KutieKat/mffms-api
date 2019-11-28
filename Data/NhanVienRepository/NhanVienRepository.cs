using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MFFMS.API.Dtos;
using MFFMS.API.Dtos.NhanVienDto;
using MFFMS.API.Helpers;
using MFFMS.API.Helpers.Params;
using MFFMS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace MFFMS.API.Data.NhanVienRepository
{
    public class NhanVienRepository : INhanVienRepository
    {
        private DataContext _context;
        private int _totalItems;
        private int _totalPages;


        public NhanVienRepository(DataContext context)
        {
            _context = context;
            _totalItems = 0;
            _totalPages = 0;
        }


        public async Task<NhanVien> Create(NhanVienForCreateDto nhanVien)
        {
            var newNhanVien = new NhanVien
            {
                MaNhanVien = GenerateId(),
                TenNhanVien = nhanVien.TenNhanVien,
                GioiTinh = nhanVien.GioiTinh,
                NgaySinh = nhanVien.NgaySinh,
                ChucVu = nhanVien.ChucVu,
                SoDienThoai = nhanVien.SoDienThoai,
                SoCMND = nhanVien.SoCMND,
                Luong = nhanVien.Luong,
                GhiChu = nhanVien.GhiChu,
                ThoiGianTao = DateTime.Now,
                ThoiGianCapNhat = DateTime.Now,
                TrangThai = 1
            };

            await _context.DanhSachNhanVien.AddAsync(newNhanVien);
            await _context.SaveChangesAsync();
            return newNhanVien;
        }

        public async Task<PagedList<NhanVien>> GetAll(NhanVienParams userParams)
        {
            var result = _context.DanhSachNhanVien.AsQueryable();
            var sortField = userParams.SortField;
            var sortOrder = userParams.SortOrder;
            var keyword = userParams.Keyword;
            var thoiGianTaoBatDau = userParams.ThoiGianTaoBatDau;
            var thoiGianTaoKetThuc = userParams.ThoiGianTaoKetThuc;
            var thoiGianCapNhatBatDau = userParams.ThoiGianCapNhatBatDau;
            var thoiGianCapNhatKetThuc = userParams.ThoiGianCapNhatKetThuc;
            var trangThai = userParams.TrangThai;
            var ngaySinhBatDau = userParams.NgaySinhBatDau;
            var ngaySinhKetThuc = userParams.NgaySinhKetThuc;
            var chucVu = userParams.ChucVu;

            if (!string.IsNullOrEmpty(keyword))
            {
                result = result.Where(x => x.TenNhanVien.ToLower().Contains(keyword.ToLower()) ||
                                           x.MaNhanVien.ToLower().Contains(keyword.ToLower()) ||
                                           x.SoDienThoai.ToLower().Contains(keyword.ToLower()) ||
                                           x.GioiTinh.ToLower().Contains(keyword.ToLower()));
            }

            if (!string.IsNullOrEmpty(chucVu))
            {
                result = result.Where(x => x.ChucVu.ToLower().Contains(chucVu.ToLower()));
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

            if (ngaySinhBatDau.GetHashCode() != 0 && ngaySinhKetThuc.GetHashCode() != 0)
            {
                result = result.Where(x => x.NgaySinh >= ngaySinhBatDau && x.NgaySinh <= ngaySinhKetThuc);
            }

            if (!string.IsNullOrEmpty(sortField) && !string.IsNullOrEmpty(sortOrder))
            {
                switch (sortField)
                {
                    case "MaNhanVien":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.MaNhanVien);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.MaNhanVien);
                        }
                        break;

                    case "TenNhanVien":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.TenNhanVien);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.TenNhanVien);
                        }
                        break;

                    case "GioiTinh":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.GioiTinh);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.GioiTinh);
                        }
                        break;

                    case "NgaySinh":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.NgaySinh);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.NgaySinh);
                        }
                        break;

                    case "ChucVu":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.ChucVu);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.ChucVu);
                        }
                        break;

                    case "SoDienThoai":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.SoDienThoai);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.SoDienThoai);
                        }
                        break;

                    case "SoCMND":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.SoCMND);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.SoCMND);
                        }
                        break;

                    case "Luong":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.Luong);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.Luong);
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
            _totalItems = result.Count();
            _totalPages = (int)Math.Ceiling((double)_totalItems / (double)userParams.PageSize);

            return await PagedList<NhanVien>.CreateAsync(result, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<NhanVien> GetById(string id)
        {
            var result = await _context.DanhSachNhanVien.FirstOrDefaultAsync(x => x.MaNhanVien == id);
            return result;
        }

        public object GetStatusStatistics(NhanVienParams userParams)
        {
            var result = _context.DanhSachNhanVien.AsQueryable();
            var sortField = userParams.SortField;
            var sortOrder = userParams.SortOrder;
            var keyword = userParams.Keyword;
            var thoiGianTaoBatDau = userParams.ThoiGianTaoBatDau;
            var thoiGianTaoKetThuc = userParams.ThoiGianTaoKetThuc;
            var thoiGianCapNhatBatDau = userParams.ThoiGianCapNhatBatDau;
            var thoiGianCapNhatKetThuc = userParams.ThoiGianCapNhatKetThuc;
            var trangThai = userParams.TrangThai;
            var ngaySinhBatDau = userParams.NgaySinhBatDau;
            var ngaySinhKetThuc = userParams.NgaySinhKetThuc;
            var chucVu = userParams.ChucVu;

            if (!string.IsNullOrEmpty(keyword))
            {
                result = result.Where(x => x.TenNhanVien.ToLower().Contains(keyword.ToLower()) ||
                                           x.MaNhanVien.ToLower().Contains(keyword.ToLower()) ||
                                           x.SoDienThoai.ToLower().Contains(keyword.ToLower()) ||
                                           x.GioiTinh.ToLower().Contains(keyword.ToLower()));
            }

            if (!string.IsNullOrEmpty(chucVu))
            {
                result = result.Where(x => x.ChucVu.ToLower().Contains(chucVu.ToLower()));
            }

            if (thoiGianTaoBatDau.GetHashCode() != 0 && thoiGianTaoKetThuc.GetHashCode() != 0)
            {
                result = result.Where(x => x.ThoiGianTao >= thoiGianTaoBatDau && x.ThoiGianTao <= thoiGianTaoKetThuc);
            }

            if (thoiGianCapNhatBatDau.GetHashCode() != 0 && thoiGianCapNhatKetThuc.GetHashCode() != 0)
            {
                result = result.Where(x => x.ThoiGianCapNhat >= thoiGianCapNhatBatDau && x.ThoiGianCapNhat <= thoiGianCapNhatKetThuc);
            }

            if (ngaySinhBatDau.GetHashCode() != 0 && ngaySinhKetThuc.GetHashCode() != 0)
            {
                result = result.Where(x => x.NgaySinh >= ngaySinhBatDau && x.NgaySinh <= ngaySinhKetThuc);
            }

            if (!string.IsNullOrEmpty(sortField) && !string.IsNullOrEmpty(sortOrder))
            {
                switch (sortField)
                {
                    case "MaNhanVien":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.MaNhanVien);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.MaNhanVien);
                        }
                        break;

                    case "TenNhanVien":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.TenNhanVien);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.TenNhanVien);
                        }
                        break;

                    case "GioiTinh":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.GioiTinh);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.GioiTinh);
                        }
                        break;

                    case "NgaySinh":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.NgaySinh);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.NgaySinh);
                        }
                        break;

                    case "ChucVu":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.ChucVu);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.ChucVu);
                        }
                        break;

                    case "SoDienThoai":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.SoDienThoai);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.SoDienThoai);
                        }
                        break;

                    case "SoCMND":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.SoCMND);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.SoCMND);
                        }
                        break;

                    case "Luong":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.Luong);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.Luong);
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
            var active = result.Count(x => x.TrangThai == 1);
            var inactive = result.Count(x => x.TrangThai == -1);

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

        public async Task<NhanVien> PermanentlyDeleteById(string id)
        {
            var nhanVienToDelete = await _context.DanhSachNhanVien.FirstOrDefaultAsync(x => x.MaNhanVien == id);

            _context.DanhSachNhanVien.Remove(nhanVienToDelete);
            await _context.SaveChangesAsync();

            return nhanVienToDelete;
        }

        public async Task<NhanVien> RestoreById(string id)
        {
            var nhanVienToRestoreById = await _context.DanhSachNhanVien.FirstOrDefaultAsync(x => x.MaNhanVien == id);

            nhanVienToRestoreById.TrangThai = 1;
            nhanVienToRestoreById.ThoiGianCapNhat = DateTime.Now;

            _context.DanhSachNhanVien.Update(nhanVienToRestoreById);
            await _context.SaveChangesAsync();

            return nhanVienToRestoreById;
        }

        public async Task<NhanVien> TemporarilyDeleteById(string id)
        {
            var nhanVienToTemporarilyDeleteById = await _context.DanhSachNhanVien.FirstOrDefaultAsync(x => x.MaNhanVien == id);

            nhanVienToTemporarilyDeleteById.TrangThai = -1;
            nhanVienToTemporarilyDeleteById.ThoiGianCapNhat = DateTime.Now;

            _context.DanhSachNhanVien.Update(nhanVienToTemporarilyDeleteById);
            await _context.SaveChangesAsync();

            return nhanVienToTemporarilyDeleteById;
        }

        public async Task<NhanVien> UpdateById(string id, NhanVienForUpdateDto nhanVien)
        {
            var oldRecord = await _context.DanhSachNhanVien.AsNoTracking().FirstOrDefaultAsync(x => x.MaNhanVien == id);
            var nhanVienToUpdate = new NhanVien
            {
                MaNhanVien = id,
                TenNhanVien = nhanVien.TenNhanVien,
                GioiTinh = nhanVien.GioiTinh,
                NgaySinh = nhanVien.NgaySinh,
                ChucVu = nhanVien.ChucVu,
                SoDienThoai = nhanVien.SoDienThoai,
                SoCMND = nhanVien.SoCMND,
                Luong = nhanVien.Luong,
                GhiChu = nhanVien.GhiChu,
                ThoiGianTao = oldRecord.ThoiGianTao,
                ThoiGianCapNhat = DateTime.Now,
                TrangThai = nhanVien.TrangThai
            };

            _context.DanhSachNhanVien.Update(nhanVienToUpdate);
            await _context.SaveChangesAsync();
            return nhanVienToUpdate;
        }

        public ValidationResultDto ValidateBeforeCreate(NhanVienForCreateDto nhanVien)
        {
            var totalSoDienThoai = _context.DanhSachNhanVien.Count(x => x.SoDienThoai.ToLower() == nhanVien.SoDienThoai.ToLower());
            var totalSoCMND = _context.DanhSachNhanVien.Count(x => x.SoCMND.ToLower() == nhanVien.SoCMND.ToLower());
            IDictionary<string, string[]> Errors = new Dictionary<string, string[]>();

            if (totalSoDienThoai >= 1 || totalSoCMND >= 1)
            {
                if (totalSoDienThoai >= 1)
                {
                    Errors.Add("soDienThoai", new string[] { "soDienThoai is duplicated!" });
                }

                if (totalSoCMND >= 1)
                {
                    Errors.Add("soCMND", new string[] { "soCMND is duplicated!" });
                }

                return new ValidationResultDto
                {
                    IsValid = false,
                    Errors = Errors
                };
            }
            else
            {
                return new ValidationResultDto
                {
                    IsValid = true
                };
            }
        }

        public ValidationResultDto ValidateBeforeUpdate(string id, NhanVienForUpdateDto nhanVien)
        {
            var totalSoDienThoai = _context.DanhSachNhanVien.Count(x => x.MaNhanVien != id && x.SoDienThoai.ToLower() == nhanVien.SoDienThoai.ToLower());
            var totalSoCMND = _context.DanhSachNhanVien.Count(x => x.MaNhanVien != id && x.SoCMND.ToLower() == nhanVien.SoCMND.ToLower());
            IDictionary<string, string[]> Errors = new Dictionary<string, string[]>();

            if (totalSoDienThoai > 0 || totalSoCMND > 0)
            {
                if (totalSoDienThoai > 0)
                {
                    Errors.Add("soDienThoai", new string[] { "soDienThoai is duplicated!" });
                }

                if (totalSoCMND > 0)
                {
                    Errors.Add("soCMND", new string[] { "soCMND is duplicated!" });
                }

                return new ValidationResultDto
                {
                    IsValid = false,
                    Errors = Errors
                };
            }
            else
            {
                return new ValidationResultDto
                {
                    IsValid = true
                };
            }
        }

        private string GenerateId()
        {
            int count = _context.DanhSachNhanVien.Count() + 1;
            string tempId = count.ToString();
            string currentYear = DateTime.Now.ToString("yy");

            while (tempId.Length < 4)
            {
                tempId = "0" + tempId;
            }

            tempId = "NV" + currentYear + tempId;

            return tempId;
        }
    }
}
