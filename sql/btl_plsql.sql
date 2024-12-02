-- Thu tuc cap nhat diem
CREATE OR REPLACE PROCEDURE CapNhatDiem(
    p_IdSinhVien VARCHAR2,
    p_IdLopHocPhan VARCHAR2,
    p_DiemQuaTrinh NUMBER,
    p_DiemKetThuc NUMBER,
    p_DiemTongKet NUMBER
) AS
BEGIN
    UPDATE Diem
    SET DiemQuaTrinh = p_DiemQuaTrinh,
        DiemKetThuc = p_DiemKetThuc,
        DiemTongKet = p_DiemTongKet
    WHERE IdSinhVien = p_IdSinhVien
      AND IdLopHocPhan = p_IdLopHocPhan;
    
    COMMIT;
END;
/
-- Chuc nang tinh diem trung binh cua 1 sinh vien
CREATE OR REPLACE FUNCTION TinhDiemTrungBinh(
    p_IdSinhVien VARCHAR2
) RETURN NUMBER IS
    v_DiemTrungBinh NUMBER;
BEGIN
    SELECT AVG(DiemTongKet)
    INTO v_DiemTrungBinh
    FROM Diem
    WHERE IdSinhVien = p_IdSinhVien;

    RETURN v_DiemTrungBinh;
END;
/

-- Ham tinh ty le sinh vien dat
CREATE OR REPLACE FUNCTION TinhTyLeSinhVienDat(
    p_IdLopHocPhan VARCHAR2
) RETURN NUMBER IS
    v_TyLeDat NUMBER;
BEGIN
    SELECT ROUND(SUM(CASE WHEN DiemTongKet >= 4 THEN 1 ELSE 0 END) * 100.0 / COUNT(*), 2)
    INTO v_TyLeDat
    FROM Diem
    WHERE IdLopHocPhan = p_IdLopHocPhan;

    RETURN v_TyLeDat;
END;
/

-- Ham lay danh sach sinh vien khong dat
CREATE OR REPLACE FUNCTION LayDanhSachSinhVienKhongDat(
    p_DiemToiThieu NUMBER
) RETURN SYS_REFCURSOR IS
    v_DanhSachSinhVien SYS_REFCURSOR;
BEGIN
    OPEN v_DanhSachSinhVien FOR
        SELECT SV.IdSinhVien, SV.HoTen, SV.Lop, D.DiemTongKet
        FROM SinhVien SV
        JOIN Diem D ON SV.IdSinhVien = D.IdSinhVien
        WHERE D.DiemTongKet < p_DiemToiThieu;

    RETURN v_DanhSachSinhVien;
END;
/

-- Thu tuc gan sinh vien vao 1 lop
CREATE OR REPLACE PROCEDURE PhanLopChoSinhVien(
    p_IdSinhVien VARCHAR2,
    p_IdLopHocPhan VARCHAR2
) AS
BEGIN
    INSERT INTO SinhVien_LopHocPhan (IdSinhVienLopHP, IdSinhVien, IdLopHocPhan)
    VALUES (
        SYS_GUID(),
        p_IdSinhVien,
        p_IdLopHocPhan
    );

    COMMIT;
END;
/

-- Lay thong tin sinh vien
CREATE OR REPLACE FUNCTION LayThongTinSinhVien(
    p_IdSinhVien VARCHAR2
) RETURN SYS_REFCURSOR IS
    v_ThongTinSinhVien SYS_REFCURSOR;
BEGIN
    OPEN v_ThongTinSinhVien FOR
        SELECT *
        FROM SinhVien
        WHERE IdSinhVien = p_IdSinhVien;

    RETURN v_ThongTinSinhVien;
END;
/
-- Thu tuc xoa tat ca thong tin ve 1 sinh vien
CREATE OR REPLACE PROCEDURE XoaSinhVien(
    p_IdSinhVien VARCHAR2
) AS
BEGIN
    DELETE FROM Diem WHERE IdSinhVien = p_IdSinhVien;
    DELETE FROM SinhVien_LopHocPhan WHERE IdSinhVien = p_IdSinhVien;
    DELETE FROM SinhVien WHERE IdSinhVien = p_IdSinhVien;

    COMMIT;
