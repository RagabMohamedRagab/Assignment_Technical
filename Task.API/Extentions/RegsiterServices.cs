using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Task.Application.Dtos;
using Task.Application.Mappers;
using Task.Core.Entities;
using Task.DataAccess.Context;
using Task.DataAccess.IRepositories;
using Task.DataAccess.Repositories;

namespace Task.API.Extentions
{
    public static class RegsiterServices
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<Jwt>(builder.Configuration.GetSection("Jwt"));
            builder.Services.AddDbContext<TaskDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));
            builder.Services.AddIdentity<user, IdentityRole>().AddEntityFrameworkStores<TaskDbContext>().AddDefaultTokenProviders();
            builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IAuthRepository, AuthRepository>();
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

        }
    }
}
