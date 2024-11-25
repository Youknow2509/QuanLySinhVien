
- Tạo Models từ database:
```shell
    dotnet ef dbcontext scaffold "User Id=vinh;Password=123;Data Source=localhost:1521/orclcdb1" Oracle.EntityFrameworkCore -o Models --context-dir Data -c QuanLySinhVienDbContext --force
```

- Restore and Rebuild project:
```shell
    dotnet restore
    dotnet build
```