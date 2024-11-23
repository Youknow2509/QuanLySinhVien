using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLDT_WPF.Migrations.QuanLySinhVienDb
{
    /// <inheritdoc />
    public partial class them_trang_thai_nhap_diem_lop_hoc_phan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "TrangThaiNhapDiem",
                table: "LopHocPhan",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrangThaiNhapDiem",
                table: "LopHocPhan");
        }
    }
}
