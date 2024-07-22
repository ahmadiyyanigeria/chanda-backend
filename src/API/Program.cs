using API.Extensions;
using Application.Queries;
using Infrastructure.Extensions;
using Infrastructure.Mailing;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDatabase(configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwagger();
builder.Services.ConfigureMvc();
builder.Services.AddHealthChecks();
builder.Services.AddMapster();
builder.Services.AddValidators();
builder.Services.AddEmailService();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));


var app = builder.Build();

// Configure the HTTP request pipeline.
app.ConfigureExceptionHandler();
app.ConfigureCors();
app.ConfigureSwagger(configuration);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/healthz");

app.Run();