using Microsoft.EntityFrameworkCore;
using MFFMS.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace MFFMS.API.Data
{
    public class DataContext : DbContext
    {
        public DbSet<TaiKhoan> DanhSachTaiKhoan { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaiKhoan>().HasKey(x => x.MaTaiKhoan);
        }
    }
}