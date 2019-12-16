using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MFFMS.API.Dtos;
using MFFMS.API.Dtos.NhaCungCapDto;
using MFFMS.API.Helpers;
using MFFMS.API.Helpers.Params;
using MFFMS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace MFFMS.API.Data.NhaCungCapRepository
{
    public class NhaCungCapRepository : INhaCungCapRepository
    {
        private readonly DataContext _context;
        private int _totalItems;
        private int _totalPages;

        public NhaCungCapRepository(DataContext context)
        {
            _context = context;
            _totalItems = 0;
            _totalPages = 0;
        }

        public async Task<NhaCungCap> Create(NhaCungCapForCreateDto nhaCungCap)
        {
            var newNhaCungCap = new NhaCungCap
            {
                MaNhaCungCap = GenerateId(),
                TenNhaCungCap = nhaCungCap.TenNhaCungCap,
                SoDienThoai = nhaCungCap.SoDienThoai,
                DiaChi = nhaCungCap.DiaChi,
                GhiChu = nhaCungCap.GhiChu,
                ThoiGianCapNhat = DateTime.Now,
                ThoiGianTao = DateTime.Now,
                TrangThai = 1
            };
            await _context.DanhSachNhaCungCap.AddAsync(newNhaCungCap);
            await _context.SaveChangesAsync();
            return newNhaCungCap;
        }

        public async Task<PagedList<NhaCungCap>> GetAll(NhaCungCapParams userParams)
        {
            var result = _context.DanhSachNhaCungCap.AsQueryable();
            var sortField = userParams.SortField;
            var sortOrder = userParams.SortOrder;
            var keyword = userParams.Keyword;

            var maNhaCungCap = userParams.MaNhaCungCap;
            var tenNhaCungCap = userParams.TenNhaCungCap;
            var soDienThoai = userParams.SoDienThoai;
            var diaChi = userParams.DiaChi;
            var ghiChu = userParams.GhiChu;

            var thoiGianTaoBatDau = userParams.ThoiGianTaoBatDau;
            var thoiGianTaoKetThuc = userParams.ThoiGianTaoKetThuc;
            var thoiGianCapNhatBatDau = userParams.ThoiGianCapNhatBatDau;
            var thoiGianCapNhatKetThuc = userParams.ThoiGianCapNhatKetThuc;
            var trangThai = userParams.TrangThai;
            var daXoa = userParams.DaXoa;

            // Nha cung cap
            if (!string.IsNullOrEmpty(maNhaCungCap))
            {
                result = result.Where(x => x.MaNhaCungCap.ToLower().Contains(maNhaCungCap.ToLower()));
            }

            if (!string.IsNullOrEmpty(tenNhaCungCap))
            {
                result = result.Where(x => x.TenNhaCungCap.ToLower().Contains(tenNhaCungCap.ToLower()));
            }

            if (!string.IsNullOrEmpty(soDienThoai))
            {
                result = result.Where(x => x.SoDienThoai.ToLower().Contains(soDienThoai.ToLower()));
            }

            if (!string.IsNullOrEmpty(diaChi))
            {
                result = result.Where(x => x.DiaChi.ToLower().Contains(diaChi.ToLower()));
            }

            if (!string.IsNullOrEmpty(ghiChu))
            {
                result = result.Where(x => x.GhiChu.ToLower().Contains(ghiChu.ToLower()));
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
                    case "MaNhaCungCap":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.MaNhaCungCap);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.MaNhaCungCap);
                        }
                        break;

                    case "TenNhaCungCap":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.TenNhaCungCap);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.TenNhaCungCap);
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

            return await PagedList<NhaCungCap>.CreateAsync(result, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<Object> GetGeneralStatistics(NhaCungCapStatisticsParams userParams)
        {
            var result = _context.DanhSachNhaCungCap.AsQueryable();
            var totalProvider = result.Count();

            return new
            {
                Total = totalProvider
            };
        }

        public async Task<NhaCungCap> GetById(string id)
        {
            var result = await _context.DanhSachNhaCungCap.FirstOrDefaultAsync(x => x.MaNhaCungCap == id);
            return result;
        }

        public object GetStatusStatistics(NhaCungCapParams userParams)
        {
            var result = _context.DanhSachNhaCungCap.AsQueryable();
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
                result = result.Where(x => x.TenNhaCungCap.ToLower().Contains(keyword.ToLower()) || x.MaNhaCungCap.ToString() == keyword);
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
                    case "MaNhaCungCap":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.MaNhaCungCap);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.MaNhaCungCap);
                        }
                        break;

                    case "TenNhaCungCap":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.TenNhaCungCap);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.TenNhaCungCap);
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

        public async Task<NhaCungCap> PermanentlyDeleteById(string id)
        {
            var nhaCungCapToDelete = await _context.DanhSachNhaCungCap.FirstOrDefaultAsync(x => x.MaNhaCungCap == id);

            _context.DanhSachNhaCungCap.Remove(nhaCungCapToDelete);
            await _context.SaveChangesAsync();

            return nhaCungCapToDelete;
        }

        public async Task<NhaCungCap> RestoreById(string id)
        {
            var nhaCungCapToRestoreById = await _context.DanhSachNhaCungCap.FirstOrDefaultAsync(x => x.MaNhaCungCap == id);

            nhaCungCapToRestoreById.DaXoa = 0;
            nhaCungCapToRestoreById.ThoiGianCapNhat = DateTime.Now;

            _context.DanhSachNhaCungCap.Update(nhaCungCapToRestoreById);
            await _context.SaveChangesAsync();

            return nhaCungCapToRestoreById;
        }

        public async Task<NhaCungCap> TemporarilyDeleteById(string id)
        {
            var nhaCungCapTemporarilyDeleteById = await _context.DanhSachNhaCungCap.FirstOrDefaultAsync(x => x.MaNhaCungCap == id);

            nhaCungCapTemporarilyDeleteById.DaXoa = 1;
            nhaCungCapTemporarilyDeleteById.ThoiGianCapNhat = DateTime.Now;

            _context.DanhSachNhaCungCap.Update(nhaCungCapTemporarilyDeleteById);
            await _context.SaveChangesAsync();

            return nhaCungCapTemporarilyDeleteById;
        }

        public async Task<NhaCungCap> UpdateById(string id, NhaCungCapForUpdateDto nhaCungCap)
        {
            var oldRecord = await _context.DanhSachNhaCungCap.AsNoTracking().FirstOrDefaultAsync(x => x.MaNhaCungCap == id);
            var nhaCungCapToUpdate = new NhaCungCap
            {
                MaNhaCungCap = id,
                TenNhaCungCap = nhaCungCap.TenNhaCungCap,
                SoDienThoai = nhaCungCap.SoDienThoai,
                DiaChi = nhaCungCap.DiaChi,
                GhiChu = nhaCungCap.GhiChu,
                ThoiGianTao = oldRecord.ThoiGianTao,
                ThoiGianCapNhat = DateTime.Now,
                TrangThai = nhaCungCap.TrangThai
            };

            _context.DanhSachNhaCungCap.Update(nhaCungCapToUpdate);
            await _context.SaveChangesAsync();
            return nhaCungCapToUpdate;
        }

        public ValidationResultDto ValidateBeforeCreate(NhaCungCapForCreateDto nhaCungCap)
        {
            var totalTenNhaCungCap = _context.DanhSachNhaCungCap.Count(x => x.TenNhaCungCap.ToLower() == nhaCungCap.TenNhaCungCap.ToLower());
            IDictionary<string, string[]> Errors = new Dictionary<string, string[]>();

            if (totalTenNhaCungCap >= 1)
            {
                Errors.Add("tenNhaCungCap", new string[] { "tenNhaCungCap is duplicated!" });

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

        public ValidationResultDto ValidateBeforeUpdate(string id, NhaCungCapForUpdateDto nhaCungCap)
        {
            var totalTenNhaCungCap = _context.DanhSachNhaCungCap.Count(x => x.MaNhaCungCap != id && x.TenNhaCungCap.ToLower() == nhaCungCap.TenNhaCungCap.ToLower());
            IDictionary<string, string[]> Errors = new Dictionary<string, string[]>();

            if (totalTenNhaCungCap > 0)
            {
                Errors.Add("tenNhaCungCap", new string[] { "tenNhaCungCap is duplicated!" });

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
            int count = _context.DanhSachNhaCungCap.Count() + 1;
            string tempId = count.ToString();
            string currentYear = DateTime.Now.ToString("yy");

            while (tempId.Length < 4)
            {
                tempId = "0" + tempId;
            }

            tempId = "NCC" + currentYear + tempId;

            return tempId;
        }

    }
}
