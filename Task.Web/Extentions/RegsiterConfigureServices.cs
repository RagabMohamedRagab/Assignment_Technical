using System.Reflection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Task.Application.Mappers;
using Task.Core.Entities;
using Task.DataAccess.Context;
using Task.DataAccess.IRepositories;
using Task.DataAccess.Repositories;

namespace Task.Web.Extentions
{
    public static class RegsiterConfigureServices
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {

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
