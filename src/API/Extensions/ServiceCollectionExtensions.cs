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
            
            config.Default.EnumMappingStrategy(EnumMappingStrategy.ByName);
            services.AddSingleton(config);
            //services.AddScoped<IMapper, ServiceMapper>();
            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection serviceCollection)
        {
            // Set FluentValidator Global Options
            ValidatorOptions.Global.DisplayNameResolver = (_, member, _) => member.Name.ToCamelCase();
            ValidatorOptions.Global.PropertyNameResolver = (_, member, _) => member.Name.ToCamelCase();

            return serviceCollection
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        }
    }
}
