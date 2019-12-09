using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MFFMS.API.Dtos;
using MFFMS.API.Dtos.DichVuDto;
using MFFMS.API.Helpers;
using MFFMS.API.Helpers.Params;
using MFFMS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace MFFMS.API.Data.DichVuRepository
{
    public class DichVuRepository : IDichVuRepository
    {
        private readonly DataContext _context;
        private int _totalItems;
        private int _totalPages;

        public DichVuRepository(DataContext context)
        {
            _context = context;
            _totalItems = 0;
            _totalPages = 0;
        }
        public async Task<DichVu> Create(DichVuForCreateDto dichVu)
        {
            var danhSachDichVu = await _context.DanhSachDichVu.OrderByDescending(x => x.MaDichVu).FirstOrDefaultAsync();
            var maDichVu = 1;
            if(danhSachDichVu == null)
            {
                maDichVu = 1;
            }
            else
            {
                maDichVu = danhSachDichVu.MaDichVu + 1;
            }

            var newDichVu = new DichVu
            {
                MaDichVu = maDichVu,
                TenDichVu = dichVu.TenDichVu,
                DonGia = dichVu.DonGia,
                DVT = dichVu.DVT,
                GhiChu = dichVu.GhiChu,
                ThoiGianTao = DateTime.Now,
                ThoiGianCapNhat = DateTime.Now,
                TrangThai = 1
            };

            await _context.DanhSachDichVu.AddAsync(newDichVu);
            await _context.SaveChangesAsync();
            return newDichVu;

        }

        public async Task<PagedList<DichVu>> GetAll(DichVuParams userParams)
        {
            var result = _context.DanhSachDichVu.AsQueryable();
            var sortField = userParams.SortField;
            var sortOrder = userParams.SortOrder;
            var keyword = userParams.Keyword;
            var thoiGianTaoBatDau = userParams.ThoiGianTaoBatDau;
            var thoiGianTaoKetThuc = userParams.ThoiGianTaoKetThuc;
            var thoiGianCapNhatBatDau = userParams.ThoiGianCapNhatBatDau;
            var thoiGianCapNhatKetThuc = userParams.ThoiGianCapNhatKetThuc;
            var trangThai = userParams.TrangThai;
            var daXoa = userParams.DaXoa;

            if (!string.IsNullOrEmpty(keyword))
            {
                result = result.Where(x => x.TenDichVu.ToLower().Contains(keyword.ToLower()) || x.DVT.ToLower().Contains(keyword.ToLower()) || x.MaDichVu.ToString() == keyword);
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

            if(daXoa == 0 || daXoa == 1)
            {
                result = result.Where(x => x.DaXoa == daXoa);
            }

            if (!string.IsNullOrEmpty(sortField) && !string.IsNullOrEmpty(sortOrder))
            {
                switch (sortField)
                {
                    case "MaDichVu":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.MaDichVu);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.MaDichVu);
                        }
                        break;

                    case "TenDichVu":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.TenDichVu);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.TenDichVu);
                        }
                        break;

                    case "DonGia":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.DonGia);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.DonGia);
                        }
                        break;
                    case "DVT":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.DVT);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.DVT);
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

            return await PagedList<DichVu>.CreateAsync(result, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<DichVu> GetById(int id)
        {
            var result = await _context.DanhSachDichVu.FirstOrDefaultAsync(x => x.MaDichVu == id);
            return result;
        }

        public object GetStatusStatistics(DichVuParams userParams)
        {
            var result = _context.DanhSachDichVu.AsQueryable();
            var sortField = userParams.SortField;
            var sortOrder = userParams.SortOrder;
            var keyword = userParams.Keyword;
            var thoiGianTaoBatDau = userParams.ThoiGianTaoBatDau;
            var thoiGianTaoKetThuc = userParams.ThoiGianTaoKetThuc;
            var thoiGianCapNhatBatDau = userParams.ThoiGianCapNhatBatDau;
            var thoiGianCapNhatKetThuc = userParams.ThoiGianCapNhatKetThuc;
            var trangThai = userParams.TrangThai;
            var daXoa = userParams.DaXoa;

            if (!string.IsNullOrEmpty(keyword))
            {
                result = result.Where(x => x.TenDichVu.ToLower().Contains(keyword.ToLower()) || x.DVT.ToLower().Contains(keyword.ToLower()) || x.MaDichVu.ToString() == keyword);
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
                    case "MaDichVu":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.MaDichVu);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.MaDichVu);
                        }
                        break;

                    case "TenDichVu":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.TenDichVu);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.TenDichVu);
                        }
                        break;

                    case "DonGia":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.DonGia);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.DonGia);
                        }
                        break;
                    case "DVT":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.DVT);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.DVT);
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

        public async Task<DichVu> PermanentlyDeleteById(int id)
        {
            var dichVuToDelete = await _context.DanhSachDichVu.FirstOrDefaultAsync(x => x.MaDichVu == id);

            _context.DanhSachDichVu.Remove(dichVuToDelete);
            await _context.SaveChangesAsync();

            return dichVuToDelete;
        }

        public async Task<DichVu> RestoreById(int id)
        {
            var dichVuToRestoreById = await _context.DanhSachDichVu.FirstOrDefaultAsync(x => x.MaDichVu == id);

            dichVuToRestoreById.DaXoa = 0;
            dichVuToRestoreById.ThoiGianCapNhat = DateTime.Now;

            _context.DanhSachDichVu.Update(dichVuToRestoreById);
            await _context.SaveChangesAsync();
            return dichVuToRestoreById;
        }

        public async Task<DichVu> TemporarilyDeleteById(int id)
        {
            var dichVuToTemporarilyDeleteById = await _context.DanhSachDichVu.FirstOrDefaultAsync(x => x.MaDichVu == id);

            dichVuToTemporarilyDeleteById.DaXoa = 1;
            dichVuToTemporarilyDeleteById.ThoiGianCapNhat = DateTime.Now;

            _context.DanhSachDichVu.Update(dichVuToTemporarilyDeleteById);
            await _context.SaveChangesAsync();
            return dichVuToTemporarilyDeleteById;
        }

        public async Task<DichVu> UpdateById(int id, DichVuForUpdateDto dichVu)
        {
            var oldRecord = await _context.DanhSachDichVu.AsNoTracking().FirstOrDefaultAsync(x => x.MaDichVu == id);
            var dichVuToUpdate = new DichVu
            {
                MaDichVu = id,
                TenDichVu = dichVu.TenDichVu,
                DonGia = dichVu.DonGia,
                DVT = dichVu.DVT,
                GhiChu = dichVu.GhiChu,
                TrangThai = dichVu.TrangThai,
                ThoiGianTao = oldRecord.ThoiGianTao,
                ThoiGianCapNhat = DateTime.Now
            };

            _context.DanhSachDichVu.Update(dichVuToUpdate);
            await _context.SaveChangesAsync();
            return dichVuToUpdate;
        }

        public ValidationResultDto ValidateBeforeCreate(DichVuForCreateDto dichVu)
        {
            var totalTenDichVu = _context.DanhSachDichVu.Count(x=>x.TenDichVu == dichVu.TenDichVu);
            IDictionary<string, string[]> Errors = new Dictionary<string, string[]>();

            if (totalTenDichVu >= 1)
            {
                Errors.Add("tenDichVu", new string[] { "tenDichVu is duplicated!" });

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

        public ValidationResultDto ValidateBeforeUpdate(int id, DichVuForUpdateDto dichVu)
        {
            var totalTenDichVu = _context.DanhSachDichVu.Count(x => x.MaDichVu != id && x.TenDichVu == dichVu.TenDichVu);
            IDictionary<string, string[]> Errors = new Dictionary<string, string[]>();

            if(totalTenDichVu > 0)
            {
                Errors.Add("tenDichVu", new string[] { "tenDichVu is duplicated!" });

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
