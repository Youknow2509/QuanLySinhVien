-- Chuong trinh hoc
INSERT INTO ChuongTrinhHoc (IdChuongTrinhHoc, TenChuongTrinhHoc)
VALUES 
    (NEWID(), N'CLC Công nghệ thông tin'),
    (NEWID(), N'Công nghệ thông tin'),
    (NEWID(), N'Khoa học máy tính'),
    (NEWID(), N'Toán ứng dụng'),
    (NEWID(), N'Logistics và quản lý chuỗi cung ứng');

-- Khoa
INSERT INTO Khoa (IdKhoa, TenKhoa)
VALUES 
    ('CNTT', N'Công nghệ thông tin'),
    ('KTE', N'Kinh tế'),
    ('KHCB', N'Khoa học cơ bản'),
    ('CDB', N'Cầu đường'),
    ('DTQT', N'Đào tạo quốc tế'),
    ('Triet',N'Mác - Lênin');

-- Giao Vien
INSERT INTO dbo.GiaoVien (IdGiaoVien, TenGiaoVien, Email, SoDienThoai, IdKhoa)
VALUES
    ('dungnb01', N'Bùi Ngọc Dũng', 'dungbn@utc.edu.vn', '0915473821', 'CNTT'),
    ('luyenct01', N'Cao Thị Luyên', 'caoluyen@utc.edu.vn', '0123456789', 'CNTT'),
    ('dunglm01', N'Lại Mạnh Dũng', 'dunglm@utc.edu.vn', '0987654321', 'CNTT'),
    ('thaontt01', N'Nguyễn Thị Thanh Thảo', 'thao@utc.edu.vn', '0123456789', 'CNTT'),
    ('huongntt01', N'Nguyễn Thị Thu Hường', 'huongtt@utc.edu.vn', '0961423789', 'CNTT'),
    ('hieunt01', N'Nguyễn Trần Hiếu', 'hieunt@utc.edu.vn', '0942365478', 'CNTT'),
    ('duongnd01', N'Nguyễn Đình Dương', 'duongnd@utc.edu.vn', '0978332467', 'CNTT'),
    ('phongpd01', N'Phạm Đình Phong', 'phongnd@utc.edu.vn', '0985432167', 'CNTT'),
    ('tichpx01', N'Phạm Xuân Tích', 'tichpx@utc.edu.vn', '0987654321', 'CNTT'),
    ('tungdc01', N'Đinh Công Tùng', 'tungdc@utc.edu.vn', '0935421789', 'CNTT'),
    ('thuydtl01', N'Đào Thị Lệ Thủy', 'thuydt@utc.edu.vn', '0921374856', 'CNTT'),
    ('abc123',N'Nguyen Van A','nguyenvana@utc.edu.vn','0912983922','KHCB');

