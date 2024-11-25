-- Tạo bảng Phòng Học 
CREATE TABLE PhongHoc (
    IdPhongHoc VARCHAR2(100 CHAR) NOT NULL,
    TenPhongHoc VARCHAR2(100 CHAR) NOT NULL,
    DiaChi VARCHAR2(255 CHAR) NOT NULL,
    CONSTRAINT PK_PhongHoc PRIMARY KEY (IdPhongHoc)
);

-- Tạo bảng Chương Trình Học
CREATE TABLE ChuongTrinhHoc (
    IdChuongTrinhHoc VARCHAR2(100 CHAR) NOT NULL,
    TenChuongTrinhHoc VARCHAR2(100 CHAR) NOT NULL,
    CONSTRAINT PK_ChuongTrinhHoc PRIMARY KEY (IdChuongTrinhHoc)
);

-- Tạo bảng Khoa
CREATE TABLE Khoa (
    IdKhoa VARCHAR2(100 CHAR) NOT NULL,
    TenKhoa VARCHAR2(100 CHAR) NOT NULL,
    CONSTRAINT PK_Khoa PRIMARY KEY (IdKhoa)
);

-- Tạo bảng Thời Gian
CREATE TABLE ThoiGian (
    IdThoiGian VARCHAR2(100 CHAR) NOT NULL,
    NgayBatDau DATE NOT NULL,
    NgayKetThuc DATE NOT NULL,
    IdPhongHoc VARCHAR2(100 CHAR) NOT NULL,
    CONSTRAINT PK_ThoiGian PRIMARY KEY (IdThoiGian),
    CONSTRAINT FK_ThoiGian_PhongHoc_IdPhongHoc FOREIGN KEY (IdPhongHoc) REFERENCES PhongHoc (IdPhongHoc)
);

-- Tạo bảng Sinh Viên
CREATE TABLE SinhVien (
    IdSinhVien VARCHAR2(100 CHAR) NOT NULL,
    HoTen VARCHAR2(100 CHAR) NOT NULL,
    Lop VARCHAR2(50 CHAR) NOT NULL,
    NgaySinh DATE NOT NULL,
    DiaChi VARCHAR2(255 CHAR) NULL,
    IdChuongTrinhHoc VARCHAR2(100 CHAR) NOT NULL,
    IdKhoa VARCHAR2(100 CHAR) NOT NULL,
    CONSTRAINT PK_SinhVien PRIMARY KEY (IdSinhVien),
    CONSTRAINT FK_SinhVien_ChuongTrinhHoc_IdChuongTrinhHoc FOREIGN KEY (IdChuongTrinhHoc) REFERENCES ChuongTrinhHoc (IdChuongTrinhHoc),
    CONSTRAINT FK_SinhVien_Khoa_IdKhoa FOREIGN KEY (IdKhoa) REFERENCES Khoa (IdKhoa)
);

-- Tạo bảng Giáo Viên
CREATE TABLE GiaoVien (
    IdGiaoVien VARCHAR2(100 CHAR) NOT NULL,
    TenGiaoVien VARCHAR2(100 CHAR) NOT NULL,
    Email VARCHAR2(100 CHAR) NULL,
    SoDienThoai VARCHAR2(15 CHAR) NULL,
    IdKhoa VARCHAR2(100 CHAR) NOT NULL,
    CONSTRAINT PK_GiaoVien PRIMARY KEY (IdGiaoVien),
    CONSTRAINT FK_GiaoVien_Khoa_IdKhoa FOREIGN KEY (IdKhoa) REFERENCES Khoa (IdKhoa)
);

-- Tạo bảng Môn Học
CREATE TABLE MonHoc (
    IdMonHoc VARCHAR2(100 CHAR) NOT NULL,
    TenMonHoc VARCHAR2(100 CHAR) NOT NULL,
    IdKhoa VARCHAR2(100 CHAR) NOT NULL,
    CONSTRAINT PK_MonHoc PRIMARY KEY (IdMonHoc),
    CONSTRAINT FK_MonHoc_Khoa_IdKhoa FOREIGN KEY (IdKhoa) REFERENCES Khoa (IdKhoa)
);

-- Tạo bảng Lớp Học Phần
CREATE TABLE LopHocPhan (
    IdLopHocPhan VARCHAR2(100 CHAR) NOT NULL,
    TenHocPhan VARCHAR2(100 CHAR) NOT NULL,
    IdGiaoVien VARCHAR2(100 CHAR) NOT NULL,
    IdMonHoc VARCHAR2(100 CHAR) NOT NULL,
    SoTinChi INT NOT NULL,
    SoTietHoc INT NOT NULL,
    ThoiGianBatDau Date NOT NULL,
    ThoiGianKetThuc Date NOT NULL,
    CONSTRAINT PK_LopHocPhan PRIMARY KEY (IdLopHocPhan),
    CONSTRAINT FK_LopHocPhan_GiaoVien_IdGiaoVien FOREIGN KEY (IdGiaoVien) REFERENCES GiaoVien (IdGiaoVien),
    CONSTRAINT FK_LopHocPhan_MonHoc_IdMonHoc FOREIGN KEY (IdMonHoc) REFERENCES MonHoc (IdMonHoc)
);

