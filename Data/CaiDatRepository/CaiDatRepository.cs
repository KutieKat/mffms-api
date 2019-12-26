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

        public async Task<CaiDat> UpdateById(string id, CaiDatForUpdateDto caiDat)
        {
            var oldRecord = await _context.DanhSachCaiDat.AsNoTracking().FirstOrDefaultAsync();
            var caiDatToUpdate = new CaiDat
            {
                MaCaiDat = oldRecord.MaCaiDat,
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

            caiDatToRestore.TenSanBong = "Tên sân bóng mini";
            caiDatToRestore.DiaChi = "Địa chỉ của sân bóng mini";
            caiDatToRestore.SoDienThoai = "0000000000";
            caiDatToRestore.Fax = "0000000000";
            caiDatToRestore.DiaChiTrenPhieu = "Địa chỉ được sử dụng trên các phiếu và báo biểu";
            caiDatToRestore.ThoiGianCapNhat = DateTime.Now;

            _context.DanhSachCaiDat.Update(caiDatToRestore);
            await _context.SaveChangesAsync();

            return caiDatToRestore;
        }

        private string GenerateId()
        {
            int count = _context.DanhSachCaiDat.Count() + 1;
            string tempId = count.ToString();
            string currentYear = DateTime.Now.ToString("yy");
 
            while (tempId.Length < 4)
            {
                tempId = "0" + tempId;
            }
 
            tempId = "CD" + currentYear + tempId;
 
            return tempId;
        }
            
    }
}
