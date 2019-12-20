using Microsoft.EntityFrameworkCore;
using MFFMS.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace MFFMS.API.Data
{
    public class DataContext : DbContext
    {
        public DbSet<TaiKhoan> DanhSachTaiKhoan { get; set; }
        public DbSet<KhachHang> DanhSachKhachHang { get; set; }
        public DbSet<NhanVien> DanhSachNhanVien { get; set; }
        public DbSet<SanBong> DanhSachSanBong { get; set; }
        public DbSet<PhieuDatSan> DanhSachPhieuDatSan { get; set; }
        public DbSet<ChiTietPhieuDatSan> DanhSachChiTietPhieuDatSan { get; set; }
        public DbSet<DichVu> DanhSachDichVu { get; set; }
        public DbSet<HoaDonDichVu> DanhSachHoaDonDichVu { get; set; }
        public DbSet<ChiTietHDDV> DanhSachChiTietHDDV { get; set; }
        public DbSet<NhaCungCap> DanhSachNhaCungCap { get; set; }
        public DbSet<TaiSanThietBi> DanhSachTaiSanThietBi { get; set; }
        public DbSet<DonNhapHang> DanhSachDonNhapHang { get; set; }
        public DbSet<ChiTietDonNhapHang> DanhSachChiTietDonNhapHang { get; set; }
        public DbSet<CaiDat> DanhSachCaiDat{ get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Keys
            modelBuilder.Entity<TaiKhoan>().HasKey(x => x.MaTaiKhoan);
            modelBuilder.Entity<KhachHang>().HasKey(x => x.MaKhachHang);
            modelBuilder.Entity<NhanVien>().HasKey(x => x.MaNhanVien);
            modelBuilder.Entity<SanBong>().HasKey(x => x.MaSanBong);
            modelBuilder.Entity<PhieuDatSan>().HasKey(x => x.MaPhieuDatSan);
            modelBuilder.Entity<ChiTietPhieuDatSan>().HasKey(x => x.MaChiTietPDS);
            modelBuilder.Entity<DichVu>().HasKey(x => x.MaDichVu);
            modelBuilder.Entity<HoaDonDichVu>().HasKey(x => x.SoHDDV);
            modelBuilder.Entity<ChiTietHDDV>().HasKey(x => new {x.SoHDDV,x.MaDichVu });
            modelBuilder.Entity<NhaCungCap>().HasKey(x => x.MaNhaCungCap);
            modelBuilder.Entity<TaiSanThietBi>().HasKey(x => x.MaTSTB);
            modelBuilder.Entity<DonNhapHang>().HasKey(x => x.MaDonNhapHang);
            modelBuilder.Entity<ChiTietDonNhapHang>().HasKey(x => new { x.MaDonNhapHang, x.MaTSTB });
            modelBuilder.Entity<CaiDat>().HasKey(x => x.MaCaiDat);

            // Requirements
            modelBuilder.Entity<KhachHang>().Property(x => x.MaKhachHang).HasMaxLength(200);
            modelBuilder.Entity<NhanVien>().Property(x => x.MaNhanVien).HasMaxLength(200);
            modelBuilder.Entity<SanBong>().Property(x => x.MaSanBong).HasMaxLength(200);
            modelBuilder.Entity<PhieuDatSan>().Property(x => x.MaPhieuDatSan).HasMaxLength(200);
            modelBuilder.Entity<ChiTietPhieuDatSan>().Property(x => x.MaChiTietPDS).HasMaxLength(200);
            modelBuilder.Entity<DichVu>().Property(x => x.MaDichVu).HasMaxLength(200);
            modelBuilder.Entity<HoaDonDichVu>().Property(x => x.SoHDDV).HasMaxLength(200);
            modelBuilder.Entity<ChiTietHDDV>().Property(x => x.SoHDDV).HasMaxLength(200);
            modelBuilder.Entity<ChiTietHDDV>().Property(x => x.MaDichVu).HasMaxLength(200);
            modelBuilder.Entity<NhaCungCap>().Property(x => x.MaNhaCungCap).HasMaxLength(200);
            modelBuilder.Entity<TaiSanThietBi>().Property(x => x.MaTSTB).HasMaxLength(200);
            modelBuilder.Entity<DonNhapHang>().Property(x => x.MaDonNhapHang).HasMaxLength(200);
            modelBuilder.Entity<ChiTietDonNhapHang>().Property(x => x.MaDonNhapHang).HasMaxLength(200);
            modelBuilder.Entity<ChiTietDonNhapHang>().Property(x => x.MaTSTB).HasMaxLength(200);


            modelBuilder.Entity<NhanVien>().HasIndex(x => x.SoDienThoai).IsUnique();
            modelBuilder.Entity<NhanVien>().Property(x => x.SoDienThoai).IsRequired();
            modelBuilder.Entity<NhanVien>().HasIndex(x => x.SoCMND).IsUnique();
            modelBuilder.Entity<NhanVien>().Property(x => x.SoCMND).IsRequired();
            modelBuilder.Entity<NhanVien>().Property(x => x.ChucVu).IsRequired();

            modelBuilder.Entity<SanBong>().HasIndex(x => x.TenSanBong).IsUnique();
            modelBuilder.Entity<SanBong>().Property(x => x.TenSanBong).IsRequired();
            modelBuilder.Entity<SanBong>().Property(x => x.ChieuDai).IsRequired();
            modelBuilder.Entity<SanBong>().Property(x => x.ChieuRong).IsRequired();

            modelBuilder.Entity<DichVu>().HasIndex(x => x.TenDichVu).IsUnique();
            modelBuilder.Entity<DichVu>().Property(x => x.TenDichVu).IsRequired();
            modelBuilder.Entity<DichVu>().Property(x => x.DonGia).IsRequired();

            modelBuilder.Entity<NhaCungCap>().HasIndex(x => x.TenNhaCungCap).IsUnique();
            modelBuilder.Entity<NhaCungCap>().Property(x => x.TenNhaCungCap).IsRequired();
            modelBuilder.Entity<NhaCungCap>().HasIndex(x => x.SoDienThoai).IsUnique();
            modelBuilder.Entity<NhaCungCap>().Property(x => x.SoDienThoai).IsRequired();

            modelBuilder.Entity<TaiSanThietBi>().HasIndex(x => x.TenTSTB).IsUnique();   
            modelBuilder.Entity<TaiSanThietBi>().Property(x => x.TenTSTB).IsRequired();

            modelBuilder.Entity<ChiTietPhieuDatSan>().Property(x => x.ThoiGianBatDau).IsRequired();
            modelBuilder.Entity<ChiTietPhieuDatSan>().Property(x => x.ThoiGianKetThuc).IsRequired();
            modelBuilder.Entity<ChiTietPhieuDatSan>().HasIndex(x => new { x.MaSanBong, x.ThoiGianBatDau }).IsUnique();
            //Relationships
            modelBuilder.Entity<HoaDonDichVu>()
                .HasOne(x => x.KhachHang)
                .WithMany(x => x.HoaDonDichVu)
                .IsRequired();
            modelBuilder.Entity<HoaDonDichVu>()
                .HasOne(x => x.NhanVien)
                .WithMany(x => x.HoaDonDichVu)
                .IsRequired();

            modelBuilder.Entity<ChiTietHDDV>()
                .HasOne(x => x.DichVu)
                .WithMany(x => x.ChiTietHDDV)
                .IsRequired();
            modelBuilder.Entity<ChiTietHDDV>()
                .HasOne(x => x.HoaDonDichVu)
                .WithMany(x => x.ChiTietHDDV)
                .IsRequired();

            modelBuilder.Entity<TaiSanThietBi>()
                .HasOne(x => x.NhaCungCap)
                .WithMany(x => x.TaiSanThietBi)
                .IsRequired();

            modelBuilder.Entity<DonNhapHang>()
                .HasOne(x => x.NhanVien)
                .WithMany(x => x.DonNhapHang)
                .IsRequired();
            modelBuilder.Entity<DonNhapHang>()
                .HasOne(x => x.NhaCungCap)
                .WithMany(x => x.DonNhapHang)
                .IsRequired();

            modelBuilder.Entity<ChiTietDonNhapHang>()
                .HasOne(x => x.DonNhapHang)
                .WithMany(x => x.ChiTietDonNhapHang)
                .IsRequired();
            modelBuilder.Entity<ChiTietDonNhapHang>()
                .HasOne(x => x.TaiSanThietBi)
                .WithMany(x => x.ChiTietDonNhapHang)
                .IsRequired();

            modelBuilder.Entity<PhieuDatSan>()
                .HasOne(x => x.KhachHang)
                .WithMany(x => x.PhieuDatSan)
                .IsRequired();
            modelBuilder.Entity<PhieuDatSan>()
                .HasOne(x => x.NhanVien)
                .WithMany(x => x.PhieuDatSan)
                .IsRequired();

            modelBuilder.Entity<ChiTietPhieuDatSan>()
                .HasOne(x => x.PhieuDatSan)
                .WithMany(x => x.ChiTietPhieuDatSan)
                .IsRequired();
            modelBuilder.Entity<ChiTietPhieuDatSan>()
                .HasOne(x => x.SanBong)
                .WithMany(x => x.ChiTietPhieuDatSan)
                .IsRequired();
        }
    }
}