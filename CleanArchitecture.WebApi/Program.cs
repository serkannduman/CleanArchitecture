using CleanArchitecture.Application.Services;
using CleanArchitecture.Persistance.Context;
using CleanArchitecture.Persistance.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICarService, CarService>();

builder.Services.AddAutoMapper(typeof(CleanArchitecture.Persistance.AssemblyeReferance).Assembly); // AytoMapper kütüphanesini ekledik.

string connectionString = builder.Configuration.GetConnectionString("SqlServer");
// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddControllers()
    .AddApplicationPart(typeof(CleanArchitecture.Presentation.AssemblyReference).Assembly); // controllerlarýn presentation katmanýndan geldiðini söyledik.

builder.Services.AddMediatR(cfr =>
cfr.RegisterServicesFromAssembly(typeof(CleanArchitecture.Application.AssemblyReference).Assembly)); // mediatr kütüphanesini register ettik.

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
