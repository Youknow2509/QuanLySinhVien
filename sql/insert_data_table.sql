-- Chuong trinh hoc
INSERT INTO ChuongTrinhHoc (IdChuongTrinhHoc, TenChuongTrinhHoc) VALUES ('clc_cntt', 'CLC Công nghệ thông tin');
INSERT INTO ChuongTrinhHoc (IdChuongTrinhHoc, TenChuongTrinhHoc) VALUES ('cntt', 'Công nghệ thông tin');
INSERT INTO ChuongTrinhHoc (IdChuongTrinhHoc, TenChuongTrinhHoc) VALUES ('khmt', 'Khoa học máy tính');
INSERT INTO ChuongTrinhHoc (IdChuongTrinhHoc, TenChuongTrinhHoc) VALUES ('tud', 'Toán ứng dụng');
INSERT INTO ChuongTrinhHoc (IdChuongTrinhHoc, TenChuongTrinhHoc) VALUES ('lvqlccu', 'Logistics và quản lý chuỗi cung ứng');

-- Khoa
INSERT INTO Khoa (IdKhoa, TenKhoa) VALUES ('CNTT', 'Công nghệ thông tin');
INSERT INTO Khoa (IdKhoa, TenKhoa) VALUES ('KTE', 'Kinh tế');
INSERT INTO Khoa (IdKhoa, TenKhoa) VALUES ('KHCB', 'Khoa học cơ bản');
INSERT INTO Khoa (IdKhoa, TenKhoa) VALUES ('CDB', 'Cầu đường');
INSERT INTO Khoa (IdKhoa, TenKhoa) VALUES ('DTQT', 'Đào tạo quốc tế');
INSERT INTO Khoa (IdKhoa, TenKhoa) VALUES ('Triet', 'Mác - Lênin');


-- Giao Vien
INSERT INTO GiaoVien (IdGiaoVien, TenGiaoVien, Email, SoDienThoai, IdKhoa) VALUES ('dungnb01', 'Bùi Ngọc Dũng', 'dungbn@utc.edu.vn', '0915473821', 'CNTT');
INSERT INTO GiaoVien (IdGiaoVien, TenGiaoVien, Email, SoDienThoai, IdKhoa) VALUES ('luyenct01', 'Cao Thị Luyên', 'caoluyen@utc.edu.vn', '0123456789', 'CNTT');
INSERT INTO GiaoVien (IdGiaoVien, TenGiaoVien, Email, SoDienThoai, IdKhoa) VALUES ('dunglm01', 'Lại Mạnh Dũng', 'dunglm@utc.edu.vn', '0987654321', 'CNTT');
INSERT INTO GiaoVien (IdGiaoVien, TenGiaoVien, Email, SoDienThoai, IdKhoa) VALUES ('thaontt01', 'Nguyễn Thị Thanh Thảo', 'thao@utc.edu.vn', '0123456789', 'CNTT');
INSERT INTO GiaoVien (IdGiaoVien, TenGiaoVien, Email, SoDienThoai, IdKhoa) VALUES ('huongntt01', 'Nguyễn Thị Thu Hường', 'huongtt@utc.edu.vn', '0961423789', 'CNTT');
INSERT INTO GiaoVien (IdGiaoVien, TenGiaoVien, Email, SoDienThoai, IdKhoa) VALUES ('hieunt01', 'Nguyễn Trần Hiếu', 'hieunt@utc.edu.vn', '0942365478', 'CNTT');
INSERT INTO GiaoVien (IdGiaoVien, TenGiaoVien, Email, SoDienThoai, IdKhoa) VALUES ('duongnd01', 'Nguyễn Đình Dương', 'duongnd@utc.edu.vn', '0978332467', 'CNTT');
INSERT INTO GiaoVien (IdGiaoVien, TenGiaoVien, Email, SoDienThoai, IdKhoa) VALUES ('phongpd01', 'Phạm Đình Phong', 'phongnd@utc.edu.vn', '0985432167', 'CNTT');
INSERT INTO GiaoVien (IdGiaoVien, TenGiaoVien, Email, SoDienThoai, IdKhoa) VALUES ('tichpx01', 'Phạm Xuân Tích', 'tichpx@utc.edu.vn', '0987654321', 'CNTT');
INSERT INTO GiaoVien (IdGiaoVien, TenGiaoVien, Email, SoDienThoai, IdKhoa) VALUES ('tungdc01', 'Đinh Công Tùng', 'tungdc@utc.edu.vn', '0935421789', 'CNTT');
INSERT INTO GiaoVien (IdGiaoVien, TenGiaoVien, Email, SoDienThoai, IdKhoa) VALUES ('thuydtl01', 'Đào Thị Lệ Thủy', 'thuydt@utc.edu.vn', '0921374856', 'CNTT');

-- Mon Hoc
INSERT INTO MonHoc (IdMonHoc, TenMonHoc, IdKhoa) VALUES ('HQTCSDL', 'Hệ quản trị cơ sở dữ liệu Oracle', 'CNTT');
INSERT INTO MonHoc (IdMonHoc, TenMonHoc, IdKhoa) VALUES ('LTTT', 'Lập trình trực quan', 'CNTT');
INSERT INTO MonHoc (IdMonHoc, TenMonHoc, IdKhoa) VALUES ('LTW', 'Lập trình Web', 'CNTT');
INSERT INTO MonHoc (IdMonHoc, TenMonHoc, IdKhoa) VALUES ('LSDCSVN', 'Lịch sử Đảng Cộng sản Việt Nam', 'CNTT');
INSERT INTO MonHoc (IdMonHoc, TenMonHoc, IdKhoa) VALUES ('MMT', 'Mạng máy tính', 'CNTT');
INSERT INTO MonHoc (IdMonHoc, TenMonHoc, IdKhoa) VALUES ('PTTKYC', 'Phân tích thiết kế yêu cầu', 'CNTT');
INSERT INTO MonHoc (IdMonHoc, TenMonHoc, IdKhoa) VALUES ('TTUD', 'Thuật toán và ứng dụng', 'CNTT');
INSERT INTO MonHoc (IdMonHoc, TenMonHoc, IdKhoa) VALUES ('XSTK', 'Xác suất thống kê', 'KHCB');
INSERT INTO MonHoc (IdMonHoc, TenMonHoc, IdKhoa) VALUES ('VLY', 'Vật Lý', 'KHCB');
INSERT INTO MonHoc (IdMonHoc, TenMonHoc, IdKhoa) VALUES ('PLDC', 'Pháp luật đại cương', 'Triet');

