using Microsoft.EntityFrameworkCore;
using System.Configuration;

using QLDT_WPF.Models;

namespace QLDT_WPF.Data;

public class QuanLySinhVienDbContext : DbContext
{
    // Variables
    public DbSet<ChuongTrinhHoc> ChuongTrinhHocs { get; set; } = null!;
    public DbSet<ChuongTrinhHocMonHoc> ChuongTrinhHocMonHocs { get; set; } = null!;
    public DbSet<Diem> Diems { get; set; } = null!;
    public DbSet<GiaoVien> GiaoViens { get; set; } = null!;
    public DbSet<Khoa> Khoas { get; set; } = null!;
    public DbSet<LopHocPhan> LopHocPhans { get; set; } = null!;
    public DbSet<MonHoc> MonHocs { get; set; } = null!;
    public DbSet<SinhVien> SinhViens { get; set; } = null!;
    public DbSet<SinhVienLopHocPhan> SinhVienLopHocPhans { get; set; } = null!;
    public DbSet<ThoiGian> ThoiGians { get; set; } = null!;
    public DbSet<ThoiGianLopHocPhan> ThoiGianLopHocPhans { get; set; } = null!;
    public DbSet<DangKyNguyenVong> DangKyNguyenVongs { get; set; } = null!;
    public DbSet<DangKyDoiLich> DangKyDoiLichs { get; set; } = null!;

    // Constructor
    public QuanLySinhVienDbContext(DbContextOptions<QuanLySinhVienDbContext> options)
        : base(options)
    {
    }

