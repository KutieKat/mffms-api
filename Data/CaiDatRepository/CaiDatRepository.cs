using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MFFMS.API.Dtos;
using MFFMS.API.Dtos.CaiDatDto;
using MFFMS.API.Helpers;
using MFFMS.API.Helpers.Params;
using MFFMS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace MFFMS.API.Data.CaiDatRepository
{
    public class CaiDatRepository :ICaiDatRepository
    {
        private readonly DataContext _context;

        public CaiDatRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<CaiDat> GetAll()
        {
            var result = await _context.DanhSachCaiDat.FirstOrDefaultAsync();

            return result;
        }

        public async Task<CaiDat> UpdateById(int id, CaiDatForUpdateDto caiDat)
        {
            var oldRecord = await _context.DanhSachCaiDat.AsNoTracking().FirstOrDefaultAsync();

            var caiDatToUpdate = new CaiDat
            {
                MaCaiDat = id,
                TenSanBong = caiDat.TenSanBong,
                DiaChi = caiDat.DiaChi,
                SoDienThoai = caiDat.SoDienThoai,
                Fax = caiDat.Fax,
                DiaChiTrenPhieu = caiDat.DiaChiTrenPhieu,
                ThoiGianTao = oldRecord.ThoiGianTao,
                ThoiGianCapNhat = DateTime.Now,
                TrangThai = caiDat.TrangThai
            };

            _context.DanhSachCaiDat.Update(caiDatToUpdate);
            await _context.SaveChangesAsync();

            return caiDatToUpdate;
        }

        public async Task<CaiDat> Restore()
        {
            var caiDatToRestore = await _context.DanhSachCaiDat.FirstOrDefaultAsync();

            caiDatToRestore.TenSanBong = "Sân bóng mini ĐHTDTT";
            caiDatToRestore.DiaChi = "Khu phố 6, phường Linh Trung, quận Thủ Đức";
            caiDatToRestore.SoDienThoai = "0782 500 555";
            caiDatToRestore.Fax = "0274 3567 225";
            caiDatToRestore.DiaChiTrenPhieu = "Khu phố 6, phường Linh Trung, quận Thủ Đức";
            caiDatToRestore.ThoiGianCapNhat = DateTime.Now;

            _context.DanhSachCaiDat.Update(caiDatToRestore);
            await _context.SaveChangesAsync();

            return caiDatToRestore;
        }
            
    }
}