-- Chuong trinh hoc - Mon hoc
INSERT INTO ChuongTrinhHoc_MonHoc (IdChuongTrinhHocMonHoc, IdChuongTrinhHoc, IdMonHoc)
SELECT 
    SYS_GUID(), 
    (SELECT IdChuongTrinhHoc 
        FROM ChuongTrinhHoc 
        WHERE TenChuongTrinhHoc = N'Công nghệ thông tin'
    ),
    IdMonHoc
FROM MonHoc;

INSERT INTO ChuongTrinhHoc_MonHoc (IdChuongTrinhHocMonHoc, IdChuongTrinhHoc, IdMonHoc)
SELECT 
    SYS_GUID(), 
    (SELECT IdChuongTrinhHoc 
        FROM ChuongTrinhHoc 
        WHERE TenChuongTrinhHoc = N'CLC Công nghệ thông tin'),
    IdMonHoc
FROM MonHoc;

-- Sinh Vien
INSERT INTO SinhVien (IdSinhVien, HoTen, Lop, NgaySinh, DiaChi, IdChuongTrinhHoc, IdKhoa)
VALUES (
    '222631159', N'Lý Trần Vinh', N'Công nghệ thông tin Việt Anh 1', TO_DATE('2004-05-12', 'YYYY-MM-DD'), N'Hà Nội',
    (SELECT IdChuongTrinhHoc FROM ChuongTrinhHoc WHERE TenChuongTrinhHoc = N'CLC Công nghệ thông tin'),
    (SELECT IdKhoa FROM Khoa WHERE TenKhoa = N'Đào tạo quốc tế')
);

INSERT INTO SinhVien (IdSinhVien, HoTen, Lop, NgaySinh, DiaChi, IdChuongTrinhHoc, IdKhoa)
VALUES (
    '222631111', N'Lê Văn Thuận', N'Công nghệ thông tin Việt Anh 1', TO_DATE('2004-10-01', 'YYYY-MM-DD'), N'Hà Nội',
    (SELECT IdChuongTrinhHoc FROM ChuongTrinhHoc WHERE TenChuongTrinhHoc = N'CLC Công nghệ thông tin'),
    (SELECT IdKhoa FROM Khoa WHERE TenKhoa = N'Công nghệ thông tin')
);

INSERT INTO SinhVien (IdSinhVien, HoTen, Lop, NgaySinh, DiaChi, IdChuongTrinhHoc, IdKhoa)
VALUES (
    '222631160', N'Lê Xuân An', N'Công nghệ thông tin 1', TO_DATE('2002-02-15', 'YYYY-MM-DD'), N'Hải Phòng',
    (SELECT IdChuongTrinhHoc FROM ChuongTrinhHoc WHERE TenChuongTrinhHoc = N'Công nghệ thông tin'),
    (SELECT IdKhoa FROM Khoa WHERE TenKhoa = N'Công nghệ thông tin')
);

INSERT INTO SinhVien (IdSinhVien, HoTen, Lop, NgaySinh, DiaChi, IdChuongTrinhHoc, IdKhoa)
VALUES (
    '222631161', N'Pham Thi J', N'Công nghệ thông tin 3', TO_DATE('2004-07-07', 'YYYY-MM-DD'), N'Hà Nội',
    (SELECT IdChuongTrinhHoc FROM ChuongTrinhHoc WHERE TenChuongTrinhHoc = N'Công nghệ thông tin'),
    (SELECT IdKhoa FROM Khoa WHERE TenKhoa = N'Công nghệ thông tin')
);

INSERT INTO SinhVien (IdSinhVien, HoTen, Lop, NgaySinh, DiaChi, IdChuongTrinhHoc, IdKhoa)
VALUES (
    '222631162', N'Nguyen Van A', N'Công nghệ thông tin 2', TO_DATE('2003-09-09', 'YYYY-MM-DD'), N'Hà Nội',
    (SELECT IdChuongTrinhHoc FROM ChuongTrinhHoc WHERE TenChuongTrinhHoc = N'Công nghệ thông tin'),
    (SELECT IdKhoa FROM Khoa WHERE TenKhoa = N'Công nghệ thông tin')
);

-- Tạo các lớp học phần với Id và tên giáo viên chỉ định
INSERT INTO LopHocPhan (IdLopHocPhan, TenHocPhan, IdGiaoVien, IdMonHoc, ThoiGianBatDau, ThoiGianKetThuc, SoTinChi, SoTietHoc)
VALUES ('HTQTORCL_QT01', N'Hệ quản trị cơ sở dữ liệu Oracle-1-1-24(QT01)', 'dungnb01', 'HQTCSDL', TO_DATE('2024-08-12', 'YYYY-MM-DD'), TO_DATE('2024-11-17', 'YYYY-MM-DD'), 3, 45);

INSERT INTO LopHocPhan (IdLopHocPhan, TenHocPhan, IdGiaoVien, IdMonHoc, ThoiGianBatDau, ThoiGianKetThuc, SoTinChi, SoTietHoc)
VALUES ('LTTT_QT01', N'Lập trình trực quan-1-1-24(QT01)', 'huongntt01', 'LTTT', TO_DATE('2024-08-12', 'YYYY-MM-DD'), TO_DATE('2024-11-17', 'YYYY-MM-DD'), 3, 45);

INSERT INTO LopHocPhan (IdLopHocPhan, TenHocPhan, IdGiaoVien, IdMonHoc, ThoiGianBatDau, ThoiGianKetThuc, SoTinChi, SoTietHoc)
VALUES ('LTW_QT01', N'Lập trình Web-1-1-24(QT01)', 'dunglm01', 'LTW', TO_DATE('2024-08-12', 'YYYY-MM-DD'), TO_DATE('2024-11-17', 'YYYY-MM-DD'), 3, 45);

INSERT INTO LopHocPhan (IdLopHocPhan, TenHocPhan, IdGiaoVien, IdMonHoc, ThoiGianBatDau, ThoiGianKetThuc, SoTinChi, SoTietHoc)
VALUES ('LSDCSVN_QT05', N'Lịch sử Đảng Cộng sản Việt Nam-1-1-24(QT05)', 'thaontt01', 'LSDCSVN', TO_DATE('2024-08-12', 'YYYY-MM-DD'), TO_DATE('2024-11-17', 'YYYY-MM-DD'), 3, 45);

