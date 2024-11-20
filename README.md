# Contact:
- **Mail**: *lytranvinh.work@gmail.com*
- **Mail**: *khiemhm04@gmail.com*

# TODO:
- Auth ...  
- Sua diem windows
- ....

# How to run:
- Run database in docker:
    + **Note**: changle path volume.
    + After running
    ```
        docker compose up -d
    ```
- Init table db
    + **Note**: Install `dotnet ef global`
    ```bash
      dotnet tool install --global dotnet-ef

    ```

    +  Create table **IdentityDbContext**:
        ```
            dotnet ef database update --context IdentityDbContext
        ```
    + Create table **QuanLySinhVienDbContext**:
        ```
            dotnet ef database update --context QuanLySinhVienDbContext
        ```
    + Create table **SessionDbContext**:
        ```
            dotnet ef database update --context SessionDbContext
        ```
    + Run application (creaete admin and db init static):
        ```
            dotnet run
        ```
    + End Application.
- Run sql template
    + New query `QuanLySinhVienDbContext` path file `/sql/QuanLySinhVienDb.sql`.
    + New query `IdentityDbContext` path file `/sql/IdentityDb.sql`.
  
- Run application 
```
    dotnet run
```

## <div align="center">Contribute</div>

<a href="https://github.com/hoangmanhkhiem/QLDT_WPF/graphs/contributors">
<img width="100%" src="https://contrib.rocks/image?repo=Youknow2509/web_QLDT_WPF" alt="Ultralytics open-source contributors"></a>


# [DEMO_WEB](https://youtu.be/7JjGT8xnrl4)
# [DEMO_APP]()