-- Thoi gian
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-14 07:00:00', '2024-09-14 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-14 09:35:00', '2024-09-14 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-14 13:00:00', '2024-09-14 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-14 15:35:00', '2024-09-14 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-15 07:00:00', '2024-09-15 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-15 09:35:00', '2024-09-15 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-15 13:00:00', '2024-09-15 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-15 15:35:00', '2024-09-15 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-16 07:00:00', '2024-09-16 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-16 09:35:00', '2024-09-16 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-16 13:00:00', '2024-09-16 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-16 15:35:00', '2024-09-16 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-17 07:00:00', '2024-09-17 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-17 09:35:00', '2024-09-17 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-17 13:00:00', '2024-09-17 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-17 15:35:00', '2024-09-17 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-18 07:00:00', '2024-09-18 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-18 09:35:00', '2024-09-18 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-18 13:00:00', '2024-09-18 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-18 15:35:00', '2024-09-18 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-19 07:00:00', '2024-09-19 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-19 09:35:00', '2024-09-19 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-19 13:00:00', '2024-09-19 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-19 15:35:00', '2024-09-19 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-20 07:00:00', '2024-09-20 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-20 09:35:00', '2024-09-20 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-20 13:00:00', '2024-09-20 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-20 15:35:00', '2024-09-20 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-21 07:00:00', '2024-09-21 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-21 09:35:00', '2024-09-21 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-21 13:00:00', '2024-09-21 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-21 15:35:00', '2024-09-21 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-22 07:00:00', '2024-09-22 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-22 09:35:00', '2024-09-22 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-22 13:00:00', '2024-09-22 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-22 15:35:00', '2024-09-22 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-23 07:00:00', '2024-09-23 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-23 09:35:00', '2024-09-23 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-23 13:00:00', '2024-09-23 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-23 15:35:00', '2024-09-23 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-24 07:00:00', '2024-09-24 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-24 09:35:00', '2024-09-24 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-24 13:00:00', '2024-09-24 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-24 15:35:00', '2024-09-24 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-25 07:00:00', '2024-09-25 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-25 09:35:00', '2024-09-25 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-25 13:00:00', '2024-09-25 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-25 15:35:00', '2024-09-25 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-26 07:00:00', '2024-09-26 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-26 09:35:00', '2024-09-26 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-26 13:00:00', '2024-09-26 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-26 15:35:00', '2024-09-26 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-27 07:00:00', '2024-09-27 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-27 09:35:00', '2024-09-27 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-27 13:00:00', '2024-09-27 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-27 15:35:00', '2024-09-27 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-28 07:00:00', '2024-09-28 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-28 09:35:00', '2024-09-28 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-28 13:00:00', '2024-09-28 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-28 15:35:00', '2024-09-28 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-29 07:00:00', '2024-09-29 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-29 09:35:00', '2024-09-29 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-29 13:00:00', '2024-09-29 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-29 15:35:00', '2024-09-29 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-30 07:00:00', '2024-09-30 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-30 09:35:00', '2024-09-30 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-30 13:00:00', '2024-09-30 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-09-30 15:35:00', '2024-09-30 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-01 07:00:00', '2024-10-01 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-01 09:35:00', '2024-10-01 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-01 13:00:00', '2024-10-01 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-01 15:35:00', '2024-10-01 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-02 07:00:00', '2024-10-02 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-02 09:35:00', '2024-10-02 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-02 13:00:00', '2024-10-02 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-02 15:35:00', '2024-10-02 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-03 07:00:00', '2024-10-03 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-03 09:35:00', '2024-10-03 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-03 13:00:00', '2024-10-03 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-03 15:35:00', '2024-10-03 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-04 07:00:00', '2024-10-04 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-04 09:35:00', '2024-10-04 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-04 13:00:00', '2024-10-04 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-04 15:35:00', '2024-10-04 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-05 07:00:00', '2024-10-05 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-05 09:35:00', '2024-10-05 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-05 13:00:00', '2024-10-05 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-05 15:35:00', '2024-10-05 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-06 07:00:00', '2024-10-06 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-06 09:35:00', '2024-10-06 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-06 13:00:00', '2024-10-06 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-06 15:35:00', '2024-10-06 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-07 07:00:00', '2024-10-07 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-07 09:35:00', '2024-10-07 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-07 13:00:00', '2024-10-07 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-07 15:35:00', '2024-10-07 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-08 07:00:00', '2024-10-08 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-08 09:35:00', '2024-10-08 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-08 13:00:00', '2024-10-08 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-08 15:35:00', '2024-10-08 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-09 07:00:00', '2024-10-09 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-09 09:35:00', '2024-10-09 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-09 13:00:00', '2024-10-09 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-09 15:35:00', '2024-10-09 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-10 07:00:00', '2024-10-10 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-10 09:35:00', '2024-10-10 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-10 13:00:00', '2024-10-10 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-10 15:35:00', '2024-10-10 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-11 07:00:00', '2024-10-11 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-11 09:35:00', '2024-10-11 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-11 13:00:00', '2024-10-11 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-11 15:35:00', '2024-10-11 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-12 07:00:00', '2024-10-12 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-12 09:35:00', '2024-10-12 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-12 13:00:00', '2024-10-12 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-12 15:35:00', '2024-10-12 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-13 07:00:00', '2024-10-13 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-13 09:35:00', '2024-10-13 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-13 13:00:00', '2024-10-13 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-13 15:35:00', '2024-10-13 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-14 07:00:00', '2024-10-14 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-14 09:35:00', '2024-10-14 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-14 13:00:00', '2024-10-14 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-14 15:35:00', '2024-10-14 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-15 07:00:00', '2024-10-15 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-15 09:35:00', '2024-10-15 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-15 13:00:00', '2024-10-15 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-15 15:35:00', '2024-10-15 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-16 07:00:00', '2024-10-16 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-16 09:35:00', '2024-10-16 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-16 13:00:00', '2024-10-16 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-16 15:35:00', '2024-10-16 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-17 07:00:00', '2024-10-17 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-17 09:35:00', '2024-10-17 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-17 13:00:00', '2024-10-17 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-17 15:35:00', '2024-10-17 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-18 07:00:00', '2024-10-18 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-18 09:35:00', '2024-10-18 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-18 13:00:00', '2024-10-18 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-18 15:35:00', '2024-10-18 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-19 07:00:00', '2024-10-19 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-19 09:35:00', '2024-10-19 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-19 13:00:00', '2024-10-19 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-19 15:35:00', '2024-10-19 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-20 07:00:00', '2024-10-20 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-20 09:35:00', '2024-10-20 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-20 13:00:00', '2024-10-20 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-20 15:35:00', '2024-10-20 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-21 07:00:00', '2024-10-21 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-21 09:35:00', '2024-10-21 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-21 13:00:00', '2024-10-21 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-21 15:35:00', '2024-10-21 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-22 07:00:00', '2024-10-22 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-22 09:35:00', '2024-10-22 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-22 13:00:00', '2024-10-22 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-22 15:35:00', '2024-10-22 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-23 07:00:00', '2024-10-23 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-23 09:35:00', '2024-10-23 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-23 13:00:00', '2024-10-23 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-23 15:35:00', '2024-10-23 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-24 07:00:00', '2024-10-24 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-24 09:35:00', '2024-10-24 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-24 13:00:00', '2024-10-24 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-24 15:35:00', '2024-10-24 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-25 07:00:00', '2024-10-25 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-25 09:35:00', '2024-10-25 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-25 13:00:00', '2024-10-25 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-25 15:35:00', '2024-10-25 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-26 07:00:00', '2024-10-26 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-26 09:35:00', '2024-10-26 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-26 13:00:00', '2024-10-26 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-26 15:35:00', '2024-10-26 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-27 07:00:00', '2024-10-27 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-27 09:35:00', '2024-10-27 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-27 13:00:00', '2024-10-27 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-27 15:35:00', '2024-10-27 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-28 07:00:00', '2024-10-28 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-28 09:35:00', '2024-10-28 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-28 13:00:00', '2024-10-28 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-28 15:35:00', '2024-10-28 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-29 07:00:00', '2024-10-29 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-29 09:35:00', '2024-10-29 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-29 13:00:00', '2024-10-29 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-29 15:35:00', '2024-10-29 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-30 07:00:00', '2024-10-30 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-30 09:35:00', '2024-10-30 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-30 13:00:00', '2024-10-30 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-30 15:35:00', '2024-10-30 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-31 07:00:00', '2024-10-31 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-31 09:35:00', '2024-10-31 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-31 13:00:00', '2024-10-31 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-10-31 15:35:00', '2024-10-31 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-01 07:00:00', '2024-11-01 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-01 09:35:00', '2024-11-01 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-01 13:00:00', '2024-11-01 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-01 15:35:00', '2024-11-01 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-02 07:00:00', '2024-11-02 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-02 09:35:00', '2024-11-02 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-02 13:00:00', '2024-11-02 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-02 15:35:00', '2024-11-02 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-03 07:00:00', '2024-11-03 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-03 09:35:00', '2024-11-03 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-03 13:00:00', '2024-11-03 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-03 15:35:00', '2024-11-03 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-04 07:00:00', '2024-11-04 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-04 09:35:00', '2024-11-04 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-04 13:00:00', '2024-11-04 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-04 15:35:00', '2024-11-04 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-05 07:00:00', '2024-11-05 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-05 09:35:00', '2024-11-05 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-05 13:00:00', '2024-11-05 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-05 15:35:00', '2024-11-05 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-06 07:00:00', '2024-11-06 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-06 09:35:00', '2024-11-06 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-06 13:00:00', '2024-11-06 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-06 15:35:00', '2024-11-06 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-07 07:00:00', '2024-11-07 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-07 09:35:00', '2024-11-07 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-07 13:00:00', '2024-11-07 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-07 15:35:00', '2024-11-07 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-08 07:00:00', '2024-11-08 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-08 09:35:00', '2024-11-08 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-08 13:00:00', '2024-11-08 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-08 15:35:00', '2024-11-08 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-09 07:00:00', '2024-11-09 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-09 09:35:00', '2024-11-09 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-09 13:00:00', '2024-11-09 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-09 15:35:00', '2024-11-09 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-10 07:00:00', '2024-11-10 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-10 09:35:00', '2024-11-10 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-10 13:00:00', '2024-11-10 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-10 15:35:00', '2024-11-10 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-11 07:00:00', '2024-11-11 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-11 09:35:00', '2024-11-11 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-11 13:00:00', '2024-11-11 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-11 15:35:00', '2024-11-11 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-12 07:00:00', '2024-11-12 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-12 09:35:00', '2024-11-12 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-12 13:00:00', '2024-11-12 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-12 15:35:00', '2024-11-12 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-13 07:00:00', '2024-11-13 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-13 09:35:00', '2024-11-13 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-13 13:00:00', '2024-11-13 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-13 15:35:00', '2024-11-13 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-14 07:00:00', '2024-11-14 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-14 09:35:00', '2024-11-14 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-14 13:00:00', '2024-11-14 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc, DiaDiem) VALUES (NEWID(), '2024-11-14 15:35:00', '2024-11-14 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-15 07:00:00', '2024-11-15 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-15 09:35:00', '2024-11-15 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-15 13:00:00', '2024-11-15 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-15 15:35:00', '2024-11-15 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-16 07:00:00', '2024-11-16 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-16 09:35:00', '2024-11-16 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-16 13:00:00', '2024-11-16 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-16 15:35:00', '2024-11-16 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-17 07:00:00', '2024-11-17 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-17 09:35:00', '2024-11-17 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-17 13:00:00', '2024-11-17 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-17 15:35:00', '2024-11-17 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-18 07:00:00', '2024-11-18 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-18 09:35:00', '2024-11-18 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-18 13:00:00', '2024-11-18 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-18 15:35:00', '2024-11-18 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-19 07:00:00', '2024-11-19 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-19 09:35:00', '2024-11-19 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-19 13:00:00', '2024-11-19 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-19 15:35:00', '2024-11-19 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-20 07:00:00', '2024-11-20 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-20 09:35:00', '2024-11-20 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-20 13:00:00', '2024-11-20 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-20 15:35:00', '2024-11-20 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-21 07:00:00', '2024-11-21 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-21 09:35:00', '2024-11-21 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-21 13:00:00', '2024-11-21 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-21 15:35:00', '2024-11-21 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-22 07:00:00', '2024-11-22 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-22 09:35:00', '2024-11-22 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-22 13:00:00', '2024-11-22 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-22 15:35:00', '2024-11-22 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-23 07:00:00', '2024-11-23 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-23 09:35:00', '2024-11-23 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-23 13:00:00', '2024-11-23 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-23 15:35:00', '2024-11-23 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-24 07:00:00', '2024-11-24 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-24 09:35:00', '2024-11-24 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-24 13:00:00', '2024-11-24 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-24 15:35:00', '2024-11-24 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-25 07:00:00', '2024-11-25 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-25 09:35:00', '2024-11-25 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-25 13:00:00', '2024-11-25 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-25 15:35:00', '2024-11-25 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-26 07:00:00', '2024-11-26 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-26 09:35:00', '2024-11-26 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-26 13:00:00', '2024-11-26 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-26 15:35:00', '2024-11-26 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-27 07:00:00', '2024-11-27 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-27 09:35:00', '2024-11-27 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-27 13:00:00', '2024-11-27 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-27 15:35:00', '2024-11-27 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-28 07:00:00', '2024-11-28 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-28 09:35:00', '2024-11-28 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-28 13:00:00', '2024-11-28 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-28 15:35:00', '2024-11-28 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-29 07:00:00', '2024-11-29 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-29 09:35:00', '2024-11-29 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-29 13:00:00', '2024-11-29 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-29 15:35:00', '2024-11-29 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-30 07:00:00', '2024-11-30 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-30 09:35:00', '2024-11-30 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-30 13:00:00', '2024-11-30 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-11-30 15:35:00', '2024-11-30 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-01 07:00:00', '2024-12-01 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-01 09:35:00', '2024-12-01 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-01 13:00:00', '2024-12-01 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-01 15:35:00', '2024-12-01 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-02 07:00:00', '2024-12-02 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-02 09:35:00', '2024-12-02 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-02 13:00:00', '2024-12-02 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-02 15:35:00', '2024-12-02 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-03 07:00:00', '2024-12-03 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-03 09:35:00', '2024-12-03 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-03 13:00:00', '2024-12-03 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-03 15:35:00', '2024-12-03 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-04 07:00:00', '2024-12-04 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-04 09:35:00', '2024-12-04 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-04 13:00:00', '2024-12-04 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-04 15:35:00', '2024-12-04 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-05 07:00:00', '2024-12-05 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-05 09:35:00', '2024-12-05 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-05 13:00:00', '2024-12-05 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-05 15:35:00', '2024-12-05 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-06 07:00:00', '2024-12-06 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-06 09:35:00', '2024-12-06 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-06 13:00:00', '2024-12-06 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-06 15:35:00', '2024-12-06 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-07 07:00:00', '2024-12-07 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-07 09:35:00', '2024-12-07 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-07 13:00:00', '2024-12-07 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-07 15:35:00', '2024-12-07 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-08 07:00:00', '2024-12-08 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-08 09:35:00', '2024-12-08 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-08 13:00:00', '2024-12-08 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-08 15:35:00', '2024-12-08 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-09 07:00:00', '2024-12-09 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-09 09:35:00', '2024-12-09 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-09 13:00:00', '2024-12-09 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-09 15:35:00', '2024-12-09 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-10 07:00:00', '2024-12-10 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-10 09:35:00', '2024-12-10 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-10 13:00:00', '2024-12-10 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-10 15:35:00', '2024-12-10 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-11 07:00:00', '2024-12-11 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-11 09:35:00', '2024-12-11 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-11 13:00:00', '2024-12-11 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-11 15:35:00', '2024-12-11 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-12 07:00:00', '2024-12-12 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-12 09:35:00', '2024-12-12 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-12 13:00:00', '2024-12-12 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-12 15:35:00', '2024-12-12 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-13 07:00:00', '2024-12-13 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-13 09:35:00', '2024-12-13 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-13 13:00:00', '2024-12-13 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-13 15:35:00', '2024-12-13 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-14 07:00:00', '2024-12-14 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-14 09:35:00', '2024-12-14 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-14 13:00:00', '2024-12-14 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-14 15:35:00', '2024-12-14 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-15 07:00:00', '2024-12-15 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-15 09:35:00', '2024-12-15 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-15 13:00:00', '2024-12-15 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-15 15:35:00', '2024-12-15 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-16 07:00:00', '2024-12-16 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-16 09:35:00', '2024-12-16 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-16 13:00:00', '2024-12-16 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-16 15:35:00', '2024-12-16 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-17 07:00:00', '2024-12-17 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-17 09:35:00', '2024-12-17 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-17 13:00:00', '2024-12-17 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-17 15:35:00', '2024-12-17 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-18 07:00:00', '2024-12-18 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-18 09:35:00', '2024-12-18 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-18 13:00:00', '2024-12-18 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-18 15:35:00', '2024-12-18 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-19 07:00:00', '2024-12-19 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-19 09:35:00', '2024-12-19 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-19 13:00:00', '2024-12-19 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-19 15:35:00', '2024-12-19 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-20 07:00:00', '2024-12-20 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-20 09:35:00', '2024-12-20 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-20 13:00:00', '2024-12-20 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-20 15:35:00', '2024-12-20 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-21 07:00:00', '2024-12-21 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-21 09:35:00', '2024-12-21 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-21 13:00:00', '2024-12-21 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-21 15:35:00', '2024-12-21 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-22 07:00:00', '2024-12-22 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-22 09:35:00', '2024-12-22 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-22 13:00:00', '2024-12-22 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-22 15:35:00', '2024-12-22 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-23 07:00:00', '2024-12-23 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-23 09:35:00', '2024-12-23 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-23 13:00:00', '2024-12-23 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-23 15:35:00', '2024-12-23 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-24 07:00:00', '2024-12-24 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-24 09:35:00', '2024-12-24 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-24 13:00:00', '2024-12-24 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-24 15:35:00', '2024-12-24 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-25 07:00:00', '2024-12-25 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-25 09:35:00', '2024-12-25 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-25 13:00:00', '2024-12-25 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-25 15:35:00', '2024-12-25 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-26 07:00:00', '2024-12-26 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-26 09:35:00', '2024-12-26 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-26 13:00:00', '2024-12-26 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-26 15:35:00', '2024-12-26 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-27 07:00:00', '2024-12-27 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-27 09:35:00', '2024-12-27 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-27 13:00:00', '2024-12-27 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-27 15:35:00', '2024-12-27 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-28 07:00:00', '2024-12-28 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-28 09:35:00', '2024-12-28 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-28 13:00:00', '2024-12-28 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-28 15:35:00', '2024-12-28 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-29 07:00:00', '2024-12-29 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-29 09:35:00', '2024-12-29 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-29 13:00:00', '2024-12-29 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-29 15:35:00', '2024-12-29 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-30 07:00:00', '2024-12-30 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-30 09:35:00', '2024-12-30 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-30 13:00:00', '2024-12-30 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-30 15:35:00', '2024-12-30 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-31 07:00:00', '2024-12-31 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-31 09:35:00', '2024-12-31 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-31 13:00:00', '2024-12-31 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2024-12-31 15:35:00', '2024-12-31 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-01 07:00:00', '2025-01-01 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-01 09:35:00', '2025-01-01 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-01 13:00:00', '2025-01-01 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-01 15:35:00', '2025-01-01 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-02 07:00:00', '2025-01-02 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-02 09:35:00', '2025-01-02 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-02 13:00:00', '2025-01-02 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-02 15:35:00', '2025-01-02 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-03 07:00:00', '2025-01-03 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-03 09:35:00', '2025-01-03 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-03 13:00:00', '2025-01-03 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-03 15:35:00', '2025-01-03 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-04 07:00:00', '2025-01-04 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-04 09:35:00', '2025-01-04 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-04 13:00:00', '2025-01-04 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-04 15:35:00', '2025-01-04 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-05 07:00:00', '2025-01-05 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-05 09:35:00', '2025-01-05 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-05 13:00:00', '2025-01-05 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-05 15:35:00', '2025-01-05 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-06 07:00:00', '2025-01-06 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-06 09:35:00', '2025-01-06 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-06 13:00:00', '2025-01-06 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-06 15:35:00', '2025-01-06 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-07 07:00:00', '2025-01-07 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-07 09:35:00', '2025-01-07 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-07 13:00:00', '2025-01-07 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-07 15:35:00', '2025-01-07 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-08 07:00:00', '2025-01-08 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-08 09:35:00', '2025-01-08 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-08 13:00:00', '2025-01-08 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-08 15:35:00', '2025-01-08 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-09 07:00:00', '2025-01-09 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-09 09:35:00', '2025-01-09 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-09 13:00:00', '2025-01-09 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-09 15:35:00', '2025-01-09 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-10 07:00:00', '2025-01-10 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-10 09:35:00', '2025-01-10 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-10 13:00:00', '2025-01-10 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-10 15:35:00', '2025-01-10 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-11 07:00:00', '2025-01-11 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-11 09:35:00', '2025-01-11 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-11 13:00:00', '2025-01-11 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-11 15:35:00', '2025-01-11 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-12 07:00:00', '2025-01-12 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-12 09:35:00', '2025-01-12 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-12 13:00:00', '2025-01-12 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-12 15:35:00', '2025-01-12 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-13 07:00:00', '2025-01-13 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-13 09:35:00', '2025-01-13 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-13 13:00:00', '2025-01-13 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-13 15:35:00', '2025-01-13 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-14 07:00:00', '2025-01-14 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-14 09:35:00', '2025-01-14 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-14 13:00:00', '2025-01-14 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-14 15:35:00', '2025-01-14 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-15 07:00:00', '2025-01-15 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-15 09:35:00', '2025-01-15 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-15 13:00:00', '2025-01-15 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-15 15:35:00', '2025-01-15 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-16 07:00:00', '2025-01-16 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-16 09:35:00', '2025-01-16 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-16 13:00:00', '2025-01-16 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-16 15:35:00', '2025-01-16 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-17 07:00:00', '2025-01-17 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-17 09:35:00', '2025-01-17 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-17 13:00:00', '2025-01-17 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-17 15:35:00', '2025-01-17 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-18 07:00:00', '2025-01-18 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-18 09:35:00', '2025-01-18 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-18 13:00:00', '2025-01-18 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-18 15:35:00', '2025-01-18 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-19 07:00:00', '2025-01-19 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-19 09:35:00', '2025-01-19 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-19 13:00:00', '2025-01-19 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-19 15:35:00', '2025-01-19 18:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-20 07:00:00', '2025-01-20 09:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-20 09:35:00', '2025-01-20 12:00:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-20 13:00:00', '2025-01-20 15:25:00','503A8');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc,DiaDiem) VALUES (NEWID(), '2025-01-20 15:35:00', '2025-01-20 18:00:00','503A8');


