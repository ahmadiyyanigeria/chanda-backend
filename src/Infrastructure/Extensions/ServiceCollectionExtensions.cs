using Application.Mailing;
using Application.Repositories;
using Infrastructure.Mailing;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            return serviceCollection
                .AddDbContext<AppDbContext>(options =>
                    options.UseNpgsql(connectionString, action => action.MigrationsAssembly("Infrastructure")))
                .AddScoped<IMemberRepository, MemberRepository>()
                .AddScoped<IUnitOfWork, UnitOfWork>();

        }

        public static IServiceCollection AddEmailService(this IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>()
                    .AddScoped<IMailProvider, MailSender>();
            return services;

        }

    }
}