-- Tạo bảng liên kết Chương Trình Học - Môn Học
CREATE TABLE ChuongTrinhHoc_MonHoc (
    IdChuongTrinhHocMonHoc VARCHAR2(100 CHAR) NOT NULL,
    IdChuongTrinhHoc VARCHAR2(100 CHAR) NOT NULL,
    IdMonHoc VARCHAR2(100 CHAR) NOT NULL,
    CONSTRAINT PK_ChuongTrinhHoc_MonHoc PRIMARY KEY (IdChuongTrinhHocMonHoc),
    CONSTRAINT FK_ChuongTrinhHoc_MonHoc_ChuongTrinhHoc_IdChuongTrinhHoc FOREIGN KEY (IdChuongTrinhHoc) REFERENCES ChuongTrinhHoc (IdChuongTrinhHoc),
    CONSTRAINT FK_ChuongTrinhHoc_MonHoc_MonHoc_IdMonHoc FOREIGN KEY (IdMonHoc) REFERENCES MonHoc (IdMonHoc)
);

-- Tạo bảng Điểm
CREATE TABLE Diem (
    IdDiem VARCHAR2(100 CHAR) NOT NULL,
    IdLopHocPhan VARCHAR2(100 CHAR) NOT NULL,
    IdSinhVien VARCHAR2(100 CHAR) NOT NULL,
    DiemQuaTrinh DECIMAL(5,2) NOT NULL,
    DiemKetThuc DECIMAL(5,2) NOT NULL,
    DiemTongKet DECIMAL(5,2) NOT NULL,
    LanHoc INT NOT NULL,
    CONSTRAINT PK_Diem PRIMARY KEY (IdDiem),
    CONSTRAINT FK_Diem_LopHocPhan_IdLopHocPhan FOREIGN KEY (IdLopHocPhan) REFERENCES LopHocPhan (IdLopHocPhan),
    CONSTRAINT FK_Diem_SinhVien_IdSinhVien FOREIGN KEY (IdSinhVien) REFERENCES SinhVien (IdSinhVien)
);

-- Tạo bảng Sinh Viên - Lớp Học Phần
CREATE TABLE SinhVien_LopHocPhan (
    IdSinhVienLopHP VARCHAR2(100 CHAR) NOT NULL,
    IdSinhVien VARCHAR2(100 CHAR) NOT NULL,
    IdLopHocPhan VARCHAR2(100 CHAR) NOT NULL,
    CONSTRAINT PK_SinhVien_LopHocPhan PRIMARY KEY (IdSinhVienLopHP),
    CONSTRAINT FK_SinhVien_LopHocPhan_LopHocPhan_IdLopHocPhan FOREIGN KEY (IdLopHocPhan) REFERENCES LopHocPhan (IdLopHocPhan),
    CONSTRAINT FK_SinhVien_LopHocPhan_SinhVien_IdSinhVien FOREIGN KEY (IdSinhVien) REFERENCES SinhVien (IdSinhVien)
);

-- Tạo bảng Thời Gian - Lớp Học Phần
CREATE TABLE ThoiGian_LopHocPhan (
    IdThoiGianLopHocPhan VARCHAR2(100 CHAR) NOT NULL,
    IdLopHocPhan VARCHAR2(100 CHAR) NOT NULL,
    IdThoiGian VARCHAR2(100 CHAR) NOT NULL,
    CONSTRAINT PK_ThoiGian_LopHocPhan PRIMARY KEY (IdThoiGianLopHocPhan),
    CONSTRAINT FK_ThoiGian_LopHocPhan_LopHocPhan_IdLopHocPhan FOREIGN KEY (IdLopHocPhan) REFERENCES LopHocPhan (IdLopHocPhan),
    CONSTRAINT FK_ThoiGian_LopHocPhan_ThoiGian_IdThoiGian FOREIGN KEY (IdThoiGian) REFERENCES ThoiGian (IdThoiGian)
);

-- Tạo bảng Đăng Ký Nguyện Vọng
CREATE TABLE DangKyNguyenVong (
    IdDangKyNguyenVong VARCHAR2(100 CHAR) NOT NULL,
    IdSinhVien VARCHAR2(100 CHAR) NOT NULL,
    IdMonHoc VARCHAR2(100 CHAR) NOT NULL,
    TrangThai INT DEFAULT -1 NOT NULL,
    CONSTRAINT PK_DangKyNguyenVong PRIMARY KEY (IdDangKyNguyenVong),
    CONSTRAINT FK_DangKyNguyenVong_MonHoc_IdMonHoc FOREIGN KEY (IdMonHoc) REFERENCES MonHoc (IdMonHoc) ON DELETE CASCADE,
    CONSTRAINT FK_DangKyNguyenVong_SinhVien_IdSinhVien FOREIGN KEY (IdSinhVien) REFERENCES SinhVien (IdSinhVien) ON DELETE CASCADE
);

