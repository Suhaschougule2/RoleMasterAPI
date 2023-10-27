

using Microsoft.EntityFrameworkCore;
using RoleMasterAPI.Data;
using RoleMasterAPI.Repositories;
using RoleMasterAPI.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("postgreSQLConnection"); 
builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));
builder.Services.AddControllersWithViews();

builder.Services.AddControllers();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("EnableCORS", builder =>
    {
        builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
    });
});



var app = builder.Build();

app.MapGet("/", () => "Hello World!");


app.MapControllers();

app.Run();
