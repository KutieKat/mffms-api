using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MFFMS.API.Dtos;
using MFFMS.API.Dtos.SanBongDto;
using MFFMS.API.Helpers;
using MFFMS.API.Helpers.Params;
using MFFMS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace MFFMS.API.Data.SanBongRepository
{
    public class SanBongRepository : ISanBongRepository
    {
        private DataContext _context;
        private int _totalItems;
        private int _totalPages;

        public SanBongRepository (DataContext context)
        {
            _context = context;
            _totalItems = 0;
            _totalPages = 0;
        }
        public async Task<SanBong> Create(SanBongForCreateDto sanBong)
        {
            var danhSachSanBong = await _context.DanhSachSanBong.OrderByDescending(x => x.MaSanBong).FirstOrDefaultAsync();
            var maSanBong = 1;
            if(danhSachSanBong == null)
            {
                maSanBong = 1;
            }
            else
            {
                maSanBong = danhSachSanBong.MaSanBong + 1;
            }

            var newSanBong = new SanBong
            {
                MaSanBong = maSanBong,
                TenSanBong = sanBong.TenSanBong,
                DienTich = sanBong.DienTich,
                GhiChu = sanBong.GhiChu,
                ThoiGianTao = DateTime.Now,
                ThoiGianCapNhat = DateTime.Now,
                TrangThai = 1
            };

            await _context.DanhSachSanBong.AddAsync(newSanBong);
            await _context.SaveChangesAsync();
            return newSanBong;
        }

        public async Task<PagedList<SanBong>> GetAll(SanBongParams userParams)
        {
            var result = _context.DanhSachSanBong.AsQueryable();
            var sortField = userParams.SortField;
            var sortOrder = userParams.SortOrder;
            var keyword = userParams.Keyword;
            var thoiGianTaoBatDau = userParams.ThoiGianTaoBatDau;
            var thoiGianTaoKetThuc = userParams.ThoiGianTaoKetThuc;
            var thoiGianCapNhatBatDau = userParams.ThoiGianCapNhatBatDau;
            var thoiGianCapNhatKetThuc = userParams.ThoiGianCapNhatKetThuc;
            var trangThai = userParams.TrangThai;
            var dienTich = userParams.DienTich;

            if (!string.IsNullOrEmpty(keyword))
            {
                result = result.Where(x => x.TenSanBong.ToLower().Contains(keyword.ToLower()) || x.DienTich.ToLower().Contains(keyword.ToLower()) || x.MaSanBong.ToString() == keyword);
            }
            if(!string.IsNullOrEmpty(dienTich))
            {
                result = result.Where(x => x.DienTich.ToLower().Contains(dienTich.ToLower()));
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
                    case "MaSanBong":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.MaSanBong);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.MaSanBong);
                        }
                        break;

                    case "TenSanBong":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.TenSanBong);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.TenSanBong);
                        }
                        break;

                    case "DienTich":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.DienTich);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.DienTich);
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

            return await PagedList<SanBong>.CreateAsync(result, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<SanBong> GetById(int id)
        {
            var result = await _context.DanhSachSanBong.FirstOrDefaultAsync(x => x.MaSanBong == id);
            return result;
        }

        public object GetStatusStatistics(SanBongParams userParams)
        {
            var result = _context.DanhSachSanBong.AsQueryable();
            var sortField = userParams.SortField;
            var sortOrder = userParams.SortOrder;
            var keyword = userParams.Keyword;
            var thoiGianTaoBatDau = userParams.ThoiGianTaoBatDau;
            var thoiGianTaoKetThuc = userParams.ThoiGianTaoKetThuc;
            var thoiGianCapNhatBatDau = userParams.ThoiGianCapNhatBatDau;
            var thoiGianCapNhatKetThuc = userParams.ThoiGianCapNhatKetThuc;
            var trangThai = userParams.TrangThai;
            var dienTich = userParams.DienTich;

            if (!string.IsNullOrEmpty(keyword))
            {
                result = result.Where(x => x.TenSanBong.ToLower().Contains(keyword.ToLower()) || x.DienTich.ToLower().Contains(keyword.ToLower()) || x.MaSanBong.ToString() == keyword);
            }
            if (!string.IsNullOrEmpty(dienTich))
            {
                result = result.Where(x => x.DienTich.ToLower().Contains(dienTich.ToLower()));
            }

            if (thoiGianTaoBatDau.GetHashCode() != 0 && thoiGianTaoKetThuc.GetHashCode() != 0)
            {
                result = result.Where(x => x.ThoiGianTao >= thoiGianTaoBatDau && x.ThoiGianTao <= thoiGianTaoKetThuc);
            }

            if (thoiGianCapNhatBatDau.GetHashCode() != 0 && thoiGianCapNhatKetThuc.GetHashCode() != 0)
            {
                result = result.Where(x => x.ThoiGianCapNhat >= thoiGianCapNhatBatDau && x.ThoiGianCapNhat <= thoiGianCapNhatKetThuc);
            }

            if (!string.IsNullOrEmpty(sortField) && !string.IsNullOrEmpty(sortOrder))
            {
                switch (sortField)
                {
                    case "MaSanBong":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.MaSanBong);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.MaSanBong);
                        }
                        break;

                    case "TenSanBong":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.TenSanBong);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.TenSanBong);
                        }
                        break;

                    case "DienTich":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.DienTich);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.DienTich);
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

        public async Task<SanBong> PermanentlyDeleteById(int id)
        {
            var sanBongToDelete = await _context.DanhSachSanBong.FirstOrDefaultAsync(x => x.MaSanBong == id);

            _context.DanhSachSanBong.Remove(sanBongToDelete);
            await _context.SaveChangesAsync();

            return sanBongToDelete;
        }

        public async Task<SanBong> RestoreById(int id)
        {
            var sanBongToRestoreById = await _context.DanhSachSanBong.FirstOrDefaultAsync(x => x.MaSanBong == id);

            sanBongToRestoreById.TrangThai = 1;
            sanBongToRestoreById.ThoiGianCapNhat = DateTime.Now;

            _context.DanhSachSanBong.Update(sanBongToRestoreById);
            await _context.SaveChangesAsync();

            return sanBongToRestoreById;
        }

        public async Task<SanBong> TemporarilyDeleteById(int id)
        {
            var sanBongToTemporarilyDeleteById = await _context.DanhSachSanBong.FirstOrDefaultAsync(x => x.MaSanBong == id);

            sanBongToTemporarilyDeleteById.TrangThai = -1;
            sanBongToTemporarilyDeleteById.ThoiGianCapNhat = DateTime.Now;

            _context.DanhSachSanBong.Update(sanBongToTemporarilyDeleteById);
            await _context.SaveChangesAsync();

            return sanBongToTemporarilyDeleteById;
        }

        public async Task<SanBong> UpdateById(int id, SanBongForUpdateDto sanBong)
        {
            var oldRecord = await _context.DanhSachSanBong.AsNoTracking().FirstOrDefaultAsync(x => x.MaSanBong == id);
            var sanBongToUpdate = new SanBong
            {
                MaSanBong = id,
                TenSanBong = sanBong.TenSanBong,
                DienTich = sanBong.DienTich,
                GhiChu = sanBong.GhiChu,
                ThoiGianTao = oldRecord.ThoiGianTao,
                ThoiGianCapNhat = DateTime.Now,
                TrangThai = sanBong.TrangThai
            };

            _context.DanhSachSanBong.Update(sanBongToUpdate);
            await _context.SaveChangesAsync();
            return sanBongToUpdate;
        }

        public ValidationResultDto ValidateBeforeCreate(SanBongForCreateDto sanBong)
        {
            var totalTenSanBong = _context.DanhSachSanBong.Count(x => x.TenSanBong.ToLower() == sanBong.TenSanBong.ToLower());
            IDictionary<string, string[]> Errors = new Dictionary<string, string[]>();

            if(totalTenSanBong >= 1)
            {
                Errors.Add("tenSanBong", new string[] { "tenSanBong is duplicated!" });

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

        public ValidationResultDto ValidateBeforeUpdate(int id, SanBongForUpdateDto sanBong)
        {
            var totalTenSanBong = _context.DanhSachSanBong.Count(x => x.MaSanBong != id && x.TenSanBong.ToLower() == sanBong.TenSanBong.ToLower());
            IDictionary<string, string[]> Errors = new Dictionary<string, string[]>();

            if(totalTenSanBong > 0)
            {
                Errors.Add("tenSanBong", new string[] { "tenSanBong is duplicated!" });

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
    }
}