-- Chuong trinh hoc - Mon hoc
INSERT INTO ChuongTrinhHoc_MonHoc (IdCTHMonHoc, IdChuongTrinhHoc, IdMonHoc)
SELECT NEWID(), 
       (SELECT IdChuongTrinhHoc FROM ChuongTrinhHoc WHERE TenChuongTrinhHoc = N'Công nghệ thông tin'),
       IdMonHoc
FROM MonHoc;

INSERT INTO ChuongTrinhHoc_MonHoc (IdCTHMonHoc, IdChuongTrinhHoc, IdMonHoc)
SELECT NEWID(), 
       (SELECT IdChuongTrinhHoc FROM ChuongTrinhHoc WHERE TenChuongTrinhHoc = N'CLC Công nghệ thông tin'),
       IdMonHoc
FROM MonHoc;


INSERT INTO SinhVien (IdSinhVien, HoTen, Lop, NgaySinh, DiaChi, IdChuongTrinhHoc, IdKhoa)
VALUES
    ('222631111', N'Hoàng Mạnh Khiêm', N'Công nghệ thông tin Việt Anh 1', '2004-10-01', N'Hà Nội', 
        (SELECT IdChuongTrinhHoc FROM ChuongTrinhHoc WHERE TenChuongTrinhHoc = N'CLC Công nghệ thông tin'), 
        (SELECT IdKhoa FROM Khoa WHERE TenKhoa = N'Công nghệ thông tin')),
    ('222631159', N'Lý Trần Vinh', N'Công nghệ thông tin Việt Anh 1', '2004-05-12', N'Nghệ An', 
        (SELECT IdChuongTrinhHoc FROM ChuongTrinhHoc WHERE TenChuongTrinhHoc = N'CLC Công nghệ thông tin'), 
        (SELECT IdKhoa FROM Khoa WHERE TenKhoa = N'Đào tạo quốc tế')),
    ('222631160', N'Le Van C', N'Công nghệ thông tin 1', '2002-02-15', N'Hải Phòng', 
        (SELECT IdChuongTrinhHoc FROM ChuongTrinhHoc WHERE TenChuongTrinhHoc = N'Công nghệ thông tin'), 
        (SELECT IdKhoa FROM Khoa WHERE TenKhoa = N'Công nghệ thông tin')),
    ('222631161', N'Pham Thi J', N'Công nghệ thông tin 3', '2004-07-07', N'Hà Nội', 
        (SELECT IdChuongTrinhHoc FROM ChuongTrinhHoc WHERE TenChuongTrinhHoc = N'Công nghệ thông tin'), 
        (SELECT IdKhoa FROM Khoa WHERE TenKhoa = N'Công nghệ thông tin'));