END;
/

-- Thu tuc cap nhat thong tin sinh vien
CREATE OR REPLACE PROCEDURE CapNhatThongTinSinhVien(
    p_IdSinhVien VARCHAR2,
    p_HoTen VARCHAR2,
    p_Lop VARCHAR2,
    p_NgaySinh DATE,
    p_DiaChi VARCHAR2
) AS
BEGIN
    UPDATE SinhVien
    SET HoTen = p_HoTen,
        Lop = p_Lop,
        NgaySinh = p_NgaySinh,
        DiaChi = p_DiaChi
    WHERE IdSinhVien = p_IdSinhVien;

    COMMIT;
END;
/

-- Ham tinh tong so tin chi cua sinh vien
CREATE OR REPLACE FUNCTION TinhTongSoTinChi(
    p_IdSinhVien VARCHAR2
) RETURN NUMBER IS
    v_TongSoTinChi NUMBER;
BEGIN
    SELECT SUM(LHP.SoTinChi)
    INTO v_TongSoTinChi
    FROM SinhVien_LopHocPhan SVLHP
    JOIN LopHocPhan LHP ON SVLHP.IdLopHocPhan = LHP.IdLopHocPhan
    WHERE SVLHP.IdSinhVien = p_IdSinhVien;

    RETURN v_TongSoTinChi;
END;
/

-- Thu tuc them 1 sinh vien moi
CREATE OR REPLACE PROCEDURE ThemSinhVienMoi(
    p_IdSinhVien VARCHAR2,
    p_HoTen VARCHAR2,
    p_Lop VARCHAR2,
    p_NgaySinh DATE,
    p_DiaChi VARCHAR2,
    p_IdChuongTrinhHoc VARCHAR2,
    p_IdKhoa VARCHAR2
) AS
BEGIN
    INSERT INTO SinhVien (IdSinhVien, HoTen, Lop, NgaySinh, DiaChi, IdChuongTrinhHoc, IdKhoa)
    VALUES (p_IdSinhVien, p_HoTen, p_Lop, p_NgaySinh, p_DiaChi, p_IdChuongTrinhHoc, p_IdKhoa);

    COMMIT;
END;
/

-- Thu tuc cap nhat trang thai dang ky nguyen vong
CREATE OR REPLACE PROCEDURE CapNhatTrangThaiDangKyNguyenVong(
    p_IdSinhVien VARCHAR2,
    p_IdMonHoc VARCHAR2,
    p_TrangThai VARCHAR2
) AS
BEGIN
    UPDATE DangKyNguyenVong
    SET TrangThai = p_TrangThai
    WHERE IdSinhVien = p_IdSinhVien AND IdMonHoc = p_IdMonHoc;

    COMMIT;
END;
/



-- Ham lay danh sach lop hoc phan da ket thuc
CREATE OR REPLACE FUNCTION LayDanhSachLopHocPhanDaKetThuc RETURN SYS_REFCURSOR IS
    v_DanhSachLopHocPhan SYS_REFCURSOR;
BEGIN
    OPEN v_DanhSachLopHocPhan FOR
        SELECT TenHocPhan, ThoiGianKetThuc
        FROM LopHocPhan
        WHERE ThoiGianKetThuc < SYSDATE;

    RETURN v_DanhSachLopHocPhan;
END;
/
-- Thu tuc xoa lop hoc phan
CREATE OR REPLACE PROCEDURE XoaLopHocPhan(
    p_IdLopHocPhan VARCHAR2
) AS
BEGIN
    DELETE FROM Diem WHERE IdLopHocPhan = p_IdLopHocPhan;
    DELETE FROM SinhVien_LopHocPhan WHERE IdLopHocPhan = p_IdLopHocPhan;
    DELETE FROM LopHocPhan WHERE IdLopHocPhan = p_IdLopHocPhan;

    COMMIT;
END;
/



