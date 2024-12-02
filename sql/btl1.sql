-- 1. Tinh tong so tin chi cua sinh vien co IdSinhVien la 'SV01'
-- Truy van nay tinh tong so tin chi ma sinh vien 'SV01' da dang ky, dua tren cac lop hoc phan da tham gia.
SELECT SV.HoTen, SUM(LHP.SoTinChi) AS TongSoTinChi
FROM SinhVien SV
JOIN SinhVien_LopHocPhan SVLHP ON SV.IdSinhVien = SVLHP.IdSinhVien
JOIN LopHocPhan LHP ON SVLHP.IdLopHocPhan = LHP.IdLopHocPhan
WHERE SV.IdSinhVien = '222631111'
GROUP BY SV.HoTen;

-- 2. Tim so lan su dung cua moi phong hoc
SELECT PH.TenPhongHoc, COUNT(TG.IdThoiGian) AS SoLanSuDung
FROM PhongHoc PH
JOIN ThoiGian TG ON PH.IdPhongHoc = TG.IdPhongHoc
GROUP BY PH.TenPhongHoc
ORDER BY SoLanSuDung DESC;

-- 3. Tinh ty le sinh vien dat va khong dat trong moi lop hoc phan
SELECT LHP.TenHocPhan,
       SUM(CASE WHEN D.DiemTongKet >= 5 THEN 1 ELSE 0 END) AS SoLuongDat,
       SUM(CASE WHEN D.DiemTongKet < 5 THEN 1 ELSE 0 END) AS SoLuongKhongDat,
       ROUND(SUM(CASE WHEN D.DiemTongKet >= 5 THEN 1 ELSE 0 END) * 100.0 / COUNT(D.IdDiem), 2) AS TyLeDat,
       ROUND(SUM(CASE WHEN D.DiemTongKet < 5 THEN 1 ELSE 0 END) * 100.0 / COUNT(D.IdDiem), 2) AS TyLeKhongDat
FROM LopHocPhan LHP
JOIN Diem D ON LHP.IdLopHocPhan = D.IdLopHocPhan
GROUP BY LHP.TenHocPhan;

--4. Tim sinh vien co diem tong ket cao nhat trong lop hoc phan 
-- Truy van nay xac dinh sinh vien co diem tong ket cao nhat trong lop hoc phan 'LHP01' bang cach sap xep diem giam dan.
-- Tim tat ca sinh vien co diem tong ket cao nhat trong lop hoc phan 'LSDCSVN_QT05'
SELECT SV.HoTen, D.DiemTongKet
FROM SinhVien SV
JOIN Diem D ON SV.IdSinhVien = D.IdSinhVien
WHERE D.IdLopHocPhan = 'LSDCSVN_QT05'
  AND D.DiemTongKet = (
      SELECT MAX(DiemTongKet)
      FROM Diem
      WHERE IdLopHocPhan = 'LSDCSVN_QT05'
  );

  -- 5. Dem so luong sinh vien dat va khong dat trong tung lop hoc phan
-- Truy van nay dem so luong sinh vien dat va khong dat diem trong tung lop hoc phan, kem ty le.
SELECT LHP.TenHocPhan,
       SUM(CASE WHEN D.DiemTongKet >= 5 THEN 1 ELSE 0 END) AS SoLuongDat,
       SUM(CASE WHEN D.DiemTongKet < 5 THEN 1 ELSE 0 END) AS SoLuongKhongDat,
       ROUND(SUM(CASE WHEN D.DiemTongKet >= 5 THEN 1 ELSE 0 END) * 100.0 / COUNT(D.IdDiem), 2) AS TyLeDat,
       ROUND(SUM(CASE WHEN D.DiemTongKet < 5 THEN 1 ELSE 0 END) * 100.0 / COUNT(D.IdDiem), 2) AS TyLeTruot
FROM LopHocPhan LHP
JOIN Diem D ON LHP.IdLopHocPhan = D.IdLopHocPhan
GROUP BY LHP.TenHocPhan;


