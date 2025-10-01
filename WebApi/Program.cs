using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApi.Data.Implementatoins;
using WebApi.Data.Interfaces;
using WebApi.Models.Misc;
using WebApi.Repositories.Implementations;
using WebApi.Repositories.Interfaces;
using WebApi.Services.Implementations;
using WebApi.Services.Interfaces;
using WebApi.Utilities.Implementations;
using WebApi.Utilities.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

builder.Services.AddControllers();

// Jwt Settings Configurations
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

// Jwt Authentication Configuration
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
        ValidateIssuer = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidateAudience = true,
        ValidAudience = jwtSettings.Audience,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

// Register Utilities here
builder.Services.AddScoped<IDbConnectionUtil, DbConnectionUtil>();
builder.Services.AddTransient<IPasswordUtil, PasswordUtil>();
builder.Services.AddTransient<IJwtUtil, JwtUtil>();

// Register Data here
builder.Services.AddScoped<IUserData, UserData>();
builder.Services.AddScoped<IConsumerData, ConsumerData>();
builder.Services.AddScoped<ISmartMeterData, SmartMeterData>();
builder.Services.AddScoped<IMeterReadingData, MeterReadingData>();
builder.Services.AddScoped<IComplaintData, ComplaintData>();
builder.Services.AddScoped<IRechargeData, RechargeData>();


// Register Repositories here
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IConsumerRepository, ConsumerRepository>();


// Register Services here
builder.Services.AddScoped<IUserService, UserService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
