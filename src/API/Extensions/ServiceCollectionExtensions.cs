using API.Filters;
using Application.Extensions;
using Application.Behaviours;
using FluentValidation;
using MediatR;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using System.Text.Json;
using Mapster;
using MapsterMapper;
using Domain.Entities;
using Application.Commands;
using Application.Paging;
using static Application.Queries.GetChandaTypes;
using static Application.Queries.GetJamaats;

namespace API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureMvc(this IServiceCollection serviceCollection)
        {
            serviceCollection
               .AddControllers(options =>
               {
                   options.OutputFormatters.RemoveType<StringOutputFormatter>();
                   options.Filters.Add<ValidationFilter>();
                   options.ModelValidatorProviders.Clear();
               })
               .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true)
               .AddJsonOptions(options =>
               {
                   // Serialize enums as strings in api responses
                   options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
                   options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                   options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
               });
        }

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new()
                    {
                        Title = "Chanda Service API",
                        Version = "v1",
                        Contact = new()
                        {
                            Name = "E-mail",
                            Email = "support@amjn.com"
                        }
                    });

                c.AddSecurityRequirement(new()
           {
                {
                    new()
                    {
                        Reference = new()
                        {
                            Id = "OAuth2",
                            Type = ReferenceType.SecurityScheme
                        },
                    },
                    new List<string>()
                }
           });
            });
            services.AddFluentValidationRulesToSwagger();
        }

        public static IServiceCollection AddMapster(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;

            TypeAdapterConfig<PaginatedList<ChandaType>, IReadOnlyList<ChandaTypeResponse>>.NewConfig()
                .MapWith(src => src.Items.Adapt<List<ChandaTypeResponse>>());

            TypeAdapterConfig<PaginatedList<Jamaat>, IReadOnlyList<JamaatResponse>>.NewConfig()
                .MapWith(src => src.Items.Adapt<List<JamaatResponse>>());

            config.NewConfig<Member, Application.Queries.GetMember.MemberResponse>()
                .Map(dest => dest.Roles, src => src.MemberRoles.Select(r => r.RoleName).ToList());
            config.NewConfig<Member, Application.Queries.GetMember.MemberResponse>()
                .Map(dest => dest.JamaatName, src => src.Jamaat.Name);
            config.NewConfig<Member, Application.Queries.GetMember.MemberResponse>()
                .Map(dest => dest.CircuitName, src => src.Jamaat.Circuit.Name);
            config.NewConfig<Invoice, Application.Queries.GetInvoice.InvoiceResponse>()
                .Map(dest => dest.JamaatName, src => src.Jamaat.Name);
            config.NewConfig<InvoiceItem, Application.Queries.GetInvoice.InvoiceItemResponse>()
                .Map(dest => dest.PayerName, src => src.Member.Name);
            config.NewConfig<ChandaItem, Application.Queries.GetInvoice.ChandaItemResponse>()
                .Map(dest => dest.ChandaTypeName, src => src.ChandaType.Name);
            config.NewConfig<Jamaat, JamaatResponse>()
                .Map(dest => dest.CircuitName, src => src.Circuit.Name);

            config.NewConfig<Invoice, Application.Queries.GetInvoice.InvoiceResponse>()
                .Map(dest => dest.JamaatName, src => src.Jamaat.Name);
            config.NewConfig<InvoiceItem, Application.Queries.GetInvoice.InvoiceItemResponse>()
                .Map(dest => dest.MonthPayedFor, src => src.MonthPaidFor.ToString());
            config.NewConfig<InvoiceItem, Application.Queries.GetInvoice.InvoiceItemResponse>()
                .Map(dest => dest.PayerName, src => src.Member.Name);

            config.NewConfig<Payment, Application.Queries.GetPaymentByReference.PaymentResponse>()
               .Map(dest => dest.InvoiceReference, src => src.Invoice.Reference);
            config.NewConfig<Payment, Application.Queries.GetPaymentByReference.PaymentResponse>()
               .Map(dest => dest.PaymentOption, src => src.Option.ToString());


            config.Default.EnumMappingStrategy(EnumMappingStrategy.ByName);
            services.AddSingleton(config);
            services.AddScoped<IMapper, Mapper>();
            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection serviceCollection)
        {
            // Set FluentValidator Global Options
            ValidatorOptions.Global.DisplayNameResolver = (_, member, _) => member.Name.ToCamelCase();
            ValidatorOptions.Global.PropertyNameResolver = (_, member, _) => member.Name.ToCamelCase();

            return serviceCollection
                .AddValidatorsFromAssemblyContaining<CreateInvoice.CommandValidator>()
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        }
    }
}
