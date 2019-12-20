using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MFFMS.API.Dtos.ChiTietDonNhapHangDto;
using MFFMS.API.Helpers;
using MFFMS.API.Helpers.Params;
using MFFMS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace MFFMS.API.Data.ChiTietDonNhapHangRepository
{
    public class ChiTietDonNhapHangRepository : IChiTietDonNhapHangRepository
    {
        private readonly DataContext _context;
        private int _totalPages;
        private int _totalItems;

        public ChiTietDonNhapHangRepository(DataContext context)
        {
            _context = context;
            _totalItems = 0;
            _totalPages = 0;
        }
        private string GenerateId()
        {
            int count = _context.DanhSachChiTietDonNhapHang.Count() + 1;
            string tempId = count.ToString();
            string currentYear = DateTime.Now.ToString("yy");
 
            while (tempId.Length < 4)
            {
                tempId = "0" + tempId;
            }
 
            tempId = "CTDNH" + currentYear + tempId;
 
            return tempId;
        }
        public async Task<ChiTietDonNhapHang> Create(ChiTietDonNhapHangForCreateDto chiTietDonNhapHang)
        {
            var newChiTietDonNhapHang = new ChiTietDonNhapHang
            {
                MaDonNhapHang = GenerateId(),
                MaTSTB = chiTietDonNhapHang.MaTSTB,
                SoLuong = chiTietDonNhapHang.SoLuong,
                DVT = chiTietDonNhapHang.DVT,
                DonGia = chiTietDonNhapHang.DonGia,
                ThanhTien = chiTietDonNhapHang.ThanhTien,
                ThoiGianCapNhat = DateTime.Now,
                ThoiGianTao = DateTime.Now,
                TrangThai = 1
            };

            await _context.DanhSachChiTietDonNhapHang.AddAsync(newChiTietDonNhapHang);
            await _context.SaveChangesAsync();
            return newChiTietDonNhapHang;
        }

        public async Task<ICollection<ChiTietDonNhapHang>> CreateMultiple(ICollection<ChiTietDonNhapHangForCreateMultiple> danhSachChiTietDonNhapHang)
        {
            ICollection<ChiTietDonNhapHang> temp = new List<ChiTietDonNhapHang>();
            for (int i = 0; i < danhSachChiTietDonNhapHang.Count; i++)
            {
                var chiTietDonNhapHang = danhSachChiTietDonNhapHang.ElementAt(i);
                var newChiTietDonNhapHang = new ChiTietDonNhapHang
                {
                    MaDonNhapHang = chiTietDonNhapHang.MaDonNhapHang,
                    MaTSTB = chiTietDonNhapHang.MaTSTB,
                    SoLuong = chiTietDonNhapHang.SoLuong,
                    DVT = chiTietDonNhapHang.DVT,
                    DonGia = chiTietDonNhapHang.DonGia,
                    ThanhTien = chiTietDonNhapHang.ThanhTien,
                    ThoiGianCapNhat = DateTime.Now,
                    ThoiGianTao = DateTime.Now,
                    TrangThai = 1
                };

                temp.Add(newChiTietDonNhapHang);

                await _context.DanhSachChiTietDonNhapHang.AddAsync(newChiTietDonNhapHang);
                await _context.SaveChangesAsync();
            }
            return temp;
        }

        public async Task<PagedList<ChiTietDonNhapHang>> GetAll(ChiTietDonNhapHangParams userParams)
        {
                var result = _context.DanhSachChiTietDonNhapHang.Include(x => x.DonNhapHang).Include(x => x.TaiSanThietBi).AsQueryable();
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
                    result = result.Where(x => x.MaDonNhapHang.ToString().ToLower().Contains(keyword.ToLower()) || x.MaTSTB.ToString() == keyword);
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
                        case "MaDonNhapHang":
                            if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                            {
                                result = result.OrderBy(x => x.MaDonNhapHang);
                            }
                            else
                            {
                                result = result.OrderByDescending(x => x.MaDonNhapHang);
                            }
                            break;

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
                        case "SoLuong":
                            if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                            {
                                result = result.OrderBy(x => x.SoLuong);
                            }
                            else
                            {
                                result = result.OrderByDescending(x => x.SoLuong);
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

                return await PagedList<ChiTietDonNhapHang>.CreateAsync(result, userParams.PageNumber, userParams.PageSize);
            }

        public async Task<ChiTietDonNhapHang> GetById(string maDonNhapHang, string maTSTB)
        {
            var result = await _context.DanhSachChiTietDonNhapHang
                .Include(x => x.DonNhapHang)
                .Include(x => x.TaiSanThietBi)
                .FirstOrDefaultAsync(x => x.MaDonNhapHang == maDonNhapHang && x.MaTSTB == maTSTB);
            return result;
        }

        public object GetStatusStatistics(ChiTietDonNhapHangParams userParams)
        {
            var result = _context.DanhSachChiTietDonNhapHang.Include(x => x.DonNhapHang).Include(x => x.TaiSanThietBi).AsQueryable();
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
                result = result.Where(x => x.MaDonNhapHang.ToString().ToLower().Contains(keyword.ToLower()) || x.MaTSTB.ToString() == keyword);
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
                    case "MaDonNhapHang":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.MaDonNhapHang);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.MaDonNhapHang);
                        }
                        break;

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
                    case "SoLuong":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.SoLuong);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.SoLuong);
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

        public async Task<ChiTietDonNhapHang> PermanentlyDeleteById(string maDonNhapHang, string maTSTB)
        {
            var chiTietDonNhapHangToDelete = await _context.DanhSachChiTietDonNhapHang.FirstOrDefaultAsync(x => x.MaDonNhapHang == maDonNhapHang || x.MaTSTB == maTSTB);

            _context.DanhSachChiTietDonNhapHang.Remove(chiTietDonNhapHangToDelete);
            await _context.SaveChangesAsync();

            return chiTietDonNhapHangToDelete;
        }

        public async Task<ChiTietDonNhapHang> RestoreById(string maDonNhapHang, string maTSTB)
        {
            var chiTietDonNhapHangToRestoreById = await _context.DanhSachChiTietDonNhapHang.FirstOrDefaultAsync(x => x.MaDonNhapHang == maDonNhapHang && x.MaTSTB == maTSTB);

            chiTietDonNhapHangToRestoreById.DaXoa = 0;
            chiTietDonNhapHangToRestoreById.ThoiGianCapNhat = DateTime.Now;

            _context.DanhSachChiTietDonNhapHang.Update(chiTietDonNhapHangToRestoreById);
            await _context.SaveChangesAsync();
            return chiTietDonNhapHangToRestoreById;
        }

        public async Task<ChiTietDonNhapHang> TemporarilyDeleteById(string maDonNhapHang, string maTSTB)
        {
            var chiTietDonNhapHangToTemporarilyDeleteById = await _context.DanhSachChiTietDonNhapHang.FirstOrDefaultAsync(x => x.MaDonNhapHang == maDonNhapHang && x.MaTSTB == maTSTB);

            chiTietDonNhapHangToTemporarilyDeleteById.DaXoa = 1;
            chiTietDonNhapHangToTemporarilyDeleteById.ThoiGianCapNhat = DateTime.Now;

            _context.DanhSachChiTietDonNhapHang.Update(chiTietDonNhapHangToTemporarilyDeleteById);
            await _context.SaveChangesAsync();
            return chiTietDonNhapHangToTemporarilyDeleteById;
        }

        public async Task<ChiTietDonNhapHang> UpdateById(string maDonNhapHang, string maTSTB, ChiTietDonNhapHangForUpdateDto chiTietDonNhapHang)
        {
            var oldRecord = await _context.DanhSachChiTietDonNhapHang.AsNoTracking().FirstOrDefaultAsync(x => x.MaDonNhapHang == maDonNhapHang && x.MaTSTB == maTSTB);
            var chiTietDonNhapHangToUpdate = new ChiTietDonNhapHang
            {
                MaDonNhapHang = maDonNhapHang,
                MaTSTB = maTSTB,
                SoLuong = chiTietDonNhapHang.SoLuong,
                DonGia = chiTietDonNhapHang.DonGia,
                DVT = chiTietDonNhapHang.DVT,
                ThanhTien = chiTietDonNhapHang.ThanhTien,
                TrangThai = chiTietDonNhapHang.TrangThai,
                ThoiGianTao = oldRecord.ThoiGianTao,
                ThoiGianCapNhat = DateTime.Now
            };

            _context.DanhSachChiTietDonNhapHang.Update(chiTietDonNhapHangToUpdate);
            await _context.SaveChangesAsync();
            return chiTietDonNhapHangToUpdate;
        }
    }
}
