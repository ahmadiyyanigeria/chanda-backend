using API.Extensions;
using API.Filters;
using Application.Queries;
using Hangfire;
using Infrastructure.Extensions;
using Infrastructure.Mailing;
using Prometheus;
using Serilog;


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;


builder.Host.UseSerilog((context, loggerConfiguration) =>
{
    loggerConfiguration.WriteTo.Console();
    loggerConfiguration.ReadFrom.Configuration(context.Configuration);
});
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDatabase(configuration);
builder.Services.AddHangfireService(configuration);
builder.Services.AddPaymentService(configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwagger();
builder.Services.ConfigureMvc();
builder.Services.AddHealthChecks();
builder.Services.AddMapster();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetRoles).Assembly));
builder.Services.AddValidators();
builder.Services.AddEmailService();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddMockAuth();
}
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = new[] { new AllowAllUsersAuthorizationFilter() }
});

// Configure the HTTP request pipeline.
app.ConfigureExceptionHandler();
app.ConfigureCors();
app.ConfigureSwagger(configuration);

app.UseHttpsRedirection();
app.UseRouting();
app.UseHttpMetrics();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/healthz");

app.Run();