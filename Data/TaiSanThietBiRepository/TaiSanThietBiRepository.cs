using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MFFMS.API.Dtos;
using MFFMS.API.Dtos.TaiSanThietBiDto;
using MFFMS.API.Helpers;
using MFFMS.API.Helpers.Params;
using MFFMS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace MFFMS.API.Data.TaiSanThietBiRepository
{
    public class TaiSanThietBiRepository : ITaiSanThietBiRepository
    {
        private DataContext _context;
        private int _totalItems;
        private int _totalPages;

        public TaiSanThietBiRepository (DataContext context)
        {
            _context = context;
            _totalItems = 0;
            _totalPages = 0;
        }

        public async Task<TaiSanThietBi> Create(TaiSanThietBiForCreateDto taiSanThietBi)
        {
            var danhSachTaiSanThietBi = await _context.DanhSachTaiSanThietBi.OrderByDescending(x => x.MaTSTB).FirstOrDefaultAsync();
            var maTSTB = 1;

            if (danhSachTaiSanThietBi == null)
            {
                maTSTB = 1;
            }
            else
            {
                maTSTB = danhSachTaiSanThietBi.MaTSTB + 1;
            }

            var newTaiSanThietBi = new TaiSanThietBi
            {
                MaTSTB = maTSTB ,
                MaNhaCungCap = taiSanThietBi.NhaCungCap.MaNhaCungCap,
                NhaCungCap = taiSanThietBi.NhaCungCap,
                TenTSTB = taiSanThietBi.TenTSTB,
                TinhTrang = taiSanThietBi.TinhTrang,
                ThongTinBaoHanh = taiSanThietBi.ThongTinBaoHanh,
                ThoiGianCapNhat = DateTime.Now,
                ThoiGianTao = DateTime.Now,
                TrangThai = 1
            };
            await _context.DanhSachTaiSanThietBi.AddAsync(newTaiSanThietBi);
            await _context.SaveChangesAsync();
            return newTaiSanThietBi;
        }

        public async Task<PagedList<TaiSanThietBi>> GetAll(TaiSanThietBiParams userParams)
        {
            var result = _context.DanhSachTaiSanThietBi.AsQueryable();
            var sortField = userParams.SortField;
            var sortOrder = userParams.SortOrder;
            var keyword = userParams.Keyword;
            var thoiGianTaoBatDau = userParams.ThoiGianTaoBatDau;
            var thoiGianTaoKetThuc = userParams.ThoiGianTaoKetThuc;
            var thoiGianCapNhatBatDau = userParams.ThoiGianCapNhatBatDau;
            var thoiGianCapNhatKetThuc = userParams.ThoiGianCapNhatKetThuc;
            var trangThai = userParams.TrangThai;

            if (!string.IsNullOrEmpty(keyword))
            {
                result = result.Where(x => x.TenTSTB.ToLower().Contains(keyword.ToLower()) || x.MaTSTB.ToString() == keyword);
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
                    case "MaTSTB":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.MaTSTB);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.MaTSTB);
                        }
                        break;

                    case "TenTSTB":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.TenTSTB);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.TenTSTB);
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

            return await PagedList<TaiSanThietBi>.CreateAsync(result, userParams.PageNumber, userParams.PageSize);
        }

         public async Task<TaiSanThietBi> GetById(int id)
        {
            var result = await _context.DanhSachTaiSanThietBi.FirstOrDefaultAsync(x => x.MaTSTB == id);
            return result;
        }

         public object GetStatusStatistics(TaiSanThietBiParams userParams)
        {
            var result = _context.DanhSachTaiSanThietBi.AsQueryable();
            var sortField = userParams.SortField;
            var sortOrder = userParams.SortOrder;
            var keyword = userParams.Keyword;
            var thoiGianTaoBatDau = userParams.ThoiGianTaoBatDau;
            var thoiGianTaoKetThuc = userParams.ThoiGianTaoKetThuc;
            var thoiGianCapNhatBatDau = userParams.ThoiGianCapNhatBatDau;
            var thoiGianCapNhatKetThuc = userParams.ThoiGianCapNhatKetThuc;
            var trangThai = userParams.TrangThai;

            if (!string.IsNullOrEmpty(keyword))
            {
                result = result.Where(x => x.TenTSTB.ToLower().Contains(keyword.ToLower()) || x.MaTSTB.ToString() == keyword);
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
                    case "MaTSTB":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.MaTSTB);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.MaTSTB);
                        }
                        break;

                    case "TenTSTB":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.TenTSTB);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.TenTSTB);
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

        public async Task<TaiSanThietBi> PermanentlyDeleteById(int id)
        {
            var TSTBToDelete = await _context.DanhSachTaiSanThietBi.FirstOrDefaultAsync(x => x.MaTSTB == id);

            _context.DanhSachTaiSanThietBi.Remove(TSTBToDelete);
            await _context.SaveChangesAsync();

            return TSTBToDelete;
        }

        public async Task<TaiSanThietBi> RestoreById(int id)
        {
            var TSTBToRestoreById = await _context.DanhSachTaiSanThietBi.FirstOrDefaultAsync(x => x.MaTSTB == id);

            TSTBToRestoreById.TrangThai = 1;
            TSTBToRestoreById.ThoiGianCapNhat = DateTime.Now;

            _context.DanhSachTaiSanThietBi.Update(TSTBToRestoreById);
            await _context.SaveChangesAsync();

            return TSTBToRestoreById;
        }

        public async Task<TaiSanThietBi> TemporarilyDeleteById(int id)
        {
            var TSTBTemporarilyDeleteById = await _context.DanhSachTaiSanThietBi.FirstOrDefaultAsync(x => x.MaTSTB == id);

            TSTBTemporarilyDeleteById.TrangThai = -1;
            TSTBTemporarilyDeleteById.ThoiGianCapNhat = DateTime.Now;

            _context.DanhSachTaiSanThietBi.Update(TSTBTemporarilyDeleteById);
            await _context.SaveChangesAsync();

            return TSTBTemporarilyDeleteById;
        }

        public async Task<TaiSanThietBi> UpdateById(int id, TaiSanThietBiForUpdateDto taiSanThietBi)
        {
            var oldRecord = await _context.DanhSachTaiSanThietBi.AsNoTracking().FirstOrDefaultAsync(x => x.MaTSTB == id);
            var TSTBToUpdate = new TaiSanThietBi
            {
                MaTSTB = id,
                MaNhaCungCap = taiSanThietBi.NhaCungCap.MaNhaCungCap,
                NhaCungCap = taiSanThietBi.NhaCungCap,
                TenTSTB = taiSanThietBi.TenTSTB,
                TinhTrang = taiSanThietBi.TinhTrang,
                ThongTinBaoHanh = taiSanThietBi.ThongTinBaoHanh,
                ThoiGianCapNhat = DateTime.Now,
                ThoiGianTao = DateTime.Now,
                TrangThai = 1
            };

            _context.DanhSachTaiSanThietBi.Update(TSTBToUpdate);
            await _context.SaveChangesAsync();
            return TSTBToUpdate;
        }

        public ValidationResultDto ValidateBeforeCreate(TaiSanThietBiForCreateDto taiSanThietBi)
        {
            var totalTSTB = _context.DanhSachTaiSanThietBi.Count(x => x.TenTSTB.ToLower() == taiSanThietBi.TenTSTB.ToLower());
            IDictionary<string, string[]> Errors = new Dictionary<string, string[]>();

            if (totalTSTB >= 1)
            {
                Errors.Add("tenTSTB", new string[] { "tenTSTB is duplicated!" });

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

        public ValidationResultDto ValidateBeforeUpdate(int id, TaiSanThietBiForUpdateDto taiSanThietBi)
        {
            var totalTSTB = _context.DanhSachTaiSanThietBi.Count(x => x.MaTSTB != id && x.TenTSTB.ToLower() == taiSanThietBi.TenTSTB.ToLower());
            IDictionary<string, string[]> Errors = new Dictionary<string, string[]>();

            if (totalTSTB > 0)
            {
                Errors.Add("tenTSTB", new string[] { "tenTSTB is duplicated!" });

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