-- Thu tuc them mon hoc moi
CREATE OR REPLACE PROCEDURE ThemMonHocMoi(
    p_IdMonHoc VARCHAR2,
    p_TenMonHoc VARCHAR2,
    p_IdKhoa VARCHAR2,
    p_SoTinChi NUMBER
) AS
BEGIN
    INSERT INTO MonHoc (IdMonHoc, TenMonHoc, IdKhoa, SoTinChi)
    VALUES (p_IdMonHoc, p_TenMonHoc, p_IdKhoa, p_SoTinChi);

    COMMIT;
END;
/

-- Ham lay danh sach giao vien khong day lop hoc phan nao
CREATE OR REPLACE FUNCTION LayDanhSachGiaoVienKhongDay RETURN SYS_REFCURSOR IS
    v_DanhSachGiaoVien SYS_REFCURSOR;
BEGIN
    OPEN v_DanhSachGiaoVien FOR
        SELECT GV.TenGiaoVien, GV.Email
        FROM GiaoVien GV
        LEFT JOIN LopHocPhan LHP ON GV.IdGiaoVien = LHP.IdGiaoVien
        WHERE LHP.IdGiaoVien IS NULL;

    RETURN v_DanhSachGiaoVien;
END;
/

-- Ham lay thong tin lop hoc phan
CREATE OR REPLACE FUNCTION LayThongTinLopHocPhan(
    p_IdLopHocPhan VARCHAR2
) RETURN SYS_REFCURSOR IS
    v_ThongTinLopHocPhan SYS_REFCURSOR;
BEGIN
    OPEN v_ThongTinLopHocPhan FOR
        SELECT LHP.*, GV.TenGiaoVien
        FROM LopHocPhan LHP
        JOIN GiaoVien GV ON LHP.IdGiaoVien = GV.IdGiaoVien
        WHERE LHP.IdLopHocPhan = p_IdLopHocPhan;

    RETURN v_ThongTinLopHocPhan;
END;
/

-- Thu tuc cap nhat thong tin mon hoc trong co so du lieu
CREATE OR REPLACE PROCEDURE CapNhatThongTinMonHoc(
    p_IdMonHoc VARCHAR2,       -- Id cua mon hoc
    p_TenMonHoc VARCHAR2,      -- Ten cua mon hoc
    p_SoTinChi NUMBER,         -- So tin chi cua mon hoc
    p_IdKhoa VARCHAR2          -- Id cua khoa
) AS
BEGIN
    UPDATE MonHoc
    SET TenMonHoc = p_TenMonHoc,
        SoTinChi = p_SoTinChi,
        IdKhoa = p_IdKhoa
    WHERE IdMonHoc = p_IdMonHoc;
    
    COMMIT;
END;
/



-- Thu tuc cap nhat thong tin giao vien trong co so du lieu
CREATE OR REPLACE PROCEDURE CapNhatThongTinGiaoVien(
    p_IdGiaoVien VARCHAR2,     -- Id cua giao vien
    p_TenGiaoVien VARCHAR2,    -- Ten cua giao vien
    p_Email VARCHAR2,          -- Email cua giao vien
    p_DienThoai VARCHAR2       -- So dien thoai cua giao vien
) AS
BEGIN
    UPDATE GiaoVien
    SET TenGiaoVien = p_TenGiaoVien,
        Email = p_Email,
        DienThoai = p_DienThoai
    WHERE IdGiaoVien = p_IdGiaoVien;
    
    COMMIT;
END;
/



-- Thu tuc xoa mot mon hoc ra khoi co so du lieu
CREATE OR REPLACE PROCEDURE XoaMonHoc(
    p_IdMonHoc VARCHAR2        -- Id cua mon hoc
) AS
BEGIN
    DELETE FROM MonHoc WHERE IdMonHoc = p_IdMonHoc;
    
    COMMIT;
END;
/



-- Thu tuc lay danh sach lop hoc phan cua mot khoa
CREATE OR REPLACE PROCEDURE LayDanhSachLopHocPhanTheoKhoa(
    p_IdKhoa VARCHAR2,         -- Id cua khoa
    v_DanhSachLopHocPhan OUT SYS_REFCURSOR -- Con tro ket qua tra ve
) AS
BEGIN
    OPEN v_DanhSachLopHocPhan FOR
        SELECT LHP.TenHocPhan, LHP.ThoiGianBatDau, LHP.ThoiGianKetThuc
        FROM LopHocPhan LHP
        JOIN MonHoc MH ON LHP.IdMonHoc = MH.IdMonHoc
        WHERE MH.IdKhoa = p_IdKhoa;