-- 6. Dem so luong lop hoc phan ma moi giao vien day va lay giao vien day nhieu nhat
-- Truy van dem so lop hoc phan cua tung giao vien va sap xep de chon giao vien day nhieu nhat.
SELECT GV.TenGiaoVien, COUNT(LHP.IdLopHocPhan) AS SoLopHocPhan
FROM GiaoVien GV
JOIN LopHocPhan LHP ON GV.IdGiaoVien = LHP.IdGiaoVien
GROUP BY GV.TenGiaoVien
HAVING COUNT(LHP.IdLopHocPhan) = (
    SELECT MAX(SoLopHocPhan)
    FROM (
        SELECT COUNT(LHP.IdLopHocPhan) AS SoLopHocPhan
        FROM GiaoVien GV
        JOIN LopHocPhan LHP ON GV.IdGiaoVien = LHP.IdGiaoVien
        GROUP BY GV.TenGiaoVien
    )
);

-- 7. Dem so luong mon hoc trong moi khoa
-- Truy van dem so luong mon hoc trong tung khoa. Dung LEFT JOIN de dam bao khoa khong co mon hoc van hien thi.
SELECT K.TenKhoa, COUNT(MH.IdMonHoc) AS SoLuongMonHoc
FROM Khoa K
LEFT JOIN MonHoc MH ON K.IdKhoa = MH.IdKhoa
GROUP BY K.TenKhoa;

-- 8. Lay trang thai dang ky nguyen vong cua sinh vien co IdSinhVien la 'SV01'
-- Truy van lay thong tin ve cac mon hoc ma sinh vien 'SV01' da dang ky nguyen vong, kem trang thai cua tung mon.
SELECT SV.HoTen, MH.TenMonHoc, DK.TrangThai
FROM SinhVien SV
JOIN DangKyNguyenVong DK ON SV.IdSinhVien = DK.IdSinhVien
JOIN MonHoc MH ON DK.IdMonHoc = MH.IdMonHoc
WHERE SV.IdSinhVien = '222631111';

-- 9. Lay cac lop hoc phan da ket thuc
-- Truy van lay danh sach cac lop hoc phan co thoi gian ket thuc truoc ngay hien tai.
SELECT TenHocPhan, ThoiGianKetThuc
FROM LopHocPhan
WHERE ThoiGianKetThuc < SYSDATE;

-- 10. Lay danh sach sinh vien va diem tong ket cua lop hoc phan 'LHP01'
-- Truy van lay danh sach sinh vien trong lop 'LHP01' cung voi diem tong ket cua tung nguoi.
SELECT SV.HoTen, SV.Lop, D.DiemTongKet
FROM SinhVien SV
JOIN Diem D ON SV.IdSinhVien = D.IdSinhVien
WHERE D.IdLopHocPhan = 'LSDCSVN_QT05';

-- 11. Lay thong tin lop hoc phan ma giao vien 'GV01' day
-- Truy van lay thong tin chi tiet ve cac lop hoc phan ma giao vien 'GV01' phu trach, bao gom thoi gian va phong hoc.
SELECT GV.TenGiaoVien, LHP.TenHocPhan, TG.NgayBatDau, TG.NgayKetThuc, PH.TenPhongHoc
FROM GiaoVien GV
JOIN LopHocPhan LHP ON GV.IdGiaoVien = LHP.IdGiaoVien
JOIN ThoiGian_LopHocPhan TGLHP ON LHP.IdLopHocPhan = TGLHP.IdLopHocPhan
JOIN ThoiGian TG ON TGLHP.IdThoiGian = TG.IdThoiGian
JOIN PhongHoc PH ON TG.IdPhongHoc = PH.IdPhongHoc
WHERE GV.IdGiaoVien = 'dunglm01';

select * from Giaovien;

-- 12. Lay thong tin chi tiet cua sinh vien co IdSinhVien la 'SV01'
-- Truy van tra ve toan bo thong tin cua sinh vien co ma 'SV01'.
SELECT *
FROM SinhVien
WHERE IdSinhVien = '222631111';

