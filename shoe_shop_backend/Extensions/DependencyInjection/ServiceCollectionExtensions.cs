using Microsoft.EntityFrameworkCore;
using shoe_shop_backend.Application.Mapping;
using shoe_shop_backend.Application.Service;
using shoe_shop_backend.Application.Service.Imp;
using shoe_shop_backend.Application.Service.Interfaces;
using shoe_shop_backend.Domain.Interfaces;
using shoe_shop_backend.Extensions.Logs;
using shoe_shop_backend.Infrastructure.Data.DBContext;
using shoe_shop_backend.Infrastructure.Repositories;
using System.Reflection;

namespace shoe_shop_backend.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApiLayerServices();
            services.AddApplicationLayerServices();
            services.AddInfrastructureLayerServices(configuration);

            return services;
        }

        // ---------- API layer: Swagger, CORS, Controllers ----------
        private static IServiceCollection AddApiLayerServices(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }

        // ---------- Application layer: Service, AutoMapper, Validator ----------
        private static IServiceCollection AddApplicationLayerServices(this IServiceCollection services)
        {



            services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(typeof(BrandsMappingProfile).Assembly);
            });



            return services;
        }

        // ---------- Infrastructure layer: DbContext, Repository ----------
        private static IServiceCollection AddInfrastructureLayerServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(connectionString , ServerVersion.AutoDetect(connectionString))
                       .AddInterceptors(new CustomLogPrettySqlInterceptor()));

            services.AddScoped<DbContext>(sp => sp.GetRequiredService<AppDbContext>());

            services.AddScoped(typeof(ICommonRepository<>), typeof(CommonRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductVariantService, ProductVariantService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();

            return services;
        }
    }
}
