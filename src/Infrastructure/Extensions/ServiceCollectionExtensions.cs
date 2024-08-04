﻿using Application.Contracts;
using Application.Mailing;
using Application.Repositories;
using Domain.Constants;
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
                .AddScoped<IFileService, FileService>();
        }

        public static IServiceCollection AddPaymentService(this IServiceCollection services, IConfiguration config)
        {
            var paymentOption = config["Payment:Option"];
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
            }

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
