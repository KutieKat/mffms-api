using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MFFMS.API.Dtos.KhachHangDto;
using MFFMS.API.Helpers;
using MFFMS.API.Helpers.Params;
using MFFMS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace MFFMS.API.Data.KhachHangRepository
{
    public class KhachHangRepository : IKhachHangRepository
    {
        private readonly DataContext _context;
        private int _totalItems;
        private int _totalPages;

        public KhachHangRepository(DataContext context)
        {
            _context = context;
            _totalItems = 0;
            _totalPages = 0;
        }

        public async Task<KhachHang> Create(KhachHangForCreateDto khachHang)
        {
            var newKhachHang = new KhachHang
            {
                MaKhachHang = GenerateId(),
                TenKhachHang = khachHang.TenKhachHang,
                GioiTinh = khachHang.GioiTinh,
                NgaySinh = khachHang.NgaySinh,
                SoDienThoai = khachHang.SoDienThoai,
                DiaChi = khachHang.DiaChi,
                GhiChu = khachHang.GhiChu,
                ThoiGianTao = DateTime.Now,
                ThoiGianCapNhat = DateTime.Now,
                TrangThai = 1
            };
            await _context.DanhSachKhachHang.AddAsync(newKhachHang);
            await _context.SaveChangesAsync();
            return newKhachHang;
        }

        public async Task<PagedList<KhachHang>> GetAll(KhachHangParams userParams)
        {
            var result = _context.DanhSachKhachHang.AsQueryable();
            var sortField = userParams.SortField;
            var sortOrder = userParams.SortOrder;

            var maKhachHang = userParams.MaKhachHang;
            var tenKhachHang = userParams.TenKhachHang;
            var gioiTinh = userParams.GioiTinh;
            var ngaySinhBatDau = userParams.NgaySinhBatDau;
            var ngaySinhKetThuc = userParams.NgaySinhKetThuc;
            var soDienThoai = userParams.SoDienThoai;
            var diaChi = userParams.DiaChi;
            var ghiChu = userParams.GhiChu;

            var thoiGianTaoBatDau = userParams.ThoiGianTaoBatDau;
            var thoiGianTaoKetThuc = userParams.ThoiGianTaoKetThuc;
            var thoiGianCapNhatBatDau = userParams.ThoiGianCapNhatBatDau;
            var thoiGianCapNhatKetThuc = userParams.ThoiGianCapNhatKetThuc;
            var trangThai = userParams.TrangThai;
            var daXoa = userParams.DaXoa;

            // KhachHang

            if (!string.IsNullOrEmpty(maKhachHang))
            {
                result = result.Where(x => x.MaKhachHang.ToLower().Contains(maKhachHang.ToLower()));
            }

            if (!string.IsNullOrEmpty(tenKhachHang))
            {
                result = result.Where(x => x.TenKhachHang.ToLower().Contains(tenKhachHang.ToLower()));
            }

            if (!string.IsNullOrEmpty(gioiTinh))
            {
                result = result.Where(x => x.GioiTinh.ToLower().Contains(gioiTinh.ToLower()));
            }

            if (ngaySinhBatDau.GetHashCode() != 0 && ngaySinhKetThuc.GetHashCode() != 0)
            {
                result = result.Where(x => x.NgaySinh >= ngaySinhBatDau && x.ThoiGianTao <= ngaySinhKetThuc);
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

            if (daXoa == 1 || daXoa == 0)
            {
                result = result.Where(x => x.DaXoa == daXoa);
            }

            if (!string.IsNullOrEmpty(sortField) && !string.IsNullOrEmpty(sortOrder))
            {
                switch (sortField)
                {
                    case "MaKhachHang":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.MaKhachHang);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.MaKhachHang);
                        }
                        break;

                    case "TenKhachHang":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.TenKhachHang);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.TenKhachHang);
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

                    case "DiaChi":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.DiaChi);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.DiaChi);
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

            return await PagedList<KhachHang>.CreateAsync(result, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<KhachHang> GetById(string id)
        {
            var result = await _context.DanhSachKhachHang.FirstOrDefaultAsync(x => x.MaKhachHang == id);
            return result;
        }

        public async Task<Object> GetGeneralStatistics(KhachHangStatisticsParams userParams)
        {
            var result = _context.DanhSachKhachHang.AsQueryable();
            var totalCustomers = 0;
            var studentCustomers = 0;
            var youthCustomers = 0;
            var adultCustomers = 0;

            if (userParams != null && userParams.StartingTime.GetHashCode() != 0 && userParams.EndingTime.GetHashCode() != 0)
            {
                totalCustomers = result.Count();  
                studentCustomers = result.Where(x => DateTime.Now.Year - x.NgaySinh.Year < 19 && x.ThoiGianTao >= userParams.StartingTime && x.ThoiGianTao <= userParams.EndingTime).Count();
                youthCustomers =  result.Where(x => DateTime.Now.Year - x.NgaySinh.Year >= 19 && DateTime.Now.Year - x.NgaySinh.Year < 30 && x.ThoiGianTao >= userParams.StartingTime && x.ThoiGianTao <= userParams.EndingTime).Count();
                adultCustomers = result.Where(x => DateTime.Now.Year - x.NgaySinh.Year >= 30 && x.ThoiGianTao >= userParams.StartingTime && x.ThoiGianTao <= userParams.EndingTime).Count();

            }
            else
            {
                totalCustomers = result.Count();
                studentCustomers = result.Where(x => DateTime.Now.Year - x.NgaySinh.Year < 19).Count();
                youthCustomers =  result.Where(x => DateTime.Now.Year - x.NgaySinh.Year >= 19 && DateTime.Now.Year - x.NgaySinh.Year < 30 ).Count();
                adultCustomers = result.Where(x => DateTime.Now.Year - x.NgaySinh.Year >= 30).Count();

            }

            return new
            {
                Total = totalCustomers,
                Student = studentCustomers,
                Youth = youthCustomers,
                Adult = adultCustomers
            };
        }

        public Object GetStatusStatistics(KhachHangParams userParams)
        {
            var result = _context.DanhSachKhachHang.AsQueryable();
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
                result = result.Where(x => x.TenKhachHang.ToLower().Contains(keyword.ToLower()) || x.SoDienThoai.ToLower().Contains(keyword.ToLower()) || x.DiaChi.ToLower().Contains(keyword.ToLower()) || x.MaKhachHang == keyword);
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
                    case "MaKhachHang":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.MaKhachHang);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.MaKhachHang);
                        }
                        break;

                    case "TenKhoa":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.TenKhachHang);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.TenKhachHang);
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
                    case "DiaChi":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.DiaChi);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.DiaChi);
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

        public async Task<KhachHang> PermanentlyDeleteById(string id)
        {
            var khachHangToDelete = await _context.DanhSachKhachHang.FirstOrDefaultAsync(x => x.MaKhachHang == id);

            _context.DanhSachKhachHang.Remove(khachHangToDelete);
            await _context.SaveChangesAsync();

            return khachHangToDelete;
        }

        public async Task<KhachHang> RestoreById(string id)
        {
            var khachHangToRestoreById = await _context.DanhSachKhachHang.FirstOrDefaultAsync(x => x.MaKhachHang == id);

            khachHangToRestoreById.DaXoa = 0;
            khachHangToRestoreById.ThoiGianCapNhat = DateTime.Now;

            _context.DanhSachKhachHang.Update(khachHangToRestoreById);
            await _context.SaveChangesAsync();

            return khachHangToRestoreById;
        }

        public async Task<KhachHang> TemporarilyDeleteById(string id)
        {
            var khachHangToTemporarilyDeleteById = await _context.DanhSachKhachHang.FirstOrDefaultAsync(x => x.MaKhachHang == id);

            khachHangToTemporarilyDeleteById.DaXoa = 1;
            khachHangToTemporarilyDeleteById.ThoiGianCapNhat = DateTime.Now;

            _context.DanhSachKhachHang.Update(khachHangToTemporarilyDeleteById);
            await _context.SaveChangesAsync();

            return khachHangToTemporarilyDeleteById;
        }

        public async Task<KhachHang> UpdateById(string id, KhachHangForUpdateDto khachHang)
        {
            var oldRecord = await _context.DanhSachKhachHang.AsNoTracking().FirstOrDefaultAsync(x => x.MaKhachHang == id);
            var khachHangToUpdate = new KhachHang
            {
                MaKhachHang = id,
                TenKhachHang = khachHang.TenKhachHang,
                GioiTinh = khachHang.GioiTinh,
                NgaySinh = khachHang.NgaySinh,
                SoDienThoai = khachHang.SoDienThoai,
                DiaChi = khachHang.DiaChi,
                GhiChu = khachHang.GhiChu,
                ThoiGianCapNhat = DateTime.Now,
                ThoiGianTao = oldRecord.ThoiGianTao,
                TrangThai = khachHang.TrangThai
            };
            _context.DanhSachKhachHang.Update(khachHangToUpdate);
            await _context.SaveChangesAsync();

            return khachHangToUpdate;
        }

        private string GenerateId()
        {
            int count = _context.DanhSachKhachHang.Count() + 1;
            string tempId = count.ToString();
            string currentYear = DateTime.Now.ToString("yy");

            while (tempId.Length < 4)
            {
                tempId = "0" + tempId;
            }

            tempId = "KH" + currentYear + tempId;

            return tempId;
        }

        private int TinhTuoi(int namSinh)
        {
            return DateTime.Now.Year - namSinh;
        }
    }
}