END;
/



-- Ham tinh so luong sinh vien trong mot lop hoc phan
CREATE OR REPLACE FUNCTION TinhSoLuongSinhVien(
    p_IdLopHocPhan VARCHAR2    -- Id cua lop hoc phan
) RETURN NUMBER IS
    v_SoLuongSinhVien NUMBER;
BEGIN
    SELECT COUNT(IdSinhVien)
    INTO v_SoLuongSinhVien
    FROM SinhVien_LopHocPhan
    WHERE IdLopHocPhan = p_IdLopHocPhan;

    RETURN v_SoLuongSinhVien;
END;
/



-- Ham tinh tong so mon hoc trong mot khoa
CREATE OR REPLACE FUNCTION TinhTongSoMonHoc(
    p_IdKhoa VARCHAR2          -- Id cua khoa
) RETURN NUMBER IS
    v_TongSoMonHoc NUMBER;
BEGIN
    SELECT COUNT(IdMonHoc)
    INTO v_TongSoMonHoc
    FROM MonHoc
    WHERE IdKhoa = p_IdKhoa;

    RETURN v_TongSoMonHoc;
END;
/




-- Ham lay danh sach sinh vien theo khoa
CREATE OR REPLACE FUNCTION LayDanhSachSinhVienTheoKhoa(
    p_IdKhoa VARCHAR2          -- Id cua khoa
) RETURN SYS_REFCURSOR IS
    v_DanhSachSinhVien SYS_REFCURSOR;
BEGIN
    OPEN v_DanhSachSinhVien FOR
        SELECT SV.*
        FROM SinhVien SV
        WHERE SV.IdKhoa = p_IdKhoa;

    RETURN v_DanhSachSinhVien;
END;
/

    


-- Ham tinh ty le sinh vien qua mot mon hoc
CREATE OR REPLACE FUNCTION TinhTyLeQuaMon(
    p_IdMonHoc VARCHAR2        -- Id cua mon hoc
) RETURN NUMBER IS
    v_TyLeQuaMon NUMBER;
BEGIN
    SELECT ROUND(SUM(CASE WHEN DiemTongKet >= 4 THEN 1 ELSE 0 END) * 100.0 / COUNT(*), 2)
    INTO v_TyLeQuaMon
    FROM Diem D
    JOIN LopHocPhan LHP ON D.IdLopHocPhan = LHP.IdLopHocPhan
    WHERE LHP.IdMonHoc = p_IdMonHoc;

    RETURN v_TyLeQuaMon;
END;
/


-- Ch?y th? t?c c?p nh?t ?i?m
BEGIN
    CapNhatDiem('222631159', 'XSTK_QT01', 7.5, 8.0, 7.8);
END;
select * from diem where idsinhvien ='222631159' and idlophocphan = 'XSTK_QT01';
select * from sinhvien where hoten like n'Lý %';



-- Ch?y ch?c n?ng tính ?i?m trung bình c?a 1 sinh viên
DECLARE
    v_DiemTrungBinh NUMBER;
BEGIN
    v_DiemTrungBinh := TinhDiemTrungBinh('222631159');
    DBMS_OUTPUT.PUT_LINE('Diem Trung Binh: ' || v_DiemTrungBinh);
END;
/

-- Ch?y th? t?c gán sinh viên vào 1 l?p
BEGIN
    PhanLopChoSinhVien('222631160', 'XSTK_QT01');
END;
/

-- Ch?y ch?c n?ng l?y thông tin sinh viên
DECLARE
    v_ThongTinSinhVien SYS_REFCURSOR;
    v_HoTen VARCHAR2(100);
    v_Lop VARCHAR2(50);
    v_NgaySinh DATE;
    v_DiaChi VARCHAR2(255);
