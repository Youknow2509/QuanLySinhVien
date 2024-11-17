INSERT INTO Roles (Id,Name, NormalizedName)
    VALUES
        ('1','Admin', 'ADMIN'),
        ('2','SinhVien', 'SINHVIEN'),
        ('3','GiaoVien', 'GIAOVIEN');

INSERT INTO Users (Id,IdClaim, FullName,Phone,Email,PasswordHash, UserName,EmailConfirmed, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount)
VALUES
    (NEWID(),'admin', 'Admin', 1, NULL, 'i1CelkDpmAmgU08yFCskzfda4mWOI12kwgW571+2OiY=', 'admin', 1, 1, 1, 1, 1);

--Gan quyen cho admin
INSERT INTO UserRoles (UserId,RoleId)
VALUES
    ((SELECT Id FROM Users WHERE UserName = 'admin'), (SELECT Id FROM Roles WHERE Name = 'Admin'));