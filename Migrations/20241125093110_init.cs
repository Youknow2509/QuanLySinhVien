using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_qlsv.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "VINH");

            migrationBuilder.CreateTable(
                name: "CHUONGTRINHHOC",
                schema: "VINH",
                columns: table => new
                {
                    IDCHUONGTRINHHOC = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
                    TENCHUONGTRINHHOC = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CHUONGTRINHHOC", x => x.IDCHUONGTRINHHOC);
                });

            migrationBuilder.CreateTable(
                name: "KHOA",
                schema: "VINH",
                columns: table => new
                {
                    IDKHOA = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
                    TENKHOA = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KHOA", x => x.IDKHOA);
                });

            migrationBuilder.CreateTable(
                name: "PHONGHOC",
                schema: "VINH",
                columns: table => new
                {
                    IDPHONGHOC = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
                    TENPHONGHOC = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
                    DIACHI = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PHONGHOC", x => x.IDPHONGHOC);
                });

            migrationBuilder.CreateTable(
                name: "GIAOVIEN",
                schema: "VINH",
                columns: table => new
                {
                    IDGIAOVIEN = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
                    TENGIAOVIEN = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
                    EMAIL = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: true),
                    SODIENTHOAI = table.Column<string>(type: "VARCHAR2(15)", unicode: false, maxLength: 15, nullable: true),
                    IDKHOA = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GIAOVIEN", x => x.IDGIAOVIEN);
                    table.ForeignKey(
                        name: "FK_GIAOVIEN_KHOA_IDKHOA",
                        column: x => x.IDKHOA,
                        principalSchema: "VINH",
                        principalTable: "KHOA",
                        principalColumn: "IDKHOA");
                });

            migrationBuilder.CreateTable(
                name: "MONHOC",
                schema: "VINH",
                columns: table => new
                {
                    IDMONHOC = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
                    TENMONHOC = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
                    IDKHOA = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MONHOC", x => x.IDMONHOC);
                    table.ForeignKey(
                        name: "FK_MONHOC_KHOA_IDKHOA",
                        column: x => x.IDKHOA,
                        principalSchema: "VINH",
                        principalTable: "KHOA",
                        principalColumn: "IDKHOA");
                });

            migrationBuilder.CreateTable(
                name: "SINHVIEN",
                schema: "VINH",
                columns: table => new
                {
                    IDSINHVIEN = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
                    HOTEN = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
                    LOP = table.Column<string>(type: "VARCHAR2(50)", unicode: false, maxLength: 50, nullable: false),
                    NGAYSINH = table.Column<DateTime>(type: "DATE", nullable: false),
                    DIACHI = table.Column<string>(type: "VARCHAR2(255)", unicode: false, maxLength: 255, nullable: true),
                    IDCHUONGTRINHHOC = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
                    IDKHOA = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SINHVIEN", x => x.IDSINHVIEN);
                    table.ForeignKey(
                        name: "FK_SINHVIEN_CHUONGTRINHHOC_IDCHUONGTRINHHOC",
                        column: x => x.IDCHUONGTRINHHOC,
                        principalSchema: "VINH",
                        principalTable: "CHUONGTRINHHOC",
                        principalColumn: "IDCHUONGTRINHHOC");
                    table.ForeignKey(
                        name: "FK_SINHVIEN_KHOA_IDKHOA",
                        column: x => x.IDKHOA,
                        principalSchema: "VINH",
                        principalTable: "KHOA",
                        principalColumn: "IDKHOA");
                });

            migrationBuilder.CreateTable(
                name: "THOIGIAN",
                schema: "VINH",
                columns: table => new
                {
                    IDTHOIGIAN = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
                    NGAYBATDAU = table.Column<DateTime>(type: "DATE", nullable: false),
                    NGAYKETTHUC = table.Column<DateTime>(type: "DATE", nullable: false),
                    IDPHONGHOC = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_THOIGIAN", x => x.IDTHOIGIAN);
                    table.ForeignKey(
                        name: "FK_THOIGIAN_PHONGHOC_IDPHONGHOC",
                        column: x => x.IDPHONGHOC,
                        principalSchema: "VINH",
                        principalTable: "PHONGHOC",
                        principalColumn: "IDPHONGHOC");
                });

            migrationBuilder.CreateTable(
                name: "CHUONGTRINHHOC_MONHOC",
                schema: "VINH",
                columns: table => new
                {
                    IDCHUONGTRINHHOCMONHOC = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
                    IDCHUONGTRINHHOC = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
                    IDMONHOC = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CHUONGTRINHHOC_MONHOC", x => x.IDCHUONGTRINHHOCMONHOC);
                    table.ForeignKey(
                        name: "FK_CHUONGTRINHHOC_MONHOC_CHUONGTRINHHOC_IDCHUONGTRINHHOC",
                        column: x => x.IDCHUONGTRINHHOC,
                        principalSchema: "VINH",
                        principalTable: "CHUONGTRINHHOC",
                        principalColumn: "IDCHUONGTRINHHOC");
                    table.ForeignKey(
                        name: "FK_CHUONGTRINHHOC_MONHOC_MONHOC_IDMONHOC",
                        column: x => x.IDMONHOC,
                        principalSchema: "VINH",
                        principalTable: "MONHOC",
                        principalColumn: "IDMONHOC");
                });

            migrationBuilder.CreateTable(
                name: "LOPHOCPHAN",
                schema: "VINH",
                columns: table => new
                {
                    IDLOPHOCPHAN = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
                    TENHOCPHAN = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
                    IDGIAOVIEN = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
                    IDMONHOC = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
                    SOTINCHI = table.Column<decimal>(type: "NUMBER(38)", nullable: false),
                    SOTIETHOC = table.Column<decimal>(type: "NUMBER(38)", nullable: false),
                    THOIGIANBATDAU = table.Column<DateTime>(type: "DATE", nullable: false),
                    THOIGIANKETTHUC = table.Column<DateTime>(type: "DATE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOPHOCPHAN", x => x.IDLOPHOCPHAN);
                    table.ForeignKey(
                        name: "FK_LOPHOCPHAN_GIAOVIEN_IDGIAOVIEN",
                        column: x => x.IDGIAOVIEN,
                        principalSchema: "VINH",
                        principalTable: "GIAOVIEN",
                        principalColumn: "IDGIAOVIEN");
                    table.ForeignKey(
                        name: "FK_LOPHOCPHAN_MONHOC_IDMONHOC",
                        column: x => x.IDMONHOC,
                        principalSchema: "VINH",
                        principalTable: "MONHOC",
                        principalColumn: "IDMONHOC");
                });

            migrationBuilder.CreateTable(
                name: "DANGKYNGUYENVONG",
                schema: "VINH",
                columns: table => new
                {
                    IDDANGKYNGUYENVONG = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
                    IDSINHVIEN = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
                    IDMONHOC = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
                    TRANGTHAI = table.Column<decimal>(type: "NUMBER(38)", nullable: false, defaultValueSql: "-1 ")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DANGKYNGUYENVONG", x => x.IDDANGKYNGUYENVONG);
                    table.ForeignKey(
                        name: "FK_DANGKYNGUYENVONG_MONHOC_IDMONHOC",
                        column: x => x.IDMONHOC,
                        principalSchema: "VINH",
                        principalTable: "MONHOC",
                        principalColumn: "IDMONHOC",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DANGKYNGUYENVONG_SINHVIEN_IDSINHVIEN",
                        column: x => x.IDSINHVIEN,
                        principalSchema: "VINH",
                        principalTable: "SINHVIEN",
                        principalColumn: "IDSINHVIEN",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DANGKYDOILICH",
                schema: "VINH",
                columns: table => new
                {
                    IDDANGKYDOILICH = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
                    IDTHOIGIAN = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
                    THOIGIANBATDAUHIENTAI = table.Column<DateTime>(type: "DATE", nullable: false),
                    THOIGIANKETTHUCHIENTAI = table.Column<DateTime>(type: "DATE", nullable: false),
                    THOIGIANBATDAUMOI = table.Column<DateTime>(type: "DATE", nullable: false),
                    THOIGIANKETTHUCMOI = table.Column<DateTime>(type: "DATE", nullable: false),
                    TRANGTHAI = table.Column<decimal>(type: "NUMBER(38)", nullable: false, defaultValueSql: "-1 ")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DANGKYDOILICH", x => x.IDDANGKYDOILICH);
                    table.ForeignKey(
                        name: "FK_DANGKYDOILICH_THOIGIAN_IDTHOIGIAN",
                        column: x => x.IDTHOIGIAN,
                        principalSchema: "VINH",
                        principalTable: "THOIGIAN",
                        principalColumn: "IDTHOIGIAN",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DIEM",
                schema: "VINH",
                columns: table => new
                {
                    IDDIEM = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
                    IDLOPHOCPHAN = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
                    IDSINHVIEN = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
                    DIEMQUATRINH = table.Column<decimal>(type: "NUMBER(5,2)", nullable: false),
                    DIEMKETTHUC = table.Column<decimal>(type: "NUMBER(5,2)", nullable: false),
                    DIEMTONGKET = table.Column<decimal>(type: "NUMBER(5,2)", nullable: false),
                    LANHOC = table.Column<decimal>(type: "NUMBER(38)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DIEM", x => x.IDDIEM);
                    table.ForeignKey(
                        name: "FK_DIEM_LOPHOCPHAN_IDLOPHOCPHAN",
                        column: x => x.IDLOPHOCPHAN,
                        principalSchema: "VINH",
                        principalTable: "LOPHOCPHAN",
                        principalColumn: "IDLOPHOCPHAN");
                    table.ForeignKey(
                        name: "FK_DIEM_SINHVIEN_IDSINHVIEN",
                        column: x => x.IDSINHVIEN,
                        principalSchema: "VINH",
                        principalTable: "SINHVIEN",
                        principalColumn: "IDSINHVIEN");
                });

            migrationBuilder.CreateTable(
                name: "SINHVIEN_LOPHOCPHAN",
                schema: "VINH",
                columns: table => new
                {
                    IDSINHVIENLOPHOCPHAN = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
                    IDSINHVIEN = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
                    IDLOPHOCPHAN = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SINHVIEN_LOPHOCPHAN", x => x.IDSINHVIENLOPHOCPHAN);
                    table.ForeignKey(
                        name: "FK_SINHVIEN_LOPHOCPHAN_LOPHOCPHAN_IDLOPHOCPHAN",
                        column: x => x.IDLOPHOCPHAN,
                        principalSchema: "VINH",
                        principalTable: "LOPHOCPHAN",
                        principalColumn: "IDLOPHOCPHAN");
                    table.ForeignKey(
                        name: "FK_SINHVIEN_LOPHOCPHAN_SINHVIEN_IDSINHVIEN",
                        column: x => x.IDSINHVIEN,
                        principalSchema: "VINH",
                        principalTable: "SINHVIEN",
                        principalColumn: "IDSINHVIEN");
                });

            migrationBuilder.CreateTable(
                name: "THOIGIAN_LOPHOCPHAN",
                schema: "VINH",
                columns: table => new
                {
                    IDTHOIGIANLOPHOCPHAN = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
                    IDLOPHOCPHAN = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false),
                    IDTHOIGIAN = table.Column<string>(type: "VARCHAR2(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_THOIGIAN_LOPHOCPHAN", x => x.IDTHOIGIANLOPHOCPHAN);
                    table.ForeignKey(
                        name: "FK_THOIGIAN_LOPHOCPHAN_LOPHOCPHAN_IDLOPHOCPHAN",
                        column: x => x.IDLOPHOCPHAN,
                        principalSchema: "VINH",
                        principalTable: "LOPHOCPHAN",
                        principalColumn: "IDLOPHOCPHAN");
                    table.ForeignKey(
                        name: "FK_THOIGIAN_LOPHOCPHAN_THOIGIAN_IDTHOIGIAN",
                        column: x => x.IDTHOIGIAN,
                        principalSchema: "VINH",
                        principalTable: "THOIGIAN",
                        principalColumn: "IDTHOIGIAN");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CHUONGTRINHHOC_TENCHUONGTRINHHOC",
                schema: "VINH",
                table: "CHUONGTRINHHOC",
                column: "TENCHUONGTRINHHOC");

            migrationBuilder.CreateIndex(
                name: "IX_CHUONGTRINHHOC_MONHOC_IDCHUONGTRINHHOC",
                schema: "VINH",
                table: "CHUONGTRINHHOC_MONHOC",
                column: "IDCHUONGTRINHHOC");

            migrationBuilder.CreateIndex(
                name: "IX_CHUONGTRINHHOC_MONHOC_IDMONHOC",
                schema: "VINH",
                table: "CHUONGTRINHHOC_MONHOC",
                column: "IDMONHOC");

            migrationBuilder.CreateIndex(
                name: "IX_DANGKYDOILICH_IDTHOIGIAN",
                schema: "VINH",
                table: "DANGKYDOILICH",
                column: "IDTHOIGIAN");

            migrationBuilder.CreateIndex(
                name: "IX_DANGKYDOILICH_THOIGIANBATDAUHIENTAI",
                schema: "VINH",
                table: "DANGKYDOILICH",
                column: "THOIGIANBATDAUHIENTAI");

            migrationBuilder.CreateIndex(
                name: "IX_DANGKYDOILICH_THOIGIANBATDAUMOI",
                schema: "VINH",
                table: "DANGKYDOILICH",
                column: "THOIGIANBATDAUMOI");

            migrationBuilder.CreateIndex(
                name: "IX_DANGKYDOILICH_THOIGIANKETTHUCHIENTAI",
                schema: "VINH",
                table: "DANGKYDOILICH",
                column: "THOIGIANKETTHUCHIENTAI");

            migrationBuilder.CreateIndex(
                name: "IX_DANGKYDOILICH_THOIGIANKETTHUCMOI",
                schema: "VINH",
                table: "DANGKYDOILICH",
                column: "THOIGIANKETTHUCMOI");

            migrationBuilder.CreateIndex(
                name: "IX_DANGKYDOILICH_TRANGTHAI",
                schema: "VINH",
                table: "DANGKYDOILICH",
                column: "TRANGTHAI");

            migrationBuilder.CreateIndex(
                name: "IX_DANGKYNGUYENVONG_IDMONHOC",
                schema: "VINH",
                table: "DANGKYNGUYENVONG",
                column: "IDMONHOC");

            migrationBuilder.CreateIndex(
                name: "IX_DANGKYNGUYENVONG_IDSINHVIEN",
                schema: "VINH",
                table: "DANGKYNGUYENVONG",
                column: "IDSINHVIEN");

            migrationBuilder.CreateIndex(
                name: "IX_DANGKYNGUYENVONG_TRANGTHAI",
                schema: "VINH",
                table: "DANGKYNGUYENVONG",
                column: "TRANGTHAI");

            migrationBuilder.CreateIndex(
                name: "IX_DIEM_DIEMKETTHUC",
                schema: "VINH",
                table: "DIEM",
                column: "DIEMKETTHUC");

            migrationBuilder.CreateIndex(
                name: "IX_DIEM_DIEMQUATRINH",
                schema: "VINH",
                table: "DIEM",
                column: "DIEMQUATRINH");

            migrationBuilder.CreateIndex(
                name: "IX_DIEM_DIEMTONGKET",
                schema: "VINH",
                table: "DIEM",
                column: "DIEMTONGKET");

            migrationBuilder.CreateIndex(
                name: "IX_DIEM_IDLOPHOCPHAN",
                schema: "VINH",
                table: "DIEM",
                column: "IDLOPHOCPHAN");

            migrationBuilder.CreateIndex(
                name: "IX_DIEM_IDSINHVIEN",
                schema: "VINH",
                table: "DIEM",
                column: "IDSINHVIEN");

            migrationBuilder.CreateIndex(
                name: "IX_DIEM_LANHOC",
                schema: "VINH",
                table: "DIEM",
                column: "LANHOC");

            migrationBuilder.CreateIndex(
                name: "IX_GIAOVIEN_EMAIL",
                schema: "VINH",
                table: "GIAOVIEN",
                column: "EMAIL");

            migrationBuilder.CreateIndex(
                name: "IX_GIAOVIEN_IDKHOA",
                schema: "VINH",
                table: "GIAOVIEN",
                column: "IDKHOA");

            migrationBuilder.CreateIndex(
                name: "IX_GIAOVIEN_SODIENTHOAI",
                schema: "VINH",
                table: "GIAOVIEN",
                column: "SODIENTHOAI");

            migrationBuilder.CreateIndex(
                name: "IX_GIAOVIEN_TENGIAOVIEN",
                schema: "VINH",
                table: "GIAOVIEN",
                column: "TENGIAOVIEN");

            migrationBuilder.CreateIndex(
                name: "IX_KHOA_TENKHOA",
                schema: "VINH",
                table: "KHOA",
                column: "TENKHOA");

            migrationBuilder.CreateIndex(
                name: "IX_LOPHOCPHAN_IDGIAOVIEN",
                schema: "VINH",
                table: "LOPHOCPHAN",
                column: "IDGIAOVIEN");

            migrationBuilder.CreateIndex(
                name: "IX_LOPHOCPHAN_IDMONHOC",
                schema: "VINH",
                table: "LOPHOCPHAN",
                column: "IDMONHOC");

            migrationBuilder.CreateIndex(
                name: "IX_LOPHOCPHAN_TENHOCPHAN",
                schema: "VINH",
                table: "LOPHOCPHAN",
                column: "TENHOCPHAN");

            migrationBuilder.CreateIndex(
                name: "IX_LOPHOCPHAN_THOIGIANBATDAU",
                schema: "VINH",
                table: "LOPHOCPHAN",
                column: "THOIGIANBATDAU");

            migrationBuilder.CreateIndex(
                name: "IX_LOPHOCPHAN_THOIGIANKETTHUC",
                schema: "VINH",
                table: "LOPHOCPHAN",
                column: "THOIGIANKETTHUC");

            migrationBuilder.CreateIndex(
                name: "IX_MONHOC_IDKHOA",
                schema: "VINH",
                table: "MONHOC",
                column: "IDKHOA");

            migrationBuilder.CreateIndex(
                name: "IX_MONHOC_TENMONHOC",
                schema: "VINH",
                table: "MONHOC",
                column: "TENMONHOC");

            migrationBuilder.CreateIndex(
                name: "IX_PHONGHOC_DIACHI",
                schema: "VINH",
                table: "PHONGHOC",
                column: "DIACHI");

            migrationBuilder.CreateIndex(
                name: "IX_PHONGHOC_TENPHONGHOC",
                schema: "VINH",
                table: "PHONGHOC",
                column: "TENPHONGHOC");

            migrationBuilder.CreateIndex(
                name: "IX_SINHVIEN_DIACHI",
                schema: "VINH",
                table: "SINHVIEN",
                column: "DIACHI");

            migrationBuilder.CreateIndex(
                name: "IX_SINHVIEN_HOTEN",
                schema: "VINH",
                table: "SINHVIEN",
                column: "HOTEN");

            migrationBuilder.CreateIndex(
                name: "IX_SINHVIEN_IDCHUONGTRINHHOC",
                schema: "VINH",
                table: "SINHVIEN",
                column: "IDCHUONGTRINHHOC");

            migrationBuilder.CreateIndex(
                name: "IX_SINHVIEN_IDKHOA",
                schema: "VINH",
                table: "SINHVIEN",
                column: "IDKHOA");

            migrationBuilder.CreateIndex(
                name: "IX_SINHVIEN_LOP",
                schema: "VINH",
                table: "SINHVIEN",
                column: "LOP");

            migrationBuilder.CreateIndex(
                name: "IX_SINHVIEN_NGAYSINH",
                schema: "VINH",
                table: "SINHVIEN",
                column: "NGAYSINH");

            migrationBuilder.CreateIndex(
                name: "IX_SINHVIEN_LOPHOCPHAN",
                schema: "VINH",
                table: "SINHVIEN_LOPHOCPHAN",
                columns: new[] { "IDSINHVIEN", "IDLOPHOCPHAN" });

            migrationBuilder.CreateIndex(
                name: "IX_SINHVIEN_LOPHOCPHAN_IDLOPHOCPHAN",
                schema: "VINH",
                table: "SINHVIEN_LOPHOCPHAN",
                column: "IDLOPHOCPHAN");

            migrationBuilder.CreateIndex(
                name: "IX_THOIGIAN_IDPHONGHOC",
                schema: "VINH",
                table: "THOIGIAN",
                column: "IDPHONGHOC");

            migrationBuilder.CreateIndex(
                name: "IX_THOIGIAN_NGAYBATDAU",
                schema: "VINH",
                table: "THOIGIAN",
                column: "NGAYBATDAU");

            migrationBuilder.CreateIndex(
                name: "IX_THOIGIAN_NGAYKETTHUC",
                schema: "VINH",
                table: "THOIGIAN",
                column: "NGAYKETTHUC");

            migrationBuilder.CreateIndex(
                name: "IX_THOIGIAN_LOPHOCPHAN",
                schema: "VINH",
                table: "THOIGIAN_LOPHOCPHAN",
                columns: new[] { "IDLOPHOCPHAN", "IDTHOIGIAN" });

            migrationBuilder.CreateIndex(
                name: "IX_THOIGIAN_LOPHOCPHAN_IDTHOIGIAN",
                schema: "VINH",
                table: "THOIGIAN_LOPHOCPHAN",
                column: "IDTHOIGIAN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CHUONGTRINHHOC_MONHOC",
                schema: "VINH");

            migrationBuilder.DropTable(
                name: "DANGKYDOILICH",
                schema: "VINH");

            migrationBuilder.DropTable(
                name: "DANGKYNGUYENVONG",
                schema: "VINH");

            migrationBuilder.DropTable(
                name: "DIEM",
                schema: "VINH");

            migrationBuilder.DropTable(
                name: "SINHVIEN_LOPHOCPHAN",
                schema: "VINH");

            migrationBuilder.DropTable(
                name: "THOIGIAN_LOPHOCPHAN",
                schema: "VINH");

            migrationBuilder.DropTable(
                name: "SINHVIEN",
                schema: "VINH");

            migrationBuilder.DropTable(
                name: "LOPHOCPHAN",
                schema: "VINH");

            migrationBuilder.DropTable(
                name: "THOIGIAN",
                schema: "VINH");

            migrationBuilder.DropTable(
                name: "CHUONGTRINHHOC",
                schema: "VINH");

            migrationBuilder.DropTable(
                name: "GIAOVIEN",
                schema: "VINH");

            migrationBuilder.DropTable(
                name: "MONHOC",
                schema: "VINH");

            migrationBuilder.DropTable(
                name: "PHONGHOC",
                schema: "VINH");

            migrationBuilder.DropTable(
                name: "KHOA",
                schema: "VINH");
        }
    }
}
