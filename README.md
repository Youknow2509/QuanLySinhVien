# Quản Lý Sinh Viên Xử Dụng DataBase Oracle

# Contact:
- **Mail**: *lytranvinh.work@gmail.com*
- **Github**: *https://github.com/Youknow2509*

# Một Số Lệnh Hay Dùng Trong Project:

- Tạo Models từ database:
```shell
    dotnet ef dbcontext scaffold "User Id=vinh;Password=123;Data Source=localhost:1521/orclcdb1" Oracle.EntityFrameworkCore -o Models --context-dir Data -c QuanLySinhVienDbContext --force
```

- Restore and Rebuild project:
```shell
    dotnet restore
    dotnet build
```

# Cách Tạo Oracle DataBase Trên Docker

- Tải SingleInstance để tạo Image: [Dowload In](https://github.com/oracle/docker-images/tree/main/OracleDatabase/SingleInstance)

- Tải Linux ARM64 Support để `Container` chạy: [Dowload In](https://www.oracle.com/database/technologies/oracle19c-linux-arm64-downloads.html)

- Chuyển đến thư mục `OracleDatabase/SingleInstance/dockerfiles` chọn `version` cần dùng cho tệp `LINUX.ARM64_1919000_db_home.zip` vừa tải vào `version` muốn xử dụng.

- Chạy `shell`:
```bash
[oracle@localhost dockerfiles]$ ./buildContainerImage.sh -h

Usage: buildContainerImage.sh -v [version] -t [image_name:tag] [-e | -s | -x | -f] [-i] [-p] [-b] [-o] [container build option]
Builds a container image for Oracle Database.

Parameters:
   -v: version to build
       Choose one of: 11.2.0.2  12.1.0.2  12.2.0.1  18.3.0  18.4.0  19.3.0  21.3.0 23.5.0
   -t: image_name:tag for the generated docker image
   -e: creates image based on 'Enterprise Edition'
   -s: creates image based on 'Standard Edition 2'
   -x: creates image based on 'Express Edition'
   -f: creates image based on Database 'Free'
   -i: ignores the MD5 checksums
   -p: creates and extends image using the patching extension
   -b: build base stage only (Used by extensions)
   -o: passes on container build option

* select one edition only: -e, -s, -x, or -f

LICENSE UPL 1.0

Copyright (c) 2014,2024 Oracle and/or its affiliates.
```
Example:
```bash
./buildContainerImage.sh -e -v 19.3.0
```

- Đợi khởi tạo Oracle Database Image ...

- Sau khi tạo xong khởi tạo Docker Container:

```bash
  docker run \
    -it --name oracle-db\
    -p 1521:1521 -p 5500:5500 -p 8080:8080 \
    -e ORACLE_SID=ORCLCDB \
    -e ORACLE_PDB=ORCLCDB1 \
    -e ORACLE_PWD= 123456789\
    -e ENABLE_TCPS=true \
    -v {Your_Path_Volume}:/opt/oracle/oradata \ # Nên tạo volume để dữ liệu được ánh xạ lưu trực tiếp trên thư mục chỉ định trên máy bạn - Nếu không muốn hoặc gặp vấn đề cấp truyền thư mục hãy xoá dòng này
    -d \
    oracle/database:19.3.0-ee
```
- **!!! Chú ý check log đợi khởi tạo 100% thì mới lên cdb và tất cả chức năng !!!**

- Sau khi tạo **Image**, ta được:
    - Port: 
        - 1521: Port Oracle Xử dụng.
        - 5500: Port Oracle Enterprise Manager Database Express (EM)  (`https://localhost:5500/em/`) - Quản Lý, Giám Sát Hệ Cơ Sở Dữ Liệu ...
        - 8080: Port thường Setup Oracle Apex - Ánh Xạ Ra Trước.
    - CDB: Container `ORCLCDB1`
    - PASSWORD: Password Root `123456789`

# Khởi Tạo Schema
- Tạo thư mục lưu **datafiles**, xem **path** thư mục để tạo **datafiles**
```bash
    cd ~
    mkdir datafiles
    cd datafiles
    pwd
    # /home/oracle/datafiles
```

- Đăng nhập vào **CBD**:
```bash
    sqlplus system/123456789@orclcdb1;
```

- Tạo `tablespace` lưu dữ liệu bài tập lớn **riêng**
```sql
    CREATE TABLESPACE TBS_BTL
    DATAFILE '/home/oracle/datafiles/datafile_tbs_btl.dbf' SIZE 100M;
```

- Kiểm tra `tablespace` vừa tạo:
```sql
    SELECT TABLESPACE_NAME, STATUS, LOGGING FROM USER_TABLESPACES WHERE TABLESPACE_NAME = 'TBS_BTL';
```

- Tạo `User` với `tablespace` vừa tạo:
```sql
    CREATE USER VINH IDENTIFIED BY 123
    DEFAULT TABLESPACE TBS_BTL
    TEMPORARY TABLESPACE TEMP
    QUOTA 10M ON TBS_BTL;
```
-> Tạo `User` với `username`: `vinh` và `password`:  `123` trong `cdb`: `orclcdb1`.

- Gán quyền đăng nhập tạo bảng cho user `vinh`
```sql
    GRANT CREATE SESSION TO VINH;
    GRANT CREATE TABLE TO VINH;
```

- Import cấu trúc bảng tại [file](sql/create_table.sql)
- Import dữ liệu **template** [file](sql/insert_data_table.sql)

-> **String connection**: `User Id=vinh;Password=123;Data Source=localhost:1521/orclcdb1`

#  Quản Trị CSDL Oracle

## Quản Lý Instant
### Các chế độ của Instance
#### Shutdown Instance
- Tắt `Instance` với các chế độ khác nhau - *Chú ý phải đăng nhập tải khoản* `dba`.
    - `NORMAL`: Đóng cơ sở dữ liệu `sau khi tất cả các user đã logout`.
    - `IMMEDIATE`: Tắt `ngay lập tức`, các `giao dịch đang chạy bị hủy`.
    - `ABORT`: Tắt ngay lập tức, không chờ xử lý.
```bash
    SHUTDOWN NORMAL;
    SHUTDOWN IMMEDIATE;
    SHUTDOWN ABORT;
```

| **Tiêu chí**	| **IMMEDIATE** | **ABORT** |
| :------------:| :------------------------------------------: | :----------: |
| Hủy giao dịch | Có (rollback tất cả các giao dịch chưa xong) | Không rollback, giao dịch bị bỏ qua |
| Ghi dữ liệu về disk | Có | Không | 
| Thời gian tắt	| Lâu hơn ABORT | Nhanh Nhất |
| Yêu cầu Instance Recovery | Không cần | Cần Instance Recovery khi khởi động |
| Trường hợp sử dụng | Khi cần tắt nhanh nhưng vẫn đảm bảo dữ liệu an toàn. | Khi gặp lỗi hoặc cần tắt khẩn cấp. |

#### Start Instance
- Khởi động Oracle Instance để sẵn sàng kết nối với cơ sở dữ liệu.
    - `NOMOUNT`: Chỉ khởi động bộ nhớ và các tiến trình nền, chưa gắn cơ sở dữ liệu.
    - `MOUNT`: Gắn cơ sở dữ liệu vào instance (truy cập control file nhưng chưa mở datafile).
    - `OPEN`: Mở toàn bộ cơ sở dữ liệu cho phép người dùng truy cập.
```sql
  STARTUP NOMOUNT;
  STARTUP MOUNT;
  STARTUP OPEN;
```

### Chế độ truy cập Instance
#### READ ONLY
- Cơ sở dữ liệu `chỉ` cho phép `đọc`, không được phép chỉnh sửa.
```sql
    ALTER DATABASE OPEN READ ONLY;
```
#### RESTRICTED SESSION
- Chỉ những user có quyền `đặc biệt` mới được truy cập (thường dùng trong bảo trì).
```sql
    ALTER SYSTEM ENABLE RESTRICTED SESSION;
```
- Gán quyền cho người dùng: 
```sql
    GRANT RESTRICTED SESSION TO {username};
```
- Kiểm tra trạng thái **RESTRICTED SESSION**
```bash
    SELECT VALUE FROM V$PARAMETER WHERE NAME = 'restricted_session';
    # TRUE: Chế độ đang bật.
    # FALSE: Chế độ đang tắt.
```
- Khi bật chế độ này:
    - Các kết nối hiện tại sẽ không bị ảnh hưởng, nhưng các **kết nối mới sẽ bị chặn** (nếu không có quyền RESTRICTED SESSION).

#### Xem trạng thái hiện tại của Instance
```bash
    SELECT INSTANCE_NAME, STATUS FROM V$INSTANCE;
```
## Quản Lý TableSpace
- **TableSpace** là đơn vị lưu trữ **logic** trong Oracle, bao gồm một hoặc nhiều **datafile** (tệp dữ liệu vật lý trên đĩa).
  
### Kiểm Tra Thông Tin TableSpace
#### Thông Tin Chung
```bash
SELECT TABLESPACE_NAME, STATUS, CONTENTS, BIGFILE, SEGMENT_SPACE_MANAGEMENT
FROM DBA_TABLESPACES;
```
- Giải thích các cột:
  - **TABLESPACE_NAME**: Tên của tablespace.
  - **STATUS**: Trạng thái hiện tại của tablespace (**ONLINE**, **OFFLINE**, **READ** **ONLY**).
  - **CONTENTS**:
    - **PERMANENT**: Lưu trữ dữ liệu vĩnh viễn.
    - **TEMPORARY**: Lưu trữ dữ liệu tạm thời.
    - **UNDO**: Lưu trữ dữ liệu undo.
  - **BIGFILE**: Tablespace có sử dụng bigfile hay không (YES hoặc NO)
    - **YES**: Tablespace là một bigfile tablespace, tức là nó chỉ chứa một file dữ liệu rất lớn (lên đến 128 TB, tùy thuộc vào kích thước block).
    - **NO**: Tablespace là một smallfile tablespace, tức là có thể chứa nhiều file dữ liệu nhỏ hơn (thường tối đa là 1022 file).
  - **SEGMENT_SPACE_MANAGEMENT**: Kiểm soát không gian segment (AUTO hoặc MANUAL).
#### Dung Lượng Sử Dụng
```bash
SELECT DATA_FILES.TABLESPACE_NAME,
       ROUND(SUM(DATA_FILES.BYTES) / 1024 / 1024, 2) AS TOTAL_SIZE_MB,
       ROUND(SUM(DATA_FILES.BYTES - NVL(FREE_SPACE.BYTES, 0)) / 1024 / 1024, 2) AS USED_SIZE_MB,
       ROUND(SUM(NVL(FREE_SPACE.BYTES, 0)) / 1024 / 1024, 2) AS FREE_SIZE_MB,
       ROUND(SUM(NVL(FREE_SPACE.BYTES, 0)) * 100 / SUM(DATA_FILES.BYTES), 2) AS FREE_PERCENT
FROM DBA_DATA_FILES DATA_FILES
LEFT JOIN DBA_FREE_SPACE FREE_SPACE
ON DATA_FILES.FILE_ID = FREE_SPACE.FILE_ID
GROUP BY DATA_FILES.TABLESPACE_NAME;
```

#### Xem datafile của một tablespace
```bash
SELECT FILE_NAME, 
       TABLESPACE_NAME,
       ROUND(BYTES / 1024 / 1024, 2) AS SIZE_MB,
       AUTOEXTENSIBLE, 
       ROUND(MAXBYTES / 1024 / 1024, 2) AS MAX_SIZE_MB,
       INCREMENT_BY * (TS.BLOCK_SIZE / 1024) AS INCREMENT_SIZE_MB
FROM DBA_DATA_FILES DF
JOIN DBA_TABLESPACES TS
ON DF.TABLESPACE_NAME = TS.TABLESPACE_NAME
WHERE DF.TABLESPACE_NAME = 'USERS'; # Thay 'USERS' bằng table muốn tìm hoặc xoá đi để xem tất cả
```

##### Kiểm tra Tablespace tạm thời (Temporary Tablespace)
```bash
  SELECT TABLESPACE_NAME, FILE_NAME,
         ROUND(BYTES/1024/1024, 2) AS SIZE_MB,
         AUTOEXTENSIBLE, MAXBYTES/1024/1024 AS MAX_SIZE_MB
  FROM DBA_TEMP_FILES;
```
##### Kiểm tra Undo Tablespace
```bash
  SELECT TABLESPACE_NAME, FILE_NAME,
       ROUND(BYTES/1024/1024, 2) AS SIZE_MB,
       AUTOEXTENSIBLE, MAXBYTES/1024/1024 AS MAX_SIZE_MB
  FROM DBA_DATA_FILES
  WHERE TABLESPACE_NAME = (SELECT VALUE FROM V$PARAMETER WHERE NAME = 'undo_tablespace');
```
#### Kiểm tra trạng thái ONLINE/OFFLINE của Tablespace
```bash
SELECT TABLESPACE_NAME, STATUS
FROM DBA_TABLESPACES;
```
#### Kiểm tra phần trăm dung lượng còn trống
```sql
SELECT TABLESPACE_NAME,
       ROUND((TOTAL_MB - USED_MB), 2) AS FREE_MB,
       ROUND((FREE_MB / TOTAL_MB) * 100, 2) AS FREE_PERCENT
FROM (
    SELECT DATA_FILES.TABLESPACE_NAME,
           SUM(DATA_FILES.BYTES) / 1024 / 1024 AS TOTAL_MB,
           SUM(DATA_FILES.BYTES - NVL(FREE_SPACE.BYTES, 0)) / 1024 / 1024 AS USED_MB,
           SUM(NVL(FREE_SPACE.BYTES, 0)) / 1024 / 1024 AS FREE_MB
    FROM DBA_DATA_FILES DATA_FILES
    LEFT JOIN DBA_FREE_SPACE FREE_SPACE
    ON DATA_FILES.FILE_ID = FREE_SPACE.FILE_ID
    GROUP BY DATA_FILES.TABLESPACE_NAME
);
```
#### Kiểm tra Tablespace đang bị đầy
```sql
    SELECT TABLESPACE_NAME,
       ROUND((TOTAL_MB - USED_MB), 2) AS FREE_MB,
       CASE
           WHEN ROUND((FREE_MB / TOTAL_MB) * 100, 2) < 10 THEN 'NEAR FULL'
           ELSE 'OK'
       END AS STATUS
FROM (
    SELECT DATA_FILES.TABLESPACE_NAME,
           SUM(DATA_FILES.BYTES) / 1024 / 1024 AS TOTAL_MB,
           SUM(DATA_FILES.BYTES - NVL(FREE_SPACE.BYTES, 0)) / 1024 / 1024 AS USED_MB,
           SUM(NVL(FREE_SPACE.BYTES, 0)) / 1024 / 1024 AS FREE_MB
    FROM DBA_DATA_FILES DATA_FILES
    LEFT JOIN DBA_FREE_SPACE FREE_SPACE
    ON DATA_FILES.FILE_ID = FREE_SPACE.FILE_ID
    GROUP BY DATA_FILES.TABLESPACE_NAME
);
```
#### Kiểm tra lịch sử cảnh báo Tablespace đầy
``` bash
SELECT OBJECT_NAME, OBJECT_TYPE, REASON, CREATION_TIME
FROM DBA_OUTSTANDING_ALERTS
WHERE OBJECT_TYPE = 'TABLESPACE';
```
#### Kiểm tra người dùng sử dụng một Tablespace
```bash 
SELECT USERNAME, DEFAULT_TABLESPACE, TEMPORARY_TABLESPACE
FROM DBA_USERS
WHERE DEFAULT_TABLESPACE = 'TABLESPACE_NAME';
```

### Các loại Tablespace trong Oracle
| **Loại** | **Tablespace	Mục đích sử dụng** |
| :--: | :------------------------- |
|SYSTEM | Lưu trữ các thông tin hệ thống và metadata. |
| SYSAUX | Lưu trữ các thành phần hỗ trợ, giảm tải cho SYSTEM Tablespace. |
| PERMANENT | Lưu trữ dữ liệu vĩnh viễn của người dùng như bảng, chỉ mục. |
| TEMPORARY	| Lưu trữ dữ liệu tạm thời cho các hoạt động như sắp xếp và kết hợp. |
| UNDO | Lưu trữ thông tin undo để rollback giao dịch và đảm bảo read-consistency. |
| BIGFILE | Quản lý một tệp dữ liệu lớn thay vì nhiều tệp nhỏ, thích hợp cho cơ sở dữ liệu lớn. |
| READ ONLY | Dữ liệu chỉ đọc, không chỉnh sửa (ví dụ: dữ liệu lịch sử). |
| Encrypted	| Lưu trữ dữ liệu được mã hóa để tăng cường bảo mật. |

### Tác vụ quản lý Tablespace
#### Tạo một Tablespace
```bash
CREATE TABLESPACE my_tablespace
DATAFILE '/path/to/datafile/my_tablespace01.dbf' SIZE 100M;
```
#### Thêm Datafile vào Tablespace
```bash
ALTER TABLESPACE my_tablespace
ADD DATAFILE '/path/to/datafile/my_tablespace02.dbf' SIZE 100M
AUTOEXTEND ON NEXT 10M MAXSIZE 1G;
```
#### Thay đổi kích thước Datafile
- Tăng kích thước của một datafile hiện tại:
```bash
ALTER DATABASE DATAFILE '/path/to/datafile/my_tablespace01.dbf' RESIZE 200M;
```
- Bật hoặc tắt tính năng autoextend cho một datafile:
```bash
ALTER DATABASE DATAFILE '/path/to/datafile/my_tablespace01.dbf'
AUTOEXTEND ON NEXT 10M MAXSIZE UNLIMITED;
```
#### Xóa Tablespace
- Xóa một tablespace và datafile đi kèm:
```bash
DROP TABLESPACE my_tablespace INCLUDING CONTENTS AND DATAFILES;
```
  - `INCLUDING CONTENTS`: Xóa tất cả dữ liệu trong tablespace.
  - `AND DATAFILES`: Xóa luôn các tệp vật lý khỏi đĩa.
#### Di chuyển vị trí Datafile
- Di chuyển datafile của một tablespace:
  - Đưa `Tablespace` về chế độ `OFFLINE`:
  ```bash
  ALTER TABLESPACE my_tablespace OFFLINE;
  ```
  - Di chuyển tệp datafile: Sử dụng lệnh OS để di chuyển tệp đến vị trí mới:
  ```bash
  mv /old/path/my_tablespace01.dbf /new/path/my_tablespace01.dbf
  ```
  - Cập nhật đường dẫn trong Oracle:
  ```bash
  ALTER DATABASE RENAME FILE '/old/path/my_tablespace01.dbf' TO '/new/path/my_tablespace01.dbf';
  ```
  - Đưa `Tablespace` về chế độ `ONLINE`:
  ```bash
  ALTER TABLESPACE my_tablespace ONLINE;
  ```
#### Bật/Tắt tự động mở rộng (Autoextend)
- Bật `autoextend` cho `datafile`:
```sql
ALTER DATABASE DATAFILE '/path/to/datafile/my_tablespace01.dbf' AUTOEXTEND ON NEXT 10M MAXSIZE 1G;
```
- Tắt `autoextend`:
```sql
ALTER DATABASE DATAFILE '/path/to/datafile/my_tablespace01.dbf' AUTOEXTEND OFF;
```

#### Đưa Tablespace về trạng thái OFFLINE/ONLINE
- `OFFLINE`: Tablespace không khả dụng cho người dùng.
```sql
ALTER TABLESPACE my_tablespace OFFLINE;
```
- `ONLINE`: Kích hoạt lại tablespace để sử dụng.
```sql
ALTER TABLESPACE my_tablespace ONLINE;
```
### Một số thông tin về datafile
#### Truy vấn danh sách các Datafile của các Tablespace vĩnh viễn:
```sql
SELECT DDF.FILE_ID,
       DDF.FILE_NAME,
       DDF.TABLESPACE_NAME,
       ROUND(DDF.BYTES / 1024 / 1024, 2) AS SIZE_MB,
       DDF.AUTOEXTENSIBLE,
       ROUND(DDF.MAXBYTES / 1024 / 1024, 2) AS MAX_SIZE_MB,
       DDF.INCREMENT_BY * (TS.BLOCK_SIZE / 1024) AS INCREMENT_SIZE_MB
FROM DBA_DATA_FILES DDF
JOIN DBA_TABLESPACES TS ON DDF.TABLESPACE_NAME = TS.TABLESPACE_NAME;
```

#### Truy vấn thông tin về các Datafile tạm thời (Temporary Tablespace):
```sql
SELECT TABLESPACE_NAME,
       FILE_NAME,
       ROUND(BYTES/1024/1024, 2) AS SIZE_MB,
       AUTOEXTENSIBLE,
       MAXBYTES/1024/1024 AS MAX_SIZE_MB
FROM DBA_TEMP_FILES;
```
#### Truy vấn trạng thái của các Datafile (ONLINE/OFFLINE):
```sql
SELECT DF.FILE# AS FILE_ID,
       DF.NAME AS FILE_NAME,
       TS.NAME AS TABLESPACE_NAME,
       DF.STATUS
FROM V$DATAFILE DF
JOIN V$TABLESPACE TS ON DF.TS# = TS.TS#;
```

#### Truy vấn dung lượng đã sử dụng và còn trống trong mỗi Datafile:
```sql
SELECT TABLESPACE_NAME,
       FILE_NAME,
       ROUND(BYTES/1024/1024, 2) AS TOTAL_SIZE_MB,
       ROUND(FREE_SPACE/1024/1024, 2) AS FREE_SIZE_MB,
       ROUND((BYTES - FREE_SPACE)/BYTES * 100, 2) AS USED_PERCENT
FROM (
    SELECT DF.TABLESPACE_NAME, DF.FILE_NAME, DF.BYTES,
           (DF.BYTES - NVL(FS.BYTES, 0)) AS FREE_SPACE
    FROM DBA_DATA_FILES DF
    LEFT JOIN (
        SELECT FILE_ID, SUM(BYTES) AS BYTES
        FROM DBA_FREE_SPACE
        GROUP BY FILE_ID
    ) FS
    ON DF.FILE_ID = FS.FILE_ID
);
```
#### Xem danh sách các Datafile có tính năng Autoextend:
```sql
SELECT DF.FILE_NAME,
       DF.TABLESPACE_NAME,
       DF.AUTOEXTENSIBLE,
       ROUND(DF.INCREMENT_BY * TS.BLOCK_SIZE / 1024 / 1024, 2) AS INCREMENT_SIZE_MB,
       ROUND(DF.MAXBYTES / 1024 / 1024, 2) AS MAX_SIZE_MB
FROM DBA_DATA_FILES DF
JOIN DBA_TABLESPACES TS ON DF.TABLESPACE_NAME = TS.TABLESPACE_NAME
WHERE DF.AUTOEXTENSIBLE = 'YES';
```
#### Xem đường dẫn vật lý và block size của các Datafile:
```sql
SELECT DF.FILE_NAME,
   DF.TABLESPACE_NAME,
   TS.BLOCK_SIZE,
   DF.BYTES / 1024 / 1024 AS SIZE_MB
FROM DBA_DATA_FILES DF
JOIN DBA_TABLESPACES TS ON DF.TABLESPACE_NAME = TS.TABLESPACE_NAME;
```
#### Tóm tắt toàn bộ thông tin chi tiết của các Datafile:
```sql
SELECT DF.FILE_ID,
       DF.FILE_NAME,
       DF.TABLESPACE_NAME,
       DF.BYTES / 1024 / 1024 AS TOTAL_SIZE_MB,
       DF.AUTOEXTENSIBLE,
       DF.MAXBYTES / 1024 / 1024 AS MAX_SIZE_MB,
       NVL(FS.FREE_SPACE_MB, 0) AS FREE_SPACE_MB,
       ROUND((DF.BYTES - NVL(FS.FREE_SPACE_MB, 0)) / DF.BYTES * 100, 2) AS USED_PERCENT
FROM DBA_DATA_FILES DF
LEFT JOIN (
    SELECT FILE_ID, SUM(BYTES) / 1024 / 1024 AS FREE_SPACE_MB
    FROM DBA_FREE_SPACE
    GROUP BY FILE_ID
) FS
ON DF.FILE_ID = FS.FILE_ID;
```
### Một số lưu ý quan trọng
- `SYSTEM` và `SYSAUX` tablespace `không` thể đưa về trạng thái `OFFLINE`.
- `TEMP` Tablespace:
  - Chỉ sử dụng để lưu dữ liệu tạm thời, không cần backup.
  - Có thể cấu hình lại:
    ``` bash
        ALTER DATABASE DEFAULT TEMPORARY TABLESPACE temp;
    ```
  - `UNDO` Tablespace:
    - Không `xóa` hoặc `sửa đổi` trực tiếp. Có thể cấu hình undo tablespace mặc định:
    ``` bash
        ALTER SYSTEM SET UNDO_TABLESPACE = undotbs2;
    ```

## Truy Vấn Thông Tin Cấu Trúc Lưu Trữ (segment, extent)

## Quản trị người dùng và quyền

## Import, Export Schema

