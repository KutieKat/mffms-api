using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MFFMS.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DanhSachCaiDat",
                columns: table => new
                {
                    MaCaiDat = table.Column<string>(nullable: false),
                    ThoiGianTao = table.Column<DateTime>(nullable: false),
                    ThoiGianCapNhat = table.Column<DateTime>(nullable: false),
                    TrangThai = table.Column<int>(nullable: false),
                    DaXoa = table.Column<int>(nullable: false),
                    TenSanBong = table.Column<string>(nullable: true),
                    DiaChi = table.Column<string>(nullable: true),
                    SoDienThoai = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    DiaChiTrenPhieu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhSachCaiDat", x => x.MaCaiDat);
                });

            migrationBuilder.CreateTable(
                name: "DanhSachDichVu",
                columns: table => new
                {
                    MaDichVu = table.Column<string>(maxLength: 200, nullable: false),
                    ThoiGianTao = table.Column<DateTime>(nullable: false),
                    ThoiGianCapNhat = table.Column<DateTime>(nullable: false),
                    TrangThai = table.Column<int>(nullable: false),
                    DaXoa = table.Column<int>(nullable: false),
                    TenDichVu = table.Column<string>(nullable: false),
                    DonGia = table.Column<double>(nullable: false),
                    DVT = table.Column<string>(nullable: true),
                    GhiChu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhSachDichVu", x => x.MaDichVu);
                });

            migrationBuilder.CreateTable(
                name: "DanhSachKhachHang",
                columns: table => new
                {
                    MaKhachHang = table.Column<string>(maxLength: 200, nullable: false),
                    ThoiGianTao = table.Column<DateTime>(nullable: false),
                    ThoiGianCapNhat = table.Column<DateTime>(nullable: false),
                    TrangThai = table.Column<int>(nullable: false),
                    DaXoa = table.Column<int>(nullable: false),
                    TenKhachHang = table.Column<string>(nullable: true),
                    GioiTinh = table.Column<string>(nullable: true),
                    NgaySinh = table.Column<DateTime>(nullable: false),
                    SoDienThoai = table.Column<string>(nullable: true),
                    DiaChi = table.Column<string>(nullable: true),
                    GhiChu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhSachKhachHang", x => x.MaKhachHang);
                });

            migrationBuilder.CreateTable(
                name: "DanhSachNhaCungCap",
                columns: table => new
                {
                    MaNhaCungCap = table.Column<string>(maxLength: 200, nullable: false),
                    ThoiGianTao = table.Column<DateTime>(nullable: false),
                    ThoiGianCapNhat = table.Column<DateTime>(nullable: false),
                    TrangThai = table.Column<int>(nullable: false),
                    DaXoa = table.Column<int>(nullable: false),
                    TenNhaCungCap = table.Column<string>(nullable: false),
                    SoDienThoai = table.Column<string>(nullable: false),
                    DiaChi = table.Column<string>(nullable: true),
                    GhiChu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhSachNhaCungCap", x => x.MaNhaCungCap);
                });

            migrationBuilder.CreateTable(
                name: "DanhSachNhanVien",
                columns: table => new
                {
                    MaNhanVien = table.Column<string>(maxLength: 200, nullable: false),
                    ThoiGianTao = table.Column<DateTime>(nullable: false),
                    ThoiGianCapNhat = table.Column<DateTime>(nullable: false),
                    TrangThai = table.Column<int>(nullable: false),
                    DaXoa = table.Column<int>(nullable: false),
                    TenNhanVien = table.Column<string>(nullable: true),
                    GioiTinh = table.Column<string>(nullable: true),
                    NgaySinh = table.Column<DateTime>(nullable: false),
                    ChucVu = table.Column<string>(nullable: false),
                    SoDienThoai = table.Column<string>(nullable: false),
                    SoCMND = table.Column<string>(nullable: false),
                    Luong = table.Column<double>(nullable: false),
                    GhiChu = table.Column<string>(nullable: true),
                    DiaChi = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhSachNhanVien", x => x.MaNhanVien);
                });

            migrationBuilder.CreateTable(
                name: "DanhSachSanBong",
                columns: table => new
                {
                    MaSanBong = table.Column<string>(maxLength: 200, nullable: false),
                    ThoiGianTao = table.Column<DateTime>(nullable: false),
                    ThoiGianCapNhat = table.Column<DateTime>(nullable: false),
                    TrangThai = table.Column<int>(nullable: false),
                    DaXoa = table.Column<int>(nullable: false),
                    TenSanBong = table.Column<string>(nullable: false),
                    ChieuDai = table.Column<double>(nullable: false),
                    ChieuRong = table.Column<double>(nullable: false),
                    DienTich = table.Column<double>(nullable: false),
                    GhiChu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhSachSanBong", x => x.MaSanBong);
                });

            migrationBuilder.CreateTable(
                name: "DanhSachTaiKhoan",
                columns: table => new
                {
                    MaTaiKhoan = table.Column<string>(nullable: false),
                    ThoiGianTao = table.Column<DateTime>(nullable: false),
                    ThoiGianCapNhat = table.Column<DateTime>(nullable: false),
                    TrangThai = table.Column<int>(nullable: false),
                    DaXoa = table.Column<int>(nullable: false),
                    TenDangNhap = table.Column<string>(nullable: true),
                    Hash = table.Column<byte[]>(nullable: true),
                    Salt = table.Column<byte[]>(nullable: true),
                    PhanQuyen = table.Column<string>(nullable: true),
                    HoVaTen = table.Column<string>(nullable: true),
                    GioiTinh = table.Column<string>(nullable: true),
                    NgaySinh = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    DiaChi = table.Column<string>(nullable: true),
                    SoDienThoai = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhSachTaiKhoan", x => x.MaTaiKhoan);
                });

            migrationBuilder.CreateTable(
                name: "DanhSachTaiSanThietBi",
                columns: table => new
                {
                    MaTSTB = table.Column<string>(maxLength: 200, nullable: false),
                    ThoiGianTao = table.Column<DateTime>(nullable: false),
                    ThoiGianCapNhat = table.Column<DateTime>(nullable: false),
                    TrangThai = table.Column<int>(nullable: false),
                    DaXoa = table.Column<int>(nullable: false),
                    MaNhaCungCap = table.Column<string>(nullable: false),
                    TenTSTB = table.Column<string>(nullable: false),
                    TinhTrang = table.Column<string>(nullable: true),
                    ThongTinBaoHanh = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhSachTaiSanThietBi", x => x.MaTSTB);
                    table.ForeignKey(
                        name: "FK_DanhSachTaiSanThietBi_DanhSachNhaCungCap_MaNhaCungCap",
                        column: x => x.MaNhaCungCap,
                        principalTable: "DanhSachNhaCungCap",
                        principalColumn: "MaNhaCungCap",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DanhSachDonNhapHang",
                columns: table => new
                {
                    MaDonNhapHang = table.Column<string>(maxLength: 200, nullable: false),
                    ThoiGianTao = table.Column<DateTime>(nullable: false),
                    ThoiGianCapNhat = table.Column<DateTime>(nullable: false),
                    TrangThai = table.Column<int>(nullable: false),
                    DaXoa = table.Column<int>(nullable: false),
                    MaNhaCungCap = table.Column<string>(nullable: false),
                    MaNhanVien = table.Column<string>(nullable: false),
                    NgayGiaoHang = table.Column<DateTime>(nullable: false),
                    GhiChu = table.Column<string>(nullable: true),
                    ThanhTien = table.Column<double>(nullable: false),
                    DaThanhToan = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhSachDonNhapHang", x => x.MaDonNhapHang);
                    table.ForeignKey(
                        name: "FK_DanhSachDonNhapHang_DanhSachNhaCungCap_MaNhaCungCap",
                        column: x => x.MaNhaCungCap,
                        principalTable: "DanhSachNhaCungCap",
                        principalColumn: "MaNhaCungCap",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DanhSachDonNhapHang_DanhSachNhanVien_MaNhanVien",
                        column: x => x.MaNhanVien,
                        principalTable: "DanhSachNhanVien",
                        principalColumn: "MaNhanVien",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DanhSachHoaDonDichVu",
                columns: table => new
                {
                    SoHDDV = table.Column<string>(maxLength: 200, nullable: false),
                    ThoiGianTao = table.Column<DateTime>(nullable: false),
                    ThoiGianCapNhat = table.Column<DateTime>(nullable: false),
                    TrangThai = table.Column<int>(nullable: false),
                    DaXoa = table.Column<int>(nullable: false),
                    MaKhachHang = table.Column<string>(nullable: false),
                    MaNhanVien = table.Column<string>(nullable: false),
                    NgaySuDung = table.Column<DateTime>(nullable: false),
                    NgayLap = table.Column<DateTime>(nullable: false),
                    GhiChu = table.Column<string>(nullable: true),
                    ThanhTien = table.Column<double>(nullable: false),
                    DaThanhToan = table.Column<double>(nullable: false),
                    DichVuMaDichVu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhSachHoaDonDichVu", x => x.SoHDDV);
                    table.ForeignKey(
                        name: "FK_DanhSachHoaDonDichVu_DanhSachDichVu_DichVuMaDichVu",
                        column: x => x.DichVuMaDichVu,
                        principalTable: "DanhSachDichVu",
                        principalColumn: "MaDichVu",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DanhSachHoaDonDichVu_DanhSachKhachHang_MaKhachHang",
                        column: x => x.MaKhachHang,
                        principalTable: "DanhSachKhachHang",
                        principalColumn: "MaKhachHang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DanhSachHoaDonDichVu_DanhSachNhanVien_MaNhanVien",
                        column: x => x.MaNhanVien,
                        principalTable: "DanhSachNhanVien",
                        principalColumn: "MaNhanVien",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DanhSachPhieuDatSan",
                columns: table => new
                {
                    MaPhieuDatSan = table.Column<string>(maxLength: 200, nullable: false),
                    ThoiGianTao = table.Column<DateTime>(nullable: false),
                    ThoiGianCapNhat = table.Column<DateTime>(nullable: false),
                    TrangThai = table.Column<int>(nullable: false),
                    DaXoa = table.Column<int>(nullable: false),
                    MaKhachHang = table.Column<string>(nullable: false),
                    MaNhanVien = table.Column<string>(nullable: false),
                    NgayLap = table.Column<DateTime>(nullable: false),
                    TongTien = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhSachPhieuDatSan", x => x.MaPhieuDatSan);
                    table.ForeignKey(
                        name: "FK_DanhSachPhieuDatSan_DanhSachKhachHang_MaKhachHang",
                        column: x => x.MaKhachHang,
                        principalTable: "DanhSachKhachHang",
                        principalColumn: "MaKhachHang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DanhSachPhieuDatSan_DanhSachNhanVien_MaNhanVien",
                        column: x => x.MaNhanVien,
                        principalTable: "DanhSachNhanVien",
                        principalColumn: "MaNhanVien",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DanhSachChiTietDonNhapHang",
                columns: table => new
                {
                    MaDonNhapHang = table.Column<string>(maxLength: 200, nullable: false),
                    MaTSTB = table.Column<string>(maxLength: 200, nullable: false),
                    ThoiGianTao = table.Column<DateTime>(nullable: false),
                    ThoiGianCapNhat = table.Column<DateTime>(nullable: false),
                    TrangThai = table.Column<int>(nullable: false),
                    DaXoa = table.Column<int>(nullable: false),
                    SoLuong = table.Column<int>(nullable: false),
                    DonGia = table.Column<double>(nullable: false),
                    DVT = table.Column<string>(nullable: true),
                    ThanhTien = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhSachChiTietDonNhapHang", x => new { x.MaDonNhapHang, x.MaTSTB });
                    table.ForeignKey(
                        name: "FK_DanhSachChiTietDonNhapHang_DanhSachDonNhapHang_MaDonNhapHang",
                        column: x => x.MaDonNhapHang,
                        principalTable: "DanhSachDonNhapHang",
                        principalColumn: "MaDonNhapHang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DanhSachChiTietDonNhapHang_DanhSachTaiSanThietBi_MaTSTB",
                        column: x => x.MaTSTB,
                        principalTable: "DanhSachTaiSanThietBi",
                        principalColumn: "MaTSTB",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DanhSachChiTietHDDV",
                columns: table => new
                {
                    SoHDDV = table.Column<string>(maxLength: 200, nullable: false),
                    MaDichVu = table.Column<string>(maxLength: 200, nullable: false),
                    ThoiGianTao = table.Column<DateTime>(nullable: false),
                    ThoiGianCapNhat = table.Column<DateTime>(nullable: false),
                    TrangThai = table.Column<int>(nullable: false),
                    DaXoa = table.Column<int>(nullable: false),
                    DonGia = table.Column<double>(nullable: false),
                    ThanhTien = table.Column<double>(nullable: false),
                    SoLuong = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhSachChiTietHDDV", x => new { x.SoHDDV, x.MaDichVu });
                    table.ForeignKey(
                        name: "FK_DanhSachChiTietHDDV_DanhSachDichVu_MaDichVu",
                        column: x => x.MaDichVu,
                        principalTable: "DanhSachDichVu",
                        principalColumn: "MaDichVu",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DanhSachChiTietHDDV_DanhSachHoaDonDichVu_SoHDDV",
                        column: x => x.SoHDDV,
                        principalTable: "DanhSachHoaDonDichVu",
                        principalColumn: "SoHDDV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DanhSachChiTietPhieuDatSan",
                columns: table => new
                {
                    MaChiTietPDS = table.Column<int>(maxLength: 200, nullable: false),
                    ThoiGianTao = table.Column<DateTime>(nullable: false),
                    ThoiGianCapNhat = table.Column<DateTime>(nullable: false),
                    TrangThai = table.Column<int>(nullable: false),
                    DaXoa = table.Column<int>(nullable: false),
                    MaPhieuDatSan = table.Column<string>(nullable: false),
                    MaSanBong = table.Column<string>(nullable: false),
                    ThoiGianBatDau = table.Column<double>(nullable: false),
                    ThoiGianKetThuc = table.Column<double>(nullable: false),
                    NgayDat = table.Column<DateTime>(nullable: false),
                    TienCoc = table.Column<double>(nullable: false),
                    ThanhTien = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhSachChiTietPhieuDatSan", x => x.MaChiTietPDS);
                    table.ForeignKey(
                        name: "FK_DanhSachChiTietPhieuDatSan_DanhSachPhieuDatSan_MaPhieuDatSan",
                        column: x => x.MaPhieuDatSan,
                        principalTable: "DanhSachPhieuDatSan",
                        principalColumn: "MaPhieuDatSan",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DanhSachChiTietPhieuDatSan_DanhSachSanBong_MaSanBong",
                        column: x => x.MaSanBong,
                        principalTable: "DanhSachSanBong",
                        principalColumn: "MaSanBong",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DanhSachChiTietDonNhapHang_MaTSTB",
                table: "DanhSachChiTietDonNhapHang",
                column: "MaTSTB");

            migrationBuilder.CreateIndex(
                name: "IX_DanhSachChiTietHDDV_MaDichVu",
                table: "DanhSachChiTietHDDV",
                column: "MaDichVu");

            migrationBuilder.CreateIndex(
                name: "IX_DanhSachChiTietPhieuDatSan_MaPhieuDatSan",
                table: "DanhSachChiTietPhieuDatSan",
                column: "MaPhieuDatSan");

            migrationBuilder.CreateIndex(
                name: "IX_DanhSachChiTietPhieuDatSan_MaSanBong_ThoiGianBatDau",
                table: "DanhSachChiTietPhieuDatSan",
                columns: new[] { "MaSanBong", "ThoiGianBatDau" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DanhSachDichVu_TenDichVu",
                table: "DanhSachDichVu",
                column: "TenDichVu",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DanhSachDonNhapHang_MaNhaCungCap",
                table: "DanhSachDonNhapHang",
                column: "MaNhaCungCap");

            migrationBuilder.CreateIndex(
                name: "IX_DanhSachDonNhapHang_MaNhanVien",
                table: "DanhSachDonNhapHang",
                column: "MaNhanVien");

            migrationBuilder.CreateIndex(
                name: "IX_DanhSachHoaDonDichVu_DichVuMaDichVu",
                table: "DanhSachHoaDonDichVu",
                column: "DichVuMaDichVu");

            migrationBuilder.CreateIndex(
                name: "IX_DanhSachHoaDonDichVu_MaKhachHang",
                table: "DanhSachHoaDonDichVu",
                column: "MaKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_DanhSachHoaDonDichVu_MaNhanVien",
                table: "DanhSachHoaDonDichVu",
                column: "MaNhanVien");

            migrationBuilder.CreateIndex(
                name: "IX_DanhSachNhaCungCap_SoDienThoai",
                table: "DanhSachNhaCungCap",
                column: "SoDienThoai",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DanhSachNhaCungCap_TenNhaCungCap",
                table: "DanhSachNhaCungCap",
                column: "TenNhaCungCap",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DanhSachNhanVien_SoCMND",
                table: "DanhSachNhanVien",
                column: "SoCMND",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DanhSachNhanVien_SoDienThoai",
                table: "DanhSachNhanVien",
                column: "SoDienThoai",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DanhSachPhieuDatSan_MaKhachHang",
                table: "DanhSachPhieuDatSan",
                column: "MaKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_DanhSachPhieuDatSan_MaNhanVien",
                table: "DanhSachPhieuDatSan",
                column: "MaNhanVien");

            migrationBuilder.CreateIndex(
                name: "IX_DanhSachSanBong_TenSanBong",
                table: "DanhSachSanBong",
                column: "TenSanBong",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DanhSachTaiSanThietBi_MaNhaCungCap",
                table: "DanhSachTaiSanThietBi",
                column: "MaNhaCungCap");

            migrationBuilder.CreateIndex(
                name: "IX_DanhSachTaiSanThietBi_TenTSTB",
                table: "DanhSachTaiSanThietBi",
                column: "TenTSTB",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DanhSachCaiDat");

            migrationBuilder.DropTable(
                name: "DanhSachChiTietDonNhapHang");

            migrationBuilder.DropTable(
                name: "DanhSachChiTietHDDV");

            migrationBuilder.DropTable(
                name: "DanhSachChiTietPhieuDatSan");

            migrationBuilder.DropTable(
                name: "DanhSachTaiKhoan");

            migrationBuilder.DropTable(
                name: "DanhSachDonNhapHang");

            migrationBuilder.DropTable(
                name: "DanhSachTaiSanThietBi");

            migrationBuilder.DropTable(
                name: "DanhSachHoaDonDichVu");

            migrationBuilder.DropTable(
                name: "DanhSachPhieuDatSan");

            migrationBuilder.DropTable(
                name: "DanhSachSanBong");

            migrationBuilder.DropTable(
                name: "DanhSachNhaCungCap");

            migrationBuilder.DropTable(
                name: "DanhSachDichVu");

            migrationBuilder.DropTable(
                name: "DanhSachKhachHang");

            migrationBuilder.DropTable(
                name: "DanhSachNhanVien");
        }
    }
}
