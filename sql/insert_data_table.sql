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

-- Tạo thời gian và gắn thời gian vào lớp học phần
-- Thêm các thời gian vào bảng ThoiGian và gắn vào lớp học phần

