using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MFFMS.API.Dtos;
using MFFMS.API.Dtos.ChiTietPhieuDatSanDto;
using MFFMS.API.Helpers;
using MFFMS.API.Helpers.Params;
using MFFMS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace MFFMS.API.Data.ChiTieuPhieuDatSanRepository
{

    public class ChiTietPhieuDatSanRepository : IChiTietPhieuDatSanRepository
    {
        private readonly DataContext _context;
        private int _totalPages;
        private int _totalItems;

        public ChiTietPhieuDatSanRepository(DataContext context)
        {
            _context = context;
            _totalItems = 0;
            _totalPages = 0;
        }
        public async Task<ChiTietPhieuDatSan> Create(ChiTietPhieuDatSanForCreateDto chiTietPhieuDatSan)
        {
            var phieuDatSan = await _context.DanhSachPhieuDatSan.FirstOrDefaultAsync(x => x.MaPhieuDatSan == chiTietPhieuDatSan.MaPhieuDatSan);
            double daThanhToan = 0;

            var danhSachChiTietPhieuDatSan_PhieuDatSan =  _context.DanhSachChiTietPhieuDatSan.Where(x => x.MaPhieuDatSan == phieuDatSan.MaPhieuDatSan);

            foreach(var item in danhSachChiTietPhieuDatSan_PhieuDatSan)
            {
                daThanhToan =daThanhToan + item.TienCoc;
            }

            daThanhToan = daThanhToan + chiTietPhieuDatSan.TienCoc;
            if(daThanhToan == 0)
            {
                phieuDatSan.TrangThai = 0;
            }
            else if(daThanhToan < phieuDatSan.TongTien)
            {
                phieuDatSan.TrangThai = 1;
            }
            else if(daThanhToan >= phieuDatSan.TongTien)
            {
                phieuDatSan.TrangThai = 2;
            }

            _context.DanhSachPhieuDatSan.Update(phieuDatSan);
            await _context.SaveChangesAsync();
            

            var danhSachChiTietPhieuDatSan = await _context.DanhSachChiTietPhieuDatSan.OrderByDescending(x => x.MaChiTietPDS).FirstOrDefaultAsync();
            var maChiTietPhieuDatSan = 1;
            if(danhSachChiTietPhieuDatSan == null)
            {
                maChiTietPhieuDatSan = 1;
            }
            else
            {
                maChiTietPhieuDatSan = danhSachChiTietPhieuDatSan.MaChiTietPDS + 1;
            }

            var newChiTietPhieuDatSan = new ChiTietPhieuDatSan
            {
                MaChiTietPDS = maChiTietPhieuDatSan,
                MaPhieuDatSan = chiTietPhieuDatSan.MaPhieuDatSan,
                MaSanBong = chiTietPhieuDatSan.MaSanBong,
                ThoiGianBatDau = chiTietPhieuDatSan.ThoiGianBatDau,
                ThoiGianKetThuc = chiTietPhieuDatSan.ThoiGianKetThuc,
                NgayDat = chiTietPhieuDatSan.NgayDat,
                TienCoc = chiTietPhieuDatSan.TienCoc,
                ThanhTien = chiTietPhieuDatSan.ThanhTien,
                DaXoa = 0
            };

            await _context.DanhSachChiTietPhieuDatSan.AddAsync(newChiTietPhieuDatSan);

            
            await _context.SaveChangesAsync();
            return newChiTietPhieuDatSan;
        }

        public async Task<ICollection<ChiTietPhieuDatSan>> CreateMultiple(ICollection<ChiTietPhieuDatSanForCreateMultipleDto> danhSachChiTietPhieuDatSan)
        {
            var result = await _context.DanhSachChiTietPhieuDatSan.OrderByDescending(x => x.MaChiTietPDS).FirstOrDefaultAsync();
            var maChiTietPDS = 1;
            if(result == null)
            {
                maChiTietPDS = 1;
            }
            else
            {
                maChiTietPDS = result.MaChiTietPDS + 1;
            }

            ICollection<ChiTietPhieuDatSan> temp = new List<ChiTietPhieuDatSan>();
            for(int i = 0; i < danhSachChiTietPhieuDatSan.Count; i++)
            {
                var chiTietPDS = danhSachChiTietPhieuDatSan.ElementAt(i);

                var phieuDatSan = await _context.DanhSachPhieuDatSan.FirstOrDefaultAsync(x => x.MaPhieuDatSan == chiTietPDS.MaPhieuDatSan);
                double daThanhToan = 0;

                var danhSachChiTietPhieuDatSan_PhieuDatSan = _context.DanhSachChiTietPhieuDatSan.Where(x => x.MaPhieuDatSan == phieuDatSan.MaPhieuDatSan);
                foreach(var item in danhSachChiTietPhieuDatSan_PhieuDatSan)
                {
                    daThanhToan = daThanhToan + item.TienCoc;
                }

                daThanhToan = daThanhToan + chiTietPDS.TienCoc;

                if (daThanhToan == 0)
                {
                    phieuDatSan.TrangThai = 0;
                }
                else if (daThanhToan < phieuDatSan.TongTien)
                {
                    phieuDatSan.TrangThai = 1;
                }
                else if (daThanhToan >= phieuDatSan.TongTien)
                {
                    phieuDatSan.TrangThai = 2;
                }

                _context.DanhSachPhieuDatSan.Update(phieuDatSan);

                var newChiTietPhieuDatSan = new ChiTietPhieuDatSan
                {
                    MaChiTietPDS = maChiTietPDS,
                    MaPhieuDatSan = chiTietPDS.MaPhieuDatSan,
                    MaSanBong = chiTietPDS.MaSanBong,
                    ThoiGianBatDau = chiTietPDS.ThoiGianBatDau,
                    ThoiGianKetThuc = chiTietPDS.ThoiGianKetThuc,
                    NgayDat = chiTietPDS.NgayDat,
                    TienCoc = chiTietPDS.TienCoc,
                    ThanhTien = chiTietPDS.ThanhTien,
                    ThoiGianTao = DateTime.Now,
                    ThoiGianCapNhat = DateTime.Now

                };
                maChiTietPDS = maChiTietPDS + 1;
                temp.Add(newChiTietPhieuDatSan);
                await _context.DanhSachChiTietPhieuDatSan.AddAsync(newChiTietPhieuDatSan);


                await _context.SaveChangesAsync();

            }
            
            return temp;

        }

        public async Task<PagedList<ChiTietPhieuDatSan>> GetAll(ChiTietPhieuDatSanParams userParams)
        {
            var result = _context.DanhSachChiTietPhieuDatSan.Include(x => x.PhieuDatSan).Include(x => x.SanBong).AsQueryable();
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
                result = result.Where(x => x.MaPhieuDatSan.ToString().ToLower().Contains(keyword.ToLower()) || x.MaSanBong.ToString().ToLower().Contains(keyword.ToLower()) || x.MaChiTietPDS.ToString() == keyword);
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
                    case "MaChiTietPDS":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.MaChiTietPDS);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.MaChiTietPDS);
                        }
                        break;

                    case "MaPhieuDatSan":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.MaPhieuDatSan);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.MaPhieuDatSan);
                        }
                        break;

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

                    case "ThoiGianBatDau":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.ThoiGianBatDau);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.ThoiGianBatDau);
                        }
                        break;

                    case "ThoiGianKetThuc":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.ThoiGianBatDau);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.ThoiGianKetThuc);
                        }
                        break;

                    case "NgayDat":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.NgayDat);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.NgayDat);
                        }
                        break;

                    case "TienCoc":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.TienCoc);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.TienCoc);
                        }
                        break;

                    case "ThanhTien":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.ThanhTien);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.ThanhTien);
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

            return await PagedList<ChiTietPhieuDatSan>.CreateAsync(result, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<ChiTietPhieuDatSan> GetById(int id)
        {
            var result = await _context.DanhSachChiTietPhieuDatSan.Include(x=>x.PhieuDatSan).Include(x=>x.SanBong).FirstOrDefaultAsync(x => x.MaChiTietPDS == id);
            return result;
        }

        public object GetStatusStatistics(ChiTietPhieuDatSanParams userParams)
        {
            var result = _context.DanhSachChiTietPhieuDatSan.Include(x => x.PhieuDatSan).Include(x => x.SanBong).AsQueryable();
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
                result = result.Where(x => x.MaPhieuDatSan.ToString().ToLower().Contains(keyword.ToLower()) || x.MaSanBong.ToString().ToLower().Contains(keyword.ToLower()) || x.MaChiTietPDS.ToString() == keyword);
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
                    case "MaChiTietPDS":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.MaChiTietPDS);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.MaChiTietPDS);
                        }
                        break;

                    case "MaPhieuDatSan":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.MaPhieuDatSan);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.MaPhieuDatSan);
                        }
                        break;

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

                    case "ThoiGianBatDau":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.ThoiGianBatDau);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.ThoiGianBatDau);
                        }
                        break;

                    case "ThoiGianKetThuc":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.ThoiGianBatDau);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.ThoiGianKetThuc);
                        }
                        break;

                    case "NgayDat":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.NgayDat);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.NgayDat);
                        }
                        break;

                    case "TienCoc":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.TienCoc);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.TienCoc);
                        }
                        break;

                    case "ThanhTien":
                        if (string.Equals(sortOrder, "ASC", StringComparison.OrdinalIgnoreCase))
                        {
                            result = result.OrderBy(x => x.ThanhTien);
                        }
                        else
                        {
                            result = result.OrderByDescending(x => x.ThanhTien);
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

        public async Task<ChiTietPhieuDatSan> PermanentlyDeleteById(int id)
        {
            var chiTietPhieuDatSanToDelete = await _context.DanhSachChiTietPhieuDatSan.FirstOrDefaultAsync(x => x.MaChiTietPDS == id);
            _context.DanhSachChiTietPhieuDatSan.Remove(chiTietPhieuDatSanToDelete);
            await _context.SaveChangesAsync();
            return chiTietPhieuDatSanToDelete;
        }

        public async Task<ChiTietPhieuDatSan> RestoreById(int id)
        {
            var chiTietPhieuDatSanToRestoreById = await _context.DanhSachChiTietPhieuDatSan.FirstOrDefaultAsync(x => x.MaChiTietPDS == id);
            chiTietPhieuDatSanToRestoreById.DaXoa = 0;
            chiTietPhieuDatSanToRestoreById.ThoiGianCapNhat = DateTime.Now;
            _context.DanhSachChiTietPhieuDatSan.Update(chiTietPhieuDatSanToRestoreById);
            await _context.SaveChangesAsync();
            return chiTietPhieuDatSanToRestoreById;
        }

        public async Task<ChiTietPhieuDatSan> TemporarilyDeleteById(int id)
        {
            var chiTietPhieuDatSanToTemporarilyDeleteById = await _context.DanhSachChiTietPhieuDatSan.FirstOrDefaultAsync(x => x.MaChiTietPDS == id);
            chiTietPhieuDatSanToTemporarilyDeleteById.DaXoa = 1;
            chiTietPhieuDatSanToTemporarilyDeleteById.ThoiGianCapNhat = DateTime.Now;
            _context.DanhSachChiTietPhieuDatSan.Update(chiTietPhieuDatSanToTemporarilyDeleteById);
            await _context.SaveChangesAsync();
            return chiTietPhieuDatSanToTemporarilyDeleteById;
        }

        public async Task<ChiTietPhieuDatSan> UpdateById(int id, ChiTietPhieuDatSanForUpdateDto chiTietPhieuDatSan)
        {
            var phieuDatSan = await _context.DanhSachPhieuDatSan.FirstOrDefaultAsync(x => x.MaPhieuDatSan == chiTietPhieuDatSan.MaPhieuDatSan);
            double daThanhToan = 0;
            var danhSachChiTietPhieuDatSan = _context.DanhSachChiTietPhieuDatSan
                .Where(x => x.MaPhieuDatSan == phieuDatSan.MaPhieuDatSan && x.MaChiTietPDS != id);

            foreach(var item in danhSachChiTietPhieuDatSan)
            {
                daThanhToan = daThanhToan + item.TienCoc;
            }

            daThanhToan = daThanhToan + chiTietPhieuDatSan.TienCoc;

            if (daThanhToan == 0)
            {
                phieuDatSan.TrangThai = 0;
            }
            else if (daThanhToan < phieuDatSan.TongTien)
            {
                phieuDatSan.TrangThai = 1;
            }
            else if (daThanhToan >= phieuDatSan.TongTien)
            {
                phieuDatSan.TrangThai = 2;
            }

            _context.DanhSachPhieuDatSan.Update(phieuDatSan);
            await _context.SaveChangesAsync();


            var oldRecord = await _context.DanhSachChiTietPhieuDatSan.AsNoTracking().FirstOrDefaultAsync(x => x.MaChiTietPDS == id);
            var chiTietPhieuDatSanToUpdateById = new ChiTietPhieuDatSan
            {
                MaChiTietPDS = id,
                MaPhieuDatSan = chiTietPhieuDatSan.MaPhieuDatSan,
                MaSanBong = chiTietPhieuDatSan.MaSanBong,
                ThoiGianBatDau = chiTietPhieuDatSan.ThoiGianBatDau,
                ThoiGianKetThuc = chiTietPhieuDatSan.ThoiGianKetThuc,
                NgayDat = chiTietPhieuDatSan.NgayDat,
                TienCoc = chiTietPhieuDatSan.TienCoc,
                ThanhTien = chiTietPhieuDatSan.ThanhTien,
                ThoiGianTao = DateTime.Now,
                ThoiGianCapNhat = DateTime.Now
            };

            _context.DanhSachChiTietPhieuDatSan.Update(chiTietPhieuDatSanToUpdateById);
            await _context.SaveChangesAsync();
            return chiTietPhieuDatSanToUpdateById;

        }

        public ValidationResultDto ValidateBeforeCreate(ChiTietPhieuDatSanForCreateDto chiTietPhieuDatSan)
        {
            
            var total = _context.DanhSachChiTietPhieuDatSan.Count(x => x.MaSanBong == chiTietPhieuDatSan.MaSanBong && x.ThoiGianBatDau <= chiTietPhieuDatSan.ThoiGianBatDau && x.ThoiGianKetThuc >= chiTietPhieuDatSan.ThoiGianBatDau);
            IDictionary<string, string[]> Errors = new Dictionary<string, string[]>();
            if(total >= 1)
            {
                Errors.Add("maSanBong and thoiGianBatDau", new string[] { "maSanBong and thoiGianBatDau is duplicated!" });
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

        public ValidationResultDto ValidateBeforeCreateMultiple(ICollection<ChiTietPhieuDatSanForCreateMultipleDto> danhSachChiTietPhieuDatSan)
        {
            var isValid = true;
            for(int i = 0; i<danhSachChiTietPhieuDatSan.Count; i++)
            {
                var chiTietPhieuDatSan = danhSachChiTietPhieuDatSan.ElementAt(i);
                var chiTietPhieuDatSanToString = danhSachChiTietPhieuDatSan.ElementAt(i).ToString();
                var hasEnougnFields = chiTietPhieuDatSan.ToString().Contains("MaPhieuDatSan") && chiTietPhieuDatSan.ToString().Contains("MaSanBong")
                    && chiTietPhieuDatSan.ToString().Contains("ThoiGianBatDau")
                    && chiTietPhieuDatSan.ToString().Contains("ThoiGianKetThuc")
                    && chiTietPhieuDatSan.ToString().Contains("NgayDat")
                    && chiTietPhieuDatSan.ToString().Contains("TienCoc")
                    && chiTietPhieuDatSan.ToString().Contains("ThanhTien");

                var isOkayToCreate = true;
                var total = _context.DanhSachChiTietPhieuDatSan.Count(x => x.MaSanBong == chiTietPhieuDatSan.MaSanBong && x.ThoiGianBatDau <= chiTietPhieuDatSan.ThoiGianBatDau && x.ThoiGianKetThuc >= chiTietPhieuDatSan.ThoiGianBatDau);
                if(total >= 1)
                {
                    isOkayToCreate = false;
                }

                if(!hasEnougnFields || !isOkayToCreate)
                {
                    isValid = false;
                    break;
                }
            }

            if (isValid)
            {
                return new ValidationResultDto
                {
                    IsValid = true
                };
            }
            else
            {
                IDictionary<string, string[]> Errors = new Dictionary<string, string[]>();
                Errors.Add("createMultiple", new string[] { "createMultiple failed!" });

                return new ValidationResultDto
                {
                    IsValid = false,
                    Errors = Errors
                };
            }
        }

        public ValidationResultDto ValidateBeforeUpdate(int id, ChiTietPhieuDatSanForUpdateDto chiTietPhieuDatSan)
        {
            var total = _context.DanhSachChiTietPhieuDatSan.Count(x => x.MaChiTietPDS != id && x.MaSanBong == chiTietPhieuDatSan.MaSanBong && x.ThoiGianBatDau <= chiTietPhieuDatSan.ThoiGianBatDau && x.ThoiGianKetThuc >= chiTietPhieuDatSan.ThoiGianBatDau);
            IDictionary<string, string[]> Errors = new Dictionary<string, string[]>();
            if(total > 0)
            {
                Errors.Add("maSanBong and thoiGianBatDau", new string[] { "email is duplicated!" });
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
