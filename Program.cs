using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Oracle.ManagedDataAccess.Client;
//
using web_qlsv.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register DbContext with DI
builder.Services.AddDbContext<QuanLySinhVienDbContext>(options =>
{
    // Use the connection string from appsettings.json
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add Swagger generation service
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API",
        Version = "v1",
        Description = "API: Quản Lý Sinh Viên",
        Contact = new OpenApiContact
        {
            Name = "Lý Trần Vinh",
            Email = "lytranvinh.work@gmail.com",
            Url = new Uri("https://github.com/Youknow2509"),
        },
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Enable Swagger middleware
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
    c.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
