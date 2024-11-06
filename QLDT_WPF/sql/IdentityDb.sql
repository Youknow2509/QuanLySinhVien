
INSERT INTO Users (Id,IdClaim, FullName,Phone,Email,PasswordHash, UserName,EmailConfirmed, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount)
VALUES
    (NEWID(),'222631111', N'Hoàng Mạnh Khiêm', '0987654321', 'khiem@gmai.com', 'i1CelkDpmAmgU08yFCskzfda4mWOI12kwgW571+2OiY=', '222631111',1,1,1,1,1),
    (NEWID(),'222631159', N'Lý Trần Vinh', '0987654321', 'vinhlt@utc.com', 'i1CelkDpmAmgU08yFCskzfda4mWOI12kwgW571+2OiY=', '222631159',1,1,1,1,1),
    (NEWID(),'222631160', N'Le Van C', '0987654321', 'levanc@utc.com', 'i1CelkDpmAmgU08yFCskzfda4mWOI12kwgW571+2OiY=', '222631160',1,1,1,1,1),
    (NEWID(),'222631161', N'Pham Thi J', '0987654321', 'phamthij@utc.com', 'i1CelkDpmAmgU08yFCskzfda4mWOI12kwgW571+2OiY=', '222631161',1,1,1,1,1),
    (NEWID(), 'dungnb01', N'Bùi Ngọc Dũng', '0915473821', 'dungbn@utc.edu.vn', 'i1CelkDpmAmgU08yFCskzfda4mWOI12kwgW571+2OiY=', 'dungnb01', 1, 1, 1, 1, 1),
    (NEWID(), 'luyenct01', N'Cao Thị Luyên', '0123456789', 'caoluyen@utc.edu.vn', 'i1CelkDpmAmgU08yFCskzfda4mWOI12kwgW571+2OiY=', 'luyenct01', 1, 1, 1, 1, 1),
    (NEWID(), 'dunglm01', N'Lại Mạnh Dũng', '0987654321', 'dunglm@utc.edu.vn', 'i1CelkDpmAmgU08yFCskzfda4mWOI12kwgW571+2OiY=', 'dunglm01', 1, 1, 1, 1, 1),
    (NEWID(), 'thaontt01', N'Nguyễn Thị Thanh Thảo', '0123456789', 'thao@utc.edu.vn', 'i1CelkDpmAmgU08yFCskzfda4mWOI12kwgW571+2OiY=', 'thaontt01', 1, 1, 1, 1, 1),
    (NEWID(), 'huongntt01', N'Nguyễn Thị Thu Hường', '0961423789', 'huongtt@utc.edu.vn', 'i1CelkDpmAmgU08yFCskzfda4mWOI12kwgW571+2OiY=', 'huongntt01', 1, 1, 1, 1, 1),
    (NEWID(), 'hieunt01', N'Nguyễn Trần Hiếu', '0942365478', 'hieunt@utc.edu.vn', 'i1CelkDpmAmgU08yFCskzfda4mWOI12kwgW571+2OiY=', 'hieunt01', 1, 1, 1, 1, 1),
    (NEWID(), 'duongnd01', N'Nguyễn Đình Dương', '0978332467', 'duongnd@utc.edu.vn', 'i1CelkDpmAmgU08yFCskzfda4mWOI12kwgW571+2OiY=', 'duongnd01', 1, 1, 1, 1, 1),
    (NEWID(), 'phongpd01', N'Phạm Đình Phong', '0985432167', 'phongnd@utc.edu.vn', 'i1CelkDpmAmgU08yFCskzfda4mWOI12kwgW571+2OiY=', 'phongpd01', 1, 1, 1, 1, 1),
    (NEWID(), 'tichpx01', N'Phạm Xuân Tích', '0987654321', 'tichpx@utc.edu.vn', 'i1CelkDpmAmgU08yFCskzfda4mWOI12kwgW571+2OiY=', 'tichpx01', 1, 1, 1, 1, 1),
    (NEWID(), 'tungdc01', N'Đinh Công Tùng', '0935421789', 'tungdc@utc.edu.vn', 'i1CelkDpmAmgU08yFCskzfda4mWOI12kwgW571+2OiY=', 'tungdc01', 1, 1, 1, 1, 1),
    (NEWID(), 'thuydtl01', N'Đào Thị Lệ Thủy', '0921374856', 'thuydt@utc.edu.vn', 'i1CelkDpmAmgU08yFCskzfda4mWOI12kwgW571+2OiY=', 'thuydtl01', 1, 1, 1, 1, 1),
    (NEWID(), 'abc123', N'Nguyen Van A', '0912983922', 'nguyenvana@utc.edu.vn', 'i1CelkDpmAmgU08yFCskzfda4mWOI12kwgW571+2OiY=', 'abc123', 1, 1, 1, 1, 1);


-- Gán vai trò 'SinhVien' cho những người dùng có IdClaim bắt đầu bằng '2%'
INSERT INTO UserRoles (UserId, RoleId)
SELECT u.Id, r.Id
FROM Users u
JOIN Roles r ON r.Name = 'SinhVien'
WHERE u.IdClaim LIKE '2%';

-- Gán vai trò 'GiaoVien' cho những người dùng có IdClaim không bắt đầu bằng '2%'
INSERT INTO UserRoles (UserId, RoleId)
SELECT u.Id, r.Id
FROM Users u
JOIN Roles r ON r.Name = 'GiaoVien'
WHERE u.IdClaim NOT LIKE '2%';
