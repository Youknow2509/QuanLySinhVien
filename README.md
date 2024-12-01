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

# Mục Lục Quản Trị CSDL Oracle
- [Quản Lý Instant](#quản-lý-instant)
    - [Các chế độ của Instance](#các-chế-độ-của-instance)
    - [Shutdown Instance](#shutdown-instance)
    - [Start Instance](#start-instance)
    - [Chế độ truy cập Instance](#chế-độ-truy-cập-instance)
- [Quản Lý TableSpace](#quản-lý-tablespace)
    - [Kiểm Tra Thông Tin TableSpace](#kiểm-tra-thông-tin-tablespace)
    - [Dung Lượng Sử Dụng](#dung-lượng-sử-dụng)
    - [Xem datafile của một tablespace](#xem-datafile-của-một-tablespace)
    - [Các loại Tablespace trong Oracle](#các-loại-tablespace-trong-oracle)
    - [Tác vụ quản lý Tablespace](#tác-vụ-quản-lý-tablespace)
- [Truy Vấn Thông Tin Cấu Trúc Lưu Trữ (segment, extent)](#truy-vấn-thông-tin-cấu-trúc-lưu-trữ-segment-extent)
    - [Quản Trị Người Dùng và Quyền](#quản-trị-người-dùng-và-quyền)
    - [Quản Trị Người Dùng](#quản-trị-người-dùng)
    - [Phân Quyền Trong Oracle](#phân-quyền-trong-oracle)
    - [Các Quyền Đặc Biệt](#các-quyền-đặc-biệt)
- [Import, Export Schema](#import-export-schema)
    - [So sánh Schema và User](#so-sánh-schema-và-user)
    - [Export](#export-schema-(expdp))
    - [Import](#import-schema-())

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
SELECT DF.FILE_NAME,
       DF.TABLESPACE_NAME,
       ROUND(DF.BYTES / 1024 / 1024, 2) AS SIZE_MB,
       DF.AUTOEXTENSIBLE,
       ROUND(DF.MAXBYTES / 1024 / 1024, 2) AS MAX_SIZE_MB,
       ROUND(DF.INCREMENT_BY * (TS.BLOCK_SIZE / 1024), 2) AS INCREMENT_SIZE_MB
FROM DBA_DATA_FILES DF
JOIN DBA_TABLESPACES TS
ON DF.TABLESPACE_NAME = TS.TABLESPACE_NAME
WHERE DF.TABLESPACE_NAME = 'TBS_BTL';
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

- Trong `btl`:
```sql
-- TABLESPACE chính
CREATE TABLESPACE TBS_BTL
DATAFILE '/home/oracle/datafiles/datafile_tbs_btl.dbf' SIZE 100M;
-- TABLESPACE 2
CREATE TABLESPACE TBS_BTL_2
DATAFILE '/home/oracle/datafiles/datafile_tbs_btl_2.dbf' SIZE 50M;
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
- `Segment`:
    - Là một đối tượng `logic` trong cơ sở dữ liệu Oracle, lưu trữ dữ liệu thực tế (ví dụ: bảng, chỉ mục, undo, tạm thời).
    - Mỗi segment bao gồm nhiều Extent.
- `Extent`:
    - Là một tập hợp các khối (blocks) liên tục trong một datafile.
    - Extent được phân bổ khi segment cần thêm không gian lưu trữ.

### Truy Vấn Thông Tin Segment

#### Danh Sách Các Segment Trong Tablespace
```sql
SELECT SEGMENT_NAME, SEGMENT_TYPE, TABLESPACE_NAME, BYTES/1024/1024 AS SIZE_MB, BLOCKS
FROM DBA_SEGMENTS
WHERE TABLESPACE_NAME = 'USERS';
```
#### Kiểm Tra Dung Lượng Sử Dụng Của Segment
```sql
SELECT SEGMENT_NAME, SEGMENT_TYPE, TABLESPACE_NAME, BYTES/1024/1024 AS SIZE_MB
FROM DBA_SEGMENTS
WHERE SEGMENT_NAME = 'EMPLOYEES';
```
### Truy Vấn Thông Tin Extent
#### Danh Sách Extent Của Một Segment
```sql
SELECT SEGMENT_NAME, EXTENT_ID, FILE_ID, BLOCK_ID, BYTES/1024 AS SIZE_KB
FROM DBA_EXTENTS
WHERE SEGMENT_NAME = 'EMPLOYEES';
```
#### Tổng Dung Lượng Của Extent Trong Một Tablespace
- Tính tổng dung lượng tất cả các extent trong một tablespace cụ thể.
```sql
SELECT TABLESPACE_NAME, SUM(BYTES)/1024/1024 AS TOTAL_SIZE_MB
FROM DBA_EXTENTS
WHERE TABLESPACE_NAME = 'USERS'
GROUP BY TABLESPACE_NAME;
```
#### Phân Bổ Extent Theo Segment
- Hiển thị số lượng extent và kích thước của mỗi segment trong một tablespace.
```sql
SELECT SEGMENT_NAME, COUNT(*) AS TOTAL_EXTENTS, SUM(BYTES)/1024/1024 AS SIZE_MB
FROM USE_EXTENTS
WHERE TABLESPACE_NAME = 'USERS'
GROUP BY SEGMENT_NAME
ORDER BY SIZE_MB DESC;
```

### Truy Vấn Thông Tin Liên Quan Đến Block
#### Danh Sách Block Của Extent
- Hiển thị thông tin về các block của từng extent trong segment.
```sql
SELECT SEGMENT_NAME, EXTENT_ID, FILE_ID, BLOCK_ID, BLOCKS
FROM DBA_EXTENTS
WHERE SEGMENT_NAME = 'EMPLOYEES';
```
#### Tổng Số Block Được Sử Dụng Trong Tablespace
- Tổng số block được sử dụng trong một tablespace cụ thể.
```sql
SELECT TABLESPACE_NAME, SUM(BLOCKS) AS TOTAL_BLOCKS
FROM DBA_EXTENTS
WHERE TABLESPACE_NAME = 'USERS'
GROUP BY TABLESPACE_NAME;
```
## Quản trị người dùng và quyền
### Quản Trị Người Dùng

#### Tạo Người Dùng Mới
- Người dùng mới trong Oracle cần được tạo cùng với tablespace mặc định và temporary tablespace.
```sql
CREATE USER username IDENTIFIED BY password
DEFAULT TABLESPACE users
TEMPORARY TABLESPACE temp
QUOTA 100M ON users;
```
- Trong `btl`:
```sql
-- Người dùng admin
CREATE USER vinh IDENTIFIED BY 123
DEFAULT TABLESPACE TBS_BTL
TEMPORARY TABLESPACE temp
QUOTA 100M ON TBS_BTL;

-- Người dùng sinh viên
CREATE USER sv IDENTIFIED BY 123
DEFAULT TABLESPACE TBS_BTL
TEMPORARY TABLESPACE temp
QUOTA 10M ON TBS_BTL;

-- Người dùng giáo viên
CREATE USER gv IDENTIFIED BY 123
DEFAULT TABLESPACE TBS_BTL
TEMPORARY TABLESPACE temp
QUOTA 1M ON TBS_BTL;

-- Người dùng test xoá
CREATE USER t_drop IDENTIFIED BY 123
DEFAULT TABLESPACE TBS_BTL
TEMPORARY TABLESPACE temp
QUOTA 1M ON TBS_BTL;

-- Người dùng test thay đổi thông tin
CREATE USER t_alter IDENTIFIED BY 123
DEFAULT TABLESPACE TBS_BTL
TEMPORARY TABLESPACE temp
QUOTA 1M ON TBS_BTL;

```

#### Sửa Đổi Thông Tin Người Dùng
- Đổi mật khẩu, tablespace mặc định, hoặc hạn mức lưu trữ (quota) của user.
```sql
  ALTER USER username IDENTIFIED BY new_password;
  ALTER USER username DEFAULT TABLESPACE new_tablespace;
  ALTER USER username QUOTA UNLIMITED ON users;
```
- Trong `btl`
```sql
    -- Đổi mật khẩu
    ALTER USER T_ALTER IDENTIFIED BY 1234;
    -- Đổi tablespace mới
    ALTER USER T_ALTER DEFAULT TABLESPACE TBS_BTL_2;
    -- Đổi QUOTA trong tablespace
    ALTER USER T_ALTER QUOTA UNLIMITED ON TBS_BTL_2;
```
#### Xóa Người Dùng
- Từ khóa `CASCADE` sẽ xóa tất cả các đối tượng (bảng, chỉ mục, v.v.) thuộc user.
```sql
DROP USER username CASCADE;
```

- Trong `btl`:
```sql
    DROP USER t_drop;
```

### Phân Quyền Trong Oracle
- Trong `btl`
```sql
  -- Tạo role admin
  CREATE ROLE admin;
  -- Cấp quyền đăng nhập cho role admin
  GRANT CREATE SESSION TO admin;
  
  -- Tạo role giáo viên
  CREATE ROLE giao_vien;
  -- Cấp quyền cho role giáo viên
  GRANT SELECT ON Diem TO giao_vien;  -- Xem điểm sinh viên
  GRANT UPDATE ON Diem TO giao_vien;  -- Sửa điểm sinh viên
  GRANT SELECT ON ThoiGian_LopHocPhan TO giao_vien;  -- Xem thời gian lớp học phần
  GRANT SELECT ON SinhVien TO giao_vien;  -- Xem thông tin sinh viên
  -- Cấp quyền đăng nhập cho role giáo viên
  GRANT CREATE SESSION TO giao_vien;

  -- Tạo role sinh viên
  CREATE ROLE sinh_vien;
  -- Cấp quyền cho role sinh viên
  GRANT SELECT ON Diem TO sinh_vien;  -- Xem điểm sinh viên
  -- Cấp quyền đăng nhập cho role sinh viên
  GRANT CREATE SESSION TO sinh_vien;

  -- Tạo role có quyền RESTRICTED SESSION
  CREATE ROLE restricted_role;
  -- Cấp quyền RESTRICTED SESSION cho role restricted_role
  GRANT RESTRICTED SESSION TO restricted_role;
  -- Cấp quyền CREATE SESSION cho role restricted_role để người dùng có thể đăng nhập
  GRANT CREATE SESSION TO restricted_role;

```
#### System Privileges
- Quyền hệ thống cho phép người dùng thực hiện các **thao tác** cụ thể trên cơ sở dữ liệu, như tạo bảng, truy cập vào các tablespace, hoặc quản trị hệ thống.
- Ví dụ về quyền: 
    `CREATE TABLE`, `CREATE USER`, `DROP ANY TABLE`, `SELECT ANY TABLE`.
- Cấp quyền hệ thống:
```sql
GRANT CREATE SESSION TO username;
GRANT CREATE TABLE, CREATE VIEW TO username;
```
- Thu hồi quyền hệ thống:
```sql
REVOKE CREATE SESSION FROM username;
REVOKE CREATE TABLE FROM username;
```

#### Object Privileges
- Quyền đối tượng cho phép người dùng thực hiện các **thao tác** cụ thể trên các đối tượng (bảng, chỉ mục, v.v.).
- Ví dụ về quyền:
    `SELECT`, `INSERT`, `UPDATE`, `DELETE`, `EXECUTE`.
- Cấp quyền đối tượng:
```sql
GRANT SELECT, INSERT ON employees TO username;
GRANT EXECUTE ON procedure_name TO username;
```
- Thu hồi quyền đối tượng:
```sql
REVOKE SELECT, INSERT ON employees FROM username;
REVOKE EXECUTE ON procedure_name FROM username;
```
#### Role (Vai Trò)
- `Role` là tập hợp các quyền được nhóm lại, giúp đơn giản hóa việc quản trị quyền cho người dùng.

##### Tạo Role:
```sql
CREATE ROLE manager_role;
```

##### Cấp quyền cho Role:
```sql
GRANT CREATE TABLE, CREATE VIEW TO manager_role;
```

##### Gán Role cho User:
```sql
GRANT manager_role TO username;
```

##### Thu hồi Role từ User:
```sql
REVOKE manager_role FROM username;
```

### Các Quyền Đặc Biệt
#### Quyền Kết Nối (CREATE SESSION)
- Cho phép người dùng kết nối vào cơ sở dữ liệu:
```sql
GRANT CREATE SESSION TO username;
```
#### Quyền Điều Hành Hệ Thống
- `DBA Role`: Cấp quyền quản trị toàn bộ cơ sở dữ liệu.
```sql
GRANT DBA TO username;
```
- Thu hồi quyền:
```sql
REVOKE DBA FROM username;
```
#### Quyền Trên Bất Kỳ Đối Tượng (ANY Privileges)
- Ví dụ: `SELECT ANY TABLE`, `DROP ANY TABLE`.
```sql
GRANT SELECT ANY TABLE TO username;
REVOKE DROP ANY TABLE FROM username;
```

### Kiểm Tra Thông Tin Người Dùng và Quyền
#### Kiểm Tra Danh Sách Người Dùng
- `ACCOUNT_STATUS`: Hiển thị trạng thái của user (`OPEN`, `LOCKED`).
```sql
SELECT USERNAME, ACCOUNT_STATUS, DEFAULT_TABLESPACE, TEMPORARY_TABLESPACE
FROM DBA_USERS;
```

#### Kiểm Tra Quyền Của Một User
- Kiểm tra quyền `hệ thống` của user.
```sql
SELECT * FROM DBA_SYS_PRIVS WHERE GRANTEE = 'USERNAME';
```
- Kiểm tra quyền trên các `đối tượng` của user.
```sql
SELECT * FROM DBA_TAB_PRIVS WHERE GRANTEE = 'USERNAME';
```

### Khóa và Mở Khóa Tài Khoản
#### Khóa Tài Khoản
```sql
ALTER USER username ACCOUNT LOCK;
```

#### Mở Khóa Tài Khoản
```sql
ALTER USER username ACCOUNT UNLOCK;
```

### Cấu Hình Thời Gian Sử Dụng Mật Khẩu
#### Kiểm Tra Hồ Sơ Cấu Hình Mật Khẩu (Profile)
```sql
SELECT * FROM DBA_PROFILES WHERE RESOURCE_NAME LIKE 'PASSWORD%';
```

#### Cấu Hình Hồ Sơ
- Câu lệnh SQL sau sẽ tạo một `profile` mới có tên `app_user_profile` với các giới hạn liên quan đến mật khẩu:
```sql
CREATE PROFILE app_user_profile LIMIT
PASSWORD_LIFE_TIME 30           -- Thời gian mật khẩu có hiệu lực là 30 ngày.
PASSWORD_REUSE_TIME 60          -- Thời gian phải đợi ít nhất 60 ngày trước khi mật khẩu có thể được tái sử dụng.
PASSWORD_REUSE_MAX 5;           -- Số lần tối đa mật khẩu có thể được tái sử dụng là 5 lần.
```
- Áp dụng Profile cho User:
```sql
ALTER USER username PROFILE app_user_profile;
```

### Đổi Mật Khẩu Người Dùng
```sql
ALTER USER username IDENTIFIED BY new_password;
```

### Kiểm Tra Session Của Người Dùng
#### Kiểm Tra Người Dùng Đang Kết Nối
```sql
SELECT SID, SERIAL#, USERNAME, STATUS FROM V$SESSION WHERE USERNAME = 'USERNAME';
```
#### Ngắt Kết Nối Session
```sql
ALTER SYSTEM KILL SESSION 'SID,SERIAL#'; -- thay sid và serial#
```

## Import, Export Schema

### So Sánh Schema Và User
| Điểm khác biệt | User | Schema |
| :------------: | :--- | :----- |
|Khái niệm|	Là tài khoản người dùng trong DB.	|Là tập hợp các đối tượng thuộc về user.|
|Tên|	Tên user phải là duy nhất trong DB.|	Tên schema trùng với tên của user.|
|Các đối tượng sở hữu|	User sở hữu các đối tượng trong schema.	|Schema chứa các đối tượng của user.|
|Tạo và Xóa	|User có thể được tạo và xóa độc lập.	|Schema không thể được tạo trực tiếp, mà phải tạo qua user.|
|Quyền Hạn	|User có thể được cấp quyền truy cập vào cơ sở dữ liệu.	|Schema không có quyền hạn riêng biệt.|
|Tác động khi xóa	|Khi xóa user, schema của user đó cũng bị xóa.	|Schema bị xóa khi xóa user.|

- Truy vấn các đối tượng trong Schema
  - Xử dụng khi ở tài khoản dba và xem một user bất kì:
  ```sql
  SELECT OBJECT_NAME, OBJECT_TYPE FROM DBA_OBJECTS WHERE OWNER = 'V';
  ```
  - Xem từ trực tiếp user hiện tại
  ```sql
  SELECT OBJECT_NAME, OBJECT_TYPE FROM USER_OBJECTS;
  ```

### Export Schema (expdp)
- **Bước 1**: Tạo Thư Mục Chứa File Dump (tệp dữ liệu xuất ra)
```sql
-- Tạo thư mục trong hệ điều hành
CREATE DIRECTORY dpump_dir AS '/path/to/directory';

-- CREATE OR REPLACE DIRECTORY dpump_dir AS '/path/to/directory';

-- Cấp quyền cho user 'v' để đọc và ghi vào thư mục này
GRANT READ, WRITE ON DIRECTORY dpump_dir TO v;
```
- **Bước 2**: Thực Hiện Export Schema
```bash
expdp v/123@orclcdb1 DIRECTORY=dpump_dir DUMPFILE=v_schema.dmp LOGFILE=v_schema_export.log SCHEMAS=v;
```
- Giải thích:
    - `v/123@orclcdb1`: Tên người dùng (user), mật khẩu, cdb.
    - `DIRECTORY=dpump_dir`: Thư mục mà Oracle sẽ sử dụng để lưu trữ các file dump và log.
    - `DUMPFILE=v_schema.dmp`: Tên file dump, nơi sẽ chứa dữ liệu schema v.
    - `LOGFILE=v_schema_export.log`: Tên file log ghi lại quá trình xuất dữ liệu.
    - `SCHEMAS=v`: Tên schema bạn muốn export.

## Tham số tuỳ chỉnh khi export
- Chỉ export metadata (cấu trúc), không bao gồm dữ liệu.
```bash
    CONTENT=METADATA_ONLY
```
- Chỉ dữ liệu 
```bash
    CONTENT=DATA_ONLY
```
- Giới hạn kích thước của mỗi file dump. Hữu ích khi export dữ liệu lớn.
```bash
    FILESIZE=500M
```
- Số luồng song song thực hiện export, tăng tốc độ xử lý với dữ liệu lớn.
```bash
    PARALLEL=4
```
- Chỉ export các đối tượng thuộc một hoặc nhiều tablespace cụ thể.
```bash
    TABLESPACES=USERS
```
- Mã hóa file dump để bảo mật
    - `ALL`: Mã hóa cả dữ liệu và metadata.
    - `DATA_ONLY`: Chỉ mã hóa dữ liệu.
    - `METADATA_ONLY`: Chỉ mã hóa metadata.
    - `NONE`: Không mã hóa (mặc định).
```bash
    ENCRYPTION=ALL ENCRYPTION_PASSWORD=my_secret_password
```

### Import Schema (impdp)
- **Bước 1**: Tạo Thư Mục Chứa File Dump (tệp dữ liệu xuất ra)
```sql
-- Tạo thư mục trong hệ điều hành
CREATE DIRECTORY dpump_dir AS '/path/to/directory';

-- CREATE OR REPLACE DIRECTORY dpump_dir AS '/path/to/directory';

-- Cấp quyền cho user 'v' để đọc và ghi vào thư mục này
GRANT READ, WRITE ON DIRECTORY dpump_dir TO v;
```
- **Bước 2**: Thực Hiện Import Schema
```bash
impdp v/123@orclcdb1 DIRECTORY=dpump_dir DUMPFILE=v_schema.dmp LOGFILE=v_schema_import.log SCHEMAS=v;
```
- Giải thích:
    - `v/123@orclcdb1`: Tên người dùng (user), mật khẩu, cdb.
    - `DIRECTORY=dpump_dir`: Thư mục mà Oracle sẽ sử dụng để lưu trữ các file dump và log.
    - `DUMPFILE=v_schema.dmp`: Tên file dump, nơi sẽ chứa dữ liệu schema v.
    - `LOGFILE=v_schema_export.log`: Tên file log ghi lại quá trình xuất dữ liệu.
    - `SCHEMAS=v`: Tên schema bạn cần import.

#### Tham số tuỳ chỉnh khi import
- Chỉ định liệu bạn có muốn import toàn bộ cơ sở dữ liệu hay không.
```bash
    FULL=Y
```
- Chỉ định tên của schema (hoặc nhiều schema) cần import
```bash
    SCHEMAS=V,V2
```
- Chỉ định các bảng cụ thể từ trong schema hoặc cơ sở dữ liệu để import.
```bash
    TABLES=V.THOIGIAN,V.LOPHOCPHAN
```
- Chỉ định chuyển đổi schema từ một schema nguồn (source) sang một schema đích
```bash
    REMAP_SCHEMA=old_schema:new_schema
```
- Chuyển đổi tablespace từ một tablespace nguồn sang một tablespace đích.
```bash
    REMAP_TABLESPACE=old_tablespace:new_tablespace
```
- Chuyển đổi tên bảng khi import dữ liệu.
```bash
    REMAP_TABLE=old_table:new_table
```
- Include, exclude ...
