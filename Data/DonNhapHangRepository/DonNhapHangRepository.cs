using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MFFMS.API.Dtos.DonNhapHangDto;
using MFFMS.API.Helpers;
using MFFMS.API.Helpers.Params;
using MFFMS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace MFFMS.API.Data.DonNhapHangRepository
{
    public class DonNhapHangRepository : IDonNhapHangRepository
    {
        private readonly DataContext _context;
        private int _totalItems;
        private int _totalPages;

        public DonNhapHangRepository(DataContext context)
        {
            _context = context;
            _totalItems = 0;
            _totalPages = 0;
        }
        private string GenerateId()
        {
            int count = _context.DanhSachDonNhapHang.Count() + 1;
            string tempId = count.ToString();
            string currentYear = DateTime.Now.ToString("yy");
 
            while (tempId.Length < 4)
            {
                tempId = "0" + tempId;
            }
 
            tempId = "DNH" + currentYear + tempId;
 
            return tempId;
        }

        public async Task<DonNhapHang> Create(DonNhapHangForCreateDto donNhapHang)
        {
            var tinhTrang = "";

            if (donNhapHang.DaThanhToan == 0)
            {
                tinhTrang = "Chưa thanh toán";
            }
            else if (donNhapHang.DaThanhToan != donNhapHang.ThanhTien && donNhapHang.DaThanhToan > 0)
            {
                tinhTrang = "Đã thanh toán một phần";
            }
            else
            {
                tinhTrang = "Đã thanh toán";
            }

            var newDonNhapHang = new DonNhapHang
            {
                MaDonNhapHang = GenerateId(),
                MaNhaCungCap = donNhapHang.MaNhaCungCap,
                MaNhanVien = donNhapHang.MaNhanVien,
                NgayGiaoHang = donNhapHang.NgayGiaoHang,
                NgayLap = donNhapHang.NgayLap,
                GhiChu = donNhapHang.GhiChu,
                ThanhTien = donNhapHang.ThanhTien,
                DaThanhToan = donNhapHang.DaThanhToan,
                TinhTrang = tinhTrang,
                ThoiGianCapNhat = DateTime.Now,
                ThoiGianTao = DateTime.Now,
                TrangThai = 1,
                DaXoa = 0
            };

            await _context.DanhSachDonNhapHang.AddAsync(newDonNhapHang);
            await _context.SaveChangesAsync();
            return newDonNhapHang;
        }

        public async Task<PagedList<DonNhapHang>> GetAll(DonNhapHangParams userParams)
        {
            var result = _context.DanhSachDonNhapHang.Include(x => x.ChiTietDonNhapHang).Include(x=>x.NhaCungCap).Include(x=>x.NhanVien).AsQueryable();
            var sortField = userParams.SortField;
            var sortOrder = userParams.SortOrder;
            
            var maDonNhapHang = userParams.MaDonNhapHang;
            var maNhaCungCap = userParams.MaNhaCungCap;
            var maNhanVien = userParams.MaNhanVien;
            var ngayGiaoHangBatDau = userParams.NgayGiaoHangBatDau;
            var ngayGiaoHangKetThuc = userParams.NgayGiaoHangKetThuc;
            var ngayLapBatDau = userParams.NgayLapBatDau;
            var ngayLapKetThuc = userParams.NgayLapKetThuc;
            var ghiChu = userParams.GhiChu;
            var thanhTienBatDau = userParams.ThanhTienBatDau;
            var thanhTienKetThuc = userParams.ThanhTienKetThuc;
            var daThanhToanBatDau = userParams.DaThanhToanBatDau;
            var daThanhToanKetThuc = userParams.DaThanhToanKetThuc;
            var tinhTrang = userParams.TinhTrang;

            var thoiGianTaoBatDau = userParams.ThoiGianTaoBatDau;
            var thoiGianTaoKetThuc = userParams.ThoiGianTaoKetThuc;
            var thoiGianCapNhatBatDau = userParams.ThoiGianCapNhatBatDau;
            var thoiGianCapNhatKetThuc = userParams.ThoiGianCapNhatKetThuc;
            var trangThai = userParams.TrangThai;
            var daXoa = userParams.DaXoa;

            // DonNhapHang 
            if (!string.IsNullOrEmpty(maDonNhapHang))
            {
                result = result.Where(x => x.MaDonNhapHang.ToLower().Contains(maDonNhapHang.ToLower()));
            }

            if (!string.IsNullOrEmpty(maNhaCungCap))
            {
                result = result.Where(x => x.MaNhaCungCap.ToLower().Contains(maNhaCungCap.ToLower()));
            }

            if (!string.IsNullOrEmpty(maNhanVien))
            {
                result = result.Where(x => x.MaNhanVien.ToLower().Contains(maNhanVien.ToLower()));
            }

            if (!string.IsNullOrEmpty(ghiChu))
            {
                result = result.Where(x => x.MaNhanVien.ToLower().Contains(ghiChu.ToLower()));
            }

            if (!string.IsNullOrEmpty(tinhTrang))
            {
                result = result.Where(x => x.TinhTrang.ToLower().Contains(tinhTrang.ToLower()));
            }

            if (ngayGiaoHangBatDau.GetHashCode() != 0 && ngayGiaoHangKetThuc.GetHashCode() != 0)
            {
                result = result.Where(x => x.NgayGiaoHang >= ngayGiaoHangBatDau && x.NgayGiaoHang <= ngayGiaoHangKetThuc);
            }

            if (ngayLapBatDau.GetHashCode() != 0 && ngayLapKetThuc.GetHashCode() != 0)
            {
                result = result.Where(x => x.NgayLap >= ngayLapBatDau && x.NgayLap <= ngayLapKetThuc);
            }

            if (thanhTienBatDau > 0 && thanhTienKetThuc > 0)
            {
                result = result.Where(x => x.ThanhTien >= thanhTienBatDau && x.ThanhTien <= thanhTienKetThuc);
            }

            if (daThanhToanBatDau > 0 && daThanhToanKetThuc > 0)
            {
                result = result.Where(x => x.DaThanhToan >= daThanhToanBatDau && x.DaThanhToan <= daThanhToanKetThuc);
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

            if (daXoa == 0 || daXoa == 1)
            {
                result = result.Where(x => x.DaXoa == daXoa);
            }

            
            if (ngayGiaoHangBatDau.GetHashCode() != 0 && ngayGiaoHangKetThuc.GetHashCode() != 0)
            {
                result = result.Where(x => x.NgayGiaoHang >= ngayGiaoHangKetThuc && x.NgayGiaoHang <= ngayGiaoHangKetThuc);
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

                    case "NgayGiaoHang":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.NgayGiaoHang);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.NgayGiaoHang);
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

                    case "TinhTrang":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.TinhTrang);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.TinhTrang);
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

            return await PagedList<DonNhapHang>.CreateAsync(result, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<DonNhapHang> GetById(string id)
        {
            var result = await _context.DanhSachDonNhapHang.Include(x => x.ChiTietDonNhapHang).Include(x => x.NhanVien).Include(x => x.NhaCungCap).FirstOrDefaultAsync(x => x.MaDonNhapHang == id);
            return result;
        }

        public object GetStatusStatistics(DonNhapHangParams userParams)
        {
            var result = _context.DanhSachDonNhapHang.Include(x => x.NhaCungCap).Include(x => x.NhanVien).AsQueryable();
            var sortField = userParams.SortField;
            var sortOrder = userParams.SortOrder;
            var keyword = userParams.Keyword;
            var thoiGianTaoBatDau = userParams.ThoiGianTaoBatDau;
            var thoiGianTaoKetThuc = userParams.ThoiGianTaoKetThuc;
            var thoiGianCapNhatBatDau = userParams.ThoiGianCapNhatBatDau;
            var thoiGianCapNhatKetThuc = userParams.ThoiGianCapNhatKetThuc;
            var trangThai = userParams.TrangThai;
            var daXoa = userParams.DaXoa;
            var ngayGiaoHangBatDau = userParams.NgayGiaoHangBatDau;
            var ngayGiaoHangKetThuc = userParams.NgayGiaoHangKetThuc;

            if (!string.IsNullOrEmpty(keyword))
            {
                result = result.Where(x => x.MaNhanVien.ToLower().Contains(keyword.ToLower()) || x.MaDonNhapHang.ToString() == keyword || x.MaDonNhapHang.ToString() == keyword);
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

            if (ngayGiaoHangBatDau.GetHashCode() != 0 && ngayGiaoHangKetThuc.GetHashCode() != 0)
            {
                result = result.Where(x => x.NgayGiaoHang >= ngayGiaoHangKetThuc && x.NgayGiaoHang <= ngayGiaoHangKetThuc);
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

                    case "NgayGiaoHang":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.NgayGiaoHang);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.NgayGiaoHang);
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

        public async Task<DonNhapHang> PermanentlyDeleteById(string id)
        {
            var donNhapHangToDelete = await _context.DanhSachDonNhapHang.FirstOrDefaultAsync(x => x.MaDonNhapHang == id);
            _context.DanhSachDonNhapHang.Remove(donNhapHangToDelete);
            await _context.SaveChangesAsync();
            return donNhapHangToDelete;
        }

        public async Task<DonNhapHang> RestoreById(string id)
        {
            var donNhapHangToRestoreById = await _context.DanhSachDonNhapHang.FirstOrDefaultAsync(x => x.MaDonNhapHang == id);
            donNhapHangToRestoreById.DaXoa = 0;
            donNhapHangToRestoreById.ThoiGianCapNhat = DateTime.Now;
            _context.DanhSachDonNhapHang.Update(donNhapHangToRestoreById);
            await _context.SaveChangesAsync();
            return donNhapHangToRestoreById;
        }

        public async Task<DonNhapHang> TemporarilyDeleteById(string id)
        {
            var donNhapHangToTemporarilyDeleteById = await _context.DanhSachDonNhapHang.FirstOrDefaultAsync(x => x.MaDonNhapHang == id);
            donNhapHangToTemporarilyDeleteById.DaXoa = 1;
            donNhapHangToTemporarilyDeleteById.ThoiGianCapNhat = DateTime.Now;
            _context.DanhSachDonNhapHang.Update(donNhapHangToTemporarilyDeleteById);
            await _context.SaveChangesAsync();
            return donNhapHangToTemporarilyDeleteById;
        }

        public async Task<DonNhapHang> UpdateById(string id, DonNhapHangForUpdateDto donNhapHang)
        {
            var oldRecord = await _context.DanhSachDonNhapHang.AsNoTracking().FirstOrDefaultAsync(x => x.MaDonNhapHang == id);
            var tinhTrang = "";

            if (donNhapHang.DaThanhToan == 0)
            {
                tinhTrang = "Chưa thanh toán";
            }
            else if (donNhapHang.DaThanhToan != donNhapHang.ThanhTien && donNhapHang.DaThanhToan > 0)
            {
                tinhTrang = "Đã thanh toán một phần";
            }
            else
            {
                tinhTrang = "Đã thanh toán";
            }

            var donNhapHangToUpdateById = new DonNhapHang
            {
                MaDonNhapHang = id,
                MaNhaCungCap = donNhapHang.MaNhaCungCap,
                MaNhanVien = donNhapHang.MaNhanVien,
                NgayGiaoHang = donNhapHang.NgayGiaoHang,
                NgayLap = donNhapHang.NgayLap,
                GhiChu = donNhapHang.GhiChu,
                ThanhTien = donNhapHang.ThanhTien,
                DaThanhToan = donNhapHang.DaThanhToan,
                TinhTrang = tinhTrang,
                TrangThai = donNhapHang.TrangThai,
                ThoiGianTao = oldRecord.ThoiGianTao,
                DaXoa = oldRecord.DaXoa
            };

            _context.DanhSachDonNhapHang.Update(donNhapHangToUpdateById);
            await _context.SaveChangesAsync();
            return donNhapHangToUpdateById;
        }

        public async Task<object> GetGeneralStatistics(DonNhapHangGeneralStatisticsParams userParams)
        {
            var result = _context.DanhSachDonNhapHang.AsQueryable();
            var total = 0.0;
            var cheapest = 0.0;
            var mostExpensive = 0.0;

            if (userParams != null && userParams.StartingTime.GetHashCode() != 0 && userParams.EndingTime.GetHashCode() != 0)
            {
                total = result.Where(x => x.ThoiGianTao >= userParams.StartingTime && x.ThoiGianTao <= userParams.EndingTime).Count();
                cheapest = result.Where(x => x.ThoiGianTao >= userParams.StartingTime && x.ThoiGianTao <= userParams.EndingTime).Min(x => x.ThanhTien);
                mostExpensive = result.Where(x => x.ThoiGianTao >= userParams.StartingTime && x.ThoiGianTao <= userParams.EndingTime).Max(x => x.ThanhTien);
            }
            else
            {
                total = result.Count();
                cheapest = result.Min(x => x.ThanhTien);
                mostExpensive = result.Max(x => x.ThanhTien);
            }

            return new
            {
                Total = total,
                Cheapest = cheapest,
                MostExpensive = mostExpensive
            };
        }
    }
}
