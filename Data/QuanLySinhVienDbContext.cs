using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

//
using web_qlsv.Models;

namespace web_qlsv.Data;

public partial class QuanLySinhVienDbContext : DbContext
{
    // List of tables
    public virtual DbSet<Chuongtrinhhoc> Chuongtrinhhocs { get; set; }

    public virtual DbSet<ChuongtrinhhocMonhoc> ChuongtrinhhocMonhocs { get; set; }

    public virtual DbSet<Dangkydoilich> Dangkydoiliches { get; set; }

    public virtual DbSet<Dangkynguyenvong> Dangkynguyenvongs { get; set; }

    public virtual DbSet<Diem> Diems { get; set; }

    public virtual DbSet<Giaovien> Giaoviens { get; set; }

    public virtual DbSet<Khoa> Khoas { get; set; }

    public virtual DbSet<Lophocphan> Lophocphans { get; set; }

    public virtual DbSet<Monhoc> Monhocs { get; set; }

    public virtual DbSet<Phonghoc> Phonghocs { get; set; }

    public virtual DbSet<Sinhvien> Sinhviens { get; set; }

    public virtual DbSet<SinhvienLophocphan> SinhvienLophocphans { get; set; }

    public virtual DbSet<Thoigian> Thoigians { get; set; }

    public virtual DbSet<ThoigianLophocphan> ThoigianLophocphans { get; set; }

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

        modelBuilder.Entity<Chuongtrinhhoc>(entity =>
        {
            entity.HasKey(e => e.Idchuongtrinhhoc);

            entity.ToTable("CHUONGTRINHHOC");

            entity.HasIndex(e => e.Tenchuongtrinhhoc, "IX_CHUONGTRINHHOC_TENCHUONGTRINHHOC");

            entity.Property(e => e.Idchuongtrinhhoc)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDCHUONGTRINHHOC");
            entity.Property(e => e.Tenchuongtrinhhoc)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TENCHUONGTRINHHOC");
        });

