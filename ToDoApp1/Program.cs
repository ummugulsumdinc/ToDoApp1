using ToDoApp1.Business.Interfaces;
using ToDoApp1.Business.Services;
using FluentValidation;
using ToDoApp1.Validators;

var builder = WebApplication.CreateBuilder(args);

// ==========================================
// 1. SERVİS KAYITLARI (Build İşleminden Önce)
// ==========================================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(); // Swagger Jeneratörü

builder.Services.AddScoped<ICalculator, CalculateService>();
builder.Services.AddValidatorsFromAssemblyContaining<PasswordCheckerValidator>();

// ==========================================
// 2. UYGULAMANIN OLUŞTURULMASI
// ==========================================
var app = builder.Build();

// ==========================================
// 3. HTTP İSTEK BORU HATI (PIPELINE) YAPILANDIRMASI
// ==========================================
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();   // Swagger JSON dosyasını oluşturur
    app.UseSwaggerUI(); // Swagger UI arayüzünü sunar
}

var env = app.Environment.EnvironmentName;

app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();//using ToDoApp1.Business.Services;
//using FluentValidation;
//using ToDoApp1.Validators;

//var builder = WebApplication.CreateBuilder(args);

//// ==========================================
//// 1. ALL SERVICE REGISTRATIONS GO HERE (BEFORE Build)
//// ==========================================
//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer(); // Moved up
//builder.Services.AddOpenApi();              // Moved up
//builder.Services.AddSwaggerGen(); // Swagger jeneratörünü ekler

//builder.Services.AddScoped<ICalculator, CalculateService>();
//builder.Services.AddValidatorsFromAssemblyContaining<PasswordCheckerValidator>();

//// ==========================================
//// 2. BUILD THE APPLICATION
//// ==========================================
//var app = builder.Build(); // <-- Container locks here

//// ==========================================
//// 3. CONFIGURE HTTP REQUEST PIPELINE (AFTER Build)
//// ==========================================
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//    app.UseSwagger(); // Swagger JSON dosyasını oluşturur
//    app.UseSwaggerUI(); // Swagger UI arayüzünü (tarayıcıda açılan ekran) sunar
//}

//var env = app.Environment.EnvironmentName;

//app.UseRouting();
//app.UseHttpsRedirection();
//app.UseAuthorization();
//app.MapControllers();

//app.Run();

//using ToDoApp1.Business.Interfaces;
//using ToDoApp1.Business.Services;
//using FluentValidation;
//using ToDoApp1.Validators;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddControllers();

//// 1. ADIM: Swagger servislerini ekleyin
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//// Mevcut servisleriniz
//builder.Services.AddScoped<ICalculator, CalculateService>();
//builder.Services.AddValidatorsFromAssemblyContaining<PasswordCheckerValidator>();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    // 2. ADIM: Swagger arayüzü ve JSON uç noktasını aktif edin
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//var env = app.Environment.EnvironmentName;

//app.UseRouting();
//app.UseHttpsRedirection();
//app.UseAuthorization();
//app.MapControllers();

//app.Run();




