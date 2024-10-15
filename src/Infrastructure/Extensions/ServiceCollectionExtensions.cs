using Application.Contracts;
using Application.Mailing;
using Application.Repositories;
using Hangfire;
using Hangfire.PostgreSql;
using Infrastructure.Mailing;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using Infrastructure.Services.PaymentServices;
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
                .AddScoped<IRoleRepository, RoleRepository>()
                .AddScoped<IInvoiceRepository, InvoiceRepository>()
                .AddScoped<IJamaatRepository, JamaatRepository>()
                .AddScoped<IChandaTypeRepository, ChandaTypeRepository>()
                .AddScoped<ICircuitRepository, CircuitRepository>()
                .AddScoped<ILedgerRepository, LedgerRepository>()
                .AddScoped<IPaymentRepository, PaymentRepository>()
                .AddScoped<ICurrentUser, CurrentUser>()
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IFileService, FileService>()
                .AddScoped<IReminderRepository, ReminderRepository>()
                .AddScoped<IReminderService, ReminderService>()
                .AddScoped<IAuditEntryRepository, AuditEntryRepository>();
        }

        public static IServiceCollection AddHangfireService(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddHangfire(config =>
            {
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                      .UseSimpleAssemblyNameTypeSerializer()
                      .UseRecommendedSerializerSettings()
                      .UsePostgreSqlStorage(
                          connectionString, // Reuse your existing connection string
                          new PostgreSqlStorageOptions
                          {
                              QueuePollInterval = TimeSpan.FromSeconds(15), // Adjust polling interval as needed
                              SchemaName = "hangfire" // Optional: Use a custom schema for Hangfire tables
                          });
            });

            services.AddHangfireServer();

            return services;
        }

        public static IServiceCollection AddPaymentService(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IPaymentGatewayFactory, PaymentGatewayFactory>();
            services.AddScoped<PaystackService>();
            services.AddScoped<VirtualPaymentService>();
            services.AddScoped<WalletPaymentService>();

            /*var paymentOption = config["Payment:Option"];
            switch (paymentOption)
            {
                case Constants.Paystack:
                    services.AddScoped<IPaymentService, PaystackService>();
                    break;
                case Constants.VirtualPayment:
                    services.AddScoped<IPaymentService,  VirtualPaymentService>(); 
                    break;
                default:
                    throw new ArgumentException("Invalid Payment Configuration.", nameof(paymentOption));
            }*/

            return services;
        }

        public static IServiceCollection AddEmailService(this IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>()
                    .AddScoped<IMailProvider, MailSender>();
            return services;

        }

    }
}