-- 13. Lay danh sach giao vien khong day lop hoc phan nao
-- Truy van tim giao vien khong tham gia giang day bat ky lop hoc phan nao bang cach su dung LEFT JOIN.
SELECT GV.TenGiaoVien, GV.Email
FROM GiaoVien GV
LEFT JOIN LopHocPhan LHP ON GV.IdGiaoVien = LHP.IdGiaoVien
WHERE LHP.IdGiaoVien IS NULL;

-- 14. Lay thong tin chi tiet ve cac hoc phan duoc to chuc tai phong hoc '503A8'
-- Truy van lay danh sach cac hoc phan to chuc tai phong '503A8', kem thoi gian bat dau va ket thuc.
SELECT PH.TenPhongHoc, TG.NgayBatDau, TG.NgayKetThuc, LHP.TenHocPhan
FROM ThoiGian TG
JOIN ThoiGian_LopHocPhan TGLHP ON TG.IdThoiGian = TGLHP.IdThoiGian
JOIN LopHocPhan LHP ON TGLHP.IdLopHocPhan = LHP.IdLopHocPhan
JOIN PhongHoc PH ON TG.IdPhongHoc = PH.IdPhongHoc
WHERE PH.IdPhongHoc = '503A8';

-- 15. Dem so luong sinh vien trong tung lop hoc phan
-- Truy van dem so luong sinh vien tham gia tung lop hoc phan.
SELECT LHP.TenHocPhan, COUNT(SVLHP.IdSinhVien) AS SoLuongSinhVien
FROM LopHocPhan LHP
LEFT JOIN SinhVien_LopHocPhan SVLHP ON LHP.IdLopHocPhan = SVLHP.IdLopHocPhan
GROUP BY LHP.TenHocPhan;

-- 16. Tinh diem trung binh cua sinh vien trong cac lop hoc phan
-- Truy van tinh diem trung binh cua tung sinh vien dua tren bang Diem.
SELECT SV.HoTen, SV.Lop, AVG(D.DiemTongKet) AS DiemTrungBinh
FROM Diem D
JOIN SinhVien SV ON D.IdSinhVien = SV.IdSinhVien
GROUP BY SV.HoTen, SV.Lop;

-- 17. Lay danh sach sinh vien co diem tong ket duoi 4
-- Truy van tim sinh vien co diem tong ket duoi muc diem dat (duoi 4).
SELECT DISTINCT SV.HoTen, SV.Lop, D.DiemTongKet
FROM SinhVien SV
JOIN Diem D ON SV.IdSinhVien = D.IdSinhVien
WHERE D.DiemTongKet < 4;



-- 18. Lay danh sach sinh vien co tong so tin chi lon hon 20
-- Truy van lay danh sach sinh vien da dang ky tong so tin chi lon hon 20.
SELECT SV.HoTen, SUM(LHP.SoTinChi) AS TongTinChi
FROM SinhVien SV
JOIN SinhVien_LopHocPhan SVLHP ON SV.IdSinhVien = SVLHP.IdSinhVien
JOIN LopHocPhan LHP ON SVLHP.IdLopHocPhan = LHP.IdLopHocPhan
GROUP BY SV.HoTen
HAVING SUM(LHP.SoTinChi) > 20;

-- 19. Tim sinh vien co so tin chi nhieu nhat
SELECT SV.HoTen, SV.TongSoTinChi
FROM (
    SELECT SV.IdSinhVien, SV.HoTen, SUM(LHP.SoTinChi) AS TongSoTinChi
    FROM SinhVien SV
    JOIN SinhVien_LopHocPhan SVLHP ON SV.IdSinhVien = SVLHP.IdSinhVien
    JOIN LopHocPhan LHP ON SVLHP.IdLopHocPhan = LHP.IdLopHocPhan
    GROUP BY SV.IdSinhVien, SV.HoTen
    ORDER BY SUM(LHP.SoTinChi) DESC
) SV
WHERE ROWNUM = 1;

