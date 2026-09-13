using Microsoft.EntityFrameworkCore;
using ToDoApp1.Data;
using ToDoApp1.Business.Interfaces;
using ToDoApp1.Business.Services;
using FluentValidation;
using ToDoApp1.Validators;
using ToDoApp1.Business.Dtos.ToDo;
using ToDoApp1.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// 1. Veritabanı Bağlantısı (SQLite)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Dependency Injection (Servis Kayıtları)
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IStatusService, StatusService>();
builder.Services.AddScoped<IToDoService, ToDoService>();
builder.Services.AddScoped<IUserService, UserService>();

// 3. FluentValidation Kayıtları
builder.Services.AddScoped<IValidator<ToDoCreateDto>, CreateToDoValidator>();
builder.Services.AddScoped<IValidator<ToDoUpdateDto>, UpdateToDoValidator>();

// 4. JWT Kimlik Doğrulama (Authentication) Ayarları
var tokenKey = builder.Configuration.GetSection("AppSettings:Token").Value;
if (!string.IsNullOrEmpty(tokenKey))
{
    var key = Encoding.ASCII.GetBytes(tokenKey);
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "ToDoApp1", Version = "v1" });

    options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "JWT Token'ınızı 'Bearer ' yazmadan, doğrudan yapıştırın."
    });

    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("bearer", document)] = []
    });
});

var app = builder.Build();

// 5. Middleware (Ara Katman) Yapılandırması
// Kendi yazdığımız hata yakalama mekanizmasını en üste ekliyoruz.
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Authentication her zaman Authorization'dan önce gelmelidir!
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