        modelBuilder.Entity<ChuongtrinhhocMonhoc>(entity =>
        {
            entity.HasKey(e => e.Idcthmonhoc);

            entity.ToTable("CHUONGTRINHHOC_MONHOC");

            entity.Property(e => e.Idcthmonhoc)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDCTHMONHOC");
            entity.Property(e => e.Idchuongtrinhhoc)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDCHUONGTRINHHOC");
            entity.Property(e => e.Idmonhoc)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDMONHOC");

            entity.HasOne(d => d.IdchuongtrinhhocNavigation).WithMany(p => p.ChuongtrinhhocMonhocs)
                .HasForeignKey(d => d.Idchuongtrinhhoc)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdmonhocNavigation).WithMany(p => p.ChuongtrinhhocMonhocs)
                .HasForeignKey(d => d.Idmonhoc)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Dangkydoilich>(entity =>
        {
            entity.HasKey(e => e.Iddangkydoilich);

            entity.ToTable("DANGKYDOILICH");

            entity.HasIndex(e => e.Thoigianbatdauhientai, "IX_DANGKYDOILICH_THOIGIANBATDAUHIENTAI");

            entity.HasIndex(e => e.Thoigianbatdaumoi, "IX_DANGKYDOILICH_THOIGIANBATDAUMOI");

            entity.HasIndex(e => e.Thoigianketthuchientai, "IX_DANGKYDOILICH_THOIGIANKETTHUCHIENTAI");

            entity.HasIndex(e => e.Thoigianketthucmoi, "IX_DANGKYDOILICH_THOIGIANKETTHUCMOI");

            entity.HasIndex(e => e.Trangthai, "IX_DANGKYDOILICH_TRANGTHAI");

            entity.Property(e => e.Iddangkydoilich)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDDANGKYDOILICH");
            entity.Property(e => e.Idthoigian)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDTHOIGIAN");
            entity.Property(e => e.Thoigianbatdauhientai)
                .HasColumnType("DATE")
                .HasColumnName("THOIGIANBATDAUHIENTAI");
            entity.Property(e => e.Thoigianbatdaumoi)
                .HasColumnType("DATE")
                .HasColumnName("THOIGIANBATDAUMOI");
            entity.Property(e => e.Thoigianketthuchientai)
                .HasColumnType("DATE")
                .HasColumnName("THOIGIANKETTHUCHIENTAI");
            entity.Property(e => e.Thoigianketthucmoi)
                .HasColumnType("DATE")
                .HasColumnName("THOIGIANKETTHUCMOI");
            entity.Property(e => e.Trangthai)
                .HasDefaultValueSql("-1 ")
                .HasColumnType("NUMBER(38)")
                .HasColumnName("TRANGTHAI");

            entity.HasOne(d => d.IdthoigianNavigation).WithMany(p => p.Dangkydoiliches).HasForeignKey(d => d.Idthoigian);
        });

        modelBuilder.Entity<Dangkynguyenvong>(entity =>
        {
            entity.HasKey(e => e.Iddangkynguyenvong);

            entity.ToTable("DANGKYNGUYENVONG");

            entity.HasIndex(e => e.Trangthai, "IX_DANGKYNGUYENVONG_TRANGTHAI");

            entity.Property(e => e.Iddangkynguyenvong)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDDANGKYNGUYENVONG");
            entity.Property(e => e.Idmonhoc)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDMONHOC");
            entity.Property(e => e.Idsinhvien)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDSINHVIEN");
            entity.Property(e => e.Trangthai)
                .HasDefaultValueSql("-1 ")
                .HasColumnType("NUMBER(38)")
                .HasColumnName("TRANGTHAI");

            entity.HasOne(d => d.IdmonhocNavigation).WithMany(p => p.Dangkynguyenvongs).HasForeignKey(d => d.Idmonhoc);

            entity.HasOne(d => d.IdsinhvienNavigation).WithMany(p => p.Dangkynguyenvongs).HasForeignKey(d => d.Idsinhvien);
        });

        modelBuilder.Entity<Diem>(entity =>
        {
            entity.HasKey(e => e.Iddiem);

            entity.ToTable("DIEM");

            entity.HasIndex(e => e.Diemketthuc, "IX_DIEM_DIEMKETTHUC");

            entity.HasIndex(e => e.Diemquatrinh, "IX_DIEM_DIEMQUATRINH");

            entity.HasIndex(e => e.Diemtongket, "IX_DIEM_DIEMTONGKET");

            entity.HasIndex(e => e.Lanhoc, "IX_DIEM_LANHOC");

            entity.Property(e => e.Iddiem)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDDIEM");
            entity.Property(e => e.Diemketthuc)
                .HasColumnType("NUMBER(5,2)")
                .HasColumnName("DIEMKETTHUC");
            entity.Property(e => e.Diemquatrinh)
                .HasColumnType("NUMBER(5,2)")
                .HasColumnName("DIEMQUATRINH");
            entity.Property(e => e.Diemtongket)
                .HasColumnType("NUMBER(5,2)")
                .HasColumnName("DIEMTONGKET");
            entity.Property(e => e.Idlophocphan)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDLOPHOCPHAN");
            entity.Property(e => e.Idsinhvien)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDSINHVIEN");
            entity.Property(e => e.Lanhoc)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("LANHOC");

            entity.HasOne(d => d.IdlophocphanNavigation).WithMany(p => p.Diems)
                .HasForeignKey(d => d.Idlophocphan)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdsinhvienNavigation).WithMany(p => p.Diems)
                .HasForeignKey(d => d.Idsinhvien)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Giaovien>(entity =>
        {
            entity.HasKey(e => e.Idgiaovien);

            entity.ToTable("GIAOVIEN");

            entity.HasIndex(e => e.Email, "IX_GIAOVIEN_EMAIL");

            entity.HasIndex(e => e.Sodienthoai, "IX_GIAOVIEN_SODIENTHOAI");

            entity.HasIndex(e => e.Tengiaovien, "IX_GIAOVIEN_TENGIAOVIEN");

            entity.Property(e => e.Idgiaovien)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDGIAOVIEN");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Idkhoa)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDKHOA");
            entity.Property(e => e.Sodienthoai)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("SODIENTHOAI");
            entity.Property(e => e.Tengiaovien)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TENGIAOVIEN");

            entity.HasOne(d => d.IdkhoaNavigation).WithMany(p => p.Giaoviens)
                .HasForeignKey(d => d.Idkhoa)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Khoa>(entity =>
        {
            entity.HasKey(e => e.Idkhoa);

            entity.ToTable("KHOA");

            entity.HasIndex(e => e.Tenkhoa, "IX_KHOA_TENKHOA");

            entity.Property(e => e.Idkhoa)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDKHOA");
            entity.Property(e => e.Tenkhoa)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TENKHOA");
        });

        modelBuilder.Entity<Lophocphan>(entity =>
        {
            entity.HasKey(e => e.Idlophocphan);

            entity.ToTable("LOPHOCPHAN");

            entity.HasIndex(e => e.Tenhocphan, "IX_LOPHOCPHAN_TENHOCPHAN");

            entity.HasIndex(e => e.Thoigianbatdau, "IX_LOPHOCPHAN_THOIGIANBATDAU");

            entity.HasIndex(e => e.Thoigianketthuc, "IX_LOPHOCPHAN_THOIGIANKETTHUC");

            entity.Property(e => e.Idlophocphan)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDLOPHOCPHAN");
            entity.Property(e => e.Idgiaovien)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDGIAOVIEN");
            entity.Property(e => e.Idmonhoc)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDMONHOC");
            entity.Property(e => e.Sotiethoc)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("SOTIETHOC");
            entity.Property(e => e.Sotinchi)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("SOTINCHI");
            entity.Property(e => e.Tenhocphan)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TENHOCPHAN");
            entity.Property(e => e.Thoigianbatdau)
                .HasColumnType("DATE")
                .HasColumnName("THOIGIANBATDAU");
            entity.Property(e => e.Thoigianketthuc)
                .HasColumnType("DATE")
                .HasColumnName("THOIGIANKETTHUC");

            entity.HasOne(d => d.IdgiaovienNavigation).WithMany(p => p.Lophocphans)
                .HasForeignKey(d => d.Idgiaovien)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdmonhocNavigation).WithMany(p => p.Lophocphans)
                .HasForeignKey(d => d.Idmonhoc)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Monhoc>(entity =>
        {
            entity.HasKey(e => e.Idmonhoc);

            entity.ToTable("MONHOC");

            entity.HasIndex(e => e.Tenmonhoc, "IX_MONHOC_TENMONHOC");

            entity.Property(e => e.Idmonhoc)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDMONHOC");
            entity.Property(e => e.Idkhoa)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDKHOA");
            entity.Property(e => e.Tenmonhoc)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TENMONHOC");

            entity.HasOne(d => d.IdkhoaNavigation).WithMany(p => p.Monhocs)
                .HasForeignKey(d => d.Idkhoa)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Phonghoc>(entity =>
        {
            entity.HasKey(e => e.Idphonghoc);

            entity.ToTable("PHONGHOC");

            entity.HasIndex(e => e.Diachi, "IX_PHONGHOC_DIACHI");

            entity.HasIndex(e => e.Tenphonghoc, "IX_PHONGHOC_TENPHONGHOC");

            entity.Property(e => e.Idphonghoc)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDPHONGHOC");
            entity.Property(e => e.Diachi)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("DIACHI");
            entity.Property(e => e.Tenphonghoc)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TENPHONGHOC");
        });

        modelBuilder.Entity<Sinhvien>(entity =>
        {
            entity.HasKey(e => e.Idsinhvien);

            entity.ToTable("SINHVIEN");

            entity.HasIndex(e => e.Diachi, "IX_SINHVIEN_DIACHI");

            entity.HasIndex(e => e.Hoten, "IX_SINHVIEN_HOTEN");

            entity.HasIndex(e => e.Lop, "IX_SINHVIEN_LOP");

            entity.HasIndex(e => e.Ngaysinh, "IX_SINHVIEN_NGAYSINH");

            entity.Property(e => e.Idsinhvien)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDSINHVIEN");
            entity.Property(e => e.Diachi)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("DIACHI");
            entity.Property(e => e.Hoten)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("HOTEN");
            entity.Property(e => e.Idchuongtrinhhoc)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDCHUONGTRINHHOC");
            entity.Property(e => e.Idkhoa)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDKHOA");
            entity.Property(e => e.Lop)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LOP");
            entity.Property(e => e.Ngaysinh)
                .HasColumnType("DATE")
                .HasColumnName("NGAYSINH");

            entity.HasOne(d => d.IdchuongtrinhhocNavigation).WithMany(p => p.Sinhviens)
                .HasForeignKey(d => d.Idchuongtrinhhoc)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdkhoaNavigation).WithMany(p => p.Sinhviens)
                .HasForeignKey(d => d.Idkhoa)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<SinhvienLophocphan>(entity =>
        {
            entity.HasKey(e => e.Idsinhvienlophp);

            entity.ToTable("SINHVIEN_LOPHOCPHAN");

            entity.HasIndex(e => new { e.Idsinhvien, e.Idlophocphan }, "IX_SINHVIEN_LOPHOCPHAN");

            entity.Property(e => e.Idsinhvienlophp)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDSINHVIENLOPHP");
            entity.Property(e => e.Idlophocphan)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDLOPHOCPHAN");
            entity.Property(e => e.Idsinhvien)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDSINHVIEN");

            entity.HasOne(d => d.IdlophocphanNavigation).WithMany(p => p.SinhvienLophocphans)
                .HasForeignKey(d => d.Idlophocphan)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdsinhvienNavigation).WithMany(p => p.SinhvienLophocphans)
                .HasForeignKey(d => d.Idsinhvien)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Thoigian>(entity =>
        {
            entity.HasKey(e => e.Idthoigian);

            entity.ToTable("THOIGIAN");

            entity.HasIndex(e => e.Ngaybatdau, "IX_THOIGIAN_NGAYBATDAU");

            entity.HasIndex(e => e.Ngayketthuc, "IX_THOIGIAN_NGAYKETTHUC");

            entity.Property(e => e.Idthoigian)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDTHOIGIAN");
            entity.Property(e => e.Idphonghoc)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDPHONGHOC");
            entity.Property(e => e.Ngaybatdau)
                .HasColumnType("DATE")
                .HasColumnName("NGAYBATDAU");
            entity.Property(e => e.Ngayketthuc)
                .HasColumnType("DATE")
                .HasColumnName("NGAYKETTHUC");

            entity.HasOne(d => d.IdphonghocNavigation).WithMany(p => p.Thoigians)
                .HasForeignKey(d => d.Idphonghoc)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<ThoigianLophocphan>(entity =>
        {
            entity.HasKey(e => e.Idthoigianlophp);

            entity.ToTable("THOIGIAN_LOPHOCPHAN");

            entity.HasIndex(e => new { e.Idlophocphan, e.Idthoigian }, "IX_THOIGIAN_LOPHOCPHAN");

            entity.Property(e => e.Idthoigianlophp)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDTHOIGIANLOPHP");
            entity.Property(e => e.Idlophocphan)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDLOPHOCPHAN");
            entity.Property(e => e.Idthoigian)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IDTHOIGIAN");

            entity.HasOne(d => d.IdlophocphanNavigation).WithMany(p => p.ThoigianLophocphans)
                .HasForeignKey(d => d.Idlophocphan)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdthoigianNavigation).WithMany(p => p.ThoigianLophocphans)
                .HasForeignKey(d => d.Idthoigian)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
