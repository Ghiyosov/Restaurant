using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Profiles;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extention_sMethods;

public static class Extention_sForDI
{
    public static void AddServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddScoped<IRestaurantService, RestaurantService>();
        serviceCollection.AddScoped<IMenuService, MenuServices>();
        serviceCollection.AddScoped<IOrderService, OrderService>();
        serviceCollection.AddScoped<IUserService, UserService>();
        serviceCollection.AddScoped<ICourierService, CourierService>();
        serviceCollection.AddScoped<IOrderDetailService, OrderDetailService>();
        serviceCollection.AddScoped<IAnalyticsQuery, AnalyticsQueryService>();
        serviceCollection.AddAutoMapper(typeof(InfrastructureProfile));
        serviceCollection.AddDbContext<Context>(opt => 
            opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
    }
}