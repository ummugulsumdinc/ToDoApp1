using ToDoApp1.Business.Interfaces;
using ToDoApp1.Business.Services;
using FluentValidation;
using ToDoApp1.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(); // Swagger Jeneratörü

builder.Services.AddScoped<ICalculator, CalculateService>();
builder.Services.AddScoped<IPasswordChecker, PasswordCheckerService>();
builder.Services.AddValidatorsFromAssemblyContaining<PasswordCheckerValidator>();

var app = builder.Build();

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

app.Run();