INSERT INTO LopHocPhan (IdLopHocPhan, TenHocPhan, IdGiaoVien, IdMonHoc, ThoiGianBatDau, ThoiGianKetThuc, SoTinChi, SoTietHoc)
VALUES ('MMT_QT01', N'Mạng máy tính-1-1-24(QT01)', 'hieunt01', 'MMT', TO_DATE('2024-08-12', 'YYYY-MM-DD'), TO_DATE('2024-11-17', 'YYYY-MM-DD'), 3, 45);

INSERT INTO LopHocPhan (IdLopHocPhan, TenHocPhan, IdGiaoVien, IdMonHoc, ThoiGianBatDau, ThoiGianKetThuc, SoTinChi, SoTietHoc)
VALUES ('PTTKYC_QT01', N'Phân tích thiết kế yêu cầu-1-1-24(QT01)', 'thuydtl01', 'PTTKYC', TO_DATE('2024-08-12', 'YYYY-MM-DD'), TO_DATE('2024-11-17', 'YYYY-MM-DD'), 3, 45);

INSERT INTO LopHocPhan (IdLopHocPhan, TenHocPhan, IdGiaoVien, IdMonHoc, ThoiGianBatDau, ThoiGianKetThuc, SoTinChi, SoTietHoc)
VALUES ('TTUD_QT01', N'Thuật toán và ứng dụng-1-1-24(QT01)', 'tichpx01', 'TTUD', TO_DATE('2024-08-12', 'YYYY-MM-DD'), TO_DATE('2024-11-17', 'YYYY-MM-DD'), 3, 45);

INSERT INTO LopHocPhan (IdLopHocPhan, TenHocPhan, IdGiaoVien, IdMonHoc, ThoiGianBatDau, ThoiGianKetThuc, SoTinChi, SoTietHoc)
VALUES ('XSTK_QT01', N'Xác suất thống kê-1-1-24(QT01)', 'phongpd01', 'XSTK', TO_DATE('2024-08-12', 'YYYY-MM-DD'), TO_DATE('2024-11-17', 'YYYY-MM-DD'), 3, 45);

INSERT INTO LopHocPhan (IdLopHocPhan, TenHocPhan, IdGiaoVien, IdMonHoc, ThoiGianBatDau, ThoiGianKetThuc, SoTinChi, SoTietHoc)
VALUES ('VLY_QT01', N'Vật Lý-1-1-24(QT)', 'tungdc01', 'VLY', TO_DATE('2024-04-08', 'YYYY-MM-DD'), TO_DATE('2024-07-14', 'YYYY-MM-DD'), 3, 45);

INSERT INTO LopHocPhan (IdLopHocPhan, TenHocPhan, IdGiaoVien, IdMonHoc, ThoiGianBatDau, ThoiGianKetThuc, SoTinChi, SoTietHoc)
VALUES ('PLDC_QT01', N'Pháp luật đại cương-1-1-24(QT01)', 'duongnd01', 'PLDC', TO_DATE('2024-12-02', 'YYYY-MM-DD'), TO_DATE('2025-01-20', 'YYYY-MM-DD'), 3, 4);

