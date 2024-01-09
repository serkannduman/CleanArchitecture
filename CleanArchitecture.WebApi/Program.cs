using CleanArchitecture.Application.Abstractions;
using CleanArchitecture.Application.Behaviors;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Infrastructure.Authentication;
using CleanArchitecture.Infrastructure.Services;
using CleanArchitecture.Persistance.Context;
using CleanArchitecture.Persistance.Repositories;
using CleanArchitecture.Persistance.Services;
using CleanArchitecture.WebApi.Middleware;
using CleanArchitecture.WebApi.OptionsSetup;
using FluentValidation;
using GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IMailService, MailService>();

builder.Services.AddTransient<ExceptionMiddleware>(); // Her çaðrýlmada yeni bir instance üretmeyi saðýyor
builder.Services.AddScoped<IUnitOfWork, UnitOfWork<AppDbContext>>(); // Generic repository kýsmýný ayarladýk.
builder.Services.AddScoped<ICarRepositoy,CarRepository>();

builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.ConfigureOptions<JwtOptionsSetup>(); //JwtOptionsSetup classýný tetikler ve oradaki metodu tetikleyip appsettings ile Jwtoption sýnýfýný eþler.
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

builder.Services.AddAuthentication().AddJwtBearer(); // Token kullandýðýmýz kütüphaneyi belirttik.

builder.Services.AddAutoMapper(typeof(CleanArchitecture.Persistance.AssemblyeReferance).Assembly); // AutoMapper kütüphanesini ekledik.

string connectionString = builder.Configuration.GetConnectionString("SqlServer");
// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddIdentity<User,IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 1;
    options.Password.RequireUppercase = false; // Þifreyi istediðimiz gibi küçük büyük harf girmeisni ayarlayabiliriz.
}).AddEntityFrameworkStores<AppDbContext>(); // identity kütüphanesi için dbcontext ayarlarý
builder.Services.AddControllers()
    .AddApplicationPart(typeof(CleanArchitecture.Presentation.AssemblyReference).Assembly); // controllerlarýn presentation katmanýndan geldiðini söyledik.

builder.Services.AddMediatR(cfr =>
cfr.RegisterServicesFromAssembly(typeof(CleanArchitecture.Application.AssemblyReference).Assembly)); // mediatr kütüphanesini register ettik.

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>)); // Fluent validation kütüphanesinin çalýþmasýný saðladýk.
builder.Services.AddValidatorsFromAssembly(typeof(CleanArchitecture.Application.AssemblyReference).Assembly); // Nerede kontrol edeceðini yolu gösterdik.

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

app.UseMiddlewareExtensions(); // WebApi/Middleware/Exceptionmiddleware klasörleri. 

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
