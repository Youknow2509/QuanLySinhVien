# Contact:
- **Mail**: *lytranvinh.work@gmail.com*
- **Github**: *https://github.com/Youknow2509*

# Quản Lý Sinh Viên Xử Dụng DataBase Oracle:

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

- TODO ....

#  Quản Trị CSDL Oracle

## Quản Lý Instant

## Quản Lý TableSpace

## Truy Vấn Thông Tin Cấu Trúc Lưu Trữ Quản Trị Người Dùng, Quyền, Chức Danh

## Import, Export Schema