-- 20. Tim lop hoc phan co diem trung binh cao nhat
SELECT TenHocPhan, DiemTrungBinh
FROM (
    SELECT 
        LHP.TenHocPhan, 
        AVG(D.DiemTongKet) AS DiemTrungBinh,
        RANK() OVER (ORDER BY AVG(D.DiemTongKet) DESC) AS RankHocPhan
    FROM LopHocPhan LHP
    JOIN Diem D ON LHP.IdLopHocPhan = D.IdLopHocPhan
    GROUP BY LHP.TenHocPhan
)
WHERE RankHocPhan = 1;

-- 21. Dem so luong sinh vien dang ky moi mon hoc va lay mon hoc co nhieu sinh vien dang ky nhat
SELECT TenMonHoc, SoLuongSinhVien
FROM (
    SELECT MH.TenMonHoc, COUNT(DK.IdSinhVien) AS SoLuongSinhVien
    FROM MonHoc MH
    JOIN DangKyNguyenVong DK ON MH.IdMonHoc = DK.IdMonHoc
    GROUP BY MH.TenMonHoc
    ORDER BY COUNT(DK.IdSinhVien) DESC
)
WHERE ROWNUM = 1;

-- 20. Tim sinh vien co tong so tin chi lon hon 15 va diem trung binh cao hon 7
SELECT SV.HoTen, SV.TongSoTinChi, SV.DiemTrungBinh
FROM (
    SELECT SV.IdSinhVien, SV.HoTen, SUM(LHP.SoTinChi) AS TongSoTinChi, AVG(D.DiemTongKet) AS DiemTrungBinh
    FROM SinhVien SV
    JOIN SinhVien_LopHocPhan SVLHP ON SV.IdSinhVien = SVLHP.IdSinhVien
    JOIN LopHocPhan LHP ON SVLHP.IdLopHocPhan = LHP.IdLopHocPhan
    JOIN Diem D ON SV.IdSinhVien = D.IdSinhVien
    GROUP BY SV.IdSinhVien, SV.HoTen
) SV
WHERE SV.TongSoTinChi > 15 AND SV.DiemTrungBinh > 7;



-- 21. Tim giao vien co so luong lop hoc phan day nhieu hon muc trung binh
SELECT GV.TenGiaoVien, COUNT(LHP.IdLopHocPhan) AS SoLopHocPhan
FROM GiaoVien GV
JOIN LopHocPhan LHP ON GV.IdGiaoVien = LHP.IdGiaoVien
GROUP BY GV.TenGiaoVien
HAVING COUNT(LHP.IdLopHocPhan) > (
    SELECT AVG(SoLopHocPhan)
    FROM (
        SELECT COUNT(LHP.IdLopHocPhan) AS SoLopHocPhan
        FROM GiaoVien GV
        JOIN LopHocPhan LHP ON GV.IdGiaoVien = LHP.IdGiaoVien
        GROUP BY GV.IdGiaoVien
    )
);

-- 22. Lay danh sach sinh vien co diem tong ket cao nhat trong moi lop hoc phan
SELECT LHP.TenHocPhan, SV.HoTen, D.DiemTongKet
FROM LopHocPhan LHP
JOIN Diem D ON LHP.IdLopHocPhan = D.IdLopHocPhan
JOIN SinhVien SV ON D.IdSinhVien = SV.IdSinhVien
WHERE D.DiemTongKet = (
    SELECT MAX(DiemTongKet)
    FROM Diem
    WHERE IdLopHocPhan = LHP.IdLopHocPhan
);

-- 23. Lay danh sach cac lop hoc phan co ty le do cao hon 80%
SELECT LHP.TenHocPhan, 
       ROUND(SUM(CASE WHEN D.DiemTongKet >= 5 THEN 1 ELSE 0 END) * 100.0 / COUNT(D.IdDiem), 2) AS TyLeDo
FROM LopHocPhan LHP
JOIN Diem D ON LHP.IdLopHocPhan = D.IdLopHocPhan
GROUP BY LHP.TenHocPhan
HAVING ROUND(SUM(CASE WHEN D.DiemTongKet >= 5 THEN 1 ELSE 0 END) * 100.0 / COUNT(D.IdDiem), 2) > 80;

