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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;

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

        public static void AddSwaggerForJwt(this IServiceCollection services)
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

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });

                // Configure Swagger to use JWT Bearer token authentication
                c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });
            });

            services.AddFluentValidationRulesToSwagger();
        }

        public static void AddMockAuth(this IServiceCollection services)
        {
            services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "https://your-keycloak-domain/auth/realms/your-realm",
                    ValidAudience = "your-client-id",
                    RoleClaimType = ClaimTypes.Role,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes("S0M3RAN0MS3CR3T!1!MAG1C!1!343456y674688847"))
                };
            });
        }

        /*public static void AddSwaggerForKaycloak(this IServiceCollection services, IConfiguration config)
        {
            services.AddSwaggerGen(c =>
            {
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Keycloak",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.OpenIdConnect,
                    OpenIdConnectUrl = new Uri($"{config["Keycloak:auth-server-url"]}realms/{config["Keycloak:realm"]}/.well-known/openid-configuration"),
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                };

                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, Array.Empty<string>()}
                });
            });
        }*/

        /*public static void AddKaycloakAuth(this IServiceCollection services, IConfiguration config)
        {
            services.AddKeycloakAuthentication(new KeycloakAuthenticationOptions()
            {
                // Keycloak server URL
                AuthServerUrl = config["Keycloak:auth-server-url"]!,
                // Realm Name
                Realm = config["Keycloak:realm"]!,
                // ClientId
                Resource = config["Keycloak:resource"]!,

                SslRequired = config["Keycloak:ssl-required"]!,
                VerifyTokenAudience = false,
            });

            services.AddKeycloakAuthorization(new KeycloakProtectionClientOptions()
            {
                AuthServerUrl = config["Keycloak:auth-server-url"]!,
                Realm = config["Keycloak:realm"]!,
                Resource = config["Keycloak:resource"]!,
                SslRequired = config["Keycloak:ssl-required"]!,
                VerifyTokenAudience = false
            });
        }*/

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
               .Map(dest => dest.PaymentReference, src => src.Reference);

            config.NewConfig<Invoice, Application.Queries.GetInvoices.InvoiceResponse>()
              .Map(dest => dest.JamaatName, src => src.Jamaat.Name);

            config.Default.EnumMappingStrategy(EnumMappingStrategy.ByName);
            services.AddSingleton(config);
            services.AddScoped<IMapper, Mapper>();
            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection serviceCollection)
        {
            // Set FluentValidator Global Options
            ValidatorOptions.Global.DisplayNameResolver = (_, member, _) => member?.Name?.ToCamelCase() ?? string.Empty;
            ValidatorOptions.Global.PropertyNameResolver = (_, member, _) => member?.Name?.ToCamelCase() ?? string.Empty;

            return serviceCollection
                .AddValidatorsFromAssemblyContaining<CreateInvoice.CommandValidator>()
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        }
    }
}
