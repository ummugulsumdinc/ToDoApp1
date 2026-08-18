using ToDoApp1.Business.Interfaces;
using ToDoApp1.Business.Services;
using FluentValidation;
using ToDoApp1.Validators;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(); // Swagger Jenerat�r�

builder.Services.AddScoped<ICalculator, CalculateService>();
builder.Services.AddSingleton<IToDoService, ToDoService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();   // Swagger JSON dosyas�n� olu�turur
    app.UseSwaggerUI(); // Swagger UI aray�z�n� sunar
}

var env = app.Environment.EnvironmentName;

app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();