-- Tạo bảng Đăng Ký Đổi Lịch
CREATE TABLE DangKyDoiLich (
    IdDangKyDoiLich VARCHAR2(100 CHAR) NOT NULL,
    IdThoiGian VARCHAR2(100 CHAR) NOT NULL,
    ThoiGianBatDauHienTai DATE NOT NULL,
    ThoiGianKetThucHienTai DATE NOT NULL,
    ThoiGianBatDauMoi Date NOT NULL,
    ThoiGianKetThucMoi DATE NOT NULL,
    TrangThai INT DEFAULT -1 NOT NULL,
    CONSTRAINT PK_DangKyDoiLich PRIMARY KEY (IdDangKyDoiLich),
    CONSTRAINT FK_DangKyDoiLich_ThoiGian_IdThoiGian FOREIGN KEY (IdThoiGian) REFERENCES ThoiGian (IdThoiGian) ON DELETE CASCADE
);

-- Tạo Index cho các bảng để tối ưu hóa tìm kiếm
-- Tạo Index cho bảng Phòng Học
CREATE INDEX IX_PhongHoc_TenPhongHoc ON PhongHoc (TenPhongHoc);
CREATE INDEX IX_PhongHoc_DiaChi ON PhongHoc (DiaChi);

-- Tạo Index cho bảng Chương Trình Học
CREATE INDEX IX_ChuongTrinhHoc_TenChuongTrinhHoc ON ChuongTrinhHoc (TenChuongTrinhHoc);

-- Tạo Index cho bảng Khoa
CREATE INDEX IX_Khoa_TenKhoa ON Khoa (TenKhoa);

-- Tạo Index cho bảng Thời Gian
CREATE INDEX IX_ThoiGian_NgayBatDau ON ThoiGian (NgayBatDau);
CREATE INDEX IX_ThoiGian_NgayKetThuc ON ThoiGian (NgayKetThuc);

-- Tạo Index cho bảng Sinh Viên
CREATE INDEX IX_SinhVien_HoTen ON SinhVien (HoTen);
CREATE INDEX IX_SinhVien_Lop ON SinhVien (Lop);
CREATE INDEX IX_SinhVien_NgaySinh ON SinhVien (NgaySinh);
CREATE INDEX IX_SinhVien_DiaChi ON SinhVien (DiaChi);

-- Tạo Index cho bảng Giáo Viên
CREATE INDEX IX_GiaoVien_TenGiaoVien ON GiaoVien (TenGiaoVien);
CREATE INDEX IX_GiaoVien_Email ON GiaoVien (Email);
CREATE INDEX IX_GiaoVien_SoDienThoai ON GiaoVien (SoDienThoai);

-- Tạo Index cho bảng Môn Học
CREATE INDEX IX_MonHoc_TenMonHoc ON MonHoc (TenMonHoc);

-- Tạo Index cho bảng Lớp Học Phần
CREATE INDEX IX_LopHocPhan_TenHocPhan ON LopHocPhan (TenHocPhan);
CREATE INDEX IX_LopHocPhan_ThoiGianBatDau ON LopHocPhan (ThoiGianBatDau);
CREATE INDEX IX_LopHocPhan_ThoiGianKetThuc ON LopHocPhan (ThoiGianKetThuc);

-- Tạo Index cho bảng Điểm
CREATE INDEX IX_Diem_DiemQuaTrinh ON Diem (DiemQuaTrinh);
CREATE INDEX IX_Diem_DiemKetThuc ON Diem (DiemKetThuc);
CREATE INDEX IX_Diem_DiemTongKet ON Diem (DiemTongKet);
CREATE INDEX IX_Diem_LanHoc ON Diem (LanHoc);

-- Tạo Index cho bảng Sinh Viên - Lớp Học Phần
CREATE INDEX IX_SinhVien_LopHocPhan ON SinhVien_LopHocPhan (IdSinhVien, IdLopHocPhan);

-- Tạo Index cho bảng Thời Gian - Lớp Học Phần
CREATE INDEX IX_ThoiGian_LopHocPhan ON ThoiGian_LopHocPhan (IdLopHocPhan, IdThoiGian);

-- Tạo Index cho bảng Đăng Ký Nguyện Vọng
CREATE INDEX IX_DangKyNguyenVong_TrangThai ON DangKyNguyenVong (TrangThai);

-- Tạo Index cho bảng Đăng Ký Đổi Lịch
CREATE INDEX IX_DangKyDoiLich_ThoiGianBatDauHienTai ON DangKyDoiLich (ThoiGianBatDauHienTai);
CREATE INDEX IX_DangKyDoiLich_ThoiGianKetThucHienTai ON DangKyDoiLich (ThoiGianKetThucHienTai);
CREATE INDEX IX_DangKyDoiLich_ThoiGianBatDauMoi ON DangKyDoiLich (ThoiGianBatDauMoi);
CREATE INDEX IX_DangKyDoiLich_ThoiGianKetThucMoi ON DangKyDoiLich (ThoiGianKetThucMoi);
CREATE INDEX IX_DangKyDoiLich_TrangThai ON DangKyDoiLich (TrangThai);

