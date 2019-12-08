using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MFFMS.API.Dtos;
using MFFMS.API.Dtos.HoaDonDichVuDto;
using MFFMS.API.Helpers;
using MFFMS.API.Helpers.Params;
using MFFMS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace MFFMS.API.Data.HoaDonDichVuRepository
{
    public class HoaDonDichVuRepository : IHoaDonDichVuRepository
    {
        private readonly DataContext _context;
        private int _totalItems;
        private int _totalPages;

        public HoaDonDichVuRepository(DataContext context)
        {
            _context = context;
            _totalItems = 0;
            _totalPages = 0;
        }


        public async Task<HoaDonDichVu> Create(HoaDonDichVuForCreateDto hoaDonDichVu)
        {
            var danhSachHDDV = await _context.DanhSachHoaDonDichVu.OrderByDescending(x => x.SoHDDV).FirstOrDefaultAsync();
            var soHDDV = 1;
            if(danhSachHDDV == null)
            {
                soHDDV = 1;
            }
            else
            {
                soHDDV = danhSachHDDV.SoHDDV + 1;
            }

            var newHoaDonDichVu = new HoaDonDichVu();
            newHoaDonDichVu.SoHDDV = soHDDV;
            newHoaDonDichVu.NgayLap = hoaDonDichVu.NgayLap;
            newHoaDonDichVu.NgaySuDung = hoaDonDichVu.NgaySuDung;
            newHoaDonDichVu.GhiChu = hoaDonDichVu.GhiChu;
            newHoaDonDichVu.MaKhachHang = hoaDonDichVu.MaKhachHang;
            newHoaDonDichVu.MaDichVu = hoaDonDichVu.MaDichVu;
            newHoaDonDichVu.ThoiGianTao = DateTime.Now;
            newHoaDonDichVu.ThoiGianCapNhat = DateTime.Now;
            newHoaDonDichVu.TrangThai = 1;
           

            await _context.DanhSachHoaDonDichVu.AddAsync(newHoaDonDichVu);
            await _context.SaveChangesAsync();
            return newHoaDonDichVu;
            


        }

        public async Task<PagedList<HoaDonDichVu>> GetAll(HoaDonDichVuParams userParams)
        {
            var result = _context.DanhSachHoaDonDichVu.Include(x=>x.KhachHang).Include(x=>x.DichVu).AsQueryable();
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
                result = result.Where(x => x.MaKhachHang.ToLower().Contains(keyword.ToLower()) || x.MaDichVu.ToString().ToLower().Contains(keyword.ToLower()) || x.SoHDDV.ToString() == keyword);
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
                    case "SoHDDV":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.SoHDDV);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.SoHDDV);
                        }
                        break;

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
                    case "NgaySuDung":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.NgaySuDung);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.NgaySuDung);
                        }
                        break;
                    case "NgayLap":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.NgayLap);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.NgayLap);
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

            return await PagedList<HoaDonDichVu>.CreateAsync(result, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<HoaDonDichVu> GetById(int id)
        {
            var result = await _context.DanhSachHoaDonDichVu.Include(x=>x.DichVu).Include(x=>x.KhachHang).FirstOrDefaultAsync(x => x.SoHDDV == id);
            return result;
        }

        public object GetStatusStatistics(HoaDonDichVuParams userParams)
        {
            var result = _context.DanhSachHoaDonDichVu.AsQueryable();
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
                result = result.Where(x => x.MaKhachHang.ToLower().Contains(keyword.ToLower()) || x.MaDichVu.ToString().ToLower().Contains(keyword.ToLower()) || x.SoHDDV.ToString() == keyword);
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
                    case "SoHDDV":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.SoHDDV);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.SoHDDV);
                        }
                        break;

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
                    case "NgaySuDung":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.NgaySuDung);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.NgaySuDung);
                        }
                        break;
                    case "NgayLap":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.NgayLap);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.NgayLap);
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

        public async Task<HoaDonDichVu> PermanentlyDeleteById(int id)
        {
            var hoaDonDichVuToDelete = await _context.DanhSachHoaDonDichVu.FirstOrDefaultAsync(x => x.SoHDDV == id);

            _context.DanhSachHoaDonDichVu.Remove(hoaDonDichVuToDelete);
            await _context.SaveChangesAsync();

            return hoaDonDichVuToDelete;
        }

        public async Task<HoaDonDichVu> RestoreById(int id)
        {
            var hoaDonDichVuToRestoreById = await _context.DanhSachHoaDonDichVu.FirstOrDefaultAsync(x => x.SoHDDV == id);

            hoaDonDichVuToRestoreById.DaXoa = 0;
            hoaDonDichVuToRestoreById.ThoiGianCapNhat = DateTime.Now;

            _context.DanhSachHoaDonDichVu.Update(hoaDonDichVuToRestoreById);
            await _context.SaveChangesAsync();
            return hoaDonDichVuToRestoreById;
        }

        public async Task<HoaDonDichVu> TemporarilyDeleteById(int id)
        {
            var hoaDonDichVuToTemporarilyDeleteById = await _context.DanhSachHoaDonDichVu.FirstOrDefaultAsync(x => x.SoHDDV == id);

            hoaDonDichVuToTemporarilyDeleteById.DaXoa = 1;
            hoaDonDichVuToTemporarilyDeleteById.ThoiGianCapNhat = DateTime.Now;

            _context.DanhSachHoaDonDichVu.Update(hoaDonDichVuToTemporarilyDeleteById);
            await _context.SaveChangesAsync();
            return hoaDonDichVuToTemporarilyDeleteById;
        }

        public async Task<HoaDonDichVu> UpdateById(int id, HoaDonDichVuForUpdateDto hoaDonDichVu)
        {
            var oldRecord = await _context.DanhSachHoaDonDichVu.AsNoTracking().FirstOrDefaultAsync(x => x.SoHDDV == id);
            var hoaDonDichVuToUpdate = new HoaDonDichVu
            {
                SoHDDV = id,
                MaKhachHang = hoaDonDichVu.MaKhachHang,
                MaDichVu = hoaDonDichVu.MaDichVu,
                NgayLap = hoaDonDichVu.NgayLap,
                NgaySuDung = hoaDonDichVu.NgaySuDung,
                TrangThai = hoaDonDichVu.TrangThai,
                ThoiGianTao = oldRecord.ThoiGianTao,
                ThoiGianCapNhat = DateTime.Now
            };

            

            _context.DanhSachHoaDonDichVu.Update(hoaDonDichVuToUpdate);
            await _context.SaveChangesAsync();
            return hoaDonDichVuToUpdate;
        }

        
    }
}
