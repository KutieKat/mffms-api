using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MFFMS.API.Dtos;
using MFFMS.API.Dtos.ChiTietHDDVDto;
using MFFMS.API.Helpers;
using MFFMS.API.Helpers.Params;
using MFFMS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace MFFMS.API.Data.ChiTietHDDVRepository
{
    public class ChiTietHDDVRepository : IChiTietHDDVRepository
    {
        private readonly DataContext _context;
        private int _totalItems;
        private int _totalPages;

        public ChiTietHDDVRepository(DataContext context)
        {
            _context = context;
            _totalItems = 0;
            _totalPages = 0;

        }

       
        public async Task<ChiTietHDDV> Create(ChiTietHDDVForCreateDto chiTietHDDV)
        {
            var newChiTietHDDV = new ChiTietHDDV
            {
                SoHDDV = chiTietHDDV.SoHDDV,
                MaDichVu = chiTietHDDV.MaDichVu,
                SoLuong = chiTietHDDV.SoLuong,
                DonGia = chiTietHDDV.DonGia,
                ThanhTien = chiTietHDDV.ThanhTien,
                ThoiGianTao = DateTime.Now,
                ThoiGianCapNhat = DateTime.Now,
                TrangThai = 1
            };

            await _context.DanhSachChiTietHDDV.AddAsync(newChiTietHDDV);
            await _context.SaveChangesAsync();
            return newChiTietHDDV;
        }

        public async Task<ICollection<ChiTietHDDV>> CreateMultiple(ICollection<ChiTietHDDVForCreateMultipleDto> danhSachChiTietHDDV)
        {
            ICollection<ChiTietHDDV> temp = new List<ChiTietHDDV>();
            for(int i = 0; i < danhSachChiTietHDDV.Count; i++)
            {
                var chiTietHDDV = danhSachChiTietHDDV.ElementAt(i);
                var newChiTietHDDV = new ChiTietHDDV
                {
                    SoHDDV = chiTietHDDV.SoHDDV,
                    MaDichVu = chiTietHDDV.MaDichVu,
                    SoLuong = chiTietHDDV.SoLuong, 
                    DonGia = chiTietHDDV.DonGia,
                    ThanhTien = chiTietHDDV.ThanhTien,
                    ThoiGianTao = DateTime.Now,
                    ThoiGianCapNhat = DateTime.Now,
                    TrangThai = 1
                };

                temp.Add(newChiTietHDDV);
                await _context.DanhSachChiTietHDDV.AddAsync(newChiTietHDDV);
                await _context.SaveChangesAsync();
            }
            return temp;
        }

        public async Task<PagedList<ChiTietHDDV>> GetAll(ChiTietHDDVParams userParams)
        {
            var result = _context.DanhSachChiTietHDDV.Include(x=>x.HoaDonDichVu).ThenInclude(x => x.KhachHang).Include(x=>x.DichVu).AsQueryable();
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
                result = result.Where(x => x.MaDichVu.ToString().ToLower().Contains(keyword.ToLower()) || x.SoHDDV.ToString() == keyword);
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

            if (daXoa == 0 || daXoa == 1)
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

            return await PagedList<ChiTietHDDV>.CreateAsync(result, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<ChiTietHDDV> GetById(string soHDDV, string maDichVu)
        {
            var result = await _context.DanhSachChiTietHDDV.Include(x => x.DichVu).Include(x => x.HoaDonDichVu).FirstOrDefaultAsync(x => x.SoHDDV == soHDDV && x.MaDichVu == maDichVu);
            return result;
        }

        public object GetStatusStatistics(ChiTietHDDVParams userParams)
        {
            var result = _context.DanhSachChiTietHDDV.Include(x => x.HoaDonDichVu).ThenInclude(x=>x.KhachHang).Include(x => x.DichVu).AsQueryable();
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
                result = result.Where(x => x.MaDichVu.ToString().ToLower().Contains(keyword.ToLower()) || x.SoHDDV.ToString() == keyword);
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

            if (daXoa == 0 || daXoa == 1)
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

        public async Task<ChiTietHDDV> PermanentlyDeleteById(string soHDDV, string maDichVu)
        {
            var chiTietHDDVToDelete = await _context.DanhSachChiTietHDDV.FirstOrDefaultAsync(x => x.SoHDDV == soHDDV || x.MaDichVu == maDichVu);

            _context.DanhSachChiTietHDDV.Remove(chiTietHDDVToDelete);
            await _context.SaveChangesAsync();

            return chiTietHDDVToDelete;
        }
        public async Task<ChiTietHDDV> RestoreById(string soHDDV, string maDichVu)
        {
            var chiTietHDDVToRestoreById = await _context.DanhSachChiTietHDDV.FirstOrDefaultAsync(x => x.SoHDDV == soHDDV && x.MaDichVu == maDichVu);

            chiTietHDDVToRestoreById.DaXoa = 0;
            chiTietHDDVToRestoreById.ThoiGianCapNhat = DateTime.Now;

            _context.DanhSachChiTietHDDV.Update(chiTietHDDVToRestoreById);
            await _context.SaveChangesAsync();
            return chiTietHDDVToRestoreById;
        }

        public async Task<ChiTietHDDV> TemporarilyDeleteById(string soHDDV, string maDichVu)
        {
            var chiTietHDDVToTemporarilyDeleteById = await _context.DanhSachChiTietHDDV.FirstOrDefaultAsync(x => x.SoHDDV == soHDDV && x.MaDichVu == maDichVu);

            chiTietHDDVToTemporarilyDeleteById.DaXoa = 1;
            chiTietHDDVToTemporarilyDeleteById.ThoiGianCapNhat = DateTime.Now;

            _context.DanhSachChiTietHDDV.Update(chiTietHDDVToTemporarilyDeleteById);
            await _context.SaveChangesAsync();
            return chiTietHDDVToTemporarilyDeleteById;
        }

        public async Task<ChiTietHDDV> UpdateById(string soHDDV, string maDichVu, ChiTietHDDVForUpdateDto chiTietHDDV)
        {
            var oldRecord = await _context.DanhSachChiTietHDDV.AsNoTracking().FirstOrDefaultAsync(x => x.SoHDDV == soHDDV && x.MaDichVu == maDichVu);
            var chiTietHDDVToUpdate = new ChiTietHDDV
            {
                SoHDDV = soHDDV,
                MaDichVu = maDichVu,
                SoLuong = chiTietHDDV.SoLuong,
                DonGia = chiTietHDDV.DonGia,
                ThanhTien = chiTietHDDV.ThanhTien,
                TrangThai = chiTietHDDV.TrangThai,
                ThoiGianTao = oldRecord.ThoiGianTao,
                ThoiGianCapNhat = DateTime.Now
            };

            _context.DanhSachChiTietHDDV.Update(chiTietHDDVToUpdate);
            await _context.SaveChangesAsync();
            return chiTietHDDVToUpdate;
        } 
    }
}