BEGIN
    v_ThongTinSinhVien := LayThongTinSinhVien('SV01');
    LOOP
        FETCH v_ThongTinSinhVien INTO v_HoTen, v_Lop, v_NgaySinh, v_DiaChi;
        EXIT WHEN v_ThongTinSinhVien%NOTFOUND;
        DBMS_OUTPUT.PUT_LINE('Ho Ten: ' || v_HoTen || ', Lop: ' || v_Lop || ', Ngay Sinh: ' || v_NgaySinh || ', Dia Chi: ' || v_DiaChi);
    END LOOP;
    CLOSE v_ThongTinSinhVien;
END;
/

-- Ch?y th? t?c xóa t?t c? thông tin v? 1 sinh viên
BEGIN
    XoaSinhVien('SV01');
END;
/

-- Ch?y th? t?c c?p nh?t thông tin sinh viên
BEGIN
    CapNhatThongTinSinhVien('SV01', 'Nguyen Van A', 'Lop 12A1', TO_DATE('2002-05-20', 'YYYY-MM-DD'), '123 Duong ABC, Ha Noi');
END;
/

-- Ch?y ch?c n?ng tính t?ng s? tín ch? c?a sinh viên
DECLARE
    v_TongSoTinChi NUMBER;
BEGIN
    v_TongSoTinChi := TinhTongSoTinChi('SV01');
    DBMS_OUTPUT.PUT_LINE('Tong So Tin Chi: ' || v_TongSoTinChi);
END;
/



-- Ch?y ch?c n?ng l?y danh sách sinh viên không ??t
DECLARE
    v_DanhSachSinhVien SYS_REFCURSOR;
    v_IdSinhVien VARCHAR2(100);
    v_HoTen VARCHAR2(100);
    v_Lop VARCHAR2(50);
    v_DiemTongKet NUMBER;
BEGIN
    v_DanhSachSinhVien := LayDanhSachSinhVienKhongDat(4);
    LOOP
        FETCH v_DanhSachSinhVien INTO v_IdSinhVien, v_HoTen, v_Lop, v_DiemTongKet;
        EXIT WHEN v_DanhSachSinhVien%NOTFOUND;
        DBMS_OUTPUT.PUT_LINE('Id: ' || v_IdSinhVien || ', Ho Ten: ' || v_HoTen || ', Lop: ' || v_Lop || ', Diem Tong Ket: ' || v_DiemTongKet);
    END LOOP;
    CLOSE v_DanhSachSinhVien;
END;
/

-- Ch?y ch?c n?ng l?y danh sách l?p h?c ph?n ?ã k?t thúc
DECLARE
    v_DanhSachLopHocPhan SYS_REFCURSOR;
    v_TenHocPhan VARCHAR2(100);
    v_ThoiGianKetThuc DATE;
BEGIN
    v_DanhSachLopHocPhan := LayDanhSachLopHocPhanDaKetThuc;
    LOOP
        FETCH v_DanhSachLopHocPhan INTO v_TenHocPhan, v_ThoiGianKetThuc;
        EXIT WHEN v_DanhSachLopHocPhan%NOTFOUND;
        DBMS_OUTPUT.PUT_LINE('Ten Hoc Phan: ' || v_TenHocPhan || ', Thoi Gian Ket Thuc: ' || v_ThoiGianKetThuc);
    END LOOP;
    CLOSE v_DanhSachLopHocPhan;
END;
/

-- Ch?y th? t?c xóa l?p h?c ph?n
BEGIN
    XoaLopHocPhan('LHP01');
END;
/

-- Ch?y ch?c n?ng tính t? l? sinh viên ??t
DECLARE
    v_TyLeDat NUMBER;
BEGIN
    v_TyLeDat := TinhTyLeSinhVienDat('XSTK_QT01');
    DBMS_OUTPUT.PUT_LINE('Ty Le Dat: ' || v_TyLeDat || '%');
END;
/

-- Ch?y th? t?c thêm môn h?c m?i
BEGIN
    ThemMonHocMoi('MH02', 'Lap Trinh C++', 'KH01', 3);
END;
/

