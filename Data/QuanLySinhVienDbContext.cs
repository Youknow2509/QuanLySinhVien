using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

//
using web_qlsv.Models;

namespace web_qlsv.Data;

public class QuanLySinhVienDbContext : DbContext
{
    // List of tables
    public virtual DbSet<ChuongTrinhHoc> ChuongTrinhHocs { get; set; }

    public virtual DbSet<ChuongTrinhHocMonHoc> ChuongTrinhHocMonHocs { get; set; }

    public virtual DbSet<DangKyDoiLich> DangKyDoiLichs { get; set; }

    public virtual DbSet<DangKyNguyenVong> DangKyNguyenVongs { get; set; }

    public virtual DbSet<Diem> Diems { get; set; }

    public virtual DbSet<GiaoVien> GiaoViens { get; set; }

    public virtual DbSet<Khoa> Khoas { get; set; }

    public virtual DbSet<LopHocPhan> LopHocPhans { get; set; }

    public virtual DbSet<MonHoc> MonHocs { get; set; }

    public virtual DbSet<PhongHoc> PhongHocs { get; set; }

    public virtual DbSet<SinhVien> SinhViens { get; set; }

    public virtual DbSet<SinhVienLopHocPhan> SinhVienLopHocPhans { get; set; }

    public virtual DbSet<ThoiGian> ThoiGians { get; set; }

    public virtual DbSet<ThoiGianLopHocPhan> ThoiGianLopHocPhans { get; set; }

    // Constructors
    public QuanLySinhVienDbContext()
    {
    }

