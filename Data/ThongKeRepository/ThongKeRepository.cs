using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MFFMS.API.Helpers.Params;
using Microsoft.EntityFrameworkCore;

namespace MFFMS.API.Data.ThongKeRepository
{
    public class ThongKeRepository : IThongKeRepository
    {
        private readonly DataContext _context;

        public ThongKeRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<object> TongSoLuotDatSan(ThongKeParams userParams)
        {
            var result = _context.DanhSachPhieuDatSan.AsQueryable();
            var startingTime = userParams.StartingTime;
            var endingTime = userParams.EndingTime;

            if (startingTime.GetHashCode() != 0 && endingTime.GetHashCode() != 0)
            {
                result = result.Where(x => x.NgayLap >= startingTime && x.NgayLap <= endingTime);
            }

            var resultToReturn = result.Select(x => new
            {
                ThoiGian = x.NgayLap.Date,
                GiaTri = x.TongTien
            }).GroupBy(x => x.ThoiGian).Select(x => new {
                ThoiGian = x.Key,
                GiaTri = x.Count()
            });

            return new
            {
                Data = resultToReturn
            };
        }

        public async Task<object> TongTienDatSan(ThongKeParams userParams)
        {
            var result = _context.DanhSachPhieuDatSan.AsQueryable();
            var startingTime = userParams.StartingTime;
            var endingTime = userParams.EndingTime;

            if (startingTime.GetHashCode() != 0 && endingTime.GetHashCode() != 0)
            {
                result = result.Where(x => x.NgayLap >= startingTime && x.NgayLap <= endingTime);
            }

            var resultToReturn = result.Select(x => new
            {
                ThoiGian = x.NgayLap.Date,
                GiaTri = x.TongTien
            }).GroupBy(x => x.ThoiGian).Select(x => new {
                ThoiGian = x.Key,
                GiaTri = x.Sum(y => y.GiaTri)
            });

            return new
            {
                Data = resultToReturn
            };
        }

        public async Task<object> TongTienDichVu(ThongKeParams userParams)
        {
            var result = _context.DanhSachHoaDonDichVu.AsQueryable();
            var startingTime = userParams.StartingTime;
            var endingTime = userParams.EndingTime;

            if (startingTime.GetHashCode() != 0 && endingTime.GetHashCode() != 0)
            {
                result = result.Where(x => x.NgayLap >= startingTime && x.NgayLap <= endingTime);
            }

            var resultToReturn = result.Select(x => new
            {
                ThoiGian = x.NgayLap.Date,
                GiaTri = x.ThanhTien
            }).GroupBy(x => x.ThoiGian).Select(x => new {
                ThoiGian = x.Key,
                GiaTri = x.Sum(y => y.GiaTri)
            });

            return new
            {
                Data = resultToReturn
            };
        }

        public async Task<object> TongTienNhapHang(ThongKeParams userParams)
        {
            var result = _context.DanhSachDonNhapHang.AsQueryable();
            var startingTime = userParams.StartingTime;
            var endingTime = userParams.EndingTime;

            if (startingTime.GetHashCode() != 0 && endingTime.GetHashCode() != 0)
            {
                result = result.Where(x => x.NgayLap >= startingTime && x.NgayLap <= endingTime);
            }

            var resultToReturn = result.Select(x => new
            {
                ThoiGian = x.NgayLap.Date,
                GiaTri = x.ThanhTien
            }).GroupBy(x => x.ThoiGian).Select(x => new {
                ThoiGian = x.Key,
                GiaTri = x.Sum(y => y.GiaTri)
            });

            return new
            {
                Data = resultToReturn
            };
        }
    }
}