-- 24. Lay danh sach sinh vien co tong so tin chi lon hon 20 va diem trung binh lon hon 7
SELECT SV.HoTen, SUM(LHP.SoTinChi) AS TongSoTinChi, AVG(D.DiemTongKet) AS DiemTrungBinh
FROM SinhVien SV
JOIN SinhVien_LopHocPhan SVLHP ON SV.IdSinhVien = SVLHP.IdSinhVien
JOIN LopHocPhan LHP ON SVLHP.IdLopHocPhan = LHP.IdLopHocPhan
JOIN Diem D ON SV.IdSinhVien = D.IdSinhVien
GROUP BY SV.HoTen
HAVING SUM(LHP.SoTinChi) > 20 AND AVG(D.DiemTongKet) > 7;

-- 25. Dem so luong mon hoc trong moi khoa va lay khoa co so luong mon hoc nhieu nhat
SELECT TenKhoa, SoLuongMonHoc
FROM (
    SELECT K.TenKhoa, COUNT(MH.IdMonHoc) AS SoLuongMonHoc
    FROM Khoa K
    JOIN MonHoc MH ON K.IdKhoa = MH.IdKhoa
    GROUP BY K.TenKhoa
    ORDER BY COUNT(MH.IdMonHoc) DESC
)
WHERE ROWNUM = 1;

-- 26. Tim sinh vien co diem tong ket thap nhat trong moi lop hoc phan
SELECT LHP.TenHocPhan, SV.HoTen, D.DiemTongKet
FROM LopHocPhan LHP
JOIN Diem D ON LHP.IdLopHocPhan = D.IdLopHocPhan
JOIN SinhVien SV ON D.IdSinhVien = SV.IdSinhVien
WHERE D.DiemTongKet = (
    SELECT MIN(DiemTongKet)
    FROM Diem
    WHERE IdLopHocPhan = LHP.IdLopHocPhan
);

-- 27. Dem so luong sinh vien hoc lai tung lop hoc phan va lay lop hoc phan co so luong sinh vien hoc lai nhieu nhat
SELECT TenHocPhan, SoLuongSinhVienHocLai
FROM (
    SELECT LHP.TenHocPhan, COUNT(D.IdSinhVien) AS SoLuongSinhVienHocLai
    FROM LopHocPhan LHP
    JOIN Diem D ON LHP.IdLopHocPhan = D.IdLopHocPhan
    WHERE D.LanHoc > 1
    GROUP BY LHP.TenHocPhan
    ORDER BY COUNT(D.IdSinhVien) DESC
)
WHERE ROWNUM = 1;

-- 28. Tinh diem trung binh cua sinh vien trong tung lop hoc phan va lay lop hoc phan co diem trung binh cao nhat
SELECT TenHocPhan, DiemTrungBinh
FROM (
    SELECT LHP.TenHocPhan, AVG(D.DiemTongKet) AS DiemTrungBinh
    FROM LopHocPhan LHP
    JOIN Diem D ON LHP.IdLopHocPhan = D.IdLopHocPhan
    GROUP BY LHP.TenHocPhan
    ORDER BY AVG(D.DiemTongKet) DESC
)
WHERE ROWNUM = 1;

-- 29. Lay danh sach sinh vien co diem trung binh cao nhat trong khoa
SELECT TenKhoa, HoTen, DiemTrungBinh
FROM (
    SELECT K.TenKhoa, SV.HoTen, AVG(D.DiemTongKet) AS DiemTrungBinh
    FROM Khoa K
    JOIN MonHoc MH ON K.IdKhoa = MH.IdKhoa
    JOIN LopHocPhan LHP ON MH.IdMonHoc = LHP.IdMonHoc
    JOIN Diem D ON LHP.IdLopHocPhan = D.IdLopHocPhan
    JOIN SinhVien SV ON D.IdSinhVien = SV.IdSinhVien
    GROUP BY K.TenKhoa, SV.HoTen
    ORDER BY AVG(D.DiemTongKet) DESC
)
WHERE ROWNUM = 1;

