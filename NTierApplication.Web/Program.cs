using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NTierApplication.DataAccess;
using NTierApplication.DataAccess.Models;
using NTierApplication.Repository;
using NTierApplication.Service;
using NTierApplication.Service.Common.Interface;
using NTierApplication.Service.Common.Service;
using NTierApplication.Service.Helpers;
using NTierApplication.Web.ActionHelpers;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();

builder.Services.AddTransient<IItemService, ItemService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IItemRepository, ItemRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ITokenService,TokenService>();
builder.Services.AddScoped<IPaginator,Paginator>();
builder.Services.AddTransient<MainContext>();

builder.Services.AddDbContext<MainContext>(options => {
    //options.UseSqlServer("Data Source=localhost\\MSSQLSERVER2022;User ID=sa;Password=1;Initial Catalog=NTierApplication;TrustServerCertificate=True;");
    options.UseSqlServer("Data Source=localhost;User ID=sa;Password=Islombek0693;Initial Catalog=NTierApplication;TrustServerCertificate=True;");
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
