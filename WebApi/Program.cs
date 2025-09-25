using WebApi.Data.Implementatoins;
using WebApi.Data.Interfaces;
using WebApi.Utilities.Implementations;
using WebApi.Utilities.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

builder.Services.AddControllers();

// Register Utilities here
builder.Services.AddScoped<IDbConnectionUtil, DbConnectionUtil>();

// Register Data here
builder.Services.AddScoped<IUserData, UserData>();


// Register Repositories here

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