    public QuanLySinhVienDbContext(DbContextOptions<QuanLySinhVienDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Get string connection in appsettings.json
        string connectionString = "User Id=vinh;Password=123;Data Source=localhost:1521/orclcdb1";
        optionsBuilder.UseOracle(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("VINH")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<ChuongTrinhHoc>(entity =>
        {
            entity.HasKey(e => e.IdChuongTrinhHoc);

            entity.ToTable("CHUONGTRINHHOC");

            entity.HasIndex(e => e.TenChuongTrinhHoc, "IX_CHUONGTRINHHOC_TENCHUONGTRINHHOC");

            entity.Property(e => e.IdChuongTrinhHoc)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDCHUONGTRINHHOC");
            entity.Property(e => e.TenChuongTrinhHoc)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TENCHUONGTRINHHOC");
        });

        modelBuilder.Entity<ChuongTrinhHocMonHoc>(entity =>
        {
            entity.HasKey(e => e.IdChuongTrinhHocMonHoc);

            entity.ToTable("CHUONGTRINHHOC_MONHOC");

            entity.Property(e => e.IdChuongTrinhHocMonHoc)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDCHUONGTRINHHOCMONHOC");
            entity.Property(e => e.IdChuongTrinhHoc)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDCHUONGTRINHHOC");
            entity.Property(e => e.IdMonHoc)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDMONHOC");

            entity.HasOne(d => d.IdChuongTrinhHocNavigation).WithMany(p => p.ChuongTrinhHocMonHocs)
                .HasForeignKey(d => d.IdChuongTrinhHoc)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdMonHocNavigation).WithMany(p => p.ChuongTrinhHocMonHocs)
                .HasForeignKey(d => d.IdMonHoc)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<DangKyDoiLich>(entity =>
        {
            entity.HasKey(e => e.IdDangKyDoiLich);

            entity.ToTable("DANGKYDOILICH");

            entity.HasIndex(e => e.ThoiGianBatDauHienTai, "IX_DANGKYDOILICH_THOIGIANBATDAUHIENTAI");

            entity.HasIndex(e => e.ThoiGianBatDauMoi, "IX_DANGKYDOILICH_THOIGIANBATDAUMOI");

            entity.HasIndex(e => e.ThoiGianKetThucHienTai, "IX_DANGKYDOILICH_THOIGIANKETTHUCHIENTAI");

            entity.HasIndex(e => e.ThoiGianKetThucMoi, "IX_DANGKYDOILICH_THOIGIANKETTHUCMOI");

            entity.HasIndex(e => e.TrangThai, "IX_DANGKYDOILICH_TRANGTHAI");

            entity.Property(e => e.IdDangKyDoiLich)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDDANGKYDOILICH");
            entity.Property(e => e.IdThoiGian)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDTHOIGIAN");
            entity.Property(e => e.ThoiGianBatDauHienTai)
                .HasColumnType("DATE")
                .HasColumnName("THOIGIANBATDAUHIENTAI");
            entity.Property(e => e.ThoiGianBatDauMoi)
                .HasColumnType("DATE")
                .HasColumnName("THOIGIANBATDAUMOI");
            entity.Property(e => e.ThoiGianKetThucHienTai)
                .HasColumnType("DATE")
                .HasColumnName("THOIGIANKETTHUCHIENTAI");
            entity.Property(e => e.ThoiGianKetThucMoi)
                .HasColumnType("DATE")
                .HasColumnName("THOIGIANKETTHUCMOI");
            entity.Property(e => e.TrangThai)
                .HasDefaultValueSql("-1 ")
                .HasColumnType("NUMBER(38)")
                .HasColumnName("TRANGTHAI");

            entity.HasOne(d => d.IdThoiGianNavigation).WithMany(p => p.DangKyDoiLichs).HasForeignKey(d => d.IdThoiGian);
        });

        modelBuilder.Entity<DangKyNguyenVong>(entity =>
        {
            entity.HasKey(e => e.IdDangKyNguyenVong);

            entity.ToTable("DANGKYNGUYENVONG");

            entity.HasIndex(e => e.TrangThai, "IX_DANGKYNGUYENVONG_TRANGTHAI");

            entity.Property(e => e.IdDangKyNguyenVong)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDDANGKYNGUYENVONG");
            entity.Property(e => e.IdMonHoc)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDMONHOC");
            entity.Property(e => e.IdSinhVien)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDSINHVIEN");
            entity.Property(e => e.TrangThai)
                .HasDefaultValueSql("-1 ")
                .HasColumnType("NUMBER(38)")
                .HasColumnName("TRANGTHAI");

            entity.HasOne(d => d.IdMonHocNavigation).WithMany(p => p.DangKyNguyenVongs).HasForeignKey(d => d.IdMonHoc);

            entity.HasOne(d => d.IdSinhVienNavigation).WithMany(p => p.DangKyNguyenVongs).HasForeignKey(d => d.IdSinhVien);
        });

        modelBuilder.Entity<Diem>(entity =>
        {
            entity.HasKey(e => e.IdDiem);

            entity.ToTable("DIEM");

            entity.HasIndex(e => e.DiemKetThuc, "IX_DIEM_DIEMKETTHUC");

            entity.HasIndex(e => e.DiemQuaTrinh, "IX_DIEM_DIEMQUATRINH");

            entity.HasIndex(e => e.DiemTongKet, "IX_DIEM_DIEMTONGKET");

            entity.HasIndex(e => e.LanHoc, "IX_DIEM_LANHOC");

            entity.Property(e => e.IdDiem)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDDIEM");
            entity.Property(e => e.DiemKetThuc)
                .HasColumnType("NUMBER(5,2)")
                .HasColumnName("DIEMKETTHUC");
            entity.Property(e => e.DiemQuaTrinh)
                .HasColumnType("NUMBER(5,2)")
                .HasColumnName("DIEMQUATRINH");
            entity.Property(e => e.DiemTongKet)
                .HasColumnType("NUMBER(5,2)")
                .HasColumnName("DIEMTONGKET");
            entity.Property(e => e.IdLopHocPhan)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDLOPHOCPHAN");
            entity.Property(e => e.IdSinhVien)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDSINHVIEN");
            entity.Property(e => e.LanHoc)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("LANHOC");

            entity.HasOne(d => d.IdLopHocPhanNavigation).WithMany(p => p.Diems)
                .HasForeignKey(d => d.IdLopHocPhan)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdSinhVienNavigation).WithMany(p => p.Diems)
                .HasForeignKey(d => d.IdSinhVien)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<GiaoVien>(entity =>
        {
            entity.HasKey(e => e.IdGiaoVien);

            entity.ToTable("GIAOVIEN");

            entity.HasIndex(e => e.Email, "IX_GIAOVIEN_EMAIL");

            entity.HasIndex(e => e.SoDienThoai, "IX_GIAOVIEN_SODIENTHOAI");

            entity.HasIndex(e => e.TenGiaoVien, "IX_GIAOVIEN_TENGIAOVIEN");

            entity.Property(e => e.IdGiaoVien)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDGIAOVIEN");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.IdKhoa)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDKHOA");
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("SODIENTHOAI");
            entity.Property(e => e.TenGiaoVien)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TENGIAOVIEN");

            entity.HasOne(d => d.IdKhoaNavigation).WithMany(p => p.GiaoViens)
                .HasForeignKey(d => d.IdKhoa)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Khoa>(entity =>
        {
            entity.HasKey(e => e.IdKhoa);

            entity.ToTable("KHOA");

            entity.HasIndex(e => e.TenKhoa, "IX_KHOA_TENKHOA");

            entity.Property(e => e.IdKhoa)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDKHOA");
            entity.Property(e => e.TenKhoa)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TENKHOA");
        });

        modelBuilder.Entity<LopHocPhan>(entity =>
        {
            entity.HasKey(e => e.IdLopHocPhan);

            entity.ToTable("LOPHOCPHAN");

            entity.HasIndex(e => e.TenHocPhan, "IX_LOPHOCPHAN_TENHOCPHAN");

            entity.HasIndex(e => e.ThoiGianBatDau, "IX_LOPHOCPHAN_THOIGIANBATDAU");

            entity.HasIndex(e => e.ThoiGianKetThuc, "IX_LOPHOCPHAN_THOIGIANKETTHUC");

            entity.Property(e => e.IdLopHocPhan)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDLOPHOCPHAN");
            entity.Property(e => e.IdGiaoVien)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDGIAOVIEN");
            entity.Property(e => e.IdMonHoc)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDMONHOC");
            entity.Property(e => e.SoTietHoc)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("SOTIETHOC");
            entity.Property(e => e.SoTinChi)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("SOTINCHI");
            entity.Property(e => e.TenHocPhan)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TENHOCPHAN");
            entity.Property(e => e.ThoiGianBatDau)
                .HasColumnType("DATE")
                .HasColumnName("THOIGIANBATDAU");
            entity.Property(e => e.ThoiGianKetThuc)
                .HasColumnType("DATE")
                .HasColumnName("THOIGIANKETTHUC");

            entity.HasOne(d => d.IdGiaoVienNavigation).WithMany(p => p.LopHocPhans)
                .HasForeignKey(d => d.IdGiaoVien)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdMonHocNavigation).WithMany(p => p.LopHocPhans)
                .HasForeignKey(d => d.IdMonHoc)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<MonHoc>(entity =>
        {
            entity.HasKey(e => e.IdMonHoc);

            entity.ToTable("MONHOC");

            entity.HasIndex(e => e.TenMonHoc, "IX_MONHOC_TENMONHOC");

            entity.Property(e => e.IdMonHoc)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDMONHOC");
            entity.Property(e => e.IdKhoa)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDKHOA");
            entity.Property(e => e.TenMonHoc)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TENMONHOC");

            entity.HasOne(d => d.IdKhoaNavigation).WithMany(p => p.MonHocs)
                .HasForeignKey(d => d.IdKhoa)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<PhongHoc>(entity =>
        {
            entity.HasKey(e => e.IdPhongHoc);

            entity.ToTable("PHONGHOC");

            entity.HasIndex(e => e.DiaChi, "IX_PHONGHOC_DIACHI");

            entity.HasIndex(e => e.TenPhongHoc, "IX_PHONGHOC_TENPHONGHOC");

            entity.Property(e => e.IdPhongHoc)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDPHONGHOC");
            entity.Property(e => e.DiaChi)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("DIACHI");
            entity.Property(e => e.TenPhongHoc)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TENPHONGHOC");
        });

        modelBuilder.Entity<SinhVien>(entity =>
        {
            entity.HasKey(e => e.IdSinhVien);

            entity.ToTable("SINHVIEN");

            entity.HasIndex(e => e.DiaChi, "IX_SINHVIEN_DIACHI");

            entity.HasIndex(e => e.HoTen, "IX_SINHVIEN_HOTEN");

            entity.HasIndex(e => e.Lop, "IX_SINHVIEN_LOP");

            entity.HasIndex(e => e.NgaySinh, "IX_SINHVIEN_NGAYSINH");

            entity.Property(e => e.IdSinhVien)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDSINHVIEN");
            entity.Property(e => e.DiaChi)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("DIACHI");
            entity.Property(e => e.HoTen)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("HOTEN");
            entity.Property(e => e.IdChuongTrinhHoc)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDCHUONGTRINHHOC");
            entity.Property(e => e.IdKhoa)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDKHOA");
            entity.Property(e => e.Lop)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LOP");
            entity.Property(e => e.NgaySinh)
                .HasColumnType("DATE")
                .HasColumnName("NGAYSINH");

            entity.HasOne(d => d.IdChuongTrinhHocNavigation).WithMany(p => p.SinhViens)
                .HasForeignKey(d => d.IdChuongTrinhHoc)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdKhoaNavigation).WithMany(p => p.SinhViens)
                .HasForeignKey(d => d.IdKhoa)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<SinhVienLopHocPhan>(entity =>
        {
            entity.HasKey(e => e.IdSinhVienLopHocPhan);

            entity.ToTable("SINHVIEN_LOPHOCPHAN");

            entity.HasIndex(e => new { e.IdSinhVien, e.IdLopHocPhan }, "IX_SINHVIEN_LOPHOCPHAN");

            entity.Property(e => e.IdSinhVienLopHocPhan)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDSINHVIENLOPHOCPHAN");
            entity.Property(e => e.IdLopHocPhan)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDLOPHOCPHAN");
            entity.Property(e => e.IdSinhVien)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDSINHVIEN");

            entity.HasOne(d => d.IdLopHocPhanNavigation).WithMany(p => p.SinhVienLopHocPhans)
                .HasForeignKey(d => d.IdLopHocPhan)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdSinhVienNavigation).WithMany(p => p.SinhVienLopHocPhans)
                .HasForeignKey(d => d.IdSinhVien)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<ThoiGian>(entity =>
        {
            entity.HasKey(e => e.IdThoiGian);

            entity.ToTable("THOIGIAN");

            entity.HasIndex(e => e.NgayBatDau, "IX_THOIGIAN_NGAYBATDAU");

            entity.HasIndex(e => e.NgayKetThuc, "IX_THOIGIAN_NGAYKETTHUC");

            entity.Property(e => e.IdThoiGian)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDTHOIGIAN");
            entity.Property(e => e.IdPhongHoc)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDPHONGHOC");
            entity.Property(e => e.NgayBatDau)
                .HasColumnType("DATE")
                .HasColumnName("NGAYBATDAU");
            entity.Property(e => e.NgayKetThuc)
                .HasColumnType("DATE")
                .HasColumnName("NGAYKETTHUC");

            entity.HasOne(d => d.IdPhongHocNavigation).WithMany(p => p.ThoiGians)
                .HasForeignKey(d => d.IdPhongHoc)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<ThoiGianLopHocPhan>(entity =>
        {
            entity.HasKey(e => e.IdThoiGianLopHocPhan);

            entity.ToTable("THOIGIAN_LOPHOCPHAN");

            entity.HasIndex(e => new { e.IdLopHocPhan, e.IdThoiGian }, "IX_THOIGIAN_LOPHOCPHAN");

            entity.Property(e => e.IdThoiGianLopHocPhan)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDTHOIGIANLOPHOCPHAN");
            entity.Property(e => e.IdLopHocPhan)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDLOPHOCPHAN");
            entity.Property(e => e.IdThoiGian)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDTHOIGIAN");

            entity.HasOne(d => d.IdLopHocPhanNavigation).WithMany(p => p.ThoiGianLopHocPhans)
                .HasForeignKey(d => d.IdLopHocPhan)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdThoiGianNavigation).WithMany(p => p.ThoiGianLopHocPhans)
                .HasForeignKey(d => d.IdThoiGian)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

    }
}