-- 30. Tim sinh vien co tong so tin chi cao nhat trong khoa
WITH TongTinChi_SinhVien AS (
    SELECT SV.IdSinhVien, K.IdKhoa, K.TenKhoa, SUM(LHP.SoTinChi) AS TongSoTinChi
    FROM Khoa K
    JOIN MonHoc MH ON K.IdKhoa = MH.IdKhoa
    JOIN LopHocPhan LHP ON MH.IdMonHoc = LHP.IdMonHoc
    JOIN SinhVien_LopHocPhan SVLHP ON LHP.IdLopHocPhan = SVLHP.IdLopHocPhan
    JOIN SinhVien SV ON SVLHP.IdSinhVien = SV.IdSinhVien
    GROUP BY SV.IdSinhVien, K.IdKhoa, K.TenKhoa
),
MaxTinChi_Khoa AS (
    SELECT IdKhoa, MAX(TongSoTinChi) AS MaxTinChi
    FROM TongTinChi_SinhVien
    GROUP BY IdKhoa
)
SELECT TTSV.TenKhoa, SV.HoTen, TTSV.TongSoTinChi
FROM TongTinChi_SinhVien TTSV
JOIN SinhVien SV ON TTSV.IdSinhVien = SV.IdSinhVien
JOIN MaxTinChi_Khoa MTK ON TTSV.IdKhoa = MTK.IdKhoa AND TTSV.TongSoTinChi = MTK.MaxTinChi;


-- 31. Tim ty le dat va truot cua sinh vien trong moi lop hoc phan cua giao vien
SELECT GV.TenGiaoVien, LHP.TenHocPhan,
       ROUND(SUM(CASE WHEN D.DiemTongKet >= 5 THEN 1 ELSE 0 END) * 100.0 / COUNT(D.IdDiem), 2) AS TyLeDat,
       ROUND(SUM(CASE WHEN D.DiemTongKet < 5 THEN 1 ELSE 0 END) * 100.0 / COUNT(D.IdDiem), 2) AS TyLeTruot
FROM GiaoVien GV
JOIN LopHocPhan LHP ON GV.IdGiaoVien = LHP.IdGiaoVien
JOIN Diem D ON LHP.IdLopHocPhan = D.IdLopHocPhan
GROUP BY GV.TenGiaoVien, LHP.TenHocPhan
ORDER BY TyLeDat DESC;

-- 32. Tim sinh vien chua dang ky hoc lop hoc phan nao
SELECT SV.HoTen
FROM SinhVien SV
LEFT JOIN SinhVien_LopHocPhan SVLHP ON SV.IdSinhVien = SVLHP.IdSinhVien
WHERE SVLHP.IdSinhVien IS NULL;



-- 33. Tim sinh vien hoan thanh tat ca cac mon hoc trong khoang thoi gian
SELECT SV.HoTen, COUNT(DISTINCT LHP.IdMonHoc) AS SoMonHocHoanThanh
FROM SinhVien SV
JOIN SinhVien_LopHocPhan SVLHP ON SV.IdSinhVien = SVLHP.IdSinhVien
JOIN LopHocPhan LHP ON SVLHP.IdLopHocPhan = LHP.IdLopHocPhan
JOIN Diem D ON SV.IdSinhVien = D.IdSinhVien AND LHP.IdLopHocPhan = D.IdLopHocPhan
WHERE D.DiemTongKet >= 5 AND LHP.ThoiGianBatDau BETWEEN TO_DATE('2024-01-01', 'YYYY-MM-DD') AND TO_DATE('2024-05-31', 'YYYY-MM-DD')
GROUP BY SV.HoTen
HAVING COUNT(DISTINCT LHP.IdMonHoc) = (
    SELECT COUNT(DISTINCT LHP2.IdMonHoc)
    FROM LopHocPhan LHP2
    WHERE LHP2.ThoiGianBatDau BETWEEN TO_DATE('2024-01-01', 'YYYY-MM-DD') AND TO_DATE('2024-05-31', 'YYYY-MM-DD')
)
ORDER BY SV.HoTen;



