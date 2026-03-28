using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DbContextConfiguration;

public static class DependencyInjection
{
    public static IServiceCollection AddDbContextWithInterceptors(
        this IServiceCollection services)
    {
        services.AddScoped<ConvertDomainEventsToOutboxMessagesInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, op) =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();

            var outboxInterceptor = sp.GetRequiredService<ConvertDomainEventsToOutboxMessagesInterceptor>();

            op.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
              .AddInterceptors(outboxInterceptor);
        });

        return services;
    }
}
