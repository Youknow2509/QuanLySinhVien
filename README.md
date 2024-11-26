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
```bash
    CREATE TABLESPACE TBS_BTL
    DATAFILE '/home/oracle/datafiles/datafile_tbs_btl.dbf' SIZE 100M;
```

- Kiểm tra `tablespace` vừa tạo:
```bash
    SELECT TABLESPACE_NAME, STATUS, LOGGING FROM USER_TABLESPACES WHERE TABLESPACE_NAME = 'TBS_BTL';
```

- Tạo `User` với `tablespace` vừa tạo:
```bash
    CREATE USER VINH IDENTIFIED BY 123
    DEFAULT TABLESPACE TBS_BTL
    TEMPORARY TABLESPACE TEMP
    QUOTA 10M ON TBS_BTL;
```
-> Tạo `User` với `username`: `vinh` và `password`:  `123` trong `cdb`: `orclcdb1`.

- Gán quyền đăng nhập tạo bảng cho user `vinh`
```bash
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
```bash
  STARTUP NOMOUNT;
  STARTUP MOUNT;
  STARTUP OPEN;
```

### Chế độ truy cập Instance
#### READ ONLY
- Cơ sở dữ liệu `chỉ` cho phép `đọc`, không được phép chỉnh sửa.
```bash
    ALTER DATABASE OPEN READ ONLY;
```
#### RESTRICTED SESSION
- Chỉ những user có quyền `đặc biệt` mới được truy cập (thường dùng trong bảo trì).
```bash
    ALTER SYSTEM ENABLE RESTRICTED SESSION;
```
- Gán quyền cho người dùng: 
```bash
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
```bash
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
```bash
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
#### SYSTEM và SYSAUX:
- Lưu trữ các cấu trúc hệ thống và metadata.
- Bắt buộc phải có trong mọi cơ sở dữ liệu.
#### USER Tablespace:
- Chứa dữ liệu của người dùng (bảng, chỉ mục, v.v.).
- Ví dụ: USERS.
#### TEMP Tablespace:
- Chứa dữ liệu tạm thời trong quá trình xử lý (sort, join, hash operations).
- Ví dụ: TEMP.
#### UNDO Tablespace:
- Lưu trữ thông tin undo để rollback giao dịch và hỗ trợ tính năng read-consistency.
- Ví dụ: UNDOTBS1.

### Tác vụ quản lý Tablespace

#### Tạo một Tablespace
#### Thêm Datafile vào Tablespace
#### Thay đổi kích thước Datafile
#### Xóa Tablespace
#### Di chuyển vị trí Datafile
#### Bật/Tắt tự động mở rộng (Autoextend)
#### Đưa Tablespace về trạng thái OFFLINE/ONLINE

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


## Truy Vấn Thông Tin Cấu Trúc Lưu Trữ 

## Quản trị người dùng và quyền

## Import, Export Schema

