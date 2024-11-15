using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLDT_WPF.Migrations.QuanLySinhVienDb
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChuongTrinhHoc",
                columns: table => new
                {
                    IdChuongTrinhHoc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValueSql: "(newid())"),
                    TenChuongTrinhHoc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChuongTrinhHoc", x => x.IdChuongTrinhHoc);
                });

            migrationBuilder.CreateTable(
                name: "Khoa",
                columns: table => new
                {
                    IdKhoa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TenKhoa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Khoa", x => x.IdKhoa);
                });

            migrationBuilder.CreateTable(
                name: "ThoiGian",
                columns: table => new
                {
                    IdThoiGian = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValueSql: "(newid())"),
                    NgayBatDau = table.Column<DateTime>(type: "datetime", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime", nullable: false),
                    DiaDiem = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThoiGian", x => x.IdThoiGian);
                });

            migrationBuilder.CreateTable(
                name: "GiaoVien",
                columns: table => new
                {
                    IdGiaoVien = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TenGiaoVien = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SoDienThoai = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    IdKhoa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaoVien", x => x.IdGiaoVien);
                    table.ForeignKey(
                        name: "FK_GiaoVien_Khoa_IdKhoa",
                        column: x => x.IdKhoa,
                        principalTable: "Khoa",
                        principalColumn: "IdKhoa");
                });

            migrationBuilder.CreateTable(
                name: "MonHoc",
                columns: table => new
                {
                    IdMonHoc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TenMonHoc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SoTinChi = table.Column<int>(type: "int", nullable: true),
                    SoTietHoc = table.Column<int>(type: "int", nullable: true),
                    IdKhoa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonHoc", x => x.IdMonHoc);
                    table.ForeignKey(
                        name: "FK_MonHoc_Khoa_IdKhoa",
                        column: x => x.IdKhoa,
                        principalTable: "Khoa",
                        principalColumn: "IdKhoa");
                });

            migrationBuilder.CreateTable(
                name: "SinhVien",
                columns: table => new
                {
                    IdSinhVien = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Lop = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "date", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IdChuongTrinhHoc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IdKhoa = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinhVien", x => x.IdSinhVien);
                    table.ForeignKey(
                        name: "FK_SinhVien_ChuongTrinhHoc_IdChuongTrinhHoc",
                        column: x => x.IdChuongTrinhHoc,
                        principalTable: "ChuongTrinhHoc",
                        principalColumn: "IdChuongTrinhHoc");
                    table.ForeignKey(
                        name: "FK_SinhVien_Khoa_IdKhoa",
                        column: x => x.IdKhoa,
                        principalTable: "Khoa",
                        principalColumn: "IdKhoa");
                });

            migrationBuilder.CreateTable(
                name: "DangKyDoiLich",
                columns: table => new
                {
                    IdDangKyDoiLich = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValueSql: "(newid())"),
                    IdThoiGian = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    ThoiGianBatDauHienTai = table.Column<DateTime>(type: "datetime", nullable: false),
                    ThoiGianKetThucHienTai = table.Column<DateTime>(type: "datetime", nullable: false),
                    ThoiGianBatDauMoi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThoiGianKetThucMoi = table.Column<DateTime>(type: "datetime", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false, defaultValue: -1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DangKyDoiLich", x => x.IdDangKyDoiLich);
                    table.ForeignKey(
                        name: "FK_DangKyDoiLich_ThoiGian_IdThoiGian",
                        column: x => x.IdThoiGian,
                        principalTable: "ThoiGian",
                        principalColumn: "IdThoiGian",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChuongTrinhHoc_MonHoc",
                columns: table => new
                {
                    IdCTHMonHoc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValueSql: "(newid())"),
                    IdChuongTrinhHoc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IdMonHoc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChuongTrinhHoc_MonHoc", x => x.IdCTHMonHoc);
                    table.ForeignKey(
                        name: "FK_ChuongTrinhHoc_MonHoc_ChuongTrinhHoc_IdChuongTrinhHoc",
                        column: x => x.IdChuongTrinhHoc,
                        principalTable: "ChuongTrinhHoc",
                        principalColumn: "IdChuongTrinhHoc");
                    table.ForeignKey(
                        name: "FK_ChuongTrinhHoc_MonHoc_MonHoc_IdMonHoc",
                        column: x => x.IdMonHoc,
                        principalTable: "MonHoc",
                        principalColumn: "IdMonHoc");
                });

            migrationBuilder.CreateTable(
                name: "LopHocPhan",
                columns: table => new
                {
                    IdLopHocPhan = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TenHocPhan = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IdGiaoVien = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IdMonHoc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ThoiGianBatDau = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ThoiGianKetThuc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LopHocPhan", x => x.IdLopHocPhan);
                    table.ForeignKey(
                        name: "FK_LopHocPhan_GiaoVien_IdGiaoVien",
                        column: x => x.IdGiaoVien,
                        principalTable: "GiaoVien",
                        principalColumn: "IdGiaoVien");
                    table.ForeignKey(
                        name: "FK_LopHocPhan_MonHoc_IdMonHoc",
                        column: x => x.IdMonHoc,
                        principalTable: "MonHoc",
                        principalColumn: "IdMonHoc");
                });

            migrationBuilder.CreateTable(
                name: "DangKyNguyenVong",
                columns: table => new
                {
                    IdDangKyNguyenVong = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValueSql: "(newid())"),
                    IdSinhVien = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IdMonHoc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false, defaultValue: -1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DangKyNguyenVong", x => x.IdDangKyNguyenVong);
                    table.ForeignKey(
                        name: "FK_DangKyNguyenVong_MonHoc_IdMonHoc",
                        column: x => x.IdMonHoc,
                        principalTable: "MonHoc",
                        principalColumn: "IdMonHoc",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DangKyNguyenVong_SinhVien_IdSinhVien",
                        column: x => x.IdSinhVien,
                        principalTable: "SinhVien",
                        principalColumn: "IdSinhVien",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Diem",
                columns: table => new
                {
                    IdDiem = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValueSql: "(newid())"),
                    IdLopHocPhan = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IdSinhVien = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DiemQuaTrinh = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    DiemKetThuc = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    DiemTongKet = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    LanHoc = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diem", x => x.IdDiem);
                    table.ForeignKey(
                        name: "FK_Diem_LopHocPhan_IdLopHocPhan",
                        column: x => x.IdLopHocPhan,
                        principalTable: "LopHocPhan",
                        principalColumn: "IdLopHocPhan");
                    table.ForeignKey(
                        name: "FK_Diem_SinhVien_IdSinhVien",
                        column: x => x.IdSinhVien,
                        principalTable: "SinhVien",
                        principalColumn: "IdSinhVien");
                });

            migrationBuilder.CreateTable(
                name: "SinhVien_LopHocPhan",
                columns: table => new
                {
                    IdSinhVienLopHP = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValueSql: "(newid())"),
                    IdSinhVien = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IdLopHocPhan = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinhVien_LopHocPhan", x => x.IdSinhVienLopHP);
                    table.ForeignKey(
                        name: "FK_SinhVien_LopHocPhan_LopHocPhan_IdLopHocPhan",
                        column: x => x.IdLopHocPhan,
                        principalTable: "LopHocPhan",
                        principalColumn: "IdLopHocPhan");
                    table.ForeignKey(
                        name: "FK_SinhVien_LopHocPhan_SinhVien_IdSinhVien",
                        column: x => x.IdSinhVien,
                        principalTable: "SinhVien",
                        principalColumn: "IdSinhVien");
                });

            migrationBuilder.CreateTable(
                name: "ThoiGian_LopHocPhan",
                columns: table => new
                {
                    IdThoigianLopHP = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValueSql: "(newid())"),
                    IdLopHocPhan = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IdThoiGian = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThoiGian_LopHocPhan", x => x.IdThoigianLopHP);
                    table.ForeignKey(
                        name: "FK_ThoiGian_LopHocPhan_LopHocPhan_IdLopHocPhan",
                        column: x => x.IdLopHocPhan,
                        principalTable: "LopHocPhan",
                        principalColumn: "IdLopHocPhan");
                    table.ForeignKey(
                        name: "FK_ThoiGian_LopHocPhan_ThoiGian_IdThoiGian",
                        column: x => x.IdThoiGian,
                        principalTable: "ThoiGian",
                        principalColumn: "IdThoiGian");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChuongTrinhHoc_MonHoc_IdChuongTrinhHoc",
                table: "ChuongTrinhHoc_MonHoc",
                column: "IdChuongTrinhHoc");

            migrationBuilder.CreateIndex(
                name: "IX_ChuongTrinhHoc_MonHoc_IdMonHoc",
                table: "ChuongTrinhHoc_MonHoc",
                column: "IdMonHoc");

            migrationBuilder.CreateIndex(
                name: "IX_DangKyDoiLich_IdThoiGian",
                table: "DangKyDoiLich",
                column: "IdThoiGian");

            migrationBuilder.CreateIndex(
                name: "IX_DangKyNguyenVong_IdMonHoc",
                table: "DangKyNguyenVong",
                column: "IdMonHoc");

            migrationBuilder.CreateIndex(
                name: "IX_DangKyNguyenVong_IdSinhVien",
                table: "DangKyNguyenVong",
                column: "IdSinhVien");

            migrationBuilder.CreateIndex(
                name: "IX_Diem_IdLopHocPhan",
                table: "Diem",
                column: "IdLopHocPhan");

            migrationBuilder.CreateIndex(
                name: "IX_Diem_IdSinhVien",
                table: "Diem",
                column: "IdSinhVien");

            migrationBuilder.CreateIndex(
                name: "IX_GiaoVien_IdKhoa",
                table: "GiaoVien",
                column: "IdKhoa");

            migrationBuilder.CreateIndex(
                name: "IX_LopHocPhan_IdGiaoVien",
                table: "LopHocPhan",
                column: "IdGiaoVien");

            migrationBuilder.CreateIndex(
                name: "IX_LopHocPhan_IdMonHoc",
                table: "LopHocPhan",
                column: "IdMonHoc");

            migrationBuilder.CreateIndex(
                name: "IX_MonHoc_IdKhoa",
                table: "MonHoc",
                column: "IdKhoa");

            migrationBuilder.CreateIndex(
                name: "IX_SinhVien_IdChuongTrinhHoc",
                table: "SinhVien",
                column: "IdChuongTrinhHoc");

            migrationBuilder.CreateIndex(
                name: "IX_SinhVien_IdKhoa",
                table: "SinhVien",
                column: "IdKhoa");

            migrationBuilder.CreateIndex(
                name: "IX_SinhVien_LopHocPhan_IdLopHocPhan",
                table: "SinhVien_LopHocPhan",
                column: "IdLopHocPhan");

            migrationBuilder.CreateIndex(
                name: "IX_SinhVien_LopHocPhan_IdSinhVien",
                table: "SinhVien_LopHocPhan",
                column: "IdSinhVien");

            migrationBuilder.CreateIndex(
                name: "IX_ThoiGian_LopHocPhan_IdLopHocPhan",
                table: "ThoiGian_LopHocPhan",
                column: "IdLopHocPhan");

            migrationBuilder.CreateIndex(
                name: "IX_ThoiGian_LopHocPhan_IdThoiGian",
                table: "ThoiGian_LopHocPhan",
                column: "IdThoiGian");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChuongTrinhHoc_MonHoc");

            migrationBuilder.DropTable(
                name: "DangKyDoiLich");

            migrationBuilder.DropTable(
                name: "DangKyNguyenVong");

            migrationBuilder.DropTable(
                name: "Diem");

            migrationBuilder.DropTable(
                name: "SinhVien_LopHocPhan");

            migrationBuilder.DropTable(
                name: "ThoiGian_LopHocPhan");

            migrationBuilder.DropTable(
                name: "SinhVien");

            migrationBuilder.DropTable(
                name: "LopHocPhan");

            migrationBuilder.DropTable(
                name: "ThoiGian");

            migrationBuilder.DropTable(
                name: "ChuongTrinhHoc");

            migrationBuilder.DropTable(
                name: "GiaoVien");

            migrationBuilder.DropTable(
                name: "MonHoc");

            migrationBuilder.DropTable(
                name: "Khoa");
        }
    }
}
