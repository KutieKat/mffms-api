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
        private readonly DataContext _context;
        private int _totalItems;
        private int _totalPages;

        public SanBongRepository(DataContext context)
        {
            _context = context;
            _totalItems = 0;
            _totalPages = 0;
        }
        
        private string GenerateId()
        {
            int count = _context.DanhSachSanBong.Count() + 1;
            string tempId = count.ToString();
            string currentYear = DateTime.Now.ToString("yy");

            while (tempId.Length < 4)
            {
                tempId = "0" + tempId;
            }

            tempId = "SB" + currentYear + tempId;

            return tempId;
        }
        
        public async Task<SanBong> Create(SanBongForCreateDto sanBong)
        {
            var newSanBong = new SanBong
            {
                MaSanBong = GenerateId(),
                TenSanBong = sanBong.TenSanBong,
                ChieuDai = sanBong.ChieuDai,
                ChieuRong = sanBong.ChieuRong,
                GhiChu = sanBong.GhiChu,
                ThoiGianTao = DateTime.Now,
                ThoiGianCapNhat = DateTime.Now,
                TrangThai = 1
            };

            newSanBong.DienTich = sanBong.ChieuDai * sanBong.ChieuRong;

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

            var maSanBong = userParams.MaSanBong;
            var tenSanBong = userParams.TenSanBong;
            var chieuDaiBatDau = userParams.ChieuDaiBatDau;
            var chieuDaiKetThuc = userParams.ChieuDaiKetThuc;
            var chieuRongBatDau = userParams.ChieuRongBatDau;
            var chieuRongKetThuc = userParams.ChieuRongKetThuc;
            var dienTichBatDau = userParams.DienTichBatDau;
            var dienTichKetThuc = userParams.DienTichKetThuc;

            var thoiGianTaoBatDau = userParams.ThoiGianTaoBatDau;
            var thoiGianTaoKetThuc = userParams.ThoiGianTaoKetThuc;
            var thoiGianCapNhatBatDau = userParams.ThoiGianCapNhatBatDau;
            var thoiGianCapNhatKetThuc = userParams.ThoiGianCapNhatKetThuc;
            var trangThai = userParams.TrangThai;
            var daXoa = userParams.DaXoa;

            // SanBong 
            if (!string.IsNullOrEmpty(maSanBong))
            {
                result = result.Where(x => x.MaSanBong.ToLower().Contains(maSanBong.ToLower()));
            }

            if (!string.IsNullOrEmpty(tenSanBong))
            {
                result = result.Where(x => x.TenSanBong.ToLower().Contains(tenSanBong.ToLower()));
            }

            if (chieuDaiBatDau.GetHashCode() != 0 && chieuDaiKetThuc.GetHashCode() != 0)
            {
                result = result.Where(x => x.ChieuDai >= chieuDaiBatDau && x.ChieuDai <= chieuDaiKetThuc);
            }	

            if (chieuRongBatDau.GetHashCode() != 0 && chieuRongKetThuc.GetHashCode() != 0)
            {
                result = result.Where(x => x.ChieuRong >= chieuRongBatDau && x.ChieuRong <= chieuRongKetThuc);
            }	

            if (dienTichBatDau.GetHashCode() != 0 && dienTichKetThuc.GetHashCode() != 0)
            {
                result = result.Where(x => x.DienTich >= dienTichBatDau && x.DienTich <= dienTichKetThuc);
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

            if (daXoa == 1 || daXoa == 0)
            {
                result = result.Where(x => x.DaXoa == daXoa);
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

                    case "ChieuDai":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.ChieuDai);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.ChieuDai);
                        }
                        break;

                    case "ChieuRong":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.ChieuRong);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.ChieuRong);
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

        public async Task<SanBong> GetById(string id)
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
            var daXoa = userParams.DaXoa;

            if (!string.IsNullOrEmpty(keyword))
            {
                result = result.Where(x => x.TenSanBong.ToLower().Contains(keyword.ToLower()) || x.MaSanBong.ToString() == keyword);
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
                    case "ChieuDai":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.ChieuDai);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.ChieuDai);
                        }
                        break;

                    case "ChieuRong":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.ChieuRong);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.ChieuRong);
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
            var active = result.Count(x => x.DaXoa == 0);
            var inactive = result.Count(x => x.DaXoa == 1);

            return new
            {
                All = all,
                Active = active,
                Inactive = inactive
            };
        }

        public async Task<Object> GetGeneralStatistics(SanBongStatisticsParams userParams)
        {
            var result = _context.DanhSachSanBong.AsQueryable();
            var totalPitch = 0; // tổng số sân bóng
            var minAreaPitch = 0.0; // diện tích nhỏ nhất
            var maxAreaePich = 0.0;  // diện tích lớn nhất       

            if (userParams != null && userParams.StartingTime.GetHashCode() != 0 && userParams.EndingTime.GetHashCode() != 0)
            {
                totalPitch = result.Count();
                minAreaPitch = result.Where(x => x.ThoiGianTao >= userParams.StartingTime && x.ThoiGianTao <= userParams.EndingTime).Min(x => x.DienTich);
                maxAreaePich = result.Where(x => x.ThoiGianTao >= userParams.StartingTime && x.ThoiGianTao <= userParams.EndingTime).Max(x => x.DienTich);
            }
            else
            {
                totalPitch = result.Count();
                minAreaPitch = result.Min(x => x.DienTich);
                maxAreaePich = result.Max(x => x.DienTich);
            }

            return new
            {
                Total = totalPitch,
                MinArea = minAreaPitch,
                MaxArea = maxAreaePich
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

        public async Task<SanBong> PermanentlyDeleteById(string id)
        {
            var sanBongToDelete = await _context.DanhSachSanBong.FirstOrDefaultAsync(x => x.MaSanBong == id);

            _context.DanhSachSanBong.Remove(sanBongToDelete);
            await _context.SaveChangesAsync();

            return sanBongToDelete;
        }

        public async Task<SanBong> RestoreById(string id)
        {
            var sanBongToRestoreById = await _context.DanhSachSanBong.FirstOrDefaultAsync(x => x.MaSanBong == id);

            sanBongToRestoreById.DaXoa = 0;
            sanBongToRestoreById.ThoiGianCapNhat = DateTime.Now;

            _context.DanhSachSanBong.Update(sanBongToRestoreById);
            await _context.SaveChangesAsync();

            return sanBongToRestoreById;
        }

        public async Task<SanBong> TemporarilyDeleteById(string id)
        {
            var sanBongToTemporarilyDeleteById = await _context.DanhSachSanBong.FirstOrDefaultAsync(x => x.MaSanBong == id);

            sanBongToTemporarilyDeleteById.DaXoa = 1;
            sanBongToTemporarilyDeleteById.ThoiGianCapNhat = DateTime.Now;

            _context.DanhSachSanBong.Update(sanBongToTemporarilyDeleteById);
            await _context.SaveChangesAsync();

            return sanBongToTemporarilyDeleteById;
        }

        public async Task<SanBong> UpdateById(string id, SanBongForUpdateDto sanBong)
        {
            var oldRecord = await _context.DanhSachSanBong.AsNoTracking().FirstOrDefaultAsync(x => x.MaSanBong == id);
            var sanBongToUpdate = new SanBong
            {
                MaSanBong = id,
                TenSanBong = sanBong.TenSanBong,
                ChieuDai = sanBong.ChieuDai,
                ChieuRong = sanBong.ChieuRong,
                GhiChu = sanBong.GhiChu,
                ThoiGianTao = oldRecord.ThoiGianTao,
                ThoiGianCapNhat = DateTime.Now,
                TrangThai = sanBong.TrangThai
            };

            sanBongToUpdate.DienTich = sanBong.ChieuDai * sanBong.ChieuRong;

            _context.DanhSachSanBong.Update(sanBongToUpdate);
            await _context.SaveChangesAsync();
            return sanBongToUpdate;
        }

        public ValidationResultDto ValidateBeforeCreate(SanBongForCreateDto sanBong)
        {
            var totalTenSanBong = _context.DanhSachSanBong.Count(x => x.TenSanBong.ToLower() == sanBong.TenSanBong.ToLower());
            IDictionary<string, string[]> Errors = new Dictionary<string, string[]>();

            if (totalTenSanBong >= 1)
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

        public ValidationResultDto ValidateBeforeUpdate(string id, SanBongForUpdateDto sanBong)
        {
            var totalTenSanBong = _context.DanhSachSanBong.Count(x => (x.MaSanBong != id) && (x.TenSanBong.ToLower() == sanBong.TenSanBong.ToLower()));
            IDictionary<string, string[]> Errors = new Dictionary<string, string[]>();

            if (totalTenSanBong > 0)
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
