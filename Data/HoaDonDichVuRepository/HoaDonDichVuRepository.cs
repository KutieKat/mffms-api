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

        private string GenerateId()
        {
            int count = _context.DanhSachHoaDonDichVu.Count() + 1;
            string tempId = count.ToString();
            string currentYear = DateTime.Now.ToString("yy");

            while (tempId.Length < 4)
            {
                tempId = "0" + tempId;
            }

            tempId = "HDDV" + currentYear + tempId;

            return tempId;
        }

        public async Task<Object> GetGeneralStatistics(HoaDonDichVuStatisticsParams userParams)
        {
            var totalHoaDonDV = 0;
            KhachHang khachHangCoHoaDonNhieuNhat = new KhachHang();
            DichVu dichVuCoHoaDonNhieuNhat = new DichVu();

            var danhSachKhachHang = _context.DanhSachKhachHang.OrderBy(x => x.ThoiGianTao).ToList();
            var danhSachDichVu = _context.DanhSachDichVu.OrderBy(x => x.ThoiGianTao).ToList();

            var demKhachHang = new List<int>();
            var demDichVu = new List<int>();


            if (userParams != null && userParams.StartingTime.GetHashCode() != 0 && userParams.EndingTime.GetHashCode() != 0)
            {
                totalHoaDonDV = _context.DanhSachHoaDonDichVu.Where(x => x.ThoiGianTao >= userParams.StartingTime && x.ThoiGianTao <= userParams.EndingTime).Count();
                var danhSachHoaDonDichVu = _context.DanhSachHoaDonDichVu.Where(x => x.ThoiGianTao >= userParams.StartingTime && x.ThoiGianTao <= userParams.EndingTime);
                var danhSachChiTietHDDV = _context.DanhSachChiTietHDDV.Where(x => x.ThoiGianTao >= userParams.StartingTime && x.ThoiGianTao <= userParams.EndingTime);
                foreach (var khachHang in danhSachKhachHang)
                {
                    int demKH = 0;
                    foreach (var hoaDonDichVu in danhSachHoaDonDichVu)
                    {
                        if (hoaDonDichVu.MaKhachHang == khachHang.MaKhachHang)
                        {
                            demKH = demKH + 1;
                        }
                    }

                    demKhachHang.Add(demKH);
                }

                int maxKH = demKhachHang.Max();
                int indexKH = demKhachHang.IndexOf(maxKH);
                khachHangCoHoaDonNhieuNhat = danhSachKhachHang.ElementAt(indexKH);


                foreach (var dichVu in danhSachDichVu)
                {
                    int demDV = 0;
                    foreach (var chiTietHDDV in danhSachChiTietHDDV)
                    {
                        if (chiTietHDDV.MaDichVu == dichVu.MaDichVu)
                        {
                            demDV = demDV + 1;
                        }
                    }

                    demDichVu.Add(demDV);
                }

                int maxDV = demDichVu.Max();
                int indexDV = demDichVu.IndexOf(maxDV);
                dichVuCoHoaDonNhieuNhat = danhSachDichVu.ElementAt(indexDV);
            }

            else
            {
                totalHoaDonDV = _context.DanhSachHoaDonDichVu.Count();
                var danhSachHoaDonDichVu = _context.DanhSachHoaDonDichVu.OrderBy(x => x.ThoiGianTao);
                var danhSachChiTietHDDV = _context.DanhSachChiTietHDDV.OrderBy(x => x.ThoiGianTao);

                foreach (var khachHang in danhSachKhachHang)
                {
                    int demKH = 0;
                    foreach (var hoaDonDichVu in danhSachHoaDonDichVu)
                    {
                        if (hoaDonDichVu.MaKhachHang == khachHang.MaKhachHang)
                        {
                            demKH = demKH + 1;
                        }
                    }

                    demKhachHang.Add(demKH);
                }

                int maxKH = demKhachHang.Max();
                int indexKH = demKhachHang.IndexOf(maxKH);
                khachHangCoHoaDonNhieuNhat = danhSachKhachHang.ElementAt(indexKH);


                foreach (var dichVu in danhSachDichVu)
                {
                    int demDV = 0;
                    foreach (var chiTietHDDV in danhSachChiTietHDDV)
                    {
                        if (chiTietHDDV.MaDichVu == dichVu.MaDichVu)
                        {
                            demDV = demDV + 1;
                        }
                    }

                    demDichVu.Add(demDV);
                }

                int maxDV = demDichVu.Max();
                int indexDV = demDichVu.IndexOf(maxDV);
                dichVuCoHoaDonNhieuNhat = danhSachDichVu.ElementAt(indexDV);
            }

            return new
            {
                totalHoaDonDV,
                khachHangCoHoaDonNhieuNhat,
                dichVuCoHoaDonNhieuNhat
            };
        }

        public async Task<HoaDonDichVu> Create(HoaDonDichVuForCreateDto hoaDonDichVu)
        {
            var tinhTrang = "";

            if (hoaDonDichVu.DaThanhToan == 0)
            {
                tinhTrang = "Chưa thanh toán";
            }
            else if (hoaDonDichVu.DaThanhToan != hoaDonDichVu.ThanhTien && hoaDonDichVu.DaThanhToan > 0)
            {
                tinhTrang = "Đã thanh toán một phần";
            }
            else
            {
                tinhTrang = "Đã thanh toán";
            }

            var newHoaDonDichVu = new HoaDonDichVu
            {
                SoHDDV = GenerateId(),
                NgayLap = hoaDonDichVu.NgayLap,
                NgaySuDung = hoaDonDichVu.NgaySuDung,
                ThanhTien = hoaDonDichVu.ThanhTien,
                DaThanhToan = hoaDonDichVu.DaThanhToan,
                TinhTrang = tinhTrang,
                GhiChu = hoaDonDichVu.GhiChu,
                MaKhachHang = hoaDonDichVu.MaKhachHang,
                MaNhanVien = hoaDonDichVu.MaNhanVien,
                ThoiGianTao = DateTime.Now,
                ThoiGianCapNhat = DateTime.Now,
                TrangThai = 1
            };
            await _context.DanhSachHoaDonDichVu.AddAsync(newHoaDonDichVu);
            await _context.SaveChangesAsync();
            return newHoaDonDichVu;
        }

        public async Task<PagedList<HoaDonDichVu>> GetAll(HoaDonDichVuParams userParams)
        {
            var result = _context.DanhSachHoaDonDichVu.Include(x => x.ChiTietHDDV).Include(x => x.KhachHang).Include(x => x.NhanVien).AsQueryable();
            var sortField = userParams.SortField;
            var sortOrder = userParams.SortOrder;
            
            var soHDDV = userParams.SoHDDV;
            var maKhachHang = userParams.MaKhachHang;
            var maNhanVien = userParams.MaNhanVien;
            var ngaySuDungBatDau = userParams.NgaySuDungBatDau;
            var ngaySuDungKetThuc = userParams.NgaySuDungKetThuc;
            var ngayLapBatDau = userParams.NgayLapBatDau;
            var ngayLapKetThuc = userParams.NgayLapKetThuc;
            var ghiChu = userParams.GhiChu;
            var tinhTrang = userParams.TinhTrang;
            var thanhTienBatDau = userParams.ThanhTienBatDau;
            var thanhTienKetThuc = userParams.ThanhTienKetThuc;
            var daThanhToanBatDau = userParams.DaThanhToanBatDau;
            var daThanhToanKetThuc = userParams.DaThanhToanKetThuc;

            var thoiGianTaoBatDau = userParams.ThoiGianTaoBatDau;
            var thoiGianTaoKetThuc = userParams.ThoiGianTaoKetThuc;
            var thoiGianCapNhatBatDau = userParams.ThoiGianCapNhatBatDau;
            var thoiGianCapNhatKetThuc = userParams.ThoiGianCapNhatKetThuc;
            var trangThai = userParams.TrangThai;
            var daXoa = userParams.DaXoa;
            
            // HoaDonDichVu
            if (!string.IsNullOrEmpty(soHDDV))
            {
                result = result.Where(x => x.SoHDDV.ToLower().Contains(soHDDV.ToLower()));
            }

            if (!string.IsNullOrEmpty(maKhachHang))
            {
                result = result.Where(x => x.MaKhachHang.ToLower().Contains(maKhachHang.ToLower()));
            }

            if (!string.IsNullOrEmpty(maNhanVien))
            {
                result = result.Where(x => x.MaNhanVien.ToLower().Contains(maNhanVien.ToLower()));
            }

            if (!string.IsNullOrEmpty(ghiChu))
            {
                result = result.Where(x => x.GhiChu.ToLower().Contains(ghiChu.ToLower()));
            }

            if (!string.IsNullOrEmpty(tinhTrang))
            {
                result = result.Where(x => x.TinhTrang.ToLower().Contains(tinhTrang.ToLower()));
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
            if (ngayLapBatDau.GetHashCode() != 0 && ngayLapKetThuc.GetHashCode() != 0)
            {
                result = result.Where(x => x.NgayLap >= ngayLapBatDau && x.NgayLap <= ngayLapKetThuc);
            }

            if (ngaySuDungBatDau.GetHashCode() != 0 && ngaySuDungKetThuc.GetHashCode() != 0)
            {
                result = result.Where(x => x.NgaySuDung >= ngaySuDungBatDau && x.NgaySuDung <= ngaySuDungKetThuc);
            }

            if (thanhTienBatDau.GetHashCode() != 0 && thanhTienKetThuc.GetHashCode() != 0)
            {
                result = result.Where(x => x.ThanhTien >= thanhTienBatDau && x.ThanhTien <= thanhTienKetThuc);
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

            return await PagedList<HoaDonDichVu>.CreateAsync(result, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<HoaDonDichVu> GetById(string id)
        {
            var result = await _context.DanhSachHoaDonDichVu.Include(x => x.ChiTietHDDV).Include(x => x.NhanVien).Include(x => x.KhachHang).FirstOrDefaultAsync(x => x.SoHDDV == id);
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
                result = result.Where(x => x.MaKhachHang.ToLower().Contains(keyword.ToLower()) || x.MaNhanVien.ToString().ToLower().Contains(keyword.ToLower()) || x.SoHDDV.ToString() == keyword);
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
                            result = result.OrderBy(x => x.MaNhanVien);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.MaNhanVien);
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

        public async Task<HoaDonDichVu> PermanentlyDeleteById(string id)
        {
            var hoaDonDichVuToDelete = await _context.DanhSachHoaDonDichVu.FirstOrDefaultAsync(x => x.SoHDDV == id);

            _context.DanhSachHoaDonDichVu.Remove(hoaDonDichVuToDelete);
            await _context.SaveChangesAsync();

            return hoaDonDichVuToDelete;
        }

        public async Task<HoaDonDichVu> RestoreById(string id)
        {
            var hoaDonDichVuToRestoreById = await _context.DanhSachHoaDonDichVu.FirstOrDefaultAsync(x => x.SoHDDV == id);

            hoaDonDichVuToRestoreById.DaXoa = 0;
            hoaDonDichVuToRestoreById.ThoiGianCapNhat = DateTime.Now;

            _context.DanhSachHoaDonDichVu.Update(hoaDonDichVuToRestoreById);
            await _context.SaveChangesAsync();
            return hoaDonDichVuToRestoreById;
        }

        public async Task<HoaDonDichVu> TemporarilyDeleteById(string id)
        {
            var hoaDonDichVuToTemporarilyDeleteById = await _context.DanhSachHoaDonDichVu.FirstOrDefaultAsync(x => x.SoHDDV == id);

            hoaDonDichVuToTemporarilyDeleteById.DaXoa = 1;
            hoaDonDichVuToTemporarilyDeleteById.ThoiGianCapNhat = DateTime.Now;

            _context.DanhSachHoaDonDichVu.Update(hoaDonDichVuToTemporarilyDeleteById);
            await _context.SaveChangesAsync();
            return hoaDonDichVuToTemporarilyDeleteById;
        }

        public async Task<HoaDonDichVu> UpdateById(string id, HoaDonDichVuForUpdateDto hoaDonDichVu)
        {
            var oldRecord = await _context.DanhSachHoaDonDichVu.AsNoTracking().FirstOrDefaultAsync(x => x.SoHDDV == id);
            var tinhTrang = "";

            if (hoaDonDichVu.DaThanhToan == 0)
            {
                tinhTrang = "Chưa thanh toán";
            }
            else if (hoaDonDichVu.DaThanhToan != hoaDonDichVu.ThanhTien && hoaDonDichVu.DaThanhToan > 0)
            {
                tinhTrang = "Đã thanh toán một phần";
            }
            else
            {
                tinhTrang = "Đã thanh toán";
            }

            var hoaDonDichVuToUpdate = new HoaDonDichVu
            {
                SoHDDV = id,
                MaKhachHang = hoaDonDichVu.MaKhachHang,
                MaNhanVien = hoaDonDichVu.MaNhanVien,
                NgayLap = hoaDonDichVu.NgayLap,
                NgaySuDung = hoaDonDichVu.NgaySuDung,
                ThanhTien = hoaDonDichVu.ThanhTien,
                DaThanhToan = hoaDonDichVu.DaThanhToan,
                TinhTrang = tinhTrang,
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