-- Mon Hoc
INSERT INTO MonHoc (IdMonHoc, TenMonHoc, SoTinChi, SoTietHoc, IdKhoa) 
VALUES 
    ('HQTCSDL', N'Hệ quản trị cơ sở dữ liệu Oracle', 3, 45, 'CNTT'),
    ('LTTT', N'Lập trình trực quan', 3, 45, 'CNTT'),
    ('LTW', N'Lập trình Web', 3, 45, 'CNTT'),
    ('LSDCSVN', N'Lịch sử Đảng Cộng sản Việt Nam', 3, 45, 'CNTT'),
    ('MMT', N'Mạng máy tính', 3, 45, 'CNTT'),
    ('PTTKYC', N'Phân tích thiết kế yêu cầu', 3, 45, 'CNTT'),
    ('TTUD', N'Thuật toán và ứng dụng', 3, 45, 'CNTT'),
    ('XSTK', N'Xác suất thống kê', 3, 45, 'KHCB'),
    ('VLY', N'Vật Lý', 3, 45, 'KHCB'),
    ('PLDC', N'Pháp luật đại cương', 3, 45,'Triet');
-- Tạo các lớp học phần với Id và tên giáo viên chỉ định
INSERT INTO LopHocPhan (IdLopHocPhan, TenHocPhan, IdGiaoVien, IdMonHoc,ThoiGianBatDau,ThoiGianKetThuc)
VALUES
    ('HTQTORCL_QT01', N'Hệ quản trị cơ sở dữ liệu Oracle-1-1-24(QT01)', 'dungnb01', 'HQTCSDL','2024-08-12','2024-11-17'),
    ('LTTT_QT01', N'Lập trình trực quan-1-1-24(QT01)', 'huongntt01', 'LTTT', '2024-08-12','2024-11-17'),
    ('LTW_QT01', N'Lập trình Web-1-1-24(QT01)', 'dunglm01', 'LTW','2024-08-12','2024-11-17'),
    ('LSDCSVN_QT05', N'Lịch sử Đảng Cộng sản Việt Nam-1-1-24(QT05)', 'thaontt01', 'LSDCSVN','2024-08-12','2024-11-17'),
    ('MMT_QT01', N'Mạng máy tính-1-1-24(QT01)', 'hieunt01', 'MMT','2024-08-12','2024-11-17'),
    ('PTTKYC_QT01', N'Phân tích thiết kế yêu cầu-1-1-24(QT01)','thuydtl01', 'PTTKYC','2024-08-12','2024-11-17'),
    ('TTUD_QT01', N'Thuật toán và ứng dụng-1-1-24(QT01)', 'tichpx01','TTUD','2024-08-12','2024-11-17'),
    ('XSTK_QT01', N'Xác suất thống kê-1-1-24(QT01)', 'phongpd01', 'XSTK','2024-08-12','2024-11-17'),
    ('VLY_QT01', N'Vật Lý-1-1-24(QT)', 'tungdc01', 'VLY','2024-04-08','2024-07-14'),
    ('PLDC_QT01',N'Pháp luật đại cương-1-1-24(QT01)','duongnd01','PLDC','2024-12-02','2025-01-20');

