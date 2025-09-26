using WebApi.Data.Implementatoins;
using WebApi.Data.Interfaces;
using WebApi.Repositories.Implementations;
using WebApi.Repositories.Interfaces;
using WebApi.Utilities.Implementations;
using WebApi.Utilities.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

builder.Services.AddControllers();

// Register Utilities here
builder.Services.AddScoped<IDbConnectionUtil, DbConnectionUtil>();
builder.Services.AddTransient<IPasswordUtil, PasswordUtil>();

// Register Data here
builder.Services.AddScoped<IUserData, UserData>();
builder.Services.AddScoped<IConsumerData, ConsumerData>();


// Register Repositories here
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IConsumerRepository, ConsumerRepository>();


// Register Services here

// Register Data here

// Register Repositories here

// Register Services here


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