    // Cấu hình kết nối với cơ sở dữ liệu nếu cần
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string connectionStrings = ConfigurationManager.ConnectionStrings["QuanLySinhVienDbConnection"].ConnectionString;
            optionsBuilder.UseSqlServer(connectionStrings);
        }
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChuongTrinhHoc>(entity =>
            {
                entity.HasKey(e => e.IdChuongTrinhHoc);

                entity.ToTable("ChuongTrinhHoc");

                entity.Property(e => e.IdChuongTrinhHoc)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.TenChuongTrinhHoc).HasMaxLength(100);
            });

        modelBuilder.Entity<ChuongTrinhHocMonHoc>(entity =>
        {
            entity.HasKey(e => e.IdCthmonHoc);

            entity.ToTable("ChuongTrinhHoc_MonHoc");

            entity.Property(e => e.IdCthmonHoc)
                .HasMaxLength(100)
                .HasColumnName("IdCTHMonHoc")
                .HasDefaultValueSql("(newid())");

            entity.Property(e => e.IdChuongTrinhHoc).HasMaxLength(100);

            entity.Property(e => e.IdMonHoc).HasMaxLength(100);

            entity.HasOne(d => d.ChuongTrinhHocs)
                .WithMany(p => p.ChuongTrinhHocMonHocs)
                .HasForeignKey(d => d.IdChuongTrinhHoc);

            entity.HasOne(d => d.MonHocs)
                .WithMany(p => p.ChuongTrinhHocMonHocs)
                .HasForeignKey(d => d.IdMonHoc);
        });

        modelBuilder.Entity<Diem>(entity =>
        {
            entity.HasKey(e => e.IdDiem);

            entity.ToTable("Diem");

            entity.Property(e => e.IdDiem)
                .HasMaxLength(100)
                .HasDefaultValueSql("(newid())");

            entity.Property(e => e.DiemKetThuc).HasColumnType("decimal(5, 2)");

            entity.Property(e => e.DiemQuaTrinh).HasColumnType("decimal(5, 2)");

            entity.Property(e => e.DiemTongKet).HasColumnType("decimal(5, 2)");

            entity.Property(e => e.IdLopHocPhan).HasMaxLength(100);

            entity.Property(e => e.IdSinhVien).HasMaxLength(100);

            entity.HasOne(d => d.LopHocPhans)
                .WithMany(p => p.Diems)
                .HasForeignKey(d => d.IdLopHocPhan);

            entity.HasOne(d => d.SinhViens)
                .WithMany(p => p.Diems)
                .HasForeignKey(d => d.IdSinhVien);
        });

        modelBuilder.Entity<GiaoVien>(entity =>
        {
            entity.HasKey(e => e.IdGiaoVien);

            entity.ToTable("GiaoVien");

            entity.Property(e => e.IdGiaoVien)
                .HasMaxLength(100);

            entity.Property(e => e.Email).HasMaxLength(100);

            entity.Property(e => e.IdKhoa).HasMaxLength(100);

            entity.Property(e => e.SoDienThoai).HasMaxLength(15);

            entity.Property(e => e.TenGiaoVien).HasMaxLength(100);

            entity.HasOne(d => d.Khoas)
                .WithMany(p => p.GiaoViens)
                .HasForeignKey(d => d.IdKhoa);
        });

        modelBuilder.Entity<Khoa>(entity =>
        {
            entity.HasKey(e => e.IdKhoa);

            entity.ToTable("Khoa");

            entity.Property(e => e.IdKhoa)
                .HasMaxLength(100);

            entity.Property(e => e.TenKhoa).HasMaxLength(100);
        });

        modelBuilder.Entity<LopHocPhan>(entity =>
        {
            entity.HasKey(e => e.IdLopHocPhan);

            entity.ToTable("LopHocPhan");

            entity.Property(e => e.IdLopHocPhan)
                .HasMaxLength(100);

            entity.Property(e => e.IdGiaoVien).HasMaxLength(100);

            entity.Property(e => e.IdMonHoc).HasMaxLength(100);

            entity.Property(e => e.TenHocPhan).HasMaxLength(100);

            entity.HasOne(d => d.GiaoViens)
                .WithMany(p => p.LopHocPhans)
                .HasForeignKey(d => d.IdGiaoVien);

            entity.HasOne(d => d.MonHocs)
                .WithMany(p => p.LopHocPhans)
                .HasForeignKey(d => d.IdMonHoc);
        });

        modelBuilder.Entity<MonHoc>(entity =>
        {
            entity.HasKey(e => e.IdMonHoc);

            entity.ToTable("MonHoc");

            entity.Property(e => e.IdMonHoc)
                .HasMaxLength(100);

            entity.Property(e => e.IdKhoa).HasMaxLength(100);

            entity.Property(e => e.TenMonHoc).HasMaxLength(100);

            entity.HasOne(d => d.Khoas)
                .WithMany(p => p.MonHocs)
                .HasForeignKey(d => d.IdKhoa);
        });

        modelBuilder.Entity<SinhVien>(entity =>
        {
            entity.HasKey(e => e.IdSinhVien);

            entity.ToTable("SinhVien");

            entity.Property(e => e.IdSinhVien)
                .HasMaxLength(100);

            entity.Property(e => e.DiaChi).HasMaxLength(255);

            entity.Property(e => e.HoTen).HasMaxLength(100);

            entity.Property(e => e.IdChuongTrinhHoc).HasMaxLength(100);

            entity.Property(e => e.Lop).HasMaxLength(50);

            entity.Property(e => e.NgaySinh).HasColumnType("date");

            entity.HasOne(d => d.ChuongTrinhHocs)
                .WithMany(p => p.SinhViens)
                .HasForeignKey(d => d.IdChuongTrinhHoc);

            entity.HasOne(d => d.Khoas)
                .WithMany(p => p.SinhViens)
                .HasForeignKey(d => d.IdKhoa);
        });

        modelBuilder.Entity<SinhVienLopHocPhan>(entity =>
        {
            entity.HasKey(e => e.IdSinhVienLopHp);

            entity.ToTable("SinhVien_LopHocPhan");

            entity.Property(e => e.IdSinhVienLopHp)
                .HasMaxLength(100)
                .HasColumnName("IdSinhVienLopHP")
                .HasDefaultValueSql("(newid())");

            entity.Property(e => e.IdLopHocPhan).HasMaxLength(100);

            entity.Property(e => e.IdSinhVien).HasMaxLength(100);

            entity.HasOne(d => d.LopHocPhans)
                .WithMany(p => p.SinhVienLopHocPhans)
                .HasForeignKey(d => d.IdLopHocPhan);

            entity.HasOne(d => d.SinhViens)
                .WithMany(p => p.SinhVienLopHocPhans)
                .HasForeignKey(d => d.IdSinhVien);
        });

        modelBuilder.Entity<ThoiGian>(entity =>
        {
            entity.HasKey(e => e.IdThoiGian);

            entity.ToTable("ThoiGian");

            entity.Property(e => e.IdThoiGian)
                .HasMaxLength(100)
                .HasDefaultValueSql("(newid())");

            entity.Property(e => e.NgayBatDau).HasColumnType("datetime");

            entity.Property(e => e.NgayKetThuc).HasColumnType("datetime");
        });

        modelBuilder.Entity<ThoiGianLopHocPhan>(entity =>
        {
            entity.HasKey(e => e.IdThoigianLopHp);

            entity.ToTable("ThoiGian_LopHocPhan");

            entity.Property(e => e.IdThoigianLopHp)
                .HasMaxLength(100)
                .HasColumnName("IdThoigianLopHP")
                .HasDefaultValueSql("(newid())");

            entity.Property(e => e.IdLopHocPhan).HasMaxLength(100);

            entity.Property(e => e.IdThoiGian).HasMaxLength(100);

            entity.HasOne(d => d.LopHocPhans)
                .WithMany(p => p.ThoiGianLopHocPhans)
                .HasForeignKey(d => d.IdLopHocPhan);

            entity.HasOne(d => d.ThoiGians)
                .WithMany(p => p.ThoiGianLopHocPhans)
                .HasForeignKey(d => d.IdThoiGian);
        });

        modelBuilder.Entity<DangKyNguyenVong>(entity =>
        {
            entity.HasKey(e => e.IdDangKyNguyenVong);

            entity.ToTable("DangKyNguyenVong");

            entity.Property(e => e.IdDangKyNguyenVong)
                .HasMaxLength(100)
                .HasDefaultValueSql("(newid())");

            entity.Property(e => e.IdMonHoc).HasMaxLength(100);

            entity.Property(e => e.IdSinhVien).HasMaxLength(100);

            // Mặc định khởi tạo trạng thái là -1. 1 Là đc chấp nhận, 0 là từ chối
            entity.Property(e => e.TrangThai).HasDefaultValue(-1);

            entity.HasOne(d => d.SinhViens)
                .WithMany(p => p.DangKyNguyenVongs)
                .HasForeignKey(d => d.IdSinhVien);

            entity.HasOne(d => d.MonHocs)
                .WithMany(p => p.DangKyNguyenVongs)
                .HasForeignKey(d => d.IdMonHoc);
        });

        modelBuilder.Entity<DangKyDoiLich>(entity =>
        {
            entity.HasKey(e => e.IdDangKyDoiLich);

            entity.ToTable("DangKyDoiLich");

            entity.Property(e => e.IdDangKyDoiLich)
                .HasMaxLength(100)
                .HasDefaultValueSql("(newid())");

            entity.Property(e => e.ThoiGianBatDauHienTai).HasColumnType("datetime");
            entity.Property(e => e.ThoiGianKetThucHienTai).HasColumnType("datetime");
            entity.Property(e => e.ThoiGianKetThucMoi).HasColumnType("datetime");
            entity.Property(e => e.ThoiGianKetThucMoi).HasColumnType("datetime");

            // Mặc định khởi tạo trạng thái là -1. 1 Là đc chấp nhận, 0 là từ chối
            entity.Property(e => e.TrangThai).HasDefaultValue(-1);

            entity.HasOne(d => d.ThoiGians)
                .WithMany(p => p.DangKyDoiLichs)
                .HasForeignKey(d => d.IdThoiGian);
        });
    }
}