-- Thêm sinh viên vào các lớp học phần dựa trên dữ liệu có sẵn
INSERT INTO SinhVien_LopHocPhan (IdSinhVienLopHP, IdSinhVien, IdLopHocPhan)
VALUES
    (NEWID(), (SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Hoàng Mạnh Khiêm'), 'LSDCSVN_QT05'),
    (NEWID(), (SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Hoàng Mạnh Khiêm'), 'LTW_QT01'),
    (NEWID(), (SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Hoàng Mạnh Khiêm'), 'LTTT_QT01'),
    (NEWID(), (SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Hoàng Mạnh Khiêm'), 'LSDCSVN_QT05'),
    (NEWID(), (SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Hoàng Mạnh Khiêm'), 'MMT_QT01'),
    (NEWID(), (SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Hoàng Mạnh Khiêm'), 'PTTKYC_QT01'),
    (NEWID(), (SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Hoàng Mạnh Khiêm'), 'TTUD_QT01'),
    (NEWID(), (SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Hoàng Mạnh Khiêm'), 'XSTK_QT01'),
    (NEWID(), (SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Hoàng Mạnh Khiêm'), 'VLY_QT01'),
    (NEWID(), (SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Lý Trần Vinh'),'XSTK_QT01'),
    (NEWID(), (SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Le Van C'),'MMT_QT01'),
    (NEWID(), (SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Pham Thi J'), 'TTUD_QT01'),
    (NEWID(), (SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Lý Trần Vinh'), 'PTTKYC_QT01'),
    (NEWID(), (SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Le Van C'), 'TTUD_QT01'),
    (NEWID(), (SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Pham Thi J'),'XSTK_QT01' );


-- Thêm điểm cho sinh viên vào các lớp học phần dựa trên dữ liệu có sẵn
INSERT INTO Diem (IdDiem, IdLopHocPhan, IdSinhVien, DiemQuaTrinh, DiemKetThuc, DiemTongKet, LanHoc) VALUES (NEWID(), 'HTQTORCL_QT01', '222631111', 0, 0, 0, 1);
INSERT INTO Diem (IdDiem, IdLopHocPhan, IdSinhVien, DiemQuaTrinh, DiemKetThuc, DiemTongKet, LanHoc) VALUES (NEWID(), 'LTTT_QT01', '222631111', 0, 0, 0, 1);
INSERT INTO Diem (IdDiem, IdLopHocPhan, IdSinhVien, DiemQuaTrinh, DiemKetThuc, DiemTongKet, LanHoc) VALUES (NEWID(), 'LTW_QT01', '222631111', 0, 0, 0, 1);
INSERT INTO Diem (IdDiem, IdLopHocPhan, IdSinhVien, DiemQuaTrinh, DiemKetThuc, DiemTongKet, LanHoc) VALUES (NEWID(), 'LSDCSVN_QT05', '222631111', 0, 0, 0, 1);
INSERT INTO Diem (IdDiem, IdLopHocPhan, IdSinhVien, DiemQuaTrinh, DiemKetThuc, DiemTongKet, LanHoc) VALUES (NEWID(), 'MMT_QT01', '222631111', 0, 0, 0, 1);
INSERT INTO Diem (IdDiem, IdLopHocPhan, IdSinhVien, DiemQuaTrinh, DiemKetThuc, DiemTongKet, LanHoc) VALUES (NEWID(), 'PTTKYC_QT01', '222631111', 0, 0, 0, 1);
INSERT INTO Diem (IdDiem, IdLopHocPhan, IdSinhVien, DiemQuaTrinh, DiemKetThuc, DiemTongKet, LanHoc) VALUES (NEWID(), 'TTUD_QT01', '222631111', 0, 0, 0, 1);
INSERT INTO Diem (IdDiem, IdLopHocPhan, IdSinhVien, DiemQuaTrinh, DiemKetThuc, DiemTongKet, LanHoc) VALUES (NEWID(), 'XSTK_QT01', '222631111', 0, 0, 0, 1);
INSERT INTO Diem (IdDiem, IdLopHocPhan, IdSinhVien, DiemQuaTrinh, DiemKetThuc, DiemTongKet, LanHoc) VALUES (NEWID(), 'VLY_QT01', '222631111', 2, 4, 3, 1);




-- Tạo các bản ghi thời gian tương ứng trong bảng ThoiGian_LopHocPhan
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'HTQTORCL_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-08-14 15:35:00' AND NgayKetThuc = '2024-08-14 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'HTQTORCL_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-08-21 15:35:00' AND NgayKetThuc = '2024-08-21 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'HTQTORCL_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-08-28 15:35:00' AND NgayKetThuc = '2024-08-28 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'HTQTORCL_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-04 15:35:00' AND NgayKetThuc = '2024-09-04 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'HTQTORCL_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-11 15:35:00' AND NgayKetThuc = '2024-09-11 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'HTQTORCL_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-18 15:35:00' AND NgayKetThuc = '2024-09-18 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'HTQTORCL_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-25 15:35:00' AND NgayKetThuc = '2024-09-25 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'HTQTORCL_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-10-02 15:35:00' AND NgayKetThuc = '2024-10-02 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'HTQTORCL_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-10-09 15:35:00' AND NgayKetThuc = '2024-10-09 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'HTQTORCL_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-10-16 15:35:00' AND NgayKetThuc = '2024-10-16 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'HTQTORCL_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-10-23 15:35:00' AND NgayKetThuc = '2024-10-23 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'HTQTORCL_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-10-30 15:35:00' AND NgayKetThuc = '2024-10-30 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'HTQTORCL_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-11-06 15:35:00' AND NgayKetThuc = '2024-11-06 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'HTQTORCL_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-11-13 15:35:00' AND NgayKetThuc = '2024-11-13 18:00:00'));


INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'LTW_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-08-13 15:35:00' AND NgayKetThuc = '2024-08-13 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'LTW_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-08-20 15:35:00' AND NgayKetThuc = '2024-08-20 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'LTW_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-08-27 15:35:00' AND NgayKetThuc = '2024-08-27 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'LTW_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-03 15:35:00' AND NgayKetThuc = '2024-09-03 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'LTW_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-10 15:35:00' AND NgayKetThuc = '2024-09-10 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'LTW_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-17 15:35:00' AND NgayKetThuc = '2024-09-17 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'LTW_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-24 15:35:00' AND NgayKetThuc = '2024-09-24 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'LTW_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-10-01 15:35:00' AND NgayKetThuc = '2024-10-01 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'LTW_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-10-08 15:35:00' AND NgayKetThuc = '2024-10-08 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'LTW_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-10-15 15:35:00' AND NgayKetThuc = '2024-10-15 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'LTW_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-10-22 15:35:00' AND NgayKetThuc = '2024-10-22 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'LTW_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-10-29 15:35:00' AND NgayKetThuc = '2024-10-29 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'LTW_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-11-05 15:35:00' AND NgayKetThuc = '2024-11-05 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'LTW_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-11-12 15:35:00' AND NgayKetThuc = '2024-11-12 18:00:00'));


INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'LTTT_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-08-13 13:00:00' AND NgayKetThuc = '2024-08-13 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'LTTT_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-08-20 13:00:00' AND NgayKetThuc = '2024-08-20 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'LTTT_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-08-27 13:00:00' AND NgayKetThuc = '2024-08-27 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'LTTT_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-03 13:00:00' AND NgayKetThuc = '2024-09-03 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'LTTT_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-10 13:00:00' AND NgayKetThuc = '2024-09-10 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'LTTT_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-17 13:00:00' AND NgayKetThuc = '2024-09-17 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'LTTT_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-24 13:00:00' AND NgayKetThuc = '2024-09-24 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'LTTT_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-10-01 13:00:00' AND NgayKetThuc = '2024-10-01 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'LTTT_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-10-08 13:00:00' AND NgayKetThuc = '2024-10-08 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'LTTT_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-10-15 13:00:00' AND NgayKetThuc = '2024-10-15 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'LTTT_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-10-22 13:00:00' AND NgayKetThuc = '2024-10-22 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'LTTT_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-10-29 13:00:00' AND NgayKetThuc = '2024-10-29 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'LTTT_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-11-05 13:00:00' AND NgayKetThuc = '2024-11-05 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'LTTT_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-11-12 13:00:00' AND NgayKetThuc = '2024-11-12 15:25:00'));


INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'MMT_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-08-17 13:00:00' AND NgayKetThuc = '2024-08-17 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'MMT_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-08-24 13:00:00' AND NgayKetThuc = '2024-08-24 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'MMT_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-08-31 13:00:00' AND NgayKetThuc = '2024-08-31 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'MMT_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-07 13:00:00' AND NgayKetThuc = '2024-09-07 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'MMT_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-14 13:00:00' AND NgayKetThuc = '2024-09-14 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'MMT_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-21 13:00:00' AND NgayKetThuc = '2024-09-21 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'MMT_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-28 13:00:00' AND NgayKetThuc = '2024-09-28 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'MMT_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-10-05 13:00:00' AND NgayKetThuc = '2024-10-05 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'MMT_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-10-12 13:00:00' AND NgayKetThuc = '2024-10-12 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'MMT_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-10-19 13:00:00' AND NgayKetThuc = '2024-10-19 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'MMT_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-10-26 13:00:00' AND NgayKetThuc = '2024-10-26 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'MMT_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-11-02 13:00:00' AND NgayKetThuc = '2024-11-02 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'MMT_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-11-09 13:00:00' AND NgayKetThuc = '2024-11-09 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'MMT_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-11-16 13:00:00' AND NgayKetThuc = '2024-11-16 15:25:00'));


INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'PTTKYC_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-08-15 15:35:00' AND NgayKetThuc = '2024-08-15 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'PTTKYC_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-08-22 15:35:00' AND NgayKetThuc = '2024-08-22 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'PTTKYC_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-08-29 15:35:00' AND NgayKetThuc = '2024-08-29 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'PTTKYC_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-05 15:35:00' AND NgayKetThuc = '2024-09-05 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'PTTKYC_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-12 15:35:00' AND NgayKetThuc = '2024-09-12 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'PTTKYC_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-19 15:35:00' AND NgayKetThuc = '2024-09-19 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'PTTKYC_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-26 15:35:00' AND NgayKetThuc = '2024-09-26 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'PTTKYC_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-10-03 15:35:00' AND NgayKetThuc = '2024-10-03 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'PTTKYC_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-10-10 15:35:00' AND NgayKetThuc = '2024-10-10 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'PTTKYC_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-10-17 15:35:00' AND NgayKetThuc = '2024-10-17 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'PTTKYC_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-10-24 15:35:00' AND NgayKetThuc = '2024-10-24 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'PTTKYC_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-10-31 15:35:00' AND NgayKetThuc = '2024-10-31 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'PTTKYC_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-11-07 15:35:00' AND NgayKetThuc = '2024-11-07 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'PTTKYC_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-11-14 15:35:00' AND NgayKetThuc = '2024-11-14 18:00:00'));


INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'TTUD_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-08-15 13:00:00' AND NgayKetThuc = '2024-08-15 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'TTUD_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-08-22 13:00:00' AND NgayKetThuc = '2024-08-22 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'TTUD_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-08-29 13:00:00' AND NgayKetThuc = '2024-08-29 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'TTUD_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-05 13:00:00' AND NgayKetThuc = '2024-09-05 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'TTUD_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-12 13:00:00' AND NgayKetThuc = '2024-09-12 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'TTUD_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-19 13:00:00' AND NgayKetThuc = '2024-09-19 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'TTUD_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-26 13:00:00' AND NgayKetThuc = '2024-09-26 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'TTUD_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-10-03 13:00:00' AND NgayKetThuc = '2024-10-03 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'TTUD_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-10-10 13:00:00' AND NgayKetThuc = '2024-10-10 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'TTUD_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-10-17 13:00:00' AND NgayKetThuc = '2024-10-17 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'TTUD_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-10-24 13:00:00' AND NgayKetThuc = '2024-10-24 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'TTUD_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-10-31 13:00:00' AND NgayKetThuc = '2024-10-31 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'TTUD_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-11-07 13:00:00' AND NgayKetThuc = '2024-11-07 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'TTUD_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-11-14 13:00:00' AND NgayKetThuc = '2024-11-14 15:25:00'));

INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'XSTK_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-08-12 13:00:00' AND NgayKetThuc = '2024-08-12 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'XSTK_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-08-19 13:00:00' AND NgayKetThuc = '2024-08-19 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'XSTK_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-08-26 13:00:00' AND NgayKetThuc = '2024-08-26 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'XSTK_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-02 13:00:00' AND NgayKetThuc = '2024-09-02 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'XSTK_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-09 13:00:00' AND NgayKetThuc = '2024-09-09 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'XSTK_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-16 13:00:00' AND NgayKetThuc = '2024-09-16 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'XSTK_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-23 13:00:00' AND NgayKetThuc = '2024-09-23 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'XSTK_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-30 13:00:00' AND NgayKetThuc = '2024-09-30 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'XSTK_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-10-07 13:00:00' AND NgayKetThuc = '2024-10-07 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'XSTK_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-10-14 13:00:00' AND NgayKetThuc = '2024-10-14 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'XSTK_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-10-21 13:00:00' AND NgayKetThuc = '2024-10-21 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'XSTK_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-10-28 13:00:00' AND NgayKetThuc = '2024-10-28 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'XSTK_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-11-04 13:00:00' AND NgayKetThuc = '2024-11-04 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'XSTK_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-11-11 13:00:00' AND NgayKetThuc = '2024-11-11 15:25:00'));


INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'VLY_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-04-12 07:00:00' AND NgayKetThuc = '2024-04-12 09:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'VLY_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-04-19 07:00:00' AND NgayKetThuc = '2024-04-19 09:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'VLY_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-04-26 07:00:00' AND NgayKetThuc = '2024-04-26 09:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'VLY_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-05-03 07:00:00' AND NgayKetThuc = '2024-05-03 09:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'VLY_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-05-10 07:00:00' AND NgayKetThuc = '2024-05-10 09:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'VLY_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-05-17 07:00:00' AND NgayKetThuc = '2024-05-17 09:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'VLY_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-05-24 07:00:00' AND NgayKetThuc = '2024-05-24 09:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'VLY_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-05-31 07:00:00' AND NgayKetThuc = '2024-05-31 09:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'VLY_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-06-07 07:00:00' AND NgayKetThuc = '2024-06-07 09:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'VLY_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-06-14 07:00:00' AND NgayKetThuc = '2024-06-14 09:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'VLY_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-06-21 07:00:00' AND NgayKetThuc = '2024-06-21 09:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'VLY_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-06-28 07:00:00' AND NgayKetThuc = '2024-06-28 09:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'VLY_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-07-05 07:00:00' AND NgayKetThuc = '2024-07-05 09:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'VLY_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-07-12 07:00:00' AND NgayKetThuc = '2024-07-12 09:25:00'));


INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'PLDC_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-12-02 09:35:00' AND NgayKetThuc = '2024-12-02 12:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'PLDC_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-12-09 09:35:00' AND NgayKetThuc = '2024-12-09 12:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'PLDC_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-12-16 09:35:00' AND NgayKetThuc = '2024-12-16 12:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'PLDC_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-12-23 09:35:00' AND NgayKetThuc = '2024-12-23 12:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'PLDC_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-12-30 09:35:00' AND NgayKetThuc = '2024-12-30 12:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'PLDC_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2025-01-06 09:35:00' AND NgayKetThuc = '2025-01-06 12:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'PLDC_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2025-01-13 09:35:00' AND NgayKetThuc = '2025-01-13 12:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'PLDC_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2025-01-20 09:35:00' AND NgayKetThuc = '2025-01-20 12:00:00'));
