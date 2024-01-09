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

builder.Services.AddTransient<ExceptionMiddleware>(); // Her �a�r�lmada yeni bir instance �retmeyi sa��yor
builder.Services.AddScoped<IUnitOfWork, UnitOfWork<AppDbContext>>(); // Generic repository k�sm�n� ayarlad�k.
builder.Services.AddScoped<ICarRepositoy,CarRepository>();

builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.ConfigureOptions<JwtOptionsSetup>(); //JwtOptionsSetup class�n� tetikler ve oradaki metodu tetikleyip appsettings ile Jwtoption s�n�f�n� e�ler.
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

builder.Services.AddAuthentication().AddJwtBearer(); // Token kulland���m�z k�t�phaneyi belirttik.

builder.Services.AddAutoMapper(typeof(CleanArchitecture.Persistance.AssemblyeReferance).Assembly); // AutoMapper k�t�phanesini ekledik.

string connectionString = builder.Configuration.GetConnectionString("SqlServer");
// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddIdentity<User,IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 1;
    options.Password.RequireUppercase = false; // �ifreyi istedi�imiz gibi k���k b�y�k harf girmeisni ayarlayabiliriz.
}).AddEntityFrameworkStores<AppDbContext>(); // identity k�t�phanesi i�in dbcontext ayarlar�
builder.Services.AddControllers()
    .AddApplicationPart(typeof(CleanArchitecture.Presentation.AssemblyReference).Assembly); // controllerlar�n presentation katman�ndan geldi�ini s�yledik.

builder.Services.AddMediatR(cfr =>
cfr.RegisterServicesFromAssembly(typeof(CleanArchitecture.Application.AssemblyReference).Assembly)); // mediatr k�t�phanesini register ettik.

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>)); // Fluent validation k�t�phanesinin �al��mas�n� sa�lad�k.
builder.Services.AddValidatorsFromAssembly(typeof(CleanArchitecture.Application.AssemblyReference).Assembly); // Nerede kontrol edece�ini yolu g�sterdik.

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

app.UseMiddlewareExtensions(); // WebApi/Middleware/Exceptionmiddleware klas�rleri. 

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