-- Thêm sinh viên vào các lớp học phần dựa trên dữ liệu có sẵn
INSERT INTO SinhVien_LopHocPhan (IdSinhVienLopHocPhan, IdSinhVien, IdLopHocPhan)
VALUES (SYS_GUID(), (SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Lý Trần Vinh'), 'LSDCSVN_QT05');

INSERT INTO SinhVien_LopHocPhan (IdSinhVienLopHocPhan, IdSinhVien, IdLopHocPhan)
VALUES (SYS_GUID(), (SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Lý Trần Vinh'), 'LTW_QT01');

INSERT INTO SinhVien_LopHocPhan (IdSinhVienLopHocPhan, IdSinhVien, IdLopHocPhan)
VALUES (SYS_GUID(), (SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Lý Trần Vinh'), 'LTTT_QT01');

INSERT INTO SinhVien_LopHocPhan (IdSinhVienLopHocPhan, IdSinhVien, IdLopHocPhan)
VALUES (SYS_GUID(), (SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Lý Trần Vinh'), 'LSDCSVN_QT05');

INSERT INTO SinhVien_LopHocPhan (IdSinhVienLopHocPhan, IdSinhVien, IdLopHocPhan)
VALUES (SYS_GUID(), (SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Lý Trần Vinh'), 'MMT_QT01');

INSERT INTO SinhVien_LopHocPhan (IdSinhVienLopHocPhan, IdSinhVien, IdLopHocPhan)
VALUES (SYS_GUID(), (SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Lý Trần Vinh'), 'PTTKYC_QT01');

INSERT INTO SinhVien_LopHocPhan (IdSinhVienLopHocPhan, IdSinhVien, IdLopHocPhan)
VALUES (SYS_GUID(), (SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Lý Trần Vinh'), 'TTUD_QT01');

INSERT INTO SinhVien_LopHocPhan (IdSinhVienLopHocPhan, IdSinhVien, IdLopHocPhan)
VALUES (SYS_GUID(), (SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Lý Trần Vinh'), 'XSTK_QT01');

INSERT INTO SinhVien_LopHocPhan (IdSinhVienLopHocPhan, IdSinhVien, IdLopHocPhan)
VALUES (SYS_GUID(), (SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Lý Trần Vinh'), 'VLY_QT01');

INSERT INTO SinhVien_LopHocPhan (IdSinhVienLopHocPhan, IdSinhVien, IdLopHocPhan)
VALUES (SYS_GUID(), (SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Lê Văn Thuận'), 'XSTK_QT01');

INSERT INTO SinhVien_LopHocPhan (IdSinhVienLopHocPhan, IdSinhVien, IdLopHocPhan)
VALUES (SYS_GUID(), (SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Lê Văn Thuận'), 'MMT_QT01');

INSERT INTO SinhVien_LopHocPhan (IdSinhVienLopHocPhan, IdSinhVien, IdLopHocPhan)
VALUES (SYS_GUID(), (SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Pham Thi J'), 'TTUD_QT01');

INSERT INTO SinhVien_LopHocPhan (IdSinhVienLopHocPhan, IdSinhVien, IdLopHocPhan)
VALUES (SYS_GUID(), (SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Lê Văn Thuận'), 'PTTKYC_QT01');

INSERT INTO SinhVien_LopHocPhan (IdSinhVienLopHocPhan, IdSinhVien, IdLopHocPhan)
VALUES (SYS_GUID(), (SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Lê Văn Thuận'), 'TTUD_QT01');

INSERT INTO SinhVien_LopHocPhan (IdSinhVienLopHocPhan, IdSinhVien, IdLopHocPhan)
VALUES (SYS_GUID(), (SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Pham Thi J'), 'XSTK_QT01');

-- Thêm điểm cho sinh viên vào các lớp học phần dựa trên dữ liệu có sẵn
INSERT INTO Diem (IdDiem, IdLopHocPhan, IdSinhVien, DiemQuaTrinh, DiemKetThuc, DiemTongKet, LanHoc) VALUES (SYS_GUID(), 'HTQTORCL_QT01', '222631159', 0, 0, 0, 1);
INSERT INTO Diem (IdDiem, IdLopHocPhan, IdSinhVien, DiemQuaTrinh, DiemKetThuc, DiemTongKet, LanHoc) VALUES (SYS_GUID(), 'LTTT_QT01', '222631159', 0, 0, 0, 1);
INSERT INTO Diem (IdDiem, IdLopHocPhan, IdSinhVien, DiemQuaTrinh, DiemKetThuc, DiemTongKet, LanHoc) VALUES (SYS_GUID(), 'LTW_QT01', '222631159', 0, 0, 0, 1);
INSERT INTO Diem (IdDiem, IdLopHocPhan, IdSinhVien, DiemQuaTrinh, DiemKetThuc, DiemTongKet, LanHoc) VALUES (SYS_GUID(), 'LSDCSVN_QT05', '222631159', 0, 0, 0, 1);
INSERT INTO Diem (IdDiem, IdLopHocPhan, IdSinhVien, DiemQuaTrinh, DiemKetThuc, DiemTongKet, LanHoc) VALUES (SYS_GUID(), 'MMT_QT01', '222631159', 0, 0, 0, 1);
INSERT INTO Diem (IdDiem, IdLopHocPhan, IdSinhVien, DiemQuaTrinh, DiemKetThuc, DiemTongKet, LanHoc) VALUES (SYS_GUID(), 'PTTKYC_QT01', '222631159', 0, 0, 0, 1);
INSERT INTO Diem (IdDiem, IdLopHocPhan, IdSinhVien, DiemQuaTrinh, DiemKetThuc, DiemTongKet, LanHoc) VALUES (SYS_GUID(), 'TTUD_QT01', '222631159', 0, 0, 0, 1);
INSERT INTO Diem (IdDiem, IdLopHocPhan, IdSinhVien, DiemQuaTrinh, DiemKetThuc, DiemTongKet, LanHoc) VALUES (SYS_GUID(), 'XSTK_QT01', '222631159', 0, 0, 0, 1);
INSERT INTO Diem (IdDiem, IdLopHocPhan, IdSinhVien, DiemQuaTrinh, DiemKetThuc, DiemTongKet, LanHoc) VALUES (SYS_GUID(), 'VLY_QT01', '222631159', 2, 4, 3, 1);

-- Tạo Phòng Học
INSERT INTO PhongHoc (IdPhongHoc, TenPhongHoc, DiaChi)
VALUES ('503A8', 'Phòng 503A8', 'Tầng 5, Tòa nhà A8');

INSERT INTO PhongHoc (IdPhongHoc, TenPhongHoc, DiaChi)
VALUES ('202A8', 'Phòng 202A8', 'Tầng 2, Tòa nhà A8');

INSERT INTO PhongHoc (IdPhongHoc, TenPhongHoc, DiaChi)
VALUES ('105A8', 'Phòng 105A8', 'Tầng 1, Tòa nhà A8');

INSERT INTO PhongHoc (IdPhongHoc, TenPhongHoc, DiaChi)
VALUES ('301A3', 'Phòng 301A3', 'Tầng 3, Tòa nhà A3');

INSERT INTO PhongHoc (IdPhongHoc, TenPhongHoc, DiaChi)
VALUES ('305A2', 'Phòng 305A2', 'Tầng 3, Tòa nhà A2');

INSERT INTO PhongHoc (IdPhongHoc, TenPhongHoc, DiaChi)
VALUES ('405A4', 'Phòng 405A4', 'Tầng 4, Tòa nhà A4');

-- Tạo thời gian và gắn thời gian vào lớp học phần
-- Thêm các thời gian vào bảng ThoiGian và gắn vào lớp học phần
--insert bang thoigian
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('HTQTORCL_QT01_B1', TO_DATE('2024-08-14 15:35:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-08-14 18:00:00', 'YYYY-MM-DD HH24:MI:SS'), '503A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian) 
    VALUES (SYS_GUID(), 'HTQTORCL_QT01', 'HTQTORCL_QT01_B1');

-- Th?i gian ngày 2024-08-21
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('HTQTORCL_QT01_B2', TO_DATE('2024-08-21 15:35:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-08-21 18:00:00', 'YYYY-MM-DD HH24:MI:SS'), '503A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian) 
    VALUES (SYS_GUID(), 'HTQTORCL_QT01', 'HTQTORCL_QT01_B2');

-- Th?i gian ngày 2024-09-04
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('HTQTORCL_QT01_B3', TO_DATE('2024-09-04 15:35:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-09-04 18:00:00', 'YYYY-MM-DD HH24:MI:SS'), '503A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian) 
    VALUES (SYS_GUID(), 'HTQTORCL_QT01', 'HTQTORCL_QT01_B3');

-- Th?i gian ngày 2024-09-11
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('HTQTORCL_QT01_B4', TO_DATE('2024-09-11 15:35:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-09-11 18:00:00', 'YYYY-MM-DD HH24:MI:SS'), '503A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian) 
    VALUES (SYS_GUID(), 'HTQTORCL_QT01', 'HTQTORCL_QT01_B4');

-- Th?i gian ngày 2024-09-18
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('HTQTORCL_QT01_B5', TO_DATE('2024-09-18 15:35:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-09-18 18:00:00', 'YYYY-MM-DD HH24:MI:SS'), '503A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian) 
    VALUES (SYS_GUID(), 'HTQTORCL_QT01', 'HTQTORCL_QT01_B5');

-- Th?i gian ngày 2024-09-25
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('HTQTORCL_QT01_B6', TO_DATE('2024-09-25 15:35:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-09-25 18:00:00', 'YYYY-MM-DD HH24:MI:SS'), '503A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian) 
    VALUES (SYS_GUID(), 'HTQTORCL_QT01', 'HTQTORCL_QT01_B6');

-- Th?i gian ngày 2024-10-02
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('HTQTORCL_QT01_B7', TO_DATE('2024-10-02 15:35:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-10-02 18:00:00', 'YYYY-MM-DD HH24:MI:SS'), '503A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian) 
    VALUES (SYS_GUID(), 'HTQTORCL_QT01', 'HTQTORCL_QT01_B7');

-- Th?i gian ngày 2024-10-09
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('HTQTORCL_QT01_B8', TO_DATE('2024-10-09 15:35:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-10-09 18:00:00', 'YYYY-MM-DD HH24:MI:SS'), '503A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian) 
    VALUES (SYS_GUID(), 'HTQTORCL_QT01', 'HTQTORCL_QT01_B8');

-- Th?i gian ngày 2024-10-16
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('HTQTORCL_QT01_B9', TO_DATE('2024-10-16 15:35:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-10-16 18:00:00', 'YYYY-MM-DD HH24:MI:SS'), '503A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian) 
    VALUES (SYS_GUID(), 'HTQTORCL_QT01', 'HTQTORCL_QT01_B9');

-- Th?i gian ngày 2024-10-23
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('HTQTORCL_QT01_B10', TO_DATE('2024-10-23 15:35:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-10-23 18:00:00', 'YYYY-MM-DD HH24:MI:SS'), '503A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian) 
    VALUES (SYS_GUID(), 'HTQTORCL_QT01', 'HTQTORCL_QT01_B10');

-- Th?i gian ngày 2024-10-30
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('HTQTORCL_QT01_B11', TO_DATE('2024-10-30 15:35:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-10-30 18:00:00', 'YYYY-MM-DD HH24:MI:SS'), '503A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian) 
    VALUES (SYS_GUID(), 'HTQTORCL_QT01', 'HTQTORCL_QT01_B11');

-- Th?i gian ngày 2024-11-06
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('HTQTORCL_QT01_B12', TO_DATE('2024-11-06 15:35:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-11-06 18:00:00', 'YYYY-MM-DD HH24:MI:SS'), '503A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian) 
    VALUES (SYS_GUID(), 'HTQTORCL_QT01', 'HTQTORCL_QT01_B12');

-- Th?i gian ngày 2024-11-13
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('HTQTORCL_QT01_B13', TO_DATE('2024-11-13 15:35:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-11-13 18:00:00', 'YYYY-MM-DD HH24:MI:SS'), '503A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian) 
    VALUES (SYS_GUID(), 'HTQTORCL_QT01', 'HTQTORCL_QT01_B13');


-- Th?i gian ngày 2024-08-13
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('LTW_QT01_B1', TO_DATE('2024-08-13 15:35:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-08-13 18:00:00', 'YYYY-MM-DD HH24:MI:SS'), '202A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'LTW_QT01', 'LTW_QT01_B1');

-- Th?i gian ngày 2024-08-20
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('LTW_QT01_B2', TO_DATE('2024-08-20 15:35:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-08-20 18:00:00', 'YYYY-MM-DD HH24:MI:SS'), '202A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'LTW_QT01', 'LTW_QT01_B2');

-- Th?i gian ngày 2024-08-27
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('LTW_QT01_B3', TO_DATE('2024-08-27 15:35:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-08-27 18:00:00', 'YYYY-MM-DD HH24:MI:SS'), '202A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'LTW_QT01', 'LTW_QT01_B3');

-- Th?i gian ngày 2024-09-03
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('LTW_QT01_B4', TO_DATE('2024-09-03 15:35:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-09-03 18:00:00', 'YYYY-MM-DD HH24:MI:SS'), '202A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'LTW_QT01', 'LTW_QT01_B4');

-- Th?i gian ngày 2024-09-10
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('LTW_QT01_B5', TO_DATE('2024-09-10 15:35:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-09-10 18:00:00', 'YYYY-MM-DD HH24:MI:SS'), '202A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'LTW_QT01', 'LTW_QT01_B5');

-- Th?i gian ngày 2024-09-17
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('LTW_QT01_B6', TO_DATE('2024-09-17 15:35:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-09-17 18:00:00', 'YYYY-MM-DD HH24:MI:SS'), '202A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'LTW_QT01', 'LTW_QT01_B6');

-- Th?i gian ngày 2024-09-24
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('LTW_QT01_B7', TO_DATE('2024-09-24 15:35:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-09-24 18:00:00', 'YYYY-MM-DD HH24:MI:SS'), '202A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'LTW_QT01', 'LTW_QT01_B7');

-- Th?i gian ngày 2024-10-01
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('LTW_QT01_B8', TO_DATE('2024-10-01 15:35:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-10-01 18:00:00', 'YYYY-MM-DD HH24:MI:SS'), '202A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'LTW_QT01', 'LTW_QT01_B8');

-- Th?i gian ngày 2024-10-08
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('LTW_QT01_B9', TO_DATE('2024-10-08 15:35:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-10-08 18:00:00', 'YYYY-MM-DD HH24:MI:SS'), '503A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'LTW_QT01', 'LTW_QT01_B9');

-- Th?i gian ngày 2024-10-15
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('LTW_QT01_B10', TO_DATE('2024-10-15 15:35:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-10-15 18:00:00', 'YYYY-MM-DD HH24:MI:SS'), '503A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'LTW_QT01', 'LTW_QT01_B10');

-- Th?i gian ngày 2024-10-22
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('LTW_QT01_B11', TO_DATE('2024-10-22 15:35:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-10-22 18:00:00', 'YYYY-MM-DD HH24:MI:SS'), '202A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'LTW_QT01', 'LTW_QT01_B11');

-- Th?i gian ngày 2024-10-29
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('LTW_QT01_B12', TO_DATE('2024-10-29 15:35:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-10-29 18:00:00', 'YYYY-MM-DD HH24:MI:SS'), '202A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'LTW_QT01', 'LTW_QT01_B12');

-- Th?i gian ngày 2024-11-05
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('LTW_QT01_B13', TO_DATE('2024-11-05 15:35:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-11-05 18:00:00', 'YYYY-MM-DD HH24:MI:SS'), '202A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'LTW_QT01', 'LTW_QT01_B13');

-- Th?i gian ngày 2024-11-12
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('LTW_QT01_B14', TO_DATE('2024-11-12 15:35:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-11-12 18:00:00', 'YYYY-MM-DD HH24:MI:SS'), '202A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'LTW_QT01', 'LTW_QT01_B14');
-- Thêm th?i gian và g?n th?i gian vào l?p h?c ph?n LTTT_QT01

-- Th?i gian ngày 2024-08-13
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('LTTT_QT01_B1', TO_DATE('2024-08-13 13:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-08-13 15:25:00', 'YYYY-MM-DD HH24:MI:SS'), '301A3');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'LTTT_QT01', 'LTTT_QT01_B1');

-- Th?i gian ngày 2024-08-20
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('LTTT_QT01_B2', TO_DATE('2024-08-20 13:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-08-20 15:25:00', 'YYYY-MM-DD HH24:MI:SS'), '301A3');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'LTTT_QT01', 'LTTT_QT01_B2');

-- Th?i gian ngày 2024-08-27
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('LTTT_QT01_B3', TO_DATE('2024-08-27 13:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-08-27 15:25:00', 'YYYY-MM-DD HH24:MI:SS'), '301A3');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'LTTT_QT01', 'LTTT_QT01_B3');

-- Th?i gian ngày 2024-09-03
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('LTTT_QT01_B4', TO_DATE('2024-09-03 13:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-09-03 15:25:00', 'YYYY-MM-DD HH24:MI:SS'), '301A3');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'LTTT_QT01', 'LTTT_QT01_B4');

-- Th?i gian ngày 2024-09-10
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('LTTT_QT01_B5', TO_DATE('2024-09-10 13:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-09-10 15:25:00', 'YYYY-MM-DD HH24:MI:SS'), '301A3');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'LTTT_QT01', 'LTTT_QT01_B5');

-- Th?i gian ngày 2024-09-17
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('LTTT_QT01_B6', TO_DATE('2024-09-17 13:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-09-17 15:25:00', 'YYYY-MM-DD HH24:MI:SS'), '301A3');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'LTTT_QT01', 'LTTT_QT01_B6');

-- Th?i gian ngày 2024-09-24
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('LTTT_QT01_B7', TO_DATE('2024-09-24 13:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-09-24 15:25:00', 'YYYY-MM-DD HH24:MI:SS'), '301A3');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'LTTT_QT01', 'LTTT_QT01_B7');

-- Th?i gian ngày 2024-10-01
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('LTTT_QT01_B8', TO_DATE('2024-10-01 13:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-10-01 15:25:00', 'YYYY-MM-DD HH24:MI:SS'), '301A3');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'LTTT_QT01', 'LTTT_QT01_B8');

-- Th?i gian ngày 2024-10-08
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('LTTT_QT01_B9', TO_DATE('2024-10-08 13:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-10-08 15:25:00', 'YYYY-MM-DD HH24:MI:SS'), '301A3');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'LTTT_QT01', 'LTTT_QT01_B9');

-- Th?i gian ngày 2024-10-15
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('LTTT_QT01_B10', TO_DATE('2024-10-15 13:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-10-15 15:25:00', 'YYYY-MM-DD HH24:MI:SS'), '301A3');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'LTTT_QT01', 'LTTT_QT01_B10');

-- Th?i gian ngày 2024-10-22
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('LTTT_QT01_B11', TO_DATE('2024-10-22 13:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-10-22 15:25:00', 'YYYY-MM-DD HH24:MI:SS'), '301A3');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'LTTT_QT01', 'LTTT_QT01_B11');

-- Th?i gian ngày 2024-10-29
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('LTTT_QT01_B12', TO_DATE('2024-10-29 13:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-10-29 15:25:00', 'YYYY-MM-DD HH24:MI:SS'), '301A3');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'LTTT_QT01', 'LTTT_QT01_B12');

-- Th?i gian ngày 2024-11-05
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('LTTT_QT01_B13', TO_DATE('2024-11-05 13:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-11-05 15:25:00', 'YYYY-MM-DD HH24:MI:SS'), '301A3');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'LTTT_QT01', 'LTTT_QT01_B13');

-- Th?i gian ngày 2024-11-12
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('LTTT_QT01_B14', TO_DATE('2024-11-12 13:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-11-12 15:25:00', 'YYYY-MM-DD HH24:MI:SS'), '301A3');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'LTTT_QT01', 'LTTT_QT01_B14');


-- Th?i gian ngày 2024-08-17
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('MMT_QT01_B1', TO_DATE('2024-08-17 13:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-08-17 15:25:00', 'YYYY-MM-DD HH24:MI:SS'), '305A2');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'MMT_QT01', 'MMT_QT01_B1');

-- Th?i gian ngày 2024-08-24
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('MMT_QT01_B2', TO_DATE('2024-08-24 13:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-08-24 15:25:00', 'YYYY-MM-DD HH24:MI:SS'), '305A2');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'MMT_QT01', 'MMT_QT01_B2');

-- Th?i gian ngày 2024-08-31
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('MMT_QT01_B3', TO_DATE('2024-08-31 13:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-08-31 15:25:00', 'YYYY-MM-DD HH24:MI:SS'), '305A2');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'MMT_QT01', 'MMT_QT01_B3');

-- Th?i gian ngày 2024-09-07
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('MMT_QT01_B4', TO_DATE('2024-09-07 13:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-09-07 15:25:00', 'YYYY-MM-DD HH24:MI:SS'), '305A2');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'MMT_QT01', 'MMT_QT01_B4');

-- Th?i gian ngày 2024-09-14
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('MMT_QT01_B5', TO_DATE('2024-09-14 13:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-09-14 15:25:00', 'YYYY-MM-DD HH24:MI:SS'), '305A2');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'MMT_QT01', 'MMT_QT01_B5');

-- Th?i gian ngày 2024-09-21
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('MMT_QT01_B6', TO_DATE('2024-09-21 13:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-09-21 15:25:00', 'YYYY-MM-DD HH24:MI:SS'), '305A2');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'MMT_QT01', 'MMT_QT01_B6');

-- Th?i gian ngày 2024-09-28
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('MMT_QT01_B7', TO_DATE('2024-09-28 13:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-09-28 15:25:00', 'YYYY-MM-DD HH24:MI:SS'), '305A2');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'MMT_QT01', 'MMT_QT01_B7');

-- Th?i gian ngày 2024-10-05
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('MMT_QT01_B8', TO_DATE('2024-10-05 13:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-10-05 15:25:00', 'YYYY-MM-DD HH24:MI:SS'), '305A2');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'MMT_QT01', 'MMT_QT01_B8');

-- Th?i gian ngày 2024-10-12
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('MMT_QT01_B9', TO_DATE('2024-10-12 13:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-10-12 15:25:00', 'YYYY-MM-DD HH24:MI:SS'), '305A2');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'MMT_QT01', 'MMT_QT01_B9');

-- Th?i gian ngày 2024-10-19
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('MMT_QT01_B10', TO_DATE('2024-10-19 13:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-10-19 15:25:00', 'YYYY-MM-DD HH24:MI:SS'), '305A2');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'MMT_QT01', 'MMT_QT01_B10');

-- Th?i gian ngày 2024-10-26
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('MMT_QT01_B11', TO_DATE('2024-10-26 13:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-10-26 15:25:00', 'YYYY-MM-DD HH24:MI:SS'), '305A2');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'MMT_QT01', 'MMT_QT01_B11');

-- Th?i gian ngày 2024-11-02
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('MMT_QT01_B12', TO_DATE('2024-11-02 13:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-11-02 15:25:00', 'YYYY-MM-DD HH24:MI:SS'), '305A2');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'MMT_QT01', 'MMT_QT01_B12');

-- Th?i gian ngày 2024-11-09
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('MMT_QT01_B13', TO_DATE('2024-11-09 13:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-11-09 15:25:00', 'YYYY-MM-DD HH24:MI:SS'), '305A2');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'MMT_QT01', 'MMT_QT01_B13');

-- Th?i gian ngày 2024-11-16
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('MMT_QT01_B14', TO_DATE('2024-11-16 13:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-11-16 15:25:00', 'YYYY-MM-DD HH24:MI:SS'), '305A2');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'MMT_QT01', 'MMT_QT01_B14');

-- Th?i gian ngày 2024-08-15
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('PTTKYC_QT01_B1', TO_DATE('2024-08-15 15:35:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-08-15 18:00:00', 'YYYY-MM-DD HH24:MI:SS'), '405A4');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'PTTKYC_QT01', 'PTTKYC_QT01_B1');

-- Th?i gian ngày 2024-08-22
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('PTTKYC_QT01_B2', TO_DATE('2024-08-22 15:35:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-08-22 18:00:00', 'YYYY-MM-DD HH24:MI:SS'), '405A4');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'PTTKYC_QT01', 'PTTKYC_QT01_B2');

-- Th?i gian ngày 2024-08-29
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('PTTKYC_QT01_B3', TO_DATE('2024-08-29 15:35:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-08-29 18:00:00', 'YYYY-MM-DD HH24:MI:SS'), '405A4');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'PTTKYC_QT01', 'PTTKYC_QT01_B3');

-- Th?i gian ngày 2024-09-05
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('PTTKYC_QT01_B4', TO_DATE('2024-09-05 15:35:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-09-05 18:00:00', 'YYYY-MM-DD HH24:MI:SS'), '405A4');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'PTTKYC_QT01', 'PTTKYC_QT01_B4');

-- Th?i gian ngày 2024-09-12
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('PTTKYC_QT01_B5', TO_DATE('2024-09-12 15:35:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-09-12 18:00:00', 'YYYY-MM-DD HH24:MI:SS'), '405A4');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'PTTKYC_QT01', 'PTTKYC_QT01_B5');

-- Th?i gian ngày 2024-09-19
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('PTTKYC_QT01_B6', TO_DATE('2024-09-19 15:35:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-09-19 18:00:00', 'YYYY-MM-DD HH24:MI:SS'), '405A4');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'PTTKYC_QT01', 'PTTKYC_QT01_B6');

-- Th?i gian ngày 2024-09-26
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('PTTKYC_QT01_B7', TO_DATE('2024-09-26 15:35:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-09-26 18:00:00', 'YYYY-MM-DD HH24:MI:SS'), '405A4');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'PTTKYC_QT01', 'PTTKYC_QT01_B7');

-- Th?i gian ngày 2024-10-03
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('PTTKYC_QT01_B8', TO_DATE('2024-10-03 15:35:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-10-03 18:00:00', 'YYYY-MM-DD HH24:MI:SS'), '405A4');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'PTTKYC_QT01', 'PTTKYC_QT01_B8');

-- Th?i gian ngày 2024-10-10
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('PTTKYC_QT01_B9', TO_DATE('2024-10-10 15:35:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-10-10 18:00:00', 'YYYY-MM-DD HH24:MI:SS'), '405A4');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'PTTKYC_QT01', 'PTTKYC_QT01_B9');

-- Th?i gian ngày 2024-10-17
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('PTTKYC_QT01_B10', TO_DATE('2024-10-17 15:35:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-10-17 18:00:00', 'YYYY-MM-DD HH24:MI:SS'), '405A4');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'PTTKYC_QT01', 'PTTKYC_QT01_B10');

-- Th?i gian ngày 2024-10-24
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('PTTKYC_QT01_B11', TO_DATE('2024-10-24 15:35:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-10-24 18:00:00', 'YYYY-MM-DD HH24:MI:SS'), '405A4');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'PTTKYC_QT01', 'PTTKYC_QT01_B11');

-- Th?i gian ngày 2024-10-31
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('PTTKYC_QT01_B12', TO_DATE('2024-10-31 15:35:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-10-31 18:00:00', 'YYYY-MM-DD HH24:MI:SS'), '405A4');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'PTTKYC_QT01', 'PTTKYC_QT01_B12');

-- Th?i gian ngày 2024-11-07
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('PTTKYC_QT01_B13', TO_DATE('2024-11-07 15:35:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-11-07 18:00:00', 'YYYY-MM-DD HH24:MI:SS'), '405A4');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'PTTKYC_QT01', 'PTTKYC_QT01_B13');

-- Th?i gian ngày 2024-11-14
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('PTTKYC_QT01_B14', TO_DATE('2024-11-14 15:35:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-11-14 18:00:00', 'YYYY-MM-DD HH24:MI:SS'), '405A4');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian)
    VALUES (SYS_GUID(), 'PTTKYC_QT01', 'PTTKYC_QT01_B14');

INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('TTUD_QT01_B1', TO_DATE('2024-08-15 13:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-08-15 15:25:00', 'YYYY-MM-DD HH24:MI:SS'), '105A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian) 
    VALUES (SYS_GUID(), 'TTUD_QT01', 'TTUD_QT01_B1');
-- Thêm th?i gian vào b?ng ThoiGian cho t?ng phiên h?c
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('TTUD_QT01_B2', TO_DATE('2024-08-22 13:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-08-22 15:25:00', 'YYYY-MM-DD HH24:MI:SS'), '105A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian) 
    VALUES (SYS_GUID(), 'TTUD_QT01', 'TTUD_QT01_B2');

INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('TTUD_QT01_B3', TO_DATE('2024-08-29 13:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-08-29 15:25:00', 'YYYY-MM-DD HH24:MI:SS'), '105A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian) 
    VALUES (SYS_GUID(), 'TTUD_QT01', 'TTUD_QT01_B3');

INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('TTUD_QT01_B4', TO_DATE('2024-09-05 13:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-09-05 15:25:00', 'YYYY-MM-DD HH24:MI:SS'), '105A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian) 
    VALUES (SYS_GUID(), 'TTUD_QT01', 'TTUD_QT01_B4');

INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('TTUD_QT01_B5', TO_DATE('2024-09-12 13:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-09-12 15:25:00', 'YYYY-MM-DD HH24:MI:SS'), '105A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian) 
    VALUES (SYS_GUID(), 'TTUD_QT01', 'TTUD_QT01_B5');

INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('TTUD_QT01_B6', TO_DATE('2024-09-19 13:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-09-19 15:25:00', 'YYYY-MM-DD HH24:MI:SS'), '105A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian) 
    VALUES (SYS_GUID(), 'TTUD_QT01', 'TTUD_QT01_B6');

INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('TTUD_QT01_B7', TO_DATE('2024-09-26 13:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-09-26 15:25:00', 'YYYY-MM-DD HH24:MI:SS'), '105A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian) 
    VALUES (SYS_GUID(), 'TTUD_QT01', 'TTUD_QT01_B7');

-- Ti?p t?c cho các phiên h?c ti?p theo
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('TTUD_QT01_B8', TO_DATE('2024-10-03 13:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-10-03 15:25:00', 'YYYY-MM-DD HH24:MI:SS'), '105A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian) 
    VALUES (SYS_GUID(), 'TTUD_QT01', 'TTUD_QT01_B8');

INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('TTUD_QT01_B9', TO_DATE('2024-10-10 13:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-10-10 15:25:00', 'YYYY-MM-DD HH24:MI:SS'), '105A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian) 
    VALUES (SYS_GUID(), 'TTUD_QT01', 'TTUD_QT01_B9');

-- Thêm th?i gian và liên k?t cho t?ng phiên h?c c?a XSTK_QT01
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('XSTK_QT01_B1', TO_DATE('2024-08-12 13:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-08-12 15:25:00', 'YYYY-MM-DD HH24:MI:SS'), '503A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian) 
    VALUES (SYS_GUID(), 'XSTK_QT01', 'XSTK_QT01_B1');

INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('XSTK_QT01_B2', TO_DATE('2024-08-19 13:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-08-19 15:25:00', 'YYYY-MM-DD HH24:MI:SS'), '503A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian) 
    VALUES (SYS_GUID(), 'XSTK_QT01', 'XSTK_QT01_B2');

INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('XSTK_QT01_B3', TO_DATE('2024-08-26 13:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-08-26 15:25:00', 'YYYY-MM-DD HH24:MI:SS'), '503A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian) 
    VALUES (SYS_GUID(), 'XSTK_QT01', 'XSTK_QT01_B3');

INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('XSTK_QT01_B4', TO_DATE('2024-09-02 13:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-09-02 15:25:00', 'YYYY-MM-DD HH24:MI:SS'), '503A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian) 
    VALUES (SYS_GUID(), 'XSTK_QT01', 'XSTK_QT01_B4');

INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('XSTK_QT01_B5', TO_DATE('2024-09-09 13:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-09-09 15:25:00', 'YYYY-MM-DD HH24:MI:SS'), '503A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian) 
    VALUES (SYS_GUID(), 'XSTK_QT01', 'XSTK_QT01_B5');

INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('XSTK_QT01_B6', TO_DATE('2024-09-16 13:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-09-16 15:25:00', 'YYYY-MM-DD HH24:MI:SS'), '503A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian) 
    VALUES (SYS_GUID(), 'XSTK_QT01', 'XSTK_QT01_B6');

INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('XSTK_QT01_B7', TO_DATE('2024-09-23 13:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-09-23 15:25:00', 'YYYY-MM-DD HH24:MI:SS'), '503A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian) 
    VALUES (SYS_GUID(), 'XSTK_QT01', 'XSTK_QT01_B7');

-- Thêm th?i gian và liên k?t cho t?ng phiên h?c c?a VLY_QT01
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('VLY_QT01_B1', TO_DATE('2024-04-12 07:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-04-12 09:25:00', 'YYYY-MM-DD HH24:MI:SS'), '503A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian) 
    VALUES (SYS_GUID(), 'VLY_QT01', 'VLY_QT01_B1');

INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('VLY_QT01_B2', TO_DATE('2024-04-19 07:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-04-19 09:25:00', 'YYYY-MM-DD HH24:MI:SS'), '503A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian) 
    VALUES (SYS_GUID(), 'VLY_QT01', 'VLY_QT01_B2');

INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('VLY_QT01_B3', TO_DATE('2024-04-26 07:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-04-26 09:25:00', 'YYYY-MM-DD HH24:MI:SS'), '503A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian) 
    VALUES (SYS_GUID(), 'VLY_QT01', 'VLY_QT01_B3');

INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('VLY_QT01_B4', TO_DATE('2024-05-03 07:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-05-03 09:25:00', 'YYYY-MM-DD HH24:MI:SS'), '503A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian) 
    VALUES (SYS_GUID(), 'VLY_QT01', 'VLY_QT01_B4');

INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('VLY_QT01_B5', TO_DATE('2024-05-10 07:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-05-10 09:25:00', 'YYYY-MM-DD HH24:MI:SS'), '503A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian) 
    VALUES (SYS_GUID(), 'VLY_QT01', 'VLY_QT01_B5');

INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('VLY_QT01_B6', TO_DATE('2024-05-17 07:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-05-17 09:25:00', 'YYYY-MM-DD HH24:MI:SS'), '503A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian) 
    VALUES (SYS_GUID(), 'VLY_QT01', 'VLY_QT01_B6');

INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, IdPhongHoc)
    VALUES ('VLY_QT01_B7', TO_DATE('2024-05-24 07:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2024-05-24 09:25:00', 'YYYY-MM-DD HH24:MI:SS'), '503A8');
INSERT INTO ThoiGian_LopHocPhan (IdThoiGianLopHocPhan, IdLopHocPhan, IdThoiGian) 
    VALUES (SYS_GUID(), 'VLY_QT01', 'VLY_QT01_B7');