-- Ch?y ch?c n?ng l?y danh sách giáo viên không d?y l?p h?c ph?n nào
DECLARE
    v_DanhSachGiaoVien SYS_REFCURSOR;
    v_TenGiaoVien VARCHAR2(100);
    v_Email VARCHAR2(100);
BEGIN
    v_DanhSachGiaoVien := LayDanhSachGiaoVienKhongDay;
    LOOP
        FETCH v_DanhSachGiaoVien INTO v_TenGiaoVien, v_Email;
        EXIT WHEN v_DanhSachGiaoVien%NOTFOUND;
        DBMS_OUTPUT.PUT_LINE('Ten Giao Vien: ' || v_TenGiaoVien || ', Email: ' || v_Email);
    END LOOP;
    CLOSE v_DanhSachGiaoVien;
END;
/

-- Ch?y ch?c n?ng l?y thông tin l?p h?c ph?n
DECLARE
    v_ThongTinLopHocPhan SYS_REFCURSOR;
    v_TenHocPhan VARCHAR2(100);
    v_TenGiaoVien VARCHAR2(100);
BEGIN
    v_ThongTinLopHocPhan := LayThongTinLopHocPhan('LHP01');
    LOOP
        FETCH v_ThongTinLopHocPhan INTO v_TenHocPhan, v_TenGiaoVien;
        EXIT WHEN v_ThongTinLopHocPhan%NOTFOUND;
        DBMS_OUTPUT.PUT_LINE('Ten Hoc Phan: ' || v_TenHocPhan || ', Ten Giao Vien: ' || v_TenGiaoVien);
    END LOOP;
    CLOSE v_ThongTinLopHocPhan;
END;
/

-- Ch?y th? t?c c?p nh?t thông tin môn h?c
BEGIN
    CapNhatThongTinMonHoc('MH01', 'Lap Trinh Java', 4, 'KH01');
END;
/

-- Ch?y th? t?c c?p nh?t thông tin giáo viên
BEGIN
    CapNhatThongTinGiaoVien('GV01', 'Le Thi C', 'lethi.c@gmail.com', '0987654321');
END;
/

-- Ch?y th? t?c xóa môn h?c
BEGIN
    XoaMonHoc('MH01');
END;
/

-- Ch?y th? t?c l?y danh sách l?p h?c ph?n c?a m?t khoa
DECLARE
    v_DanhSachLopHocPhan SYS_REFCURSOR;
    v_TenHocPhan VARCHAR2(100);
    v_ThoiGianBatDau DATE;
    v_ThoiGianKetThuc DATE;
BEGIN
    LayDanhSachLopHocPhanTheoKhoa('KH01', v_DanhSachLopHocPhan);
    LOOP
        FETCH v_DanhSachLopHocPhan INTO v_TenHocPhan, v_ThoiGianBatDau, v_ThoiGianKetThuc;
        EXIT WHEN v_DanhSachLopHocPhan%NOTFOUND;
        DBMS_OUTPUT.PUT_LINE('Ten Hoc Phan: ' || v_TenHocPhan || ', Thoi Gian Bat Dau: ' || v_ThoiGianBatDau || ', Thoi Gian Ket Thuc: ' || v_ThoiGianKetThuc);
    END LOOP;
    CLOSE v_DanhSachLopHocPhan;
END;
/

-- Ch?y ch?c n?ng tính s? l??ng sinh viên trong m?t l?p h?c ph?n
DECLARE
    v_SoLuongSinhVien NUMBER;
BEGIN
    v_SoLuongSinhVien := TinhSoLuongSinhVien('XSTK_QT01');
    DBMS_OUTPUT.PUT_LINE('So Luong Sinh Vien: ' || v_SoLuongSinhVien);
END;
/

-- Ch?y ch?c n?ng tính t?ng s? môn h?c trong m?t khoa
DECLARE
    v_TongSoMonHoc NUMBER;
BEGIN
    v_TongSoMonHoc := TinhTongSoMonHoc('DTQT');
    DBMS_OUTPUT.PUT_LINE('Tong So Mon Hoc: ' || v_TongSoMonHoc);
END;
/


