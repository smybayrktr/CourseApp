using System;
using CourseApp.Infrastructure.Data;
using CourseApp.Infrastructure.Repositories;
using CourseApp.Services;
using CourseApp.Services.Mappings;
using Microsoft.EntityFrameworkCore;

namespace CourseApp.Api.Extensions
{
    public static class IoCExtensions
    {
        public static IServiceCollection AddInjections(this IServiceCollection services, string connectionString)
        {

            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ICourseRepository, EfCourseRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICategoryRepository, EfCategoryRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddAutoMapper(typeof(MapProfile));
            services.AddDbContext<CourseDbContext>(opt => opt.UseSqlServer(connectionString));
            return services;
        }
    }
}

