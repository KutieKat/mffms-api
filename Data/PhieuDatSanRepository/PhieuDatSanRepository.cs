using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MFFMS.API.Dtos.PhieuDatSanDto;
using MFFMS.API.Helpers;
using MFFMS.API.Helpers.Params;
using MFFMS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace MFFMS.API.Data.PhieuDatSanRepository
{
    public class PhieuDatSanRepository : IPhieuDatSanRepository
    {
        private readonly DataContext _context;
        private int _totalItems;
        private int _totalPages;

        public PhieuDatSanRepository(DataContext context)
        {
            _context = context;
            _totalItems = 0;
            _totalPages = 0;
        }

        public async Task<PhieuDatSan> Create(PhieuDatSanForCreateDto phieuDatSan)
        {
            var danhSachPhieuDatSan = await _context.DanhSachPhieuDatSan.OrderByDescending(x => x.MaPhieuDatSan).FirstOrDefaultAsync();
            var maPhieuDatSan = 1;
            if (danhSachPhieuDatSan == null)
            {
                maPhieuDatSan = 1;
            }
            else
            {
                maPhieuDatSan = danhSachPhieuDatSan.MaPhieuDatSan + 1;
            }

            var newPhieuDatSan = new PhieuDatSan
            {
                MaPhieuDatSan = maPhieuDatSan,
                MaKhachHang = phieuDatSan.MaKhachHang,
                MaNhanVien = phieuDatSan.MaNhanVien,
                NgayLap = DateTime.Now,
                TongTien = phieuDatSan.TongTien,
                ThoiGianCapNhat = DateTime.Now,
                ThoiGianTao = DateTime.Now,
                TrangThai = 0,
                DaXoa = 0
            };

            await _context.DanhSachPhieuDatSan.AddAsync(newPhieuDatSan);
            await _context.SaveChangesAsync();
            return newPhieuDatSan;
        }

       
        

        public async Task<PagedList<PhieuDatSan>> GetAll(PhieuDatSanParams userParams)
        {
            var result = _context.DanhSachPhieuDatSan.Include(x => x.KhachHang).Include(x => x.NhanVien).AsQueryable();
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
                result = result.Where(x => x.MaKhachHang.ToLower().Contains(keyword.ToLower()) || x.MaNhanVien.ToLower().Contains(keyword.ToLower())) ;
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
                    case "PhieuDatDatSan":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.MaPhieuDatSan);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.MaPhieuDatSan);
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

                    case "TongTien":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.TongTien);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.TongTien);
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

            return await PagedList<PhieuDatSan>.CreateAsync(result, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<PhieuDatSan> GetById(int id)
        {
            var result = await _context.DanhSachPhieuDatSan.Include(x => x.KhachHang).Include(x => x.NhanVien).FirstOrDefaultAsync(x => x.MaPhieuDatSan == id);
            return result;
        }

        public object GetStatusStatistics(PhieuDatSanParams userParams)
        {
            var result = _context.DanhSachPhieuDatSan.Include(x => x.KhachHang).Include(x => x.NhanVien).AsQueryable();
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
                result = result.Where(x => x.MaKhachHang.ToLower().Contains(keyword.ToLower()) || x.MaNhanVien.ToLower().Contains(keyword.ToLower()));
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
                    case "PhieuDatDatSan":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.MaPhieuDatSan);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.MaPhieuDatSan);
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

                    case "TongTien":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.TongTien);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.TongTien);
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

        public async Task<PhieuDatSan> PermanentlyDeleteById(int id)
        {
            var phieuDatSanToDelete = await _context.DanhSachPhieuDatSan.FirstOrDefaultAsync(x => x.MaPhieuDatSan == id);
            _context.DanhSachPhieuDatSan.Remove(phieuDatSanToDelete);
            await _context.SaveChangesAsync();
            return phieuDatSanToDelete;
        }

        public async Task<PhieuDatSan> RestoreById(int id)
        {
            var phieuDatSanToRestoreById = await _context.DanhSachPhieuDatSan.FirstOrDefaultAsync(x => x.MaPhieuDatSan == id);
            phieuDatSanToRestoreById.DaXoa = 0;
            phieuDatSanToRestoreById.ThoiGianCapNhat = DateTime.Now;
            _context.DanhSachPhieuDatSan.Update(phieuDatSanToRestoreById);
            await _context.SaveChangesAsync();
            return phieuDatSanToRestoreById;
        }

        public async Task<PhieuDatSan> TemporarilyDeleteById(int id)
        {
            var phieuDatSanToTemporarilyDeleteById = await _context.DanhSachPhieuDatSan.FirstOrDefaultAsync(x => x.MaPhieuDatSan == id);
            phieuDatSanToTemporarilyDeleteById.DaXoa = 1;
            phieuDatSanToTemporarilyDeleteById.ThoiGianCapNhat = DateTime.Now;
            _context.DanhSachPhieuDatSan.Update(phieuDatSanToTemporarilyDeleteById);
            await _context.SaveChangesAsync();
            return phieuDatSanToTemporarilyDeleteById;
        }

        public async Task<PhieuDatSan> UpdateById(int id, PhieuDatSanForUpdateDto phieuDatSan)
        {
            var oldRecord = await _context.DanhSachPhieuDatSan.AsNoTracking().FirstOrDefaultAsync(x => x.MaPhieuDatSan == id);
            var phieuDatSanToUpdateById = new PhieuDatSan
            {
                MaPhieuDatSan = id,
                MaKhachHang = phieuDatSan.MaKhachHang,
                MaNhanVien = phieuDatSan.MaNhanVien,
                NgayLap = oldRecord.NgayLap,
                TongTien = phieuDatSan.TongTien,
                TrangThai = phieuDatSan.TrangThai,
                ThoiGianTao = oldRecord.ThoiGianTao,
                DaXoa = oldRecord.DaXoa
            };

            _context.DanhSachPhieuDatSan.Update(phieuDatSanToUpdateById);
            await _context.SaveChangesAsync();
            return phieuDatSanToUpdateById;
        }